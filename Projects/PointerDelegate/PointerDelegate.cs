using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Mod.LowLevel
{
    /// <summary>
    /// Use this to indicate the func will return nothing.
    /// </summary>
    public sealed class VoidReturn
    {
        private VoidReturn() { }
    }
    /// <summary>
    /// Use this to indicate a parameter or return-value to be passed ByRef.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ByRefParam
    {
        private IntPtr _Address;
    }
    #region Creators
    public struct FunctionPointer : IFunctionPointerUnmanagedIndicator
    {
        public IntPtr Pfn;
        private bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public FunctionPointer(IntPtr pfn)
        {
            Pfn = pfn;
            _IsUnmanaged = false;
        }
        public FunctionPointer(IntPtr pfn, bool unmanaged)
        {
            Pfn = pfn;
            _IsUnmanaged= unmanaged;
        }

        public static implicit operator IntPtr(FunctionPointer thiz)
        {
            return thiz.Pfn;
        }
        public static explicit operator FunctionPointer(IntPtr pfn)
        {
            return new FunctionPointer(pfn);
        }
    }
    public static partial class FreeInvokable
    {
        public static FunctionPointer AsFunctionPointer(this IntPtr pfn) { return new FunctionPointer(pfn); }
        public static FunctionPointer AsFunctionPointer(this IntPtr pfn, bool unmanaged) { return new FunctionPointer(pfn, unmanaged); }
        public static PointerFunc<R> CreateFreeInvokable<R>(this FunctionPointer pfn) { return new PointerFunc<R>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1> CreateFreeInvokable<R, U1>(this FunctionPointer pfn) { return new PointerFunc<R, U1>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2> CreateFreeInvokable<R, U1, U2>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3> CreateFreeInvokable<R, U1, U2, U3>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4> CreateFreeInvokable<R, U1, U2, U3, U4>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5> CreateFreeInvokable<R, U1, U2, U3, U4, U5>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(pfn, pfn.IsUnmanaged); }
        public static PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(this FunctionPointer pfn) { return new PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(pfn, pfn.IsUnmanaged); }
        public static T AsUnmanaged<T>(this T wrapper, bool unmanaged = true) where T : IFunctionPointerUnmanagedIndicator
        {
            wrapper.IsUnmanaged = unmanaged;
            return wrapper;
        }
        public static FreeFunc<R> CreateFreeInvokable<R>(this Func<R> func) { return new FreeFunc<R>(func); }
        public static FreeFunc<R, U1> CreateFreeInvokable<R, U1>(this Func<U1, R> func) { return new FreeFunc<R, U1>(func); }
        public static FreeFunc<R, U1, U2> CreateFreeInvokable<R, U1, U2>(this Func<U1, U2, R> func) { return new FreeFunc<R, U1, U2>(func); }
        public static FreeFunc<R, U1, U2, U3> CreateFreeInvokable<R, U1, U2, U3>(this Func<U1, U2, U3, R> func) { return new FreeFunc<R, U1, U2, U3>(func); }
        public static FreeFunc<R, U1, U2, U3, U4> CreateFreeInvokable<R, U1, U2, U3, U4>(this Func<U1, U2, U3, U4, R> func) { return new FreeFunc<R, U1, U2, U3, U4>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5> CreateFreeInvokable<R, U1, U2, U3, U4, U5>(this Func<U1, U2, U3, U4, U5, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6>(this Func<U1, U2, U3, U4, U5, U6, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>(this Func<U1, U2, U3, U4, U5, U6, U7, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(this Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(func); }
        public static FreeFunc<R> CreateFreeInvokable<R>(this Action act) { return new FreeFunc<R>(act); }
        public static FreeFunc<R, U1> CreateFreeInvokable<R, U1>(this Action<U1> act) { return new FreeFunc<R, U1>(act); }
        public static FreeFunc<R, U1, U2> CreateFreeInvokable<R, U1, U2>(this Action<U1, U2> act) { return new FreeFunc<R, U1, U2>(act); }
        public static FreeFunc<R, U1, U2, U3> CreateFreeInvokable<R, U1, U2, U3>(this Action<U1, U2, U3> act) { return new FreeFunc<R, U1, U2, U3>(act); }
        public static FreeFunc<R, U1, U2, U3, U4> CreateFreeInvokable<R, U1, U2, U3, U4>(this Action<U1, U2, U3, U4> act) { return new FreeFunc<R, U1, U2, U3, U4>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5> CreateFreeInvokable<R, U1, U2, U3, U4, U5>(this Action<U1, U2, U3, U4, U5> act) { return new FreeFunc<R, U1, U2, U3, U4, U5>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6>(this Action<U1, U2, U3, U4, U5, U6> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>(this Action<U1, U2, U3, U4, U5, U6, U7> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>(this Action<U1, U2, U3, U4, U5, U6, U7, U8> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(act); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(this Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> act) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(act); }
    }
    #endregion

    public abstract class FreeInvokableBase : ICloneable, IFreeInvokable
    {
        protected FreeInvokableBase() { }

        protected uint _RefParamFlags;
        protected internal static bool GetRefParamFlag(uint flags, int paramIndex)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            bool flag = (flags & (1u << paramIndex)) != 0;
            return flag;
        }
        protected bool GetRefParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_RefParamFlags, paramIndex);
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
        protected bool JudgeParamRefFlag(Type ut)
        {
            if (ut == typeof(ByRefParam))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool SetRefParamFlag(int paramIndex, Type ut)
        {
            var isref = JudgeParamRefFlag(ut);
            SetRefParamFlag(paramIndex, isref);
            return isref;
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
        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }

        public void SetParamFlag(int index, ParamFlag flag)
        {
            if (index == -1)
            {
                // return
                _ReturnCategory = (int)flag;
            }
            else
            {
                SetRefParamFlag(index, flag == ParamFlag.ByRef);
            }
        }
        public ParamFlag GetParamFlag(int index)
        {
            if (index == -1)
            {
                // return
                return (ParamFlag)_ReturnCategory;
            }
            else
            {
                return GetRefParamFlag(index) ? ParamFlag.ByRef : ParamFlag.ByValue;
            }
        }

        #region DynamicGenerator
        protected static ref T ConvertAddressToRef<T>(IntPtr address)
        {
            throw new NotImplementedException();
        }
        protected static IntPtr ConvertRefToAddress<T>(in T r)
        {
            throw new NotImplementedException();
        }
        protected static U ConvertParam<P, U>(P p)
        {
            U u = default(U);
            ref P ru = ref ConvertRef<U, P>(in u);
            ru = p;
            return u;
        }

        protected static U ConvertParam<P, U>(in P p, bool isref)
        {
            if (isref)
            {
                var address = ConvertRefToAddress(in p);
                return ConvertParam<IntPtr, U>(address);
            }
            else
            {
                return ConvertParam<P, U>(p);
            }
        }
        protected static ref R ConvertAddressToRef<R>(R r)
        {
            IntPtr address = ConvertParam<R, IntPtr>(r);
            return ref ConvertAddressToRef<R>(address);
        }
        protected static ref T ConvertRef<F, T>(in F f)
        {
            for (; ; )
            {
                IntPtr address = ConvertRefToAddress(in f);
                ref T result = ref ConvertAddressToRef<T>(address);
                if (address == ConvertRefToAddress(in f))
                {
                    return ref result;
                }
            }
        }
        protected static bool _IsDynamicCodeDisabled = false;
        protected internal static T CreateDelegate<T>(MethodInfo dm) where T : Delegate
        {
            return (T)(dm is System.Reflection.Emit.DynamicMethod dynDm
                ? dynDm.CreateDelegate(typeof(T))
                : Delegate.CreateDelegate(typeof(T), dm));
        }
        //protected struct ParamInfo : IEquatable<ParamInfo>
        //{
        //    public Type ParamType;
        //    public bool IsByRef;

        //    public override int GetHashCode()
        //    {
        //        var hash = ParamType?.GetHashCode() ?? 0;
        //        return IsByRef ? ~hash : hash;
        //    }
        //    public bool Equals(ParamInfo other)
        //    {
        //        return ParamType == other.ParamType
        //            && IsByRef == other.IsByRef;
        //    }
        //    public override bool Equals(object obj)
        //    {
        //        return obj is ParamInfo other && Equals(other);
        //    }
        //    public static bool operator==(ParamInfo a, ParamInfo b)
        //    {
        //        return a.Equals(b);
        //    }
        //    public static bool operator!=(ParamInfo a, ParamInfo b)
        //    {
        //        return !a.Equals(b);
        //    }
        //}
        //protected struct ParamInfos : IEquatable<ParamInfos>
        //{
        //    public ParamInfo P0;
        //    public ParamInfo P1;
        //    public ParamInfo P2;
        //    public ParamInfo P3;
        //    public ParamInfo P4;
        //    public ParamInfo P5;
        //    public ParamInfo P6;
        //    public ParamInfo P7;
        //    public ParamInfo P8;
        //    public ParamInfo P9;
        //    public ParamInfo P10;
        //    public ParamInfo P11;
        //    public ParamInfo P12;
        //    public ParamInfo P13;
        //    public ParamInfo P14;
        //    public ParamInfo P15;
        //    public ParamInfo P16;

        //    public override int GetHashCode()
        //    {
        //        return P0.GetHashCode() ^ P1.GetHashCode() ^ P2.GetHashCode()
        //            ^ P3.GetHashCode() ^ P4.GetHashCode() ^ P5.GetHashCode()
        //            ^ P6.GetHashCode() ^ P7.GetHashCode() ^ P8.GetHashCode()
        //            ^ P9.GetHashCode() ^ P10.GetHashCode() ^ P11.GetHashCode()
        //            ^ P12.GetHashCode() ^ P13.GetHashCode() ^ P14.GetHashCode()
        //            ^ P15.GetHashCode() ^ P16.GetHashCode();
        //    }
        //    public bool Equals(ParamInfos other)
        //    {
        //        return P0 == other.P0 && P1 == other.P1 && P2 == other.P2
        //            && P3 == other.P3 && P4 == other.P4 && P5 == other.P5
        //            && P6 == other.P6 && P7 == other.P7 && P8 == other.P8
        //            && P9 == other.P9 && P10 == other.P10 && P11 == other.P11
        //            && P12 == other.P12 && P13 == other.P13 && P14 == other.P14
        //            && P15 == other.P15 && P16 == other.P16;
        //    }
        //    public override bool Equals(object obj)
        //    {
        //        return obj is ParamInfos other && Equals(other);
        //    }
        //    public static bool operator ==(ParamInfos a, ParamInfos b)
        //    {
        //        return a.Equals(b);
        //    }
        //    public static bool operator !=(ParamInfos a, ParamInfos b)
        //    {
        //        return !a.Equals(b);
        //    }
        //}
        #endregion
    }
    public abstract class FreeInvokable<R> : FreeInvokableBase, IFreeInvokableFunc<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
        }
        public abstract ref R Invoke(out R r);
        public virtual R Invoke()
        {
            return FreeInvokable.Invoke(this);
        }
    }
    public abstract class FreeInvokable<R, U1> : FreeInvokableBase, IFreeInvokableFunc1<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
        }
        public abstract R Invoke(U1 p1);
        public abstract ref R Invoke<P1>(out R r, in P1 p1);
        public R Invoke<P1>(in P1 p1)
        {
            return FreeInvokable.Invoke(this, in p1);
        }
    }
    public abstract class FreeInvokable<R, U1, U2> : FreeInvokableBase, IFreeInvokableFunc2<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
        }
        public abstract R Invoke(U1 p1, U2 p2);
        public abstract ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2);
        public R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            return FreeInvokable.Invoke(this, in p1, in p2);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3> : FreeInvokableBase, IFreeInvokableFunc3<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3);
        public abstract ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3);
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4> : FreeInvokableBase, IFreeInvokableFunc4<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4);
        public abstract ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4);
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5> : FreeInvokableBase, IFreeInvokableFunc5<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5);
        public abstract ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6> : FreeInvokableBase, IFreeInvokableFunc6<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokableBase, IFreeInvokableFunc7<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokableBase, IFreeInvokableFunc8<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokableBase, IFreeInvokableFunc9<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokableBase, IFreeInvokableFunc10<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokableBase, IFreeInvokableFunc11<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
            SetRefParamFlag(10, typeof(U11));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokableBase, IFreeInvokableFunc12<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
            SetRefParamFlag(10, typeof(U11));
            SetRefParamFlag(11, typeof(U12));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokableBase, IFreeInvokableFunc13<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
            SetRefParamFlag(10, typeof(U11));
            SetRefParamFlag(11, typeof(U12));
            SetRefParamFlag(12, typeof(U13));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokableBase, IFreeInvokableFunc14<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
            SetRefParamFlag(10, typeof(U11));
            SetRefParamFlag(11, typeof(U12));
            SetRefParamFlag(12, typeof(U13));
            SetRefParamFlag(13, typeof(U14));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokableBase, IFreeInvokableFunc15<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
            SetRefParamFlag(10, typeof(U11));
            SetRefParamFlag(11, typeof(U12));
            SetRefParamFlag(12, typeof(U13));
            SetRefParamFlag(13, typeof(U14));
            SetRefParamFlag(14, typeof(U15));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokableBase, IFreeInvokableFunc16<R>
    {
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(0, typeof(U1));
            SetRefParamFlag(1, typeof(U2));
            SetRefParamFlag(2, typeof(U3));
            SetRefParamFlag(3, typeof(U4));
            SetRefParamFlag(4, typeof(U5));
            SetRefParamFlag(5, typeof(U6));
            SetRefParamFlag(6, typeof(U7));
            SetRefParamFlag(7, typeof(U8));
            SetRefParamFlag(8, typeof(U9));
            SetRefParamFlag(9, typeof(U10));
            SetRefParamFlag(10, typeof(U11));
            SetRefParamFlag(11, typeof(U12));
            SetRefParamFlag(12, typeof(U13));
            SetRefParamFlag(13, typeof(U14));
            SetRefParamFlag(14, typeof(U15));
            SetRefParamFlag(15, typeof(U16));
        }
        public abstract R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16);
        public abstract ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return FreeInvokable.Invoke(this, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
    }

    internal static class PointerFuncEmit
    {
        // ConvertParam<P, U>(P p) — the 1-arg generic overload on FreeInvokableBase
        // (bit-pattern reinterpretation: returns U whose bits are p's pointer value).
        static readonly System.Reflection.MethodInfo s_convertParam1;
        // netstandard2.0's ILGenerator ref lacks the unmanaged EmitCalli overload
        // (OpCode, Interop.CallingConvention, Type, Type[]) — present on both the
        // net472 and CoreCLR runtimes; reflect it once.
        static readonly System.Reflection.MethodInfo s_emitCalliUnmanaged;

        static PointerFuncEmit()
        {
            foreach (var m in typeof(FreeInvokableBase).GetMethods(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static))
            {
                if (m.Name == "ConvertParam" && m.IsGenericMethodDefinition
                    && m.GetParameters().Length == 1)
                {
                    s_convertParam1 = m;
                    break;
                }
            }
            s_emitCalliUnmanaged = typeof(System.Reflection.Emit.ILGenerator).GetMethod(
                "EmitCalli",
                new[] { typeof(System.Reflection.Emit.OpCode),
                        typeof(System.Runtime.InteropServices.CallingConvention),
                        typeof(Type), typeof(Type[]) });
        }

        // netstandard2.0's AppDomain ref lacks DefineDynamicAssembly; reflect it (present
        // on the .NET Framework / Mono runtimes, which are the only paths reaching the
        // AssemblyBuilder fallback — DynamicMethod rejects ByRef returns there).
        // Unity's Mono may not support RunAndCollect — degrade to Run.
        static System.Reflection.MethodInfo s_defineDynamicAssembly;
        static System.Reflection.Emit.AssemblyBuilder CreateCollectibleBuilder(string name)
        {
            if (s_defineDynamicAssembly == null)
            {
                s_defineDynamicAssembly = typeof(AppDomain).GetMethod("DefineDynamicAssembly",
                    new[] { typeof(AssemblyName), typeof(System.Reflection.Emit.AssemblyBuilderAccess) });
            }
            try
            {
                return (System.Reflection.Emit.AssemblyBuilder)s_defineDynamicAssembly.Invoke(
                    AppDomain.CurrentDomain,
                    new object[] { new AssemblyName(name), System.Reflection.Emit.AssemblyBuilderAccess.RunAndCollect });
            }
            catch (Exception)
            {
                // Mono (Unity Editor) may reject RunAndCollect — plain Run keeps the
                // assembly alive for the process lifetime (acceptable: emit caches are
                // static and bounded by instantiation count anyway).
                return (System.Reflection.Emit.AssemblyBuilder)s_defineDynamicAssembly.Invoke(
                    AppDomain.CurrentDomain,
                    new object[] { new AssemblyName(name), System.Reflection.Emit.AssemblyBuilderAccess.Run });
            }
        }

        // netstandard2.0's TypeBuilder ref lacks CreateType — same reflective treatment.
        static System.Reflection.MethodInfo s_createType;
        static Type CreateBuilderType(System.Reflection.Emit.TypeBuilder type)
        {
            if (s_createType == null)
                s_createType = typeof(System.Reflection.Emit.TypeBuilder).GetMethod("CreateType");
            return (Type)s_createType.Invoke(type, null);
        }

        // Emits DynamicInvoke: ref R (IntPtr pfn, out R r, in U1 u1, ..., in Un un).
        // All flag decisions are made HERE at emit time — the emitted IL is a single
        // straight-line calli plus return-value massaging, no branches.
        // Returns MethodInfo (DynamicMethod on CoreCLR; an AssemblyBuilder static
        // method on .NET Framework / Mono, where DynamicMethod rejects ByRef returns).
        public static System.Reflection.MethodInfo EmitDynamicInvoker(bool unmanaged, Type returnType, Type[] Ux, int returnFlag, uint paramFlags)
        {
            // arg 0 = pfn, arg 1 = r (R&), arg 2+i = ux (Ux&).
            var paramTypes = new Type[2 + Ux.Length];
            paramTypes[0] = typeof(IntPtr);
            paramTypes[1] = returnType.MakeByRefType();
            for (int i = 0; i < Ux.Length; i++)
                paramTypes[2 + i] = Ux[i].MakeByRefType();

            try
            {
                var dm = new DynamicMethod("DynamicInvoke", returnType.MakeByRefType(), paramTypes,
                    typeof(FreeInvokableBase), true);
                EmitBody(dm.GetILGenerator(), unmanaged, returnType, Ux, returnFlag, paramFlags);
                return dm;
            }
            catch (Exception)
            {
                // .NET Framework throws NotSupportedException here; Unity Mono throws
                // ArgumentException ("Return type can't be a byref type", C0d probe) —
                // catch both (any failure) and fall back to a collectible
                // AssemblyBuilder method (MethodBuilder allows ByRef returns). The
                // emitted type derives from FreeInvokableBase so the protected static
                // conversion primitives (ConvertParam etc.) stay callable.
                var ab = CreateCollectibleBuilder("PointerFuncEmit_" + Guid.NewGuid().ToString("N"));
                var mod = ab.DefineDynamicModule("PointerFuncEmitMod");
                var type = mod.DefineType("PointerFuncEmitType",
                    System.Reflection.TypeAttributes.Abstract | System.Reflection.TypeAttributes.Sealed,
                    typeof(FreeInvokableBase));
                var mb = type.DefineMethod("DynamicInvoke",
                    System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Static,
                    returnType.MakeByRefType(), paramTypes);
                EmitBody(mb.GetILGenerator(), unmanaged, returnType, Ux, returnFlag, paramFlags);
                return CreateBuilderType(type).GetMethod("DynamicInvoke");
            }
        }

        static void EmitBody(System.Reflection.Emit.ILGenerator il, bool unmanaged, Type returnType, Type[] Ux, int returnFlag, uint paramFlags)
        {

            // Push params: ByRef → ldarg (pass the Ux& itself); ByValue → ldarg + ldobj Ux.
            // The callsite matches exactly what was pushed.
            var callSiteParams = new Type[Ux.Length];
            for (int i = 0; i < Ux.Length; i++)
            {
                il.Emit(OpCodes.Ldarg, 2 + i);
                if (FreeInvokableBase.GetRefParamFlag(paramFlags, i))
                {
                    callSiteParams[i] = Ux[i].MakeByRefType();
                }
                else
                {
                    il.Emit(OpCodes.Ldobj, Ux[i]);
                    callSiteParams[i] = Ux[i];
                }
            }

            // Push the function pointer, then calli.
            il.Emit(OpCodes.Ldarg, 0);

            // NOTE: Reflection.Emit cannot express CALLCONV_UNMANAGED (0x09) — the
            // (CallingConventions)9 trick gets silently masked down to Standard
            // (managed, 0x00) inside SignatureHelper. The Interop.CallingConvention
            // overload is the real unmanaged path: Cdecl emits 0x01 (unmanaged C),
            // which on Windows matches delegate* unmanaged default (x64: Microsoft
            // x64 ABI; x86: Cdecl) instruction-for-instruction.
            void EmitCalli(Type ret, Type[] ps)
            {
                if (unmanaged)
                    s_emitCalliUnmanaged.Invoke(il, new object[] { OpCodes.Calli,
                        System.Runtime.InteropServices.CallingConvention.Cdecl, ret, ps });
                else
                    il.EmitCalli(OpCodes.Calli, CallingConventions.Standard, ret, ps, null);
            }

            if (returnFlag == 0)
            {
                // void: calli void(...); r = default; return ref r;
                EmitCalli(typeof(void), callSiteParams);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Initobj, returnType);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ret);
            }
            else if (returnFlag == 1)
            {
                // by-value: calli R(...); r = calli_return; return ref r;
                EmitCalli(returnType, callSiteParams);
                var tmp = il.DeclareLocal(returnType);
                il.Emit(OpCodes.Stloc, tmp);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ldloc, tmp);
                il.Emit(OpCodes.Stobj, returnType);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ret);
            }
            else
            {
                // by-ref: calli ref R(...);
                //          r = ConvertParam<IntPtr, R>((IntPtr)(void*)calli_return);
                //          return ref calli_return;
                EmitCalli(returnType.MakeByRefType(), callSiteParams);
                var tmp = il.DeclareLocal(returnType);
                // Stack: [R&]. dup → [R&, R&]; conv.u → [R&, native int];
                // call ConvertParam<IntPtr, R>(ptr) → [R&, R].
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Conv_U);
                il.Emit(OpCodes.Call, s_convertParam1.MakeGenericMethod(typeof(IntPtr), returnType));
                il.Emit(OpCodes.Stloc, tmp);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ldloc, tmp);
                il.Emit(OpCodes.Stobj, returnType);
                // Stack: [R&] — return ref calli_return.
                il.Emit(OpCodes.Ret);
            }
        }

        // Emits DynamicPlainInvoke: R (IntPtr pfn, U1 u1, ..., Un un) — the runtime-Emit
        // twin of the woven InvokePlain (non-generic Invoke). Params enter BY VALUE
        // (the non-generic Invoke signature), so paramFlags is irrelevant here and the
        // callsite params are Ux as-is — mirroring the woven shape exactly:
        //   returnFlag == 0 → calli void(...); return default(R);
        //   otherwise      → calli R(...); return the result.
        // The return type is never a byref, so the DynamicMethod fast path works on
        // every runtime; the AssemblyBuilder fallback is only a safety net.
        public static System.Reflection.MethodInfo EmitDynamicPlainInvoker(bool unmanaged, Type returnType, Type[] Ux, int returnFlag, uint paramFlags)
        {
            // arg 0 = pfn, arg 1+i = ux (Ux by value).
            var paramTypes = new Type[1 + Ux.Length];
            paramTypes[0] = typeof(IntPtr);
            for (int i = 0; i < Ux.Length; i++)
                paramTypes[1 + i] = Ux[i];

            try
            {
                var dm = new DynamicMethod("DynamicPlainInvoke", returnType, paramTypes,
                    typeof(FreeInvokableBase), true);
                EmitPlainBody(dm.GetILGenerator(), unmanaged, returnType, Ux, returnFlag);
                return dm;
            }
            catch (Exception)
            {
                var ab = CreateCollectibleBuilder("PointerFuncEmit_" + Guid.NewGuid().ToString("N"));
                var mod = ab.DefineDynamicModule("PointerFuncEmitMod");
                var type = mod.DefineType("PointerFuncEmitType",
                    System.Reflection.TypeAttributes.Abstract | System.Reflection.TypeAttributes.Sealed,
                    typeof(FreeInvokableBase));
                var mb = type.DefineMethod("DynamicPlainInvoke",
                    System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Static,
                    returnType, paramTypes);
                EmitPlainBody(mb.GetILGenerator(), unmanaged, returnType, Ux, returnFlag);
                return CreateBuilderType(type).GetMethod("DynamicPlainInvoke");
            }
        }

        static void EmitPlainBody(System.Reflection.Emit.ILGenerator il, bool unmanaged, Type returnType, Type[] Ux, int returnFlag)
        {
            // Push params by value, then the function pointer — [U1..Un, pfn].
            for (int i = 0; i < Ux.Length; i++)
                il.Emit(OpCodes.Ldarg, 1 + i);
            il.Emit(OpCodes.Ldarg, 0);

            void EmitCalli(Type ret, Type[] ps)
            {
                if (unmanaged)
                    s_emitCalliUnmanaged.Invoke(il, new object[] { OpCodes.Calli,
                        System.Runtime.InteropServices.CallingConvention.Cdecl, ret, ps });
                else
                    il.EmitCalli(OpCodes.Calli, CallingConventions.Standard, ret, ps, null);
            }

            if (returnFlag == 0)
            {
                // void: calli void(...); return default(R);
                EmitCalli(typeof(void), Ux);
                var tmp = il.DeclareLocal(returnType);
                il.Emit(OpCodes.Ldloca, tmp);
                il.Emit(OpCodes.Initobj, returnType);
                il.Emit(OpCodes.Ldloc, tmp);
                il.Emit(OpCodes.Ret);
            }
            else
            {
                // by-value / by-ref: calli R(...) — same callsite the woven InvokePlain
                // uses; return the calli result directly.
                EmitCalli(returnType, Ux);
                il.Emit(OpCodes.Ret);
            }
        }
    }
    public interface IFunctionPointerUnmanagedIndicator
    {
        bool IsUnmanaged { get; set; }
    }
    public class PointerFunc<R> : FreeInvokable<R>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke()
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), Array.Empty<Type>(), _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain();
            }
            else
            {
                return del(_Pfn);
            }
        }
        private R InvokePlain()
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke(out R r)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), Array.Empty<Type>(), _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r);
            }
            else
            {
                return ref del(_Pfn, out r);
            }
        }
        protected ref R InvokeFallback(out R r)
        {
            var fallback = Invoke();
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R> Clone()
        {
            return MemberwiseClone() as PointerFunc<R>;
        }
    }
    public class PointerFunc<R, U1> : FreeInvokable<R, U1>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1);
            }
            else
            {
                return del(_Pfn, p1);
            }
        }
        private R InvokePlain(U1 p1)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1>(out R r, in P1 p1)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var fallback = Invoke(u1);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1>;
        }
    }
    public class PointerFunc<R, U1, U2> : FreeInvokable<R, U1, U2>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2);
            }
            else
            {
                return del(_Pfn, p1, p2);
            }
        }
        private R InvokePlain(U1 p1, U2 p2)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var fallback = Invoke(u1, u2);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2>;
        }
    }
    public class PointerFunc<R, U1, U2, U3> : FreeInvokable<R, U1, U2, U3>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3);
            }
            else
            {
                return del(_Pfn, p1, p2, p3);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var fallback = Invoke(u1, u2, u3);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4> : FreeInvokable<R, U1, U2, U3, U4>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var fallback = Invoke(u1, u2, u3, u4);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5> : FreeInvokable<R, U1, U2, U3, U4, U5>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var fallback = Invoke(u1, u2, u3, u4, u5);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6> : FreeInvokable<R, U1, U2, U3, U4, U5, U6>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var u14 = ConvertParam<U14, U14>(in p14, GetRefParamFlag(13));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var u14 = ConvertParam<U14, U14>(in p14, GetRefParamFlag(13));
            var u15 = ConvertParam<U15, U15>(in p15, GetRefParamFlag(14));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>, IFunctionPointerUnmanagedIndicator
    {
        protected IntPtr _Pfn;
        protected bool _IsUnmanaged;
        public bool IsUnmanaged { get => _IsUnmanaged; set => _IsUnmanaged = value; }
        public PointerFunc(IntPtr fn)
        {
            _Pfn = fn;
        }
        public PointerFunc(IntPtr fn, bool unmanaged)
        {
            _Pfn = fn;
            IsUnmanaged = unmanaged;
        }
        protected delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16);
        protected static ConcurrentDictionary<ulong, PlainInvoker> _DynamicPlainInvokerCache = new ConcurrentDictionary<ulong, PlainInvoker>();
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            PlainInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_DynamicPlainInvokerCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15), typeof(U16) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _DynamicPlainInvokerCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            throw new NotImplementedException();
        }
        protected delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15, in U16 u16);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_IsUnmanaged)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(_IsUnmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15), typeof(U16) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                    , in ConvertRef<P16, U16>(in p16)
                );
            }
            else
            {
                return ref del(_Pfn, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                    , in ConvertRef<P16, U16>(in p16)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15, in U16 p16)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var u14 = ConvertParam<U14, U14>(in p14, GetRefParamFlag(13));
            var u15 = ConvertParam<U15, U15>(in p15, GetRefParamFlag(14));
            var u16 = ConvertParam<U16, U16>(in p16, GetRefParamFlag(15));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15, u16);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>;
        }
    }

    internal static class FreeFuncEmit
    {
        // ConvertParam<P, U>(P p) — the 1-arg generic overload on FreeInvokableBase
        // (bit-pattern reinterpretation: returns U whose bits are p's pointer value).
        static readonly System.Reflection.MethodInfo s_convertParam1;
        // ConvertAddressToRef<T>(IntPtr address) — the IntPtr-arg generic overload on
        // FreeInvokableBase (returns ref T pointing at address).
        static readonly System.Reflection.MethodInfo s_convertAddressToRef;

        static FreeFuncEmit()
        {
            foreach (var m in typeof(FreeInvokableBase).GetMethods(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static))
            {
                if (!m.IsGenericMethodDefinition || m.GetParameters().Length != 1)
                    continue;
                if (m.Name == "ConvertParam")
                {
                    s_convertParam1 = m;
                }
                else if (m.Name == "ConvertAddressToRef" && m.GetParameters()[0].ParameterType == typeof(IntPtr))
                {
                    s_convertAddressToRef = m;
                }
            }
        }

        // Emits DynamicInvoke: ref R (Delegate del, out R r, in U1 u1, ..., in Un un).
        // Calls delType.Invoke via callvirt (delType = Func<U1..Un, R> or Action<U1..Un>).
        // All flag decisions are made HERE at emit time — the emitted IL is a single
        // straight-line callvirt plus return-value massaging, no branches.
        // Returns MethodInfo (DynamicMethod on CoreCLR; a collectible-AssemblyBuilder
        // static method on .NET Framework, where DynamicMethod rejects ByRef returns).
        public static System.Reflection.MethodInfo EmitDynamicInvoker(Type delType, Type returnType, Type[] Ux, int returnFlag, uint paramFlags)
        {
            // delType default: the Action<> family over Ux.
            if (delType == null)
                delType = Ux.Length == 0 ? typeof(Action)
                    : Type.GetType("System.Action`" + Ux.Length).MakeGenericType(Ux);
            var invoke = delType.GetMethod("Invoke");
            bool is_action = invoke.ReturnType == typeof(void);

            // arg 0 = del (Delegate), arg 1 = r (R&), arg 2+i = ux (Ux&).
            var paramTypes = new Type[2 + Ux.Length];
            paramTypes[0] = typeof(Delegate);
            paramTypes[1] = returnType.MakeByRefType();
            for (int i = 0; i < Ux.Length; i++)
                paramTypes[2 + i] = Ux[i].MakeByRefType();

            try
            {
                var dm = new DynamicMethod("DynamicInvoke", returnType.MakeByRefType(), paramTypes,
                    typeof(FreeInvokableBase), true);
                EmitBody(dm.GetILGenerator(), delType, returnType, Ux, returnFlag, paramFlags, invoke, is_action);
                return dm;
            }
            catch (Exception)
            {
                // .NET Framework throws NotSupportedException; Unity Mono throws
                // ArgumentException (C0d probe) — catch both (any failure) and fall
                // back to a collectible AssemblyBuilder method (MethodBuilder allows
                // ByRef returns). The emitted type derives from FreeInvokableBase so
                // the protected static conversion primitives stay callable.
                var ab = CreateCollectibleBuilder("FreeFuncEmit_" + Guid.NewGuid().ToString("N"));
                var mod = ab.DefineDynamicModule("FreeFuncEmitMod");
                var type = mod.DefineType("FreeFuncEmitType",
                    System.Reflection.TypeAttributes.Abstract | System.Reflection.TypeAttributes.Sealed,
                    typeof(FreeInvokableBase));
                var mb = type.DefineMethod("DynamicInvoke",
                    System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Static,
                    returnType.MakeByRefType(), paramTypes);
                EmitBody(mb.GetILGenerator(), delType, returnType, Ux, returnFlag, paramFlags, invoke, is_action);
                return CreateBuilderType(type).GetMethod("DynamicInvoke");
            }
        }

        // netstandard2.0's AppDomain ref lacks DefineDynamicAssembly; reflect it (present
        // on the .NET Framework / Mono runtimes, which are the only paths reaching this
        // fallback). Unity's Mono may not support RunAndCollect — degrade to Run.
        static System.Reflection.MethodInfo s_defineDynamicAssembly;
        static System.Reflection.Emit.AssemblyBuilder CreateCollectibleBuilder(string name)
        {
            if (s_defineDynamicAssembly == null)
            {
                s_defineDynamicAssembly = typeof(AppDomain).GetMethod("DefineDynamicAssembly",
                    new[] { typeof(AssemblyName), typeof(System.Reflection.Emit.AssemblyBuilderAccess) });
            }
            try
            {
                return (System.Reflection.Emit.AssemblyBuilder)s_defineDynamicAssembly.Invoke(
                    AppDomain.CurrentDomain,
                    new object[] { new AssemblyName(name), System.Reflection.Emit.AssemblyBuilderAccess.RunAndCollect });
            }
            catch (Exception)
            {
                return (System.Reflection.Emit.AssemblyBuilder)s_defineDynamicAssembly.Invoke(
                    AppDomain.CurrentDomain,
                    new object[] { new AssemblyName(name), System.Reflection.Emit.AssemblyBuilderAccess.Run });
            }
        }

        // netstandard2.0's TypeBuilder ref lacks CreateType — same reflective treatment.
        static System.Reflection.MethodInfo s_createType;
        static Type CreateBuilderType(System.Reflection.Emit.TypeBuilder type)
        {
            if (s_createType == null)
                s_createType = typeof(System.Reflection.Emit.TypeBuilder).GetMethod("CreateType");
            return (Type)s_createType.Invoke(type, null);
        }

        static void EmitBody(System.Reflection.Emit.ILGenerator il, Type delType, Type returnType, Type[] Ux,
            int returnFlag, uint paramFlags, System.Reflection.MethodInfo invoke, bool is_action)
        {
            // Receiver first: (delType)del.
            il.Emit(OpCodes.Ldarg, 0);
            il.Emit(OpCodes.Castclass, delType);

            // Push params — the callsite is delType.Invoke(U1, ..., Un), always by value:
            // ByRef → the delegate receives the ADDRESS of the slot
            //         (conv.u + ConvertParam<IntPtr, Ux> bit-reinterpretation);
            // ByValue → ldobj Ux (the slot's contents).
            for (int i = 0; i < Ux.Length; i++)
            {
                il.Emit(OpCodes.Ldarg, 2 + i);
                if (FreeInvokableBase.GetRefParamFlag(paramFlags, i))
                {
                    il.Emit(OpCodes.Conv_U);
                    il.Emit(OpCodes.Call, s_convertParam1.MakeGenericMethod(typeof(IntPtr), Ux[i]));
                }
                else
                {
                    il.Emit(OpCodes.Ldobj, Ux[i]);
                }
            }

            il.Emit(OpCodes.Callvirt, invoke);
            // Stack: [R] (Func) or [] (Action).

            if (is_action || returnFlag == 0)
            {
                // Nothing usable from the call: Action leaves nothing (returnFlag 1/2 →
                // fabricate a default R / ref R), Func + returnFlag 0 → discard the result.
                if (!is_action)
                    il.Emit(OpCodes.Pop);
                // r = default; return ref r;
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Initobj, returnType);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ret);
            }
            else if (returnFlag == 1)
            {
                // by-value: r = invoke_return; return ref r;
                var tmp = il.DeclareLocal(returnType);
                il.Emit(OpCodes.Stloc, tmp);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ldloc, tmp);
                il.Emit(OpCodes.Stobj, returnType);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ret);
            }
            else
            {
                // by-ref: the Func's R return value IS an address.
                // r = value; return ref ConvertAddressToRef<R>(ConvertParam<R, IntPtr>(value));
                var tmp = il.DeclareLocal(returnType);
                il.Emit(OpCodes.Stloc, tmp);
                il.Emit(OpCodes.Ldarg, 1);
                il.Emit(OpCodes.Ldloc, tmp);
                il.Emit(OpCodes.Stobj, returnType);
                // Stack: [] — rebuild the return: value → IntPtr bits → ref R at that address.
                il.Emit(OpCodes.Ldloc, tmp);
                il.Emit(OpCodes.Call, s_convertParam1.MakeGenericMethod(returnType, typeof(IntPtr)));
                il.Emit(OpCodes.Call, s_convertAddressToRef.MakeGenericMethod(returnType));
                il.Emit(OpCodes.Ret);
            }
        }
    }
    public class FreeFunc<R> : FreeInvokable<R>
    {
        protected Delegate _Del;
        public FreeFunc(Func<R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action del)
        {
            _Del = del;
        }
        public override R Invoke()
        {
            if (_Del is Func<R> func)
            {
                return func();
            }
            else if (_Del is Action act)
            {
                act();
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke(out R r)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action), typeof(R), Array.Empty<Type>(), _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r);
            }
            else
            {
                return ref del(_Del, out r);
            }
        }
        protected ref R InvokeFallback(out R r)
        {
            var fallback = Invoke();
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R> Clone()
        {
            return MemberwiseClone() as FreeFunc<R>;
        }
    }
    public class FreeFunc<R, U1> : FreeInvokable<R, U1>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1)
        {
            if (_Del is Func<U1, R> func)
            {
                return func(p1);
            }
            else if (_Del is Action<U1> act)
            {
                act(p1);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1>(out R r, in P1 p1)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1>), typeof(R), new[] { typeof(U1) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var fallback = Invoke(u1);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1>;
        }
    }
    public class FreeFunc<R, U1, U2> : FreeInvokable<R, U1, U2>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2)
        {
            if (_Del is Func<U1, U2, R> func)
            {
                return func(p1, p2);
            }
            else if (_Del is Action<U1, U2> act)
            {
                act(p1, p2);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2>), typeof(R), new[] { typeof(U1), typeof(U2) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var fallback = Invoke(u1, u2);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2>;
        }
    }
    public class FreeFunc<R, U1, U2, U3> : FreeInvokable<R, U1, U2, U3>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3)
        {
            if (_Del is Func<U1, U2, U3, R> func)
            {
                return func(p1, p2, p3);
            }
            else if (_Del is Action<U1, U2, U3> act)
            {
                act(p1, p2, p3);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var fallback = Invoke(u1, u2, u3);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4> : FreeInvokable<R, U1, U2, U3, U4>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            if (_Del is Func<U1, U2, U3, U4, R> func)
            {
                return func(p1, p2, p3, p4);
            }
            else if (_Del is Action<U1, U2, U3, U4> act)
            {
                act(p1, p2, p3, p4);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var fallback = Invoke(u1, u2, u3, u4);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5> : FreeInvokable<R, U1, U2, U3, U4, U5>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, R> func)
            {
                return func(p1, p2, p3, p4, p5);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5> act)
            {
                act(p1, p2, p3, p4, p5);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var fallback = Invoke(u1, u2, u3, u4, u5);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6> : FreeInvokable<R, U1, U2, U3, U4, U5, U6>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6> act)
            {
                act(p1, p2, p3, p4, p5, p6);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var u14 = ConvertParam<U14, U14>(in p14, GetRefParamFlag(13));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var u14 = ConvertParam<U14, U14>(in p14, GetRefParamFlag(13));
            var u15 = ConvertParam<U15, U15>(in p15, GetRefParamFlag(14));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>
    {
        protected Delegate _Del;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> del)
        {
            _Del = del;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> del)
        {
            _Del = del;
        }
        public override R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
            return default;
        }
        protected delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15, in U16 u16);
        protected static ConcurrentDictionary<ulong, DynamicInvoker> _EmitCache = new ConcurrentDictionary<ulong, DynamicInvoker>();
        public override ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            var emitkey = (ulong)_ReturnCategory;
            emitkey <<= 32;
            emitkey |= _RefParamFlags;
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R>)
                emitkey |= 1UL << 34;
            DynamicInvoker del = null;
            if (!_IsDynamicCodeDisabled && !_EmitCache.TryGetValue(emitkey, out del))
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>), typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15), typeof(U16) }, _ReturnCategory, _RefParamFlags);
                    del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    del = _EmitCache.GetOrAdd(emitkey, del);
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            if (del == null)
            {
                return ref InvokeFallback(out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                    , in ConvertRef<P16, U16>(in p16)
                );
            }
            else
            {
                return ref del(_Del, out r
                    , in ConvertRef<P1, U1>(in p1)
                    , in ConvertRef<P2, U2>(in p2)
                    , in ConvertRef<P3, U3>(in p3)
                    , in ConvertRef<P4, U4>(in p4)
                    , in ConvertRef<P5, U5>(in p5)
                    , in ConvertRef<P6, U6>(in p6)
                    , in ConvertRef<P7, U7>(in p7)
                    , in ConvertRef<P8, U8>(in p8)
                    , in ConvertRef<P9, U9>(in p9)
                    , in ConvertRef<P10, U10>(in p10)
                    , in ConvertRef<P11, U11>(in p11)
                    , in ConvertRef<P12, U12>(in p12)
                    , in ConvertRef<P13, U13>(in p13)
                    , in ConvertRef<P14, U14>(in p14)
                    , in ConvertRef<P15, U15>(in p15)
                    , in ConvertRef<P16, U16>(in p16)
                    );
            }
        }
        protected ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15, in U16 p16)
        {
            var u1 = ConvertParam<U1, U1>(in p1, GetRefParamFlag(0));
            var u2 = ConvertParam<U2, U2>(in p2, GetRefParamFlag(1));
            var u3 = ConvertParam<U3, U3>(in p3, GetRefParamFlag(2));
            var u4 = ConvertParam<U4, U4>(in p4, GetRefParamFlag(3));
            var u5 = ConvertParam<U5, U5>(in p5, GetRefParamFlag(4));
            var u6 = ConvertParam<U6, U6>(in p6, GetRefParamFlag(5));
            var u7 = ConvertParam<U7, U7>(in p7, GetRefParamFlag(6));
            var u8 = ConvertParam<U8, U8>(in p8, GetRefParamFlag(7));
            var u9 = ConvertParam<U9, U9>(in p9, GetRefParamFlag(8));
            var u10 = ConvertParam<U10, U10>(in p10, GetRefParamFlag(9));
            var u11 = ConvertParam<U11, U11>(in p11, GetRefParamFlag(10));
            var u12 = ConvertParam<U12, U12>(in p12, GetRefParamFlag(11));
            var u13 = ConvertParam<U13, U13>(in p13, GetRefParamFlag(12));
            var u14 = ConvertParam<U14, U14>(in p14, GetRefParamFlag(13));
            var u15 = ConvertParam<U15, U15>(in p15, GetRefParamFlag(14));
            var u16 = ConvertParam<U16, U16>(in p16, GetRefParamFlag(15));
            var fallback = Invoke(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15, u16);
            r = fallback;
            if (_ReturnCategory == 2)
            {
                return ref ConvertAddressToRef(fallback);
            }
            else
            {
                return ref r;
            }
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>;
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
