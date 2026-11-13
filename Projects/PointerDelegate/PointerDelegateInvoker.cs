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
        Ref2Ref = 3,
    }
    public interface IFreeInvokable
    {
        // methods below should be static methods. and should be readonly that  should not be modified outside cctor.
        //void SetParamFlag(int index, ParamFlag flag);
        //ParamFlag GetParamFlag(int index);
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

    public interface IPlainInvokableFunc<R>
    {
        R Invoke();
    }
    public interface IPlainInvokableFunc<R, U1>
    {
        R Invoke(U1 u1);
    }
    public interface IPlainInvokableFunc<R, U1, U2>
    {
        R Invoke(U1 u1, U2 u2);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3>
    {
        R Invoke(U1 u1, U2 u2, U3 u3);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15);
    }
    public interface IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>
    {
        R Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15, U16 u16);
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

        public static ref R Invoke<R>(this IFreeInvokableFunc<R> func, out R r)
        {
            return ref func.Invoke(out r);
        }
        public static ref R Invoke<R, P1>(this IFreeInvokableFunc1<R> func, out R r, in P1 p1)
        {
            return ref func.Invoke(out r, in p1);
        }
        public static ref R Invoke<R, P1, P2>(this IFreeInvokableFunc2<R> func, out R r, in P1 p1, in P2 p2)
        {
            return ref func.Invoke(out r, in p1, in p2);
        }
        public static ref R Invoke<R, P1, P2, P3>(this IFreeInvokableFunc3<R> func, out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3);
        }
        public static ref R Invoke<R, P1, P2, P3, P4>(this IFreeInvokableFunc4<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5>(this IFreeInvokableFunc5<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6>(this IFreeInvokableFunc6<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7>(this IFreeInvokableFunc7<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8>(this IFreeInvokableFunc8<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9>(this IFreeInvokableFunc9<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(this IFreeInvokableFunc10<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(this IFreeInvokableFunc11<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(this IFreeInvokableFunc12<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(this IFreeInvokableFunc13<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(this IFreeInvokableFunc14<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(this IFreeInvokableFunc15<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public static ref R Invoke<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(this IFreeInvokableFunc16<R> func, out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref func.Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }

        public static R Invoke<R>(this IPlainInvokableFunc<R> func)
        {
            return func.Invoke();
        }
        public static R Invoke<R, U1>(this IPlainInvokableFunc<R, U1> func, in U1 u1)
        {
            return func.Invoke(in u1);
        }
        public static R Invoke<R, U1, U2>(this IPlainInvokableFunc<R, U1, U2> func, in U1 u1, in U2 u2)
        {
            return func.Invoke(in u1, in u2);
        }
        public static R Invoke<R, U1, U2, U3>(this IPlainInvokableFunc<R, U1, U2, U3> func, in U1 u1, in U2 u2, in U3 u3)
        {
            return func.Invoke(in u1, in u2, in u3);
        }
        public static R Invoke<R, U1, U2, U3, U4>(this IPlainInvokableFunc<R, U1, U2, U3, U4> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4)
        {
            return func.Invoke(in u1, in u2, in u3, in u4);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10, in u11);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10, in u11, in u12);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10, in u11, in u12, in u13);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10, in u11, in u12, in u13, in u14);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10, in u11, in u12, in u13, in u14, in u15);
        }
        public static R Invoke<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(this IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> func, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15, in U16 u16)
        {
            return func.Invoke(in u1, in u2, in u3, in u4, in u5, in u6, in u7, in u8, in u9, in u10, in u11, in u12, in u13, in u14, in u15, in u16);
        }
    }
}
