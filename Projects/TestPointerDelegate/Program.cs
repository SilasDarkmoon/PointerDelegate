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
            var invoker1 = new PointerFunc<IntPtr, IntPtr>(fn1);
            var invoker1_2 = invoker1.Clone().WithRefParam(0);
            var invoker1_3 = invoker1.Clone().WithRefParam(0, false);
            Console.WriteLine(invoker1.Invoke(ref a));
            Console.WriteLine(invoker1.Invoke(b));
            Console.WriteLine(invoker1_2.Invoke(ref a));
            Console.WriteLine(invoker1_2.Invoke(b));
            Console.WriteLine(invoker1_3.Invoke(ref a));
            Console.WriteLine(invoker1_3.Invoke(b));
        }

        public static IntPtr TestFunc1(ref int p)
        {
            unsafe
            {
                return (IntPtr)Unsafe.AsPointer<int>(ref p);
            }
        }
    }
}
