using System;
using System.Collections.Generic;
using System.Text;

namespace Mod.LowLevel
{
    public enum ParamFlag
    {
        Void = 0,
        ByValue = 1,
        ByRef = 2,
    }
    public interface IFreeInvokable
    {
        void SetParamFlag(int index, ParamFlag flag);
        ParamFlag GetParamFlag(int index);
    }
    public static partial class FreeInvokable
    {
        public static F WithRefParam<F>(this F func, int index, bool isref = true) where F : IFreeInvokable
        {
            func.SetParamFlag(index, isref ? ParamFlag.ByRef : ParamFlag.ByValue);
            return func;
        }
        public static F WithRefReturn<F>(this F func, bool isref = true) where F : IFreeInvokable
        {
            if (isref)
            {
                func.SetParamFlag(-1, ParamFlag.ByRef);
            }
            else
            {
                if (func.GetParamFlag(-1) == ParamFlag.ByRef)
                {
                    func.SetParamFlag(-1, ParamFlag.ByValue);
                }
            }
            return func;
        }
    }
    #region IFreeInvokableFunc
    public interface IFreeInvokableFunc<R> : IFreeInvokable
    {
        ref R Invoke(out R r);
    }
    public interface IFreeInvokableFunc1<R> : IFreeInvokable
    {
        ref R Invoke<P1>(out R r, in P1 p1);
    }
    public interface IFreeInvokableFunc2<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2);
    }
    public interface IFreeInvokableFunc3<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3);
    }
    public interface IFreeInvokableFunc4<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4);
    }
    public interface IFreeInvokableFunc5<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
    }
    public interface IFreeInvokableFunc6<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
    }
    public interface IFreeInvokableFunc7<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
    }
    public interface IFreeInvokableFunc8<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
    }
    public interface IFreeInvokableFunc9<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
    }
    public interface IFreeInvokableFunc10<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
    }
    public interface IFreeInvokableFunc11<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
    }
    public interface IFreeInvokableFunc12<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
    }
    public interface IFreeInvokableFunc13<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
    }
    public interface IFreeInvokableFunc14<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
    }
    public interface IFreeInvokableFunc15<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
    }
    public interface IFreeInvokableFunc16<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
    }
    #endregion
    public static partial class FreeInvokable
    {
        public static R Invoke<R>(this IFreeInvokableFunc<R> func)
        {
            return func.Invoke(out var r);
        }
        public static R Invoke<R, P1>(this IFreeInvokableFunc1<R> func, in P1 p1)
        {
            return func.Invoke(out var r, in p1);
        }
        public static R Invoke<R, P1, P2>(this IFreeInvokableFunc2<R> func, in P1 p1, in P2 p2)
        {
            return func.Invoke(out var r, in p1, in p2);
        }
        public static R Invoke<R, P1, P2, P3>(this IFreeInvokableFunc3<R> func, in P1 p1, in P2 p2, in P3 p3)
        {
            return func.Invoke(out var r, in p1, in p2, in p3);
        }
        public static R Invoke<R, P1, P2, P3, P4>(this IFreeInvokableFunc4<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5>(this IFreeInvokableFunc5<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6>(this IFreeInvokableFunc6<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7>(this IFreeInvokableFunc7<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8>(this IFreeInvokableFunc8<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9>(this IFreeInvokableFunc9<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(this IFreeInvokableFunc10<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(this IFreeInvokableFunc11<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(this IFreeInvokableFunc12<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(this IFreeInvokableFunc13<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(this IFreeInvokableFunc14<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(this IFreeInvokableFunc15<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public static R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(this IFreeInvokableFunc16<R> func, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return func.Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
    }
}