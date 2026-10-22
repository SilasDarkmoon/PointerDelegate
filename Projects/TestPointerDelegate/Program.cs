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
                var invoker1 = new PointerFunc<ByRefParam, ByRefParam>(fn1);
                ByRefParam r;
                ref var ret = ref invoker1.Invoke(out r, in b);
                // fn returns &b; ByRef branch: *r = &b (r holds the pointer); ret refs 'b'.
                bool pass1;
                unsafe { pass1 = Unsafe.AsRef<IntPtr>(Unsafe.AsPointer(ref r)) == (IntPtr)Unsafe.AsPointer(ref b); }
                Check("PointerFunc IntPtr ret: r == &b", pass1);
                bool sameAsB;
                unsafe { sameAsB = (IntPtr)Unsafe.AsPointer(ref ret) == (IntPtr)Unsafe.AsPointer(ref b); }
                Check("PointerFunc IntPtr ret: ret refs 'b'", sameAsB);
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass2: PointerFunc, ByRef return + ByRef param
            {
                var invoker1 = new PointerFunc<ByRefParam, ByRefParam>(fn1);
                invoker1.SetParamFlag(0, ParamFlag.ByRef);
                ByRefParam r;
                ref var ret = ref invoker1.Invoke(out r, in a);
                // ByRef param: native gets &a; fn returns &a; ByRef ret: *r = &a; ret refs 'a'.
                bool pass2;
                unsafe { pass2 = Unsafe.AsRef<IntPtr>(Unsafe.AsPointer(ref r)) == (IntPtr)Unsafe.AsPointer(ref a); }
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
                var invoker1 = new PointerFunc<ByRefParam, ByRefParam>(fn5);
                ByRefParam r;
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
                var invoker1 = new PointerFunc<VoidReturn, ByRefParam>(fn6);
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
                var invoker1 = new FreeFunc<int, int>(act);
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
                            // U1=IntPtr with the ByRef flag set explicitly: native receives
                            // the field's address (only ByRefParam/ByRefLocalSlot default to ByRef).
                            var inv = new PointerFunc<IntPtr, IntPtr>(fnRead).WithRefParam(0);
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
                            var inv2 = new PointerFunc<IntPtr, IntPtr>(fnRead).WithRefParam(0);
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
                // NOTE: a dedicated Thread, NOT Task.Run — 128 CPU-bound workers saturate
                // the thread pool and a pooled control task would starve (no forced GCs).
                var control = new System.Threading.Thread(() =>
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
                control.Start();

                System.Threading.Thread.Sleep(5000);
                cts.Cancel();
                try { System.Threading.Tasks.Task.WaitAll(workers); }
                catch (AggregateException ae)
                {
                    var first = ae.InnerExceptions.Count > 0 ? ae.InnerExceptions[0] : ae;
                    Console.WriteLine($"    [GC stress] worker fault: {first}");
                    Console.WriteLine(first.StackTrace);
                }
                control.Join();

                int gen0Count = GC.CollectionCount(0) - gc0Before;
                int gen1Count = GC.CollectionCount(1) - gc1Before;
                int gen2Count = GC.CollectionCount(2) - gcCountBefore;
                // totalCalls > 0 guards against a vacuous pass (all workers faulting
                // instantly would otherwise report errors == 0 with nothing exercised).
                Check($"GC stress: errors == 0 ({totalCalls} calls, {workerCount} workers, gen0={gen0Count}, gen1={gen1Count}, gen2={gen2Count})",
                    totalErrors == 0 && totalCalls > 0);
                Console.WriteLine("------------------------------------------------------------------------");
            }

            // === ByRefLocalSlot tests: Ux=ByRefLocalSlot makes the invoker wrap the argument slot in
            // a GC-tracked indirect ref; the callee reads the current address through it. ===
            RunByRefLocalSlotTests();

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

        #region ByRefLocalSlot tests
        // Ux=ByRefLocalSlot is a marker: the invoker wraps the ARGUMENT SLOT in a ByRefLocalSlot
        // (Ref2Ref.Address = ldarga of the `in Ux` param slot — a GC-tracked byref the
        // collector keeps updated), so the callee reading *(IntPtr*)slot.Ref2Ref.Address
        // (the Address property) always sees the current address of the caller's
        // argument, across compacting GCs. Three paths are covered:
        //   1. PointerFunc hot-emit  (PointerFuncEmit: ldarga + new ByRefLocalSlot + calli)
        //   2. FreeFunc hot-emit     (FreeFuncEmit: ldarga + new ByRefLocalSlot + callvirt)
        //   3. InvokeFallback        (Generator-injected FixupByRefLocalSlot + woven InvokePlain)
        // Paths 1/2 vs 3 are switched via the static _IsDynamicCodeDisabled flag.

        sealed class CookieHolder { public long Cookie; }

        static IntPtr s_SlotAddrBefore, s_SlotAddrAfter;
        static long s_SlotValueAfter;

        // Callee (no GC): records the current address + value read through the ByRefLocalSlot.
        public static void ByRefLocalSlotProbeFn(ByRefLocalSlot slot)
        {
            unsafe
            {
                s_SlotAddrBefore = slot.Address;
                s_SlotValueAfter = *(long*)slot.Address;
            }
        }

        // Callee (compacting GCs inside): records the address before/after + value after.
        // The ByRefLocalSlot must keep tracking: Address after the GCs == the field's new home.
        public static void ByRefLocalSlotGcProbeFn(ByRefLocalSlot slot)
        {
            unsafe
            {
                s_SlotAddrBefore = slot.Address;
                GC.Collect(0, GCCollectionMode.Forced, true, true);
                GC.Collect(1, GCCollectionMode.Forced, true, true);
                GC.Collect(2, GCCollectionMode.Forced, true, true);
                s_SlotAddrAfter = slot.Address;
                s_SlotValueAfter = *(long*)slot.Address;
            }
        }

        // Stress callee: reads the cookie through the ByRefLocalSlot while a control thread compacts.
        public static long ByRefLocalSlotReadFn(ByRefLocalSlot slot)
        {
            unsafe { return *(long*)slot.Address; }
        }

        // Mixed-params callee: ByRefLocalSlot + plain by-value param, with compacting GCs inside.
        static long s_SlotMixedArg;
        public static void ByRefLocalSlotMixedGcProbeFn(ByRefLocalSlot slot, long plainArg)
        {
            unsafe
            {
                s_SlotAddrBefore = slot.Address;
                GC.Collect(0, GCCollectionMode.Forced, true, true);
                GC.Collect(1, GCCollectionMode.Forced, true, true);
                s_SlotAddrAfter = slot.Address;
                s_SlotValueAfter = *(long*)slot.Address;
                s_SlotMixedArg = plainArg;
            }
        }

        #region Px=ByRefLocalSlot, Ux=IntPtr pass-through tests
        // The pass-through shape: the caller ALREADY holds a ByRefLocalSlot (wrapped by an
        // outer invoker call) and forwards it through an invoker whose Ux is IntPtr.
        // ConvertRef<ByRefLocalSlot, IntPtr> bit-reinterprets the 8-byte struct (its single
        // field Ref2Ref.Address = the tracked byref slot's address) into the IntPtr the
        // callee receives BY VALUE. The tracking chain stays intact: the callee
        // double-dereferences (slot address → CURRENT value address → value), and the slot
        // itself is a GC-tracked byref in the outer invoker's frame, so the current address
        // survives compacting GCs — the raw IntPtr stays valid without pinning.

        static IntPtr s_PtInnerFn, s_PtInnerGcFn;
        static IntPtr s_PtOuterSlotAddr;    // recorded by the outer callee: slot.Ref2Ref.Address
        static IntPtr s_PtInnerReceived;    // recorded by the inner callee: the IntPtr it received

        // Inner callee: p = the passed-through slot address; double-deref reads the value.
        public static long PtInnerReadFn(IntPtr p)
        {
            unsafe
            {
                s_PtInnerReceived = p;
                return *(long*)*(IntPtr*)p;
            }
        }

        // Inner callee (compacting GCs inside): the raw IntPtr must still yield the CURRENT
        // address through the outer frame's tracked slot after the relocations.
        public static long PtInnerReadGcFn(IntPtr p)
        {
            unsafe
            {
                s_SlotAddrBefore = *(IntPtr*)p;     // current value address before the GCs
                GC.Collect(0, GCCollectionMode.Forced, true, true);
                GC.Collect(1, GCCollectionMode.Forced, true, true);
                GC.Collect(2, GCCollectionMode.Forced, true, true);
                s_SlotAddrAfter = *(IntPtr*)p;      // current value address after (relocated)
                return *(long*)*(IntPtr*)p;
            }
        }

        // Outer callee (forwards through an inner PointerFunc, Ux=IntPtr).
        public static long PtOuterPfFn(ByRefLocalSlot slot)
        {
            s_PtOuterSlotAddr = slot.Ref2Ref.Address;
            var inv = new PointerFunc<long, IntPtr>(s_PtInnerFn);
            long r;
            inv.Invoke(out r, in slot);             // P1=ByRefLocalSlot → U1=IntPtr bit pass-through
            return r;
        }

        // Outer callee (forwards through an inner FreeFunc, Ux=IntPtr).
        public static long PtOuterFfFn(ByRefLocalSlot slot)
        {
            s_PtOuterSlotAddr = slot.Ref2Ref.Address;
            var inv = new FreeFunc<long, IntPtr>(new Func<IntPtr, long>(PtInnerReadFn));
            long r;
            inv.Invoke(out r, in slot);
            return r;
        }

        // Outer callee (GC variant — the inner read happens after compacting GCs).
        public static long PtOuterPfGcFn(ByRefLocalSlot slot)
        {
            var inv = new PointerFunc<long, IntPtr>(s_PtInnerGcFn);
            long r;
            inv.Invoke(out r, in slot);
            return r;
        }
        #endregion

        static void RunByRefLocalSlotTests()
        {
            var miProbe = typeof(Program).GetMethod(nameof(ByRefLocalSlotProbeFn));
            RuntimeHelpers.PrepareMethod(miProbe.MethodHandle);
            var fnProbe = miProbe.MethodHandle.GetFunctionPointer();
            var miGcProbe = typeof(Program).GetMethod(nameof(ByRefLocalSlotGcProbeFn));
            RuntimeHelpers.PrepareMethod(miGcProbe.MethodHandle);
            var fnGcProbe = miGcProbe.MethodHandle.GetFunctionPointer();
            var miRead = typeof(Program).GetMethod(nameof(ByRefLocalSlotReadFn));
            RuntimeHelpers.PrepareMethod(miRead.MethodHandle);
            var fnRead = miRead.MethodHandle.GetFunctionPointer();

            Console.WriteLine("  [ByRefLocalSlot] basic + GC-compaction tracking tests...");

            // ---- fallback path first (emit globally disabled → InvokeFallback with the
            // Generator-injected FixupByRefLocalSlot + woven InvokePlain) ----
            SetDynamicCodeDisabled(true);
            {
                var pfBasic = new PointerFunc<VoidReturn, ByRefLocalSlot>(fnProbe);
                var pfGc = new PointerFunc<VoidReturn, ByRefLocalSlot>(fnGcProbe);
                ByRefLocalSlotBasic("PointerFunc fallback", h => { VoidReturn r; pfBasic.Invoke(out r, in h.Cookie); });
                ByRefLocalSlotGc("PointerFunc fallback", h => { VoidReturn r; pfGc.Invoke(out r, in h.Cookie); });

                var ffBasic = new FreeFunc<VoidReturn, ByRefLocalSlot>(new Action<ByRefLocalSlot>(ByRefLocalSlotProbeFn));
                var ffGc = new FreeFunc<VoidReturn, ByRefLocalSlot>(new Action<ByRefLocalSlot>(ByRefLocalSlotGcProbeFn));
                ByRefLocalSlotBasic("FreeFunc fallback", h => { VoidReturn r; ffBasic.Invoke(out r, in h.Cookie); });
                ByRefLocalSlotGc("FreeFunc fallback", h => { VoidReturn r; ffGc.Invoke(out r, in h.Cookie); });
            }

            // ---- emit path (DynamicInvoke: ldarga + new ByRefLocalSlot(slot) construction) ----
            SetDynamicCodeDisabled(false);
            {
                var pfBasic = new PointerFunc<VoidReturn, ByRefLocalSlot>(fnProbe);
                var pfGc = new PointerFunc<VoidReturn, ByRefLocalSlot>(fnGcProbe);
                ByRefLocalSlotBasic("PointerFunc emit", h => { VoidReturn r; pfBasic.Invoke(out r, in h.Cookie); });
                ByRefLocalSlotGc("PointerFunc emit", h => { VoidReturn r; pfGc.Invoke(out r, in h.Cookie); });

                var ffBasic = new FreeFunc<VoidReturn, ByRefLocalSlot>(new Action<ByRefLocalSlot>(ByRefLocalSlotProbeFn));
                var ffGc = new FreeFunc<VoidReturn, ByRefLocalSlot>(new Action<ByRefLocalSlot>(ByRefLocalSlotGcProbeFn));
                ByRefLocalSlotBasic("FreeFunc emit", h => { VoidReturn r; ffBasic.Invoke(out r, in h.Cookie); });
                ByRefLocalSlotGc("FreeFunc emit", h => { VoidReturn r; ffGc.Invoke(out r, in h.Cookie); });
            }
            Console.WriteLine("------------------------------------------------------------------------");

            // ---- mixed params: ByRefLocalSlot + plain by-value long in one callsite, on both
            // the fallback and the emit path (per-param wrapping must be independent) ----
            var miMixed = typeof(Program).GetMethod(nameof(ByRefLocalSlotMixedGcProbeFn));
            RuntimeHelpers.PrepareMethod(miMixed.MethodHandle);
            var fnMixed = miMixed.MethodHandle.GetFunctionPointer();
            foreach (var (mixedName, mixedDisable) in new[] { ("fallback", true), ("emit", false) })
            {
                SetDynamicCodeDisabled(mixedDisable);
                bool moved = false;
                for (int round = 0; round < 64 && !moved; round++)
                {
                    var holder = new CookieHolder { Cookie = 0x2000000000L + round };
                    long plain = 0x0BAD0BAD0BADL + round;
                    for (int g = 0; g < 32; g++) { var junk = new byte[(round * 5 + g) % 128 + 1]; junk[0] = 1; }
                    s_SlotAddrBefore = s_SlotAddrAfter = IntPtr.Zero; s_SlotValueAfter = 0; s_SlotMixedArg = 0;
                    var inv = new PointerFunc<VoidReturn, ByRefLocalSlot, long>(fnMixed);
                    VoidReturn vr;
                    inv.Invoke(out vr, in holder.Cookie, in plain);
                    IntPtr cur;
                    unsafe { cur = (IntPtr)Unsafe.AsPointer(ref holder.Cookie); }
                    if (s_SlotAddrBefore == s_SlotAddrAfter) continue;
                    moved = true;
                    Check($"ByRefLocalSlot mixed ({mixedName}): Address after compaction == new &arg", s_SlotAddrAfter == cur);
                    Check($"ByRefLocalSlot mixed ({mixedName}): value through ByRefLocalSlot after compaction", s_SlotValueAfter == holder.Cookie);
                    Check($"ByRefLocalSlot mixed ({mixedName}): plain param passed through", s_SlotMixedArg == plain);
                }
                Check($"ByRefLocalSlot mixed ({mixedName}): field relocated within 64 rounds", moved);
            }
            Console.WriteLine("------------------------------------------------------------------------");

            // ---- GC stress: workers call through both wrappers (field + array-element
            // args) while a control thread forces compacting GCs; every read through
            // the ByRefLocalSlot must still see the right cookie ----
            RunByRefLocalSlotStress("fallback", true, fnRead);
            RunByRefLocalSlotStress("emit", false, fnRead);

            // ---- Px=ByRefLocalSlot, Ux=IntPtr pass-through ----
            RunPtIntPtrPassthroughTests();
        }

        // Px=ByRefLocalSlot, Ux=IntPtr: chained calls where the inner invoker's Ux is
        // IntPtr — the ByRefLocalSlot's bits (Ref2Ref.Address) must arrive at the callee
        // as the IntPtr value (no re-wrap, no double indirection), and the tracking chain
        // (outer frame's GC-tracked slot) must survive compacting GCs.
        static void RunPtIntPtrPassthroughTests()
        {
            Console.WriteLine("  [ByRefLocalSlot → IntPtr pass-through] chained tests...");
            var miInner = typeof(Program).GetMethod(nameof(PtInnerReadFn));
            RuntimeHelpers.PrepareMethod(miInner.MethodHandle);
            s_PtInnerFn = miInner.MethodHandle.GetFunctionPointer();
            var miInnerGc = typeof(Program).GetMethod(nameof(PtInnerReadGcFn));
            RuntimeHelpers.PrepareMethod(miInnerGc.MethodHandle);
            s_PtInnerGcFn = miInnerGc.MethodHandle.GetFunctionPointer();

            var miOuterPf = typeof(Program).GetMethod(nameof(PtOuterPfFn));
            RuntimeHelpers.PrepareMethod(miOuterPf.MethodHandle);
            var fnOuterPf = miOuterPf.MethodHandle.GetFunctionPointer();
            var miOuterFf = typeof(Program).GetMethod(nameof(PtOuterFfFn));
            RuntimeHelpers.PrepareMethod(miOuterFf.MethodHandle);
            var fnOuterFf = miOuterFf.MethodHandle.GetFunctionPointer();
            var miOuterGc = typeof(Program).GetMethod(nameof(PtOuterPfGcFn));
            RuntimeHelpers.PrepareMethod(miOuterGc.MethodHandle);
            var fnOuterGc = miOuterGc.MethodHandle.GetFunctionPointer();

            foreach (var (phaseName, disable) in new[] { ("fallback", true), ("emit", false) })
            {
                SetDynamicCodeDisabled(disable);

                // Basic: the outer invoker wraps &holder.Cookie into a ByRefLocalSlot; the
                // inner callee must receive exactly slot.Ref2Ref.Address as its IntPtr
                // (bit-pattern pass-through) and read the cookie via double-deref.
                foreach (var (innerName, fnOuter) in new[] { ("PointerFunc", fnOuterPf), ("FreeFunc", fnOuterFf) })
                {
                    var holder = new CookieHolder { Cookie = 0x2448244824482448L };
                    s_PtOuterSlotAddr = s_PtInnerReceived = IntPtr.Zero;
                    var inv = new PointerFunc<long, ByRefLocalSlot>(fnOuter);
                    long got;
                    inv.Invoke(out got, in holder.Cookie);   // outer wrap; outer forwards via Ux=IntPtr
                    Check($"IntPtr pass-through ({innerName}, {phaseName}): inner receives slot.Ref2Ref.Address", s_PtInnerReceived == s_PtOuterSlotAddr);
                    Check($"IntPtr pass-through ({innerName}, {phaseName}): double-deref reads cookie", got == holder.Cookie);
                }

                // GC: compacting GCs inside the innermost callee, after the pass-through
                // hop — the raw IntPtr still yields the CURRENT address (the slot it
                // points at is GC-tracked in the outer invoker's frame), so the
                // relocated cookie is still found.
                {
                    bool verified = false;
                    for (int round = 0; round < 64 && !verified; round++)
                    {
                        var holder = new CookieHolder { Cookie = 0x9000000000L + round };
                        for (int g = 0; g < 32; g++) { var junk = new byte[(round * 13 + g) % 128 + 1]; junk[0] = 1; }
                        s_SlotAddrBefore = s_SlotAddrAfter = IntPtr.Zero;
                        var inv = new PointerFunc<long, ByRefLocalSlot>(fnOuterGc);
                        long got;
                        inv.Invoke(out got, in holder.Cookie);
                        // verified only when the holder actually relocated AND the value tracked
                        verified = s_SlotAddrBefore != s_SlotAddrAfter && got == holder.Cookie;
                    }
                    Check($"IntPtr pass-through GC ({phaseName}): raw IntPtr tracks compaction via outer slot", verified);
                }
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }

        static void SetDynamicCodeDisabled(bool disabled)
        {
            var flagField = typeof(FreeInvokableBase).GetField("_IsDynamicCodeDisabled",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            flagField.SetValue(null, disabled);
        }

        // Basic (no GC): the ByRefLocalSlot's Address == the argument's current address, and
        // the value read through it matches.
        static void ByRefLocalSlotBasic(string name, Action<CookieHolder> call)
        {
            var holder = new CookieHolder { Cookie = 0x5A5A5A5A5A5A5A5AL };
            call(holder);                            // warm up (first call may emit)
            holder.Cookie = 0x1234567890ABCDEFL;
            s_SlotAddrBefore = IntPtr.Zero; s_SlotValueAfter = 0;
            call(holder);
            IntPtr cur;
            unsafe { cur = (IntPtr)Unsafe.AsPointer(ref holder.Cookie); }
            Check($"{name} basic: Address == &arg", s_SlotAddrBefore == cur);
            Check($"{name} basic: value through ByRefLocalSlot", s_SlotValueAfter == 0x1234567890ABCDEFL);
            Console.WriteLine("  --------");
        }

        // GC compaction: the callee forces compacting GCs and re-reads through the
        // ByRefLocalSlot; the address must follow the relocated field.
        static void ByRefLocalSlotGc(string name, Action<CookieHolder> call)
        {
            for (int round = 0; round < 64; round++)
            {
                var holder = new CookieHolder { Cookie = 0x1000000000L + round };
                // garbage of varying sizes to fragment the heap so the compacting GCs
                // inside the callee actually relocate the holder
                for (int g = 0; g < 32; g++) { var junk = new byte[(round * 7 + g) % 128 + 1]; junk[0] = (byte)round; }
                s_SlotAddrBefore = s_SlotAddrAfter = IntPtr.Zero; s_SlotValueAfter = 0;
                call(holder);
                IntPtr cur;
                unsafe { cur = (IntPtr)Unsafe.AsPointer(ref holder.Cookie); }
                if (s_SlotAddrBefore == s_SlotAddrAfter) continue;    // didn't relocate — retry
                Check($"{name} GC: field relocated (round {round})", true);
                Check($"{name} GC: Address after compaction == new &arg (tracks heap movement)", s_SlotAddrAfter == cur);
                Check($"{name} GC: value through ByRefLocalSlot after compaction", s_SlotValueAfter == holder.Cookie);
                Console.WriteLine("  --------");
                return;
            }
            Check($"{name} GC: field relocated within 64 rounds", false);
        }

        static void RunByRefLocalSlotStress(string phaseName, bool disableEmit, IntPtr fnRead)
        {
            SetDynamicCodeDisabled(disableEmit);
            Console.WriteLine($"  [ByRefLocalSlot stress - {phaseName}] starting...");
            int workerCount = Environment.ProcessorCount * 2;
            long totalCalls = 0, totalErrors = 0;
            var workers = new System.Threading.Tasks.Task[workerCount];
            var cts = new System.Threading.CancellationTokenSource();
            int gc2Before = GC.CollectionCount(2);

            for (int w = 0; w < workerCount; w++)
            {
                workers[w] = System.Threading.Tasks.Task.Run(() =>
                {
                    long calls = 0, errors = 0;
                    var holder = new CookieHolder();
                    var arr = new long[4];
                    long cookie = 0;
                    while (!cts.IsCancellationRequested)
                    {
                        cookie++;
                        if ((cookie & 511) == 0)
                        {
                            holder = new CookieHolder();
                            arr = new long[4];
                        }
                        holder.Cookie = cookie;
                        arr[0] = cookie;

                        // PointerFunc: class-field argument (&holder.Cookie — interior of heap object)
                        var inv = new PointerFunc<long, ByRefLocalSlot>(fnRead);
                        long r;
                        inv.Invoke(out r, in holder.Cookie);
                        if (r != cookie)
                        {
                            errors++;
                            if (errors <= 3)
                                Console.WriteLine($"    [Worker] ByRefLocalSlot stress PointerFunc field mismatch: got {r:x}, want {cookie:x}");
                        }
                        calls++;

                        // PointerFunc: array-element argument (&arr[0] — interior of managed array)
                        var inv2 = new PointerFunc<long, ByRefLocalSlot>(fnRead);
                        long r2;
                        inv2.Invoke(out r2, in arr[0]);
                        if (r2 != cookie)
                        {
                            errors++;
                            if (errors <= 3)
                                Console.WriteLine($"    [Worker] ByRefLocalSlot stress PointerFunc array mismatch: got {r2:x}, want {cookie:x}");
                        }
                        calls++;

                        // FreeFunc: class-field argument through the delegate
                        var finv = new FreeFunc<long, ByRefLocalSlot>(new Func<ByRefLocalSlot, long>(ByRefLocalSlotReadFn));
                        long r3;
                        finv.Invoke(out r3, in holder.Cookie);
                        if (r3 != cookie)
                        {
                            errors++;
                            if (errors <= 3)
                                Console.WriteLine($"    [Worker] ByRefLocalSlot stress FreeFunc field mismatch: got {r3:x}, want {cookie:x}");
                        }
                        calls++;

                        // Churn: garbage of varying sizes to fragment the heap and
                        // encourage the compacting GC to actually move our objects.
                        for (int g = 0; g < 32; g++)
                        {
                            var junk = new byte[(int)(cookie + g) % 128 + 1];
                            junk[0] = (byte)cookie;
                        }
                    }
                    System.Threading.Interlocked.Add(ref totalCalls, calls);
                    System.Threading.Interlocked.Add(ref totalErrors, errors);
                });
            }

            // Control thread: force frequent compacting GCs across generations.
            // NOTE: a dedicated Thread, NOT Task.Run — the CPU-bound workers saturate the
            // thread pool and a pooled control task would starve (no forced GCs at all).
            var control = new System.Threading.Thread(() =>
            {
                int tick = 0;
                while (!cts.IsCancellationRequested)
                {
                    GC.Collect(0, GCCollectionMode.Forced, true, true);
                    GC.Collect(1, GCCollectionMode.Forced, true, true);
                    if ((tick++ & 7) == 0)
                    {
                        GC.Collect(2, GCCollectionMode.Forced, true, true);
                    }
                    System.Threading.Thread.Sleep(1);
                }
            });
            control.Start();

            System.Threading.Thread.Sleep(5000);
            cts.Cancel();
            try { System.Threading.Tasks.Task.WaitAll(workers); }
            catch (AggregateException ae)
            {
                var first = ae.InnerExceptions.Count > 0 ? ae.InnerExceptions[0] : ae;
                Console.WriteLine($"    [ByRefLocalSlot stress] worker fault: {first}");
                Console.WriteLine(first.StackTrace);
            }
            control.Join();

            // totalCalls > 0 guards against a vacuous pass (all workers faulting
            // instantly would otherwise report errors == 0 with nothing exercised).
            Check($"ByRefLocalSlot stress ({phaseName}): errors == 0 ({totalCalls} calls, gen2={GC.CollectionCount(2) - gc2Before})",
                totalErrors == 0 && totalCalls > 0);
            Console.WriteLine("------------------------------------------------------------------------");
        }
        #endregion
    }
}
