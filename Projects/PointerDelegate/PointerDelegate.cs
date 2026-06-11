using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mod.LowLevel
{
    public static class PointerDelegateExtensions
    {
        public static ref T ToRef<T>(this ByRefParam fakeobj)
        {
            throw new NotImplementedException();
        }
        public static ref T ToRef<T>(this ByRefPtr fakeptr)
        {
            throw new NotImplementedException();
        }
        public static ByRefParam ToFakeRefObj<T>(ref T r)
        {
            throw new NotImplementedException();
        }
        public static ByRefParam ToFakeObj<T>(this ref T r) where T : struct
        {
            return ToFakeRefObj(ref r);
        }
        public static ByRefPtr ToFakeRefPtr<T>(ref T r)
        {
            throw new NotImplementedException();
        }
        public static ByRefPtr ToRefPtr<T>(this ref T r) where T : struct
        {
            return ToFakeRefPtr(ref r);
        }
    }

    /// <summary>
    /// Use this to indicate an parameter is a by-ref parameter. When used as return value, it is pretended to be an object but actually a ref.
    /// </summary>
    public sealed class ByRefParam
    {
        private ByRefParam() { }
    }
    /// <summary>
    /// Use this to indicate an parameter is a by-ref parameter. Used when method receives or returns a IntPtr, but caller passes or expects a ref parameter.
    /// </summary>
    public struct ByRefPtr
    {
        private object _InnerRef;
    }
    /// <summary>
    /// Use this to indicate the func will return nothing.
    /// </summary>
    public sealed class VoidReturn
    {
        private VoidReturn() { }
    }

    public abstract class FreeInvokable : ICloneable, IFreeInvokable
    {
        protected FreeInvokable() { }

        protected uint _RefParamFlags;
        protected bool GetRefParamFlag(int paramIndex)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            bool flag = (_RefParamFlags & (1u << paramIndex)) != 0;
            return flag;
        }
        protected void SetRefParamFlag(int paramIndex, bool isRefParam)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            if (isRefParam)
            {
                _RefParamFlags |= (1u << paramIndex);
            }
            else
            {
                _RefParamFlags &= ~(1u << paramIndex);
            }
        }
        //protected enum ReturnCategory
        //{
        //    Void = 0,
        //    Val = 1,
        //    Ref = 2,
        //}
        protected int _ReturnCategory;
        protected int JudgeReturnCategory(Type ut)
        {
            if (ut == typeof(VoidReturn))
            {
                return 0;
            }
            else if (ut == typeof(ByRefParam) || ut == typeof(ByRefPtr))
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }
    }
    public abstract class FreeInvokable<R> : FreeInvokable, IFreeInvokableFunc<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
        }
        public abstract R Invoke();
    }
    public abstract class FreeInvokable<R, U1> : FreeInvokable, IFreeInvokableFunc1<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1>(in P1 p1);
    }
    public abstract class FreeInvokable<R, U1, U2> : FreeInvokable, IFreeInvokableFunc2<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2>(in P1 p1, in P2 p2);
    }
    public abstract class FreeInvokable<R, U1, U2, U3> : FreeInvokable, IFreeInvokableFunc3<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4> : FreeInvokable, IFreeInvokableFunc4<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5> : FreeInvokable, IFreeInvokableFunc5<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6> : FreeInvokable, IFreeInvokableFunc6<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable, IFreeInvokableFunc7<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable, IFreeInvokableFunc8<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable, IFreeInvokableFunc9<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable, IFreeInvokableFunc10<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable, IFreeInvokableFunc11<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
            SetRefParamFlag(10, typeof(U11) == typeof(ByRefParam) || typeof(U11) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable, IFreeInvokableFunc12<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
            SetRefParamFlag(10, typeof(U11) == typeof(ByRefParam) || typeof(U11) == typeof(ByRefPtr));
            SetRefParamFlag(11, typeof(U12) == typeof(ByRefParam) || typeof(U12) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable, IFreeInvokableFunc13<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
            SetRefParamFlag(10, typeof(U11) == typeof(ByRefParam) || typeof(U11) == typeof(ByRefPtr));
            SetRefParamFlag(11, typeof(U12) == typeof(ByRefParam) || typeof(U12) == typeof(ByRefPtr));
            SetRefParamFlag(12, typeof(U13) == typeof(ByRefParam) || typeof(U13) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable, IFreeInvokableFunc14<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
            SetRefParamFlag(10, typeof(U11) == typeof(ByRefParam) || typeof(U11) == typeof(ByRefPtr));
            SetRefParamFlag(11, typeof(U12) == typeof(ByRefParam) || typeof(U12) == typeof(ByRefPtr));
            SetRefParamFlag(12, typeof(U13) == typeof(ByRefParam) || typeof(U13) == typeof(ByRefPtr));
            SetRefParamFlag(13, typeof(U14) == typeof(ByRefParam) || typeof(U14) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable, IFreeInvokableFunc15<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
            SetRefParamFlag(10, typeof(U11) == typeof(ByRefParam) || typeof(U11) == typeof(ByRefPtr));
            SetRefParamFlag(11, typeof(U12) == typeof(ByRefParam) || typeof(U12) == typeof(ByRefPtr));
            SetRefParamFlag(12, typeof(U13) == typeof(ByRefParam) || typeof(U13) == typeof(ByRefPtr));
            SetRefParamFlag(13, typeof(U14) == typeof(ByRefParam) || typeof(U14) == typeof(ByRefPtr));
            SetRefParamFlag(14, typeof(U15) == typeof(ByRefParam) || typeof(U15) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable, IFreeInvokableFunc16<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1) == typeof(ByRefParam) || typeof(U1) == typeof(ByRefPtr));
            SetRefParamFlag(1, typeof(U2) == typeof(ByRefParam) || typeof(U2) == typeof(ByRefPtr));
            SetRefParamFlag(2, typeof(U3) == typeof(ByRefParam) || typeof(U3) == typeof(ByRefPtr));
            SetRefParamFlag(3, typeof(U4) == typeof(ByRefParam) || typeof(U4) == typeof(ByRefPtr));
            SetRefParamFlag(4, typeof(U5) == typeof(ByRefParam) || typeof(U5) == typeof(ByRefPtr));
            SetRefParamFlag(5, typeof(U6) == typeof(ByRefParam) || typeof(U6) == typeof(ByRefPtr));
            SetRefParamFlag(6, typeof(U7) == typeof(ByRefParam) || typeof(U7) == typeof(ByRefPtr));
            SetRefParamFlag(7, typeof(U8) == typeof(ByRefParam) || typeof(U8) == typeof(ByRefPtr));
            SetRefParamFlag(8, typeof(U9) == typeof(ByRefParam) || typeof(U9) == typeof(ByRefPtr));
            SetRefParamFlag(9, typeof(U10) == typeof(ByRefParam) || typeof(U10) == typeof(ByRefPtr));
            SetRefParamFlag(10, typeof(U11) == typeof(ByRefParam) || typeof(U11) == typeof(ByRefPtr));
            SetRefParamFlag(11, typeof(U12) == typeof(ByRefParam) || typeof(U12) == typeof(ByRefPtr));
            SetRefParamFlag(12, typeof(U13) == typeof(ByRefParam) || typeof(U13) == typeof(ByRefPtr));
            SetRefParamFlag(13, typeof(U14) == typeof(ByRefParam) || typeof(U14) == typeof(ByRefPtr));
            SetRefParamFlag(14, typeof(U15) == typeof(ByRefParam) || typeof(U15) == typeof(ByRefPtr));
            SetRefParamFlag(15, typeof(U16) == typeof(ByRefParam) || typeof(U16) == typeof(ByRefPtr));
        }
        public abstract R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
    }

    public class PointerFunc<R> : FreeInvokable<R>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public override R Invoke()
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R> Clone()
        {
            return MemberwiseClone() as PointerFunc<R>;
        }
    }
    public class PointerFunc<R, U1> : FreeInvokable<R, U1>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1>(in P1 p1)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1>;
        }
    }
    public class PointerFunc<R, U1, U2> : FreeInvokable<R, U1, U2>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2>;
        }
    }
    public class PointerFunc<R, U1, U2, U3> : FreeInvokable<R, U1, U2, U3>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4> : FreeInvokable<R, U1, U2, U3, U4>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5> : FreeInvokable<R, U1, U2, U3, U4, U5>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6> : FreeInvokable<R, U1, U2, U3, U4, U5, U6>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>
    {
        protected IntPtr _Pfn;
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            throw new NotImplementedException();
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>;
        }
    }

    public class FreeFunc<R> : FreeInvokable<R>
    {
        protected Func<R> _Del;
        public FreeFunc(Action del)
        {
            _Del = new Func<R>(() =>
            {
                del();
                return default(R);
            });
        }
        public FreeFunc(Func<R> del)
        {
            _Del = del;
        }
        public override R Invoke()
        {
            return _Del();
        }
        public FreeFunc<R> Clone()
        {
            return MemberwiseClone() as FreeFunc<R>;
        }
    }
    public class FreeFunc<R, U1> : FreeInvokable<R, U1>
    {
        protected Func<U1, R> _Del;
        public FreeFunc(Action<U1> del)
        {
            _Del = new Func<U1, R>((p1) =>
            {
                del(p1);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1)
        {
            return _Del(p1);
        }
        public override R Invoke<P1>(in P1 p1)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1>;
        }
    }
    public class FreeFunc<R, U1, U2> : FreeInvokable<R, U1, U2>
    {
        protected Func<U1, U2, R> _Del;
        public FreeFunc(Action<U1, U2> del)
        {
            _Del = new Func<U1, U2, R>((p1, p2) =>
            {
                del(p1, p2);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2)
        {
            return _Del(p1, p2);
        }
        public override R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2>;
        }
    }
    public class FreeFunc<R, U1, U2, U3> : FreeInvokable<R, U1, U2, U3>
    {
        protected Func<U1, U2, U3, R> _Del;
        public FreeFunc(Action<U1, U2, U3> del)
        {
            _Del = new Func<U1, U2, U3, R>((p1, p2, p3) =>
            {
                del(p1, p2, p3);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3)
        {
            return _Del(p1, p2, p3);
        }
        public override R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4> : FreeInvokable<R, U1, U2, U3, U4>
    {
        protected Func<U1, U2, U3, U4, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4> del)
        {
            _Del = new Func<U1, U2, U3, U4, R>((p1, p2, p3, p4) =>
            {
                del(p1, p2, p3, p4);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            return _Del(p1, p2, p3, p4);
        }
        public override R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5> : FreeInvokable<R, U1, U2, U3, U4, U5>
    {
        protected Func<U1, U2, U3, U4, U5, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, R>((p1, p2, p3, p4, p5) =>
            {
                del(p1, p2, p3, p4, p5);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            return _Del(p1, p2, p3, p4, p5);
        }
        public override R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6> : FreeInvokable<R, U1, U2, U3, U4, U5, U6>
    {
        protected Func<U1, U2, U3, U4, U5, U6, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, R>((p1, p2, p3, p4, p5, p6) =>
            {
                del(p1, p2, p3, p4, p5, p6);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            return _Del(p1, p2, p3, p4, p5, p6);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, R>((p1, p2, p3, p4, p5, p6, p7) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, R>((p1, p2, p3, p4, p5, p6, p7, p8) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>
    {
        protected Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> _Del;
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> del)
        {
            _Del = new Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R>((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16) =>
            {
                del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
                return default(R);
            });
        }
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> del)
        {
            _Del = del;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            return _Del(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
        }
        public override R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>;
        }
    }

    //public delegate ref R RefFunc<R>();
    //public delegate ref R RefFunc<U1, R>(U1 p1);
    //public delegate ref R RefFunc<U1, U2, R>(U1 p1, U2 p2);
    //public delegate ref R RefFunc<U1, U2, U3, R>(U1 p1, U2 p2, U3 p3);
    //public delegate ref R RefFunc<U1, U2, U3, U4, R>(U1 p1, U2 p2, U3 p3, U4 p4);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15);
    //public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16);
}
