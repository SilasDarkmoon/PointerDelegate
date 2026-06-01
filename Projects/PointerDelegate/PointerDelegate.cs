using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mod.LowLevel
{
    public static class PointerDelegateExtensions
    {
        public static T WithRefParam<T>(this T thiz, int paramIndex, bool? isRefParam) where T : PointerDelegate
        {
            thiz.SetRefParamFlag(paramIndex, isRefParam);
            return thiz;
        }
        public static T WithRefParam<T>(this T thiz, int paramIndex) where T : PointerDelegate
        {
            thiz.SetRefParamFlag(paramIndex, true);
            return thiz;
        }
        public static T WithRefParam<T>(this T thiz, params int[] paramIndices) where T : PointerDelegate
        {
            if (paramIndices != null)
            {
                for (int i = 0; i < paramIndices.Length; ++i)
                {
                    thiz.SetRefParamFlag(paramIndices[i], true);
                }
            }
            return thiz;
        }
        public static T WithRefParam<T>(this T thiz, params bool?[] flags) where T : PointerDelegate
        {
            if (flags != null)
            {
                for (int i = 0; i < flags.Length; ++i)
                {
                    thiz.SetRefParamFlag(i, flags[i]);
                }
            }
            return thiz;
        }
        public static ref T ToRef<T>(this ByRefParam fakeobj)
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
    }

    /// <summary>
    /// Use this to indicate an parameter is a by-ref parameter. When used as return value, it is pretended to be an object but actually a ref.
    /// </summary>
    public sealed class ByRefParam
    {
        private ByRefParam() { }
    }
    /// <summary>
    /// Use this to indicate the func will return nothing.
    /// </summary>
    public sealed class VoidReturn
    {
        private VoidReturn() { }
    }

    public abstract class PointerDelegate : ICloneable, IFreeInvokable
    {
        protected IntPtr _Pfn;
        protected PointerDelegate(IntPtr fn)
        {
            _Pfn = fn;
        }
        protected Delegate _Del;
        protected PointerDelegate(Delegate del)
        {
            _Del = del;
        }

        protected uint _RefParamFlags;
        public bool? GetRefParamFlag(int paramIndex)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            bool isNull = (_RefParamFlags & (1u << (paramIndex * 2 + 1))) == 0;
            if (isNull) return null;
            bool flag = (_RefParamFlags & (1u << (paramIndex * 2))) != 0;
            return flag;
        }
        public void SetRefParamFlag(int paramIndex, bool? isRefParam)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            if (isRefParam == null)
            {
                _RefParamFlags &= ~(3u << (paramIndex * 2));
            }
            else
            {
                _RefParamFlags |= (1u << (paramIndex * 2 + 1));
                if (isRefParam.Value)
                {
                    _RefParamFlags |= (1u << (paramIndex * 2));
                }
                else
                {
                    _RefParamFlags &= ~(1u << (paramIndex * 2));
                }
            }
        }
        protected bool IsRefParam(int paramIndex, Type ut, Type pt)
        {
            bool? refFlag = GetRefParamFlag(paramIndex);
            if (refFlag.HasValue) return refFlag.Value;
            if (ut == typeof(ByRefParam))
            {
                SetRefParamFlag(paramIndex, true);
                return true;
            }
            return ut == typeof(IntPtr) && pt != typeof(IntPtr);
        }
        //protected enum RefParamCategory
        //{
        //    Val = 0,
        //    Ref = 1,
        //    Obj = 2,
        //}
        protected int GetRefParamCategory<U, P>(int paramIndex)
        {
            bool? refFlag = GetRefParamFlag(paramIndex);
            var ut = typeof(U);
            var nt = typeof(IntPtr);
            if (refFlag.HasValue)
            {
                if (refFlag.Value)
                {
                    if (ut == nt)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 0;
                }
            }
            var ot = typeof(ByRefParam);
            if (ut == ot)
            {
                SetRefParamFlag(paramIndex, true);
                return 2;
            }
            if (ut == nt && typeof(P) != nt)
            {
                return 1;
            }
            return 0;
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
            else if (ut == typeof(ByRefParam))
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        //protected enum ImpCategory
        //{
        //    FuncPointer = 0,
        //    Action = 1,
        //    Func = 2,
        //}
        protected int _ImpCategory;
        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }
    }

    public class PointerFunc<R> : PointerDelegate, IFreeInvokableFunc<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke()
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R> Clone()
        {
            return MemberwiseClone() as PointerFunc<R>;
        }
    }
    public class PointerFunc<R, U1> : PointerDelegate, IFreeInvokableFunc1<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1>(in P1 p1)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1> WithRefParam(bool? flag1)
        {
            SetRefParamFlag(0, flag1);
            return this;
        }
        public PointerFunc<R, U1> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1>;
        }
    }
    public class PointerFunc<R, U1, U2> : PointerDelegate, IFreeInvokableFunc2<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2> WithRefParam(bool? flag1, bool? flag2)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            return this;
        }
        public PointerFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2>;
        }
    }
    public class PointerFunc<R, U1, U2, U3> : PointerDelegate, IFreeInvokableFunc3<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3> WithRefParam(bool? flag1, bool? flag2, bool? flag3)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            return this;
        }
        public PointerFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4> : PointerDelegate, IFreeInvokableFunc4<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5> : PointerDelegate, IFreeInvokableFunc5<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6> : PointerDelegate, IFreeInvokableFunc6<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> : PointerDelegate, IFreeInvokableFunc7<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : PointerDelegate, IFreeInvokableFunc8<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : PointerDelegate, IFreeInvokableFunc9<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : PointerDelegate, IFreeInvokableFunc10<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : PointerDelegate, IFreeInvokableFunc11<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10, bool? flag11)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            SetRefParamFlag(10, flag11);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : PointerDelegate, IFreeInvokableFunc12<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10, bool? flag11, bool? flag12)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            SetRefParamFlag(10, flag11);
            SetRefParamFlag(11, flag12);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : PointerDelegate, IFreeInvokableFunc13<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10, bool? flag11, bool? flag12, bool? flag13)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            SetRefParamFlag(10, flag11);
            SetRefParamFlag(11, flag12);
            SetRefParamFlag(12, flag13);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : PointerDelegate, IFreeInvokableFunc14<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10, bool? flag11, bool? flag12, bool? flag13, bool? flag14)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            SetRefParamFlag(10, flag11);
            SetRefParamFlag(11, flag12);
            SetRefParamFlag(12, flag13);
            SetRefParamFlag(13, flag14);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : PointerDelegate, IFreeInvokableFunc15<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10, bool? flag11, bool? flag12, bool? flag13, bool? flag14, bool? flag15)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            SetRefParamFlag(10, flag11);
            SetRefParamFlag(11, flag12);
            SetRefParamFlag(12, flag13);
            SetRefParamFlag(13, flag14);
            SetRefParamFlag(14, flag15);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : PointerDelegate, IFreeInvokableFunc16<R>
    {
        public PointerFunc(IntPtr fn) : base(fn)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 0;
        }
        public PointerFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 1;
        }
        public PointerFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> del) : base(del)
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            _ImpCategory = 2;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            throw new NotImplementedException();
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> WithRefParam(bool? flag1, bool? flag2, bool? flag3, bool? flag4, bool? flag5, bool? flag6, bool? flag7, bool? flag8, bool? flag9, bool? flag10, bool? flag11, bool? flag12, bool? flag13, bool? flag14, bool? flag15, bool? flag16)
        {
            SetRefParamFlag(0, flag1);
            SetRefParamFlag(1, flag2);
            SetRefParamFlag(2, flag3);
            SetRefParamFlag(3, flag4);
            SetRefParamFlag(4, flag5);
            SetRefParamFlag(5, flag6);
            SetRefParamFlag(6, flag7);
            SetRefParamFlag(7, flag8);
            SetRefParamFlag(8, flag9);
            SetRefParamFlag(9, flag10);
            SetRefParamFlag(10, flag11);
            SetRefParamFlag(11, flag12);
            SetRefParamFlag(12, flag13);
            SetRefParamFlag(13, flag14);
            SetRefParamFlag(14, flag15);
            SetRefParamFlag(15, flag16);
            return this;
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>;
        }
    }

    public delegate ref R RefFunc<R>();
    public delegate ref R RefFunc<U1, R>(U1 p1);
    public delegate ref R RefFunc<U1, U2, R>(U1 p1, U2 p2);
    public delegate ref R RefFunc<U1, U2, U3, R>(U1 p1, U2 p2, U3 p3);
    public delegate ref R RefFunc<U1, U2, U3, U4, R>(U1 p1, U2 p2, U3 p3, U4 p4);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15);
    public delegate ref R RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R>(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16);
}
