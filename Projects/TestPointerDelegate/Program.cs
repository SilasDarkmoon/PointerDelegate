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
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass2:
            {
                var invoker1 = new PointerFunc<IntPtr, ByRefParam>(fn1);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            Func<IntPtr, IntPtr> del2 = TestFunc2;
            // Pass3
            {
                var invoker1 = new PointerFunc<IntPtr, IntPtr>(del2);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            Func<ByRefParam, IntPtr> del3 = TestFunc3;
            // Pass4
            {
                var invoker1 = new PointerFunc<IntPtr, ByRefParam>(del3);
                Console.WriteLine(invoker1.Invoke(ref a));
                Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            // Pass5
            var mi5 = typeof(Program).GetMethod("TestFunc5");
            RuntimeHelpers.PrepareMethod(mi5.MethodHandle);
            var fn5 = mi5.MethodHandle.GetFunctionPointer();
            {
                var invoker1 = new PointerFunc<ByRefParam, ByRefParam>(fn5);
                Console.WriteLine(invoker1.Invoke(ref a).ToRef<int>());
                //Console.WriteLine(invoker1.Invoke(b));
                Console.WriteLine("------------------------------------------------------------------------");
            }
            var mi6 = typeof(Program).GetMethod("TestFunc6");
            RuntimeHelpers.PrepareMethod(mi6.MethodHandle);
            var fn6 = mi6.MethodHandle.GetFunctionPointer();
            // Pass6
            {
                var invoker1 = new PointerFunc<VoidReturn, ByRefParam>(fn6);
                Console.WriteLine(invoker1.Invoke(a.ToFakeObj()));
                Console.WriteLine(a);
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
        public static ref int TestFunc5(ref int r)
        {
            ++r;
            return ref r;
        }
        public static void TestFunc6(ref int r)
        {
            Console.WriteLine("in TestFunc6");
            ++r;
        }
    }
}
