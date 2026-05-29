using Mod.LowLevel;
using System;
using System.Runtime.CompilerServices;

namespace TestPointerDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr a = (IntPtr)10;
            long b = 300;

            var mi1 = typeof(Program).GetMethod("TestFunc1");
            RuntimeHelpers.PrepareMethod(mi1.MethodHandle);
            var fn1 = mi1.MethodHandle.GetFunctionPointer();
            // Pass1
            {
                var invoker1 = new PointerFunc<IntPtr, IntPtr>(fn1);
                var invoker1_2 = invoker1.Clone().WithRefParam(0);
                var invoker1_3 = invoker1.Clone().WithRefParam(0, false);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine(invoker1_2.Invoke(ref a));
                Console.WriteLine(invoker1_2.Invoke(b));
                Console.WriteLine(invoker1_3.Invoke(ref a));
                Console.WriteLine(invoker1_3.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass2:
            {
                var invoker1 = new PointerFunc<IntPtr, ByRefParam>(fn1);
                var invoker1_2 = invoker1.Clone().WithRefParam(0);
                var invoker1_3 = invoker1.Clone().WithRefParam(0, false);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine(invoker1_2.Invoke(ref a));
                Console.WriteLine(invoker1_2.Invoke(b));
                Console.WriteLine(invoker1_3.Invoke(ref a));
                Console.WriteLine(invoker1_3.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            Func<IntPtr, IntPtr> del2 = TestFunc2;
            // Pass3
            {
                var invoker1 = new PointerFunc<IntPtr, IntPtr>(del2);
                var invoker1_2 = invoker1.Clone().WithRefParam(0);
                var invoker1_3 = invoker1.Clone().WithRefParam(0, false);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine(invoker1_2.Invoke(ref a));
                Console.WriteLine(invoker1_2.Invoke(b));
                Console.WriteLine(invoker1_3.Invoke(ref a));
                Console.WriteLine(invoker1_3.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            Func<ByRefParam, IntPtr> del3 = TestFunc3;
            // Pass4
            {
                var invoker1 = new PointerFunc<IntPtr, ByRefParam>(del3);
                var invoker1_2 = invoker1.Clone().WithRefParam(0);
                var invoker1_3 = invoker1.Clone().WithRefParam(0, false);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine(invoker1_2.Invoke(ref a));
                Console.WriteLine(invoker1_2.Invoke(b));
                Console.WriteLine(invoker1_3.Invoke(ref a));
                Console.WriteLine(invoker1_3.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
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
        public static IntPtr TestFunc3(ByRefParam o)
        {
            unsafe
            {
                TypedReference tr = __makeref(o);
                IntPtr pptr = *(IntPtr*)(&tr);
                IntPtr address = *(IntPtr*)pptr;
                return address;
            }
        }
    }
}
