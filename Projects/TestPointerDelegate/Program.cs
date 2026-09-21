using Mod.LowLevel;
using System;
using System.Runtime.CompilerServices;

namespace TestPointerDelegate
{
    class Program
    {
        static int passCount = 0;
        static int failCount = 0;

        static void Check(string name, bool cond)
        {
            if (cond) { passCount++; Console.WriteLine($"  [PASS] {name}"); }
            else { failCount++; Console.WriteLine($"  [FAIL] {name}"); }
        }

        static void Main(string[] args)
        {
            IntPtr a = (IntPtr)10;
            long b = 300;

            var mi1 = typeof(Program).GetMethod(nameof(TestFunc1));
            RuntimeHelpers.PrepareMethod(mi1.MethodHandle);
            var fn1 = mi1.MethodHandle.GetFunctionPointer();
            // Pass1: PointerFunc, IntPtr return (default ByRef category 2), ByValue param
            {
                var invoker1 = new PointerFunc<IntPtr, IntPtr>(fn1);
                IntPtr r;
                ref var ret = ref invoker1.Invoke(out r, in b);
                // fn returns &b; ByRef branch: *r = &b (r holds the pointer); ret refs 'b'.
                bool pass1;
                unsafe { pass1 = r == (IntPtr)Unsafe.AsPointer(ref b); }
                Check("PointerFunc IntPtr ret: r == &b", pass1);
                bool sameAsB;
                unsafe { sameAsB = (IntPtr)Unsafe.AsPointer(ref ret) == (IntPtr)Unsafe.AsPointer(ref b); }
                Check("PointerFunc IntPtr ret: ret refs 'b'", sameAsB);
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass2: PointerFunc, ByRef return + ByRef param
            {
                var invoker1 = new PointerFunc<IntPtr, IntPtr>(fn1);
                invoker1.SetParamFlag(0, ParamFlag.ByRef);
                IntPtr r;
                ref var ret = ref invoker1.Invoke(out r, in a);
                // ByRef param: native gets &a; fn returns &a; ByRef ret: *r = &a; ret refs 'a'.
                bool pass2;
                unsafe { pass2 = r == (IntPtr)Unsafe.AsPointer(ref a); }
                Check("PointerFunc ByRef ret: r == &a", pass2);
                bool sameAsA;
                unsafe { sameAsA = (IntPtr)Unsafe.AsPointer(ref ret) == (IntPtr)Unsafe.AsPointer(ref a); }
                Check("PointerFunc ByRef ret: ret refs 'a'", sameAsA);
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass3: FreeFunc, ByValue return forced (IntPtr treated as plain value)
            Func<IntPtr, IntPtr> del2 = TestFunc2;
            {
                var invoker1 = new FreeFunc<IntPtr, IntPtr>(del2);
                invoker1.SetParamFlag(-1, ParamFlag.ByValue);
                IntPtr r;
                ref var ret = ref invoker1.Invoke(out r, in a);
                // ByValue branch: r = del(a) == 10; ret refs r.
                Check("FreeFunc ByValue ret: r == 10", r == (IntPtr)10);
                Check("FreeFunc ByValue ret: ref r == r", Unsafe.AreSame(ref ret, ref r));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass5: PointerFunc ByRef return with R=IntPtr (native returns ref int)
            var mi5 = typeof(Program).GetMethod(nameof(TestFunc5));
            RuntimeHelpers.PrepareMethod(mi5.MethodHandle);
            var fn5 = mi5.MethodHandle.GetFunctionPointer();
            {
                int local = 41;
                // U1=IntPtr → ByRef: native receives the address of the caller's variable.
                // TestFunc5(IntPtr p) treats p as int* and increments *p, returning ref to it.
                var invoker1 = new PointerFunc<IntPtr, IntPtr>(fn5);
                IntPtr r;
                ref var ret = ref invoker1.Invoke(out r, in local);
                // native: ++(*p) → local becomes 42; returns the pointer to 'local'.
                Check("PointerFunc ByRef ret: local == 42", local == 42);
                bool retRefsLocal;
                unsafe { retRefsLocal = (IntPtr)Unsafe.AsPointer(ref ret) == (IntPtr)Unsafe.AsPointer(ref local); }
                Check("PointerFunc ByRef ret (IntPtr): ret refs local", retRefsLocal);
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass6: PointerFunc, void fn (VoidReturn)
            var mi6 = typeof(Program).GetMethod(nameof(TestFunc6));
            RuntimeHelpers.PrepareMethod(mi6.MethodHandle);
            var fn6 = mi6.MethodHandle.GetFunctionPointer();
            {
                int local = 9;
                IntPtr localPtr;
                unsafe { localPtr = (IntPtr)Unsafe.AsPointer(ref local); }
                // U1=IntPtr → param 0 defaults to ByRef: native gets the variable address.
                var invoker1 = new PointerFunc<VoidReturn, IntPtr>(fn6);
                VoidReturn r;
                ref var ret = ref invoker1.Invoke(out r, in localPtr);
                Check("PointerFunc void ret: r == default", r == null);
                Check("PointerFunc void ret: local == 10", local == 10);
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass7: FreeAction
            {
                int local = 5;
                Action<int> act = static (int x) => { Console.WriteLine($"    in FreeAction lambda, x = {x}"); };
                var invoker1 = new FreeAction<int, int>(act);
                int r;
                ref var ret = ref invoker1.Invoke(out r, in local);
                Check("FreeAction: r == default", r == 0);
                Check("FreeAction: ref r == r", Unsafe.AreSame(ref ret, ref r));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass8: FreeFunc with VoidReturn
            {
                var invoker1 = new FreeFunc<VoidReturn, int>(TestFunc7);
                int local = 7;
                VoidReturn r;
                ref var ret = ref invoker1.Invoke(out r, in local);
                Check("FreeFunc VoidReturn: r == default", r == null);
                Check("FreeFunc VoidReturn: ref r == r", Unsafe.AreSame(ref ret, ref r));
                Console.WriteLine("------------------------------------------------------------------------");
            }

            // Pass9: GC stress — parallel ByRef calls with frequent compacting gen2 GCs.
            // The ByRef param (in IntPtr) is passed the address of a HEAP location
            // (class field / array element). When a compacting GC relocates the owner,
            // the injected Invoke frame's byref slot must be reported to the GC as an
            // interior byref and rewritten, so the native fn / delegate receives the
            // correct (post-relocation) address and reads the right cookie.
            {
                Console.WriteLine("  [GC stress] starting...");
                var miRead = typeof(Program).GetMethod(nameof(ReadCookieFn));
                RuntimeHelpers.PrepareMethod(miRead.MethodHandle);
                var fnRead = miRead.MethodHandle.GetFunctionPointer();
                int workerCount = Environment.ProcessorCount * 2;
                long totalCalls = 0, totalErrors = 0;
                var workers = new System.Threading.Tasks.Task[workerCount];
                var cts = new System.Threading.CancellationTokenSource();
                int gcCountBefore = GC.CollectionCount(2);
                int gc0Before = GC.CollectionCount(0), gc1Before = GC.CollectionCount(1);

                for (int w = 0; w < workerCount; w++)
                {
                    workers[w] = System.Threading.Tasks.Task.Run(() =>
                    {
                        long calls = 0, errors = 0;
                        // Heap locations the compacting GC may relocate.
                        // Re-allocated every 512 iterations to keep them young
                        // (gen0/gen1) so high-frequency compacting GCs move them.
                        var slot = new Slot();                 // class with IntPtr field
                        var arr = new IntPtr[4];                // managed IntPtr array
                        long cookie = 0;
                        while (!cts.IsCancellationRequested)
                        {
                            cookie++;
                            if ((cookie & 511) == 0)
                            {
                                slot = new Slot();
                                arr = new IntPtr[4];
                            }
                            // Write the cookie into both heap locations.
                            slot.Addr = (IntPtr)cookie;
                            arr[0] = (IntPtr)cookie;

                            // Path A: PointerFunc + class-field byref (&slot.Addr — interior of heap object).
                            // IntPtr param defaults to ByRef: native receives the field's address.
                            var inv = new PointerFunc<IntPtr, IntPtr>(fnRead);
                            IntPtr r;
                            inv.Invoke(out r, in slot.Addr);
                            if (r.ToInt64() != cookie)
                            {
                                errors++;
                                if (errors <= 3)
                                    Console.WriteLine($"    [Worker] PointerFunc field-byref mismatch: got {r.ToInt64():x}, want {cookie:x}");
                            }
                            calls++;

                            // Path B: PointerFunc + array-element byref (&arr[0] — interior of managed array).
                            var inv2 = new PointerFunc<IntPtr, IntPtr>(fnRead);
                            IntPtr r2;
                            inv2.Invoke(out r2, in arr[0]);
                            if (r2.ToInt64() != cookie)
                            {
                                errors++;
                                if (errors <= 3)
                                    Console.WriteLine($"    [Worker] PointerFunc array-byref mismatch: got {r2.ToInt64():x}, want {cookie:x}");
                            }
                            calls++;

                            // Path C: FreeFunc + class-field byref — the injected IL derefs the
                            // (relocated) byref and passes the value to the managed delegate.
                            var finv = new FreeFunc<IntPtr, IntPtr>(EchoIntPtrDel);
                            IntPtr r3;
                            finv.Invoke(out r3, in slot.Addr);
                            if (r3.ToInt64() != cookie)
                            {
                                errors++;
                                if (errors <= 3)
                                    Console.WriteLine($"    [Worker] FreeFunc field-byref mismatch: got {r3.ToInt64():x}, want {cookie:x}");
                            }
                            calls++;

                            // Churn: garbage of varying sizes to fragment the heap and
                            // encourage the compacting GC to actually move our objects.
                            for (int g = 0; g < 32; g++)
                            {
                                var junk = new byte[(cookie + g) % 128 + 1];
                                junk[0] = (byte)cookie;
                            }
                        }
                        System.Threading.Interlocked.Add(ref totalCalls, calls);
                        System.Threading.Interlocked.Add(ref totalErrors, errors);
                    });
                }

                // Control thread: force frequent compacting GCs across generations —
                // gen0/gen1 are cheap and compact every time (high relocation frequency);
                // gen2 compaction is expensive, done periodically.
                var control = System.Threading.Tasks.Task.Run(() =>
                {
                    int tick = 0;
                    while (!cts.IsCancellationRequested)
                    {
                        GC.Collect(0, GCCollectionMode.Forced, blocking: true, compacting: true);
                        GC.Collect(1, GCCollectionMode.Forced, blocking: true, compacting: true);
                        if ((tick++ & 7) == 0)
                        {
                            GC.Collect(2, GCCollectionMode.Forced, blocking: true, compacting: true);
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                });

                System.Threading.Thread.Sleep(5000);
                cts.Cancel();
                try { System.Threading.Tasks.Task.WaitAll(workers); } catch (AggregateException) { }
                control.Wait();

                int gen0Count = GC.CollectionCount(0) - gc0Before;
                int gen1Count = GC.CollectionCount(1) - gc1Before;
                int gen2Count = GC.CollectionCount(2) - gcCountBefore;
                Check($"GC stress: errors == 0 ({totalCalls} calls, {workerCount} workers, gen0={gen0Count}, gen1={gen1Count}, gen2={gen2Count})", totalErrors == 0);
                Console.WriteLine("------------------------------------------------------------------------");
            }

            Console.WriteLine($"Total: {passCount} passed, {failCount} failed");
            if (failCount > 0) Environment.Exit(1);
        }

        // Heap slot for the GC stress test: the IntPtr field's address is passed byref.
        sealed class Slot { public IntPtr Addr; }

        // Native fn for GC stress: p is the address of a heap location (Slot.Addr or arr[0]);
        // reads the IntPtr stored there (the cookie).
        public static IntPtr ReadCookieFn(IntPtr p)
        {
            unsafe
            {
                return *(IntPtr*)p;
            }
        }

        // Delegate for GC stress: echoes its IntPtr argument (the deref'd cookie value).
        public static IntPtr EchoIntPtrDel(IntPtr p)
        {
            return p;
        }

        public static IntPtr TestFunc1(ref int p)
        {
            unsafe
            {
                return (IntPtr)Unsafe.AsPointer<int>(ref p);
            }
        }

        public static IntPtr TestFunc2(IntPtr p)
        {
            return p;
        }

        public static ref int TestFunc5(IntPtr p)
        {
            unsafe
            {
                ref int r = ref *(int*)p;
                ++r;
                return ref r;
            }
        }
        public static void TestFunc6(IntPtr p)
        {
            Console.WriteLine("    in TestFunc6");
            unsafe
            {
                // ByRef param: p is the address of the caller's IntPtr variable;
                // deref once to get the target pointer, then increment the pointee.
                ++*(int*)*(IntPtr*)p;
            }
        }
        public static VoidReturn TestFunc7(int r)
        {
            Console.WriteLine($"    in TestFunc7, r = {r}");
            return null;
        }
    }
}
