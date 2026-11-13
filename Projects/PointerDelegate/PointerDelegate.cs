using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

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
        /// <summary>
        /// The raw address carried in this ByRefParam. Only meaningful as ByRefLocalSlot's
        /// Ref2Ref slot (where it holds the address of a byref slot); plain ByRefParam
        /// markers used as Ux parameter types never read or write it. The setter is
        /// internal: writing the slot address is reserved to the library (ByRefLocalSlot
        /// construction and the InvokeFallback fixup) — external code builds slots
        /// via new ByRefLocalSlot(IntPtr).
        /// </summary>
        public IntPtr Address
        {
            get { return _Address; }
            internal set { _Address = value; }
        }
    }
    /// <summary>
    /// An indirect ref over a BYREF SLOT on the caller's evaluation stack: Ref2Ref.Address
    /// holds the slot's address, and reading Address dereferences the slot, yielding the
    /// CURRENT (GC-updated) address of the referenced value — a ByRefLocalSlot handed to a callee
    /// keeps tracking heap compaction without pinning, as long as the wrapping frame lives.
    /// Use ByRefLocalSlot as a Ux parameter type of PointerFunc/FreeFunc and the invoker wraps
    /// the argument slot automatically: the hot-emit path builds slot with
    /// slot.Ref2Ref.Address = ldarga.x (x = that Ux argument's slot), and the Generator
    /// injects the equivalent FixupByRefLocalSlot call into InvokeFallback. The callee
    /// receives the ByRefLocalSlot BY VALUE and reads the current address through
    /// *(IntPtr*)slot.Ref2Ref.Address (or the Address property).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ByRefLocalSlot
    {
        public ByRefParam Ref2Ref;
        /// <summary>
        /// The CURRENT address of the referenced value — dereferences the byref slot,
        /// which the GC keeps updated across compacting collections. The setter
        /// (re-pointing the slot) is internal, reserved to the library.
        /// </summary>
        public IntPtr Address
        {
            get { unsafe { return *(IntPtr*)Ref2Ref.Address; } }
            internal set { unsafe { *(IntPtr*)Ref2Ref.Address = value; } }
        }
        /// <summary>Wraps a byref-slot address (e.g. the ldarga of an `in` parameter slot).</summary>
        public ByRefLocalSlot(IntPtr slotAddress)
        {
            Ref2Ref = default(ByRefParam);
            Ref2Ref.Address = slotAddress;
        }
    }
    public interface IFunctionPointerUnmanagedIndicator
    {
        bool IsUnmanaged { get; set; }
    }
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
            _IsUnmanaged = unmanaged;
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
    #region Creators
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
        public static FreeFunc<R> CreateFreeInvokable<R>(this RefFunc<R> func) { return new FreeFunc<R>(func); }
        public static FreeFunc<R, U1> CreateFreeInvokable<R, U1>(this RefFunc<U1, R> func) { return new FreeFunc<R, U1>(func); }
        public static FreeFunc<R, U1, U2> CreateFreeInvokable<R, U1, U2>(this RefFunc<U1, U2, R> func) { return new FreeFunc<R, U1, U2>(func); }
        public static FreeFunc<R, U1, U2, U3> CreateFreeInvokable<R, U1, U2, U3>(this RefFunc<U1, U2, U3, R> func) { return new FreeFunc<R, U1, U2, U3>(func); }
        public static FreeFunc<R, U1, U2, U3, U4> CreateFreeInvokable<R, U1, U2, U3, U4>(this RefFunc<U1, U2, U3, U4, R> func) { return new FreeFunc<R, U1, U2, U3, U4>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5> CreateFreeInvokable<R, U1, U2, U3, U4, U5>(this RefFunc<U1, U2, U3, U4, U5, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6>(this RefFunc<U1, U2, U3, U4, U5, U6, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>(func); }
        public static FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> CreateFreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(this RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> func) { return new FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>(func); }
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

        protected internal static ParamFlag GetRefParamFlag(ulong flags, int paramIndex)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            int shift = paramIndex << 1;
            return (ParamFlag)((flags & (3uL << shift)) >> shift);
        }
        protected internal static void SetRefParamFlag(ref ulong flags, int paramIndex, ParamFlag flag)
        {
            //if (paramIndices >= 16 || paramIndices < 0) throw new ArgumentOutOfRangeException(nameof(paramIndices), $"{nameof(paramIndices)} must be [0, 15]");
            int shift = paramIndex << 1;
            flags = (flags & ~(3uL << shift)) | (((ulong)flag) << shift);
        }
        protected internal static void SetRefParamFlag(ref ulong flags, int paramIndex, Type ut)
        {
            SetRefParamFlag(ref flags, paramIndex, JudgeParamRefFlag(ut));
        }
        protected internal static ParamFlag JudgeParamRefFlag(Type ut)
        {
            if (ut == typeof(ByRefParam))
            {
                return ParamFlag.ByRef;
            }
            else if (ut == typeof(ByRefLocalSlot))
            {
                return ParamFlag.Ref2Ref;
            }
            else
            {
                return ParamFlag.ByValue;
            }
            // Param should have no void type.
        }
        protected internal static ParamFlag JudgeReturnCategory(Type ut)
        {
            if (ut == typeof(VoidReturn))
            {
                return ParamFlag.Void;
            }
            else if (ut == typeof(ByRefParam))
            {
                return ParamFlag.ByRef;
            }
            else
            {
                return ParamFlag.ByValue;
            }
            // return should have no local slot type.
        }
        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }

        #region DynamicGenerator
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected static ref T ConvertAddressToRef<T>(IntPtr address)
        {
            unsafe
            {
#pragma warning disable CS8500
                return ref *(T*)address;
#pragma warning restore CS8500
            }
        }
        protected static IntPtr ConvertRefToAddress<T>(in T r)
        {
            unsafe
            {
#pragma warning disable CS8500
                fixed (T* pr = &r)
                {
                    return (IntPtr)(void*)pr;
                }
#pragma warning restore CS8500
            }
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
            throw new NotImplementedException();
        }
        protected static bool _IsDynamicCodeDisabled = false;
        protected internal static T CreateDelegate<T>(MethodInfo dm) where T : Delegate
        {
            return (T)(dm is System.Reflection.Emit.DynamicMethod dynDm
                ? dynDm.CreateDelegate(typeof(T))
                : Delegate.CreateDelegate(typeof(T), dm));
        }
        #endregion
    }
    public abstract class FreeInvokable<R> : FreeInvokableBase, IPlainInvokableFunc<R>, IFreeInvokableFunc<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
        }
        R IPlainInvokableFunc<R>.Invoke()
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc<R>.Invoke(out R r)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1> : FreeInvokableBase, IPlainInvokableFunc<R, U1>, IFreeInvokableFunc1<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
        }
        R IPlainInvokableFunc<R, U1>.Invoke(U1 u1)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc1<R>.Invoke<P1>(out R r, in P1 p1)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2>, IFreeInvokableFunc2<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
        }
        R IPlainInvokableFunc<R, U1, U2>.Invoke(U1 u1, U2 u2)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc2<R>.Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3>, IFreeInvokableFunc3<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
        }
        R IPlainInvokableFunc<R, U1, U2, U3>.Invoke(U1 u1, U2 u2, U3 u3)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc3<R>.Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4>, IFreeInvokableFunc4<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc4<R>.Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5>, IFreeInvokableFunc5<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc5<R>.Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6>, IFreeInvokableFunc6<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc6<R>.Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7>, IFreeInvokableFunc7<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc7<R>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>, IFreeInvokableFunc8<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc8<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>, IFreeInvokableFunc9<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc9<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>, IFreeInvokableFunc10<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc10<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>, IFreeInvokableFunc11<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
            SetRefParamFlag(ref _ParamFlags, 10, typeof(U11));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc11<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>, IFreeInvokableFunc12<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
            SetRefParamFlag(ref _ParamFlags, 10, typeof(U11));
            SetRefParamFlag(ref _ParamFlags, 11, typeof(U12));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc12<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>, IFreeInvokableFunc13<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
            SetRefParamFlag(ref _ParamFlags, 10, typeof(U11));
            SetRefParamFlag(ref _ParamFlags, 11, typeof(U12));
            SetRefParamFlag(ref _ParamFlags, 12, typeof(U13));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc13<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>, IFreeInvokableFunc14<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
            SetRefParamFlag(ref _ParamFlags, 10, typeof(U11));
            SetRefParamFlag(ref _ParamFlags, 11, typeof(U12));
            SetRefParamFlag(ref _ParamFlags, 12, typeof(U13));
            SetRefParamFlag(ref _ParamFlags, 13, typeof(U14));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc14<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>, IFreeInvokableFunc15<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
            SetRefParamFlag(ref _ParamFlags, 10, typeof(U11));
            SetRefParamFlag(ref _ParamFlags, 11, typeof(U12));
            SetRefParamFlag(ref _ParamFlags, 12, typeof(U13));
            SetRefParamFlag(ref _ParamFlags, 13, typeof(U14));
            SetRefParamFlag(ref _ParamFlags, 14, typeof(U15));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc15<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokableBase, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>, IFreeInvokableFunc16<R>
    {
        protected internal static ParamFlag _ReturnCategory;
        protected internal static ulong _ParamFlags;
        public static ParamFlag GetReturnCategory()
        {
            return _ReturnCategory;
        }
        public static ParamFlag GetParamFlag(int paramIndex)
        {
            return GetRefParamFlag(_ParamFlags, paramIndex);
        }
        protected FreeInvokable()
        {
            _ReturnCategory = JudgeReturnCategory(typeof(R));
            SetRefParamFlag(ref _ParamFlags, 0, typeof(U1));
            SetRefParamFlag(ref _ParamFlags, 1, typeof(U2));
            SetRefParamFlag(ref _ParamFlags, 2, typeof(U3));
            SetRefParamFlag(ref _ParamFlags, 3, typeof(U4));
            SetRefParamFlag(ref _ParamFlags, 4, typeof(U5));
            SetRefParamFlag(ref _ParamFlags, 5, typeof(U6));
            SetRefParamFlag(ref _ParamFlags, 6, typeof(U7));
            SetRefParamFlag(ref _ParamFlags, 7, typeof(U8));
            SetRefParamFlag(ref _ParamFlags, 8, typeof(U9));
            SetRefParamFlag(ref _ParamFlags, 9, typeof(U10));
            SetRefParamFlag(ref _ParamFlags, 10, typeof(U11));
            SetRefParamFlag(ref _ParamFlags, 11, typeof(U12));
            SetRefParamFlag(ref _ParamFlags, 12, typeof(U13));
            SetRefParamFlag(ref _ParamFlags, 13, typeof(U14));
            SetRefParamFlag(ref _ParamFlags, 14, typeof(U15));
            SetRefParamFlag(ref _ParamFlags, 15, typeof(U16));
        }
        R IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>.Invoke(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15, U16 u16)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc16<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
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
        // ByRefLocalSlot::.ctor(IntPtr slotAddress) — wraps a byref-slot address (ByRefLocalSlot params).
        static readonly System.Reflection.ConstructorInfo s_byRefLocalSlotCtor =
            typeof(ByRefLocalSlot).GetConstructor(new[] { typeof(IntPtr) });

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
        public static System.Reflection.MethodInfo EmitDynamicInvoker(bool unmanaged, Type returnType, Type[] Ux, int returnFlag, ulong paramFlags)
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

        static void EmitBody(System.Reflection.Emit.ILGenerator il, bool unmanaged, Type returnType, Type[] Ux, int returnFlag, ulong paramFlags)
        {

            // Push params: ByRef → ldarg (pass the Ux& itself); ByValue → ldarg + ldobj Ux.
            // The callsite matches exactly what was pushed.
            // ByRefLocalSlot params (checked FIRST, flag-independent): create a fresh ByRefLocalSlot
            // wrapping THIS method's `in Ux` argument slot — slot.Ref2Ref.Address =
            // ldarga(2+i) — and pass slot BY VALUE. The slot is a GC-tracked byref the
            // collector keeps updated (ldarga makes it address-taken → reported for the
            // whole method), so the callee reading *(IntPtr*)slot.Ref2Ref.Address always
            // sees the current address of the caller's argument, across compacting GCs.
            var callSiteParams = new Type[Ux.Length];
            var pinned = new List<System.Reflection.Emit.LocalBuilder>();
            for (int i = 0; i < Ux.Length; i++)
            {
                var pflag = FreeInvokableBase.GetRefParamFlag(paramFlags, i);
                if (pflag == ParamFlag.Ref2Ref)
                {
                    // newobj leaves the 8-byte struct directly on the eval stack as the
                    // calli argument — no stloc/ldloc round-trip (and no boxing: valuetype
                    // newobj is a stack temp, not a box).
                    il.Emit(OpCodes.Ldarga, 2 + i);
                    il.Emit(OpCodes.Conv_U);
                    il.Emit(OpCodes.Newobj, s_byRefLocalSlotCtor);
                    callSiteParams[i] = typeof(ByRefLocalSlot);
                }
                else if (pflag == ParamFlag.ByRef)
                {
                    il.Emit(OpCodes.Ldarg, 2 + i);
                    callSiteParams[i] = Ux[i].MakeByRefType();
                }
                else
                {
                    il.Emit(OpCodes.Ldarg, 2 + i);
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
        public static System.Reflection.MethodInfo EmitDynamicPlainInvoker(bool unmanaged, Type returnType, Type[] Ux, int returnFlag, ulong paramFlags)
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
    public sealed class PointerFunc<R> : FreeInvokable<R>, IPlainInvokableFunc<R>, IFreeInvokableFunc<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn);
        private static PlainInvoker _PlainInvokerManaged;
        private static PlainInvoker _PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref _PlainInvokerManaged;
            if (unmanaged)
                invoker = ref _PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), Array.Empty<Type>(), (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke()
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(GetReturnCategory());
            }
            else
            {
                return del(_Pfn);
            }
        }
        private R InvokePlain(ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn()
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r);
        private static DynamicInvoker _DynamicInvokerManaged;
        private static DynamicInvoker _DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref _DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), Array.Empty<Type>(), (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke(out R r)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
            if (del == null)
            {
                return ref InvokeFallback(out r);
            }
            else
            {
                return ref del(_Pfn, out r);
            }
        }
        private ref R InvokeFallback(out R r)
        {
            if (_ReturnCategory == ParamFlag.ByRef)
            {
                ref R result = ref InvokeRefReturn();
                r = default;
                ConvertRef<R, IntPtr>(in r) = ConvertRefToAddress(in result);
                return ref result;
            }
            else
            {
                r = InvokePlain(GetReturnCategory());
                return ref r;
            }
        }
        public PointerFunc<R> Clone()
        {
            return MemberwiseClone() as PointerFunc<R>;
        }
    }
    public sealed class PointerFunc<R, U1> : FreeInvokable<R, U1>, IPlainInvokableFunc<R, U1>, IFreeInvokableFunc1<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1);
            }
        }
        private R InvokePlain(U1 p1, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1>(out R r, in P1 p1)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1)
        {
            r = default;
            return ref r;
        }

        public R Invoke<P1>(in P1 p1)
        {
            return Invoke(out var r, in p1);
        }

        public PointerFunc<R, U1> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1>;
        }
    }
    public sealed class PointerFunc<R, U1, U2> : FreeInvokable<R, U1, U2>, IPlainInvokableFunc<R, U1, U2>, IFreeInvokableFunc2<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            return Invoke(out var r, in p1, in p2);
        }

        public PointerFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3> : FreeInvokable<R, U1, U2, U3>, IPlainInvokableFunc<R, U1, U2, U3>, IFreeInvokableFunc3<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            return Invoke(out var r, in p1, in p2, in p3);
        }

        public PointerFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4> : FreeInvokable<R, U1, U2, U3, U4>, IPlainInvokableFunc<R, U1, U2, U3, U4>, IFreeInvokableFunc4<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4);
        }

        public PointerFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5> : FreeInvokable<R, U1, U2, U3, U4, U5>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5>, IFreeInvokableFunc5<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6> : FreeInvokable<R, U1, U2, U3, U4, U5, U6>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6>, IFreeInvokableFunc6<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7>, IFreeInvokableFunc7<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>, IFreeInvokableFunc8<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>, IFreeInvokableFunc9<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>, IFreeInvokableFunc10<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>, IFreeInvokableFunc11<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>, IFreeInvokableFunc12<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>, IFreeInvokableFunc13<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>, IFreeInvokableFunc14<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>, IFreeInvokableFunc15<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }

        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public sealed class PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>, IFreeInvokableFunc16<R>, IFunctionPointerUnmanagedIndicator
    {
        private IntPtr _Pfn;
        private bool _IsUnmanaged;
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
        private delegate R PlainInvoker(IntPtr pfn, U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16);
        private static PlainInvoker PlainInvokerManaged;
        private static PlainInvoker PlainInvokerUnmanaged;
        private static PlainInvoker GetOrCreatePlainInvoker(bool unmanaged)
        {
            ref PlainInvoker invoker = ref PlainInvokerManaged;
            if (unmanaged)
                invoker = ref PlainInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicPlainInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15), typeof(U16) }, (int)_ReturnCategory, _ParamFlags);
                    PlainInvoker del = CreateDelegate<PlainInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            PlainInvoker del = GetOrCreatePlainInvoker(_IsUnmanaged);
            if (del == null)
            {
                return InvokePlain(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, GetReturnCategory());
            }
            else
            {
                return del(_Pfn, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
        }
        private R InvokePlain(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16, ParamFlag retc)
        {
            throw new NotImplementedException();
        }
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15, U16 u16)
        {
            throw new NotImplementedException();
        }
        private delegate ref R DynamicInvoker(IntPtr pfn, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15, in U16 u16);
        private static DynamicInvoker DynamicInvokerManaged;
        private static DynamicInvoker DynamicInvokerUnmanaged;
        private static DynamicInvoker GetOrCreateDynamicInvoker(bool unmanaged)
        {
            ref DynamicInvoker invoker = ref DynamicInvokerManaged;
            if (unmanaged)
                invoker = ref DynamicInvokerUnmanaged;
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = PointerFuncEmit.EmitDynamicInvoker(unmanaged, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15), typeof(U16) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_IsUnmanaged);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15, in U16 p16)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
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
        // ByRefLocalSlot::.ctor(IntPtr slotAddress) — wraps a byref-slot address (ByRefLocalSlot params).
        static readonly System.Reflection.ConstructorInfo s_byRefLocalSlotCtor =
            typeof(ByRefLocalSlot).GetConstructor(new[] { typeof(IntPtr) });
        // IntPtr::op_Explicit(void*) — bridges the conv.u native int into the IntPtr
        // struct ConvertParam<IntPtr, Ux> expects (raw native int → struct param is a
        // type break the verifier rejects).
        static readonly System.Reflection.MethodInfo s_intPtrOpExplicit =
            typeof(IntPtr).GetMethod("op_Explicit",
                new[] { typeof(void*) });

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
        public static System.Reflection.MethodInfo EmitDynamicInvoker(Type delType, Type returnType, Type[] Ux, int returnFlag, ulong paramFlags)
        {
            // delType default: the Action<> family over Ux.
            if (delType == null)
                delType = Ux.Length == 0 ? typeof(Action)
                    : Type.GetType("System.Action`" + Ux.Length).MakeGenericType(Ux);
            var invoke = delType.GetMethod("Invoke");
            bool is_action = invoke.ReturnType == typeof(void);
            bool is_reffunc = invoke.ReturnType.Name.Contains("&");

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
                EmitBody(dm.GetILGenerator(), delType, returnType, Ux, returnFlag, paramFlags, invoke, is_action, is_reffunc);
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
                EmitBody(mb.GetILGenerator(), delType, returnType, Ux, returnFlag, paramFlags, invoke, is_action, is_reffunc);
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
            int returnFlag, ulong paramFlags, System.Reflection.MethodInfo invoke, bool is_action, bool is_reffunc)
        {
            // Receiver first: (delType)del.
            il.Emit(OpCodes.Ldarg, 0);
            il.Emit(OpCodes.Castclass, delType);

            // Push params — the callsite is delType.Invoke(U1, ..., Un), always by value:
            // ByRefLocalSlot (checked FIRST, flag-independent) → a fresh ByRefLocalSlot wrapping this
            //   method's `in Ux` argument slot (slot.Ref2Ref.Address = ldarga(2+i)), passed
            //   by value — the delegate/callee reads the GC-updated current address
            //   through it: *(IntPtr*)slot.Ref2Ref.Address;
            // ByRef → the delegate receives the ADDRESS of the slot
            //         (conv.u + ConvertParam<IntPtr, Ux> bit-reinterpretation);
            // ByValue → ldobj Ux (the slot's contents).
            var pinned = new List<System.Reflection.Emit.LocalBuilder>();
            for (int i = 0; i < Ux.Length; i++)
            {
                var pflag = FreeInvokableBase.GetRefParamFlag(paramFlags, i);
                if (pflag == ParamFlag.Ref2Ref)
                {
                    // newobj leaves the 8-byte struct directly on the eval stack as the
                    // delegate-Invoke argument — no stloc/ldloc round-trip (and no boxing).
                    il.Emit(OpCodes.Ldarga, 2 + i);
                    il.Emit(OpCodes.Conv_U);
                    il.Emit(OpCodes.Newobj, s_byRefLocalSlotCtor);
                }
                else if (pflag == ParamFlag.ByRef)
                {
                    // fixed Ux* pux = &px - the pinned local pins the byref target for the
                    // whole delegate-call window (same fix as the woven InvokeFallback).
                    var pux = il.DeclareLocal(Ux[i].MakeByRefType(), true);
                    il.Emit(OpCodes.Ldarg, 2 + i);
                    il.Emit(OpCodes.Stloc, pux);
                    il.Emit(OpCodes.Ldloc, pux);
                    il.Emit(OpCodes.Conv_U);
                    il.Emit(OpCodes.Call, s_intPtrOpExplicit);
                    il.Emit(OpCodes.Call, s_convertParam1.MakeGenericMethod(typeof(IntPtr), Ux[i]));
                    pinned.Add(pux);
                }
                else
                {
                    il.Emit(OpCodes.Ldarg, 2 + i);
                    il.Emit(OpCodes.Ldobj, Ux[i]);
                }
            }

            il.Emit(OpCodes.Callvirt, invoke);
            // Stack: [R] (Func) or [R&] (RefFunc) or [] (Action).
            // Release the pins: the wrapper has returned, so every ux._Address snapshot
            // has been consumed. The stloc also keeps each pinned local's liveness
            // covering the callvirt safepoint above.
            for (int i = 0; i < pinned.Count; i++)
            {
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Conv_U);
                il.Emit(OpCodes.Stloc, pinned[i]);
            }

            if (is_reffunc)
            {
                // RefFunc branch: callvirt returned a BYREF (R&) — it stays on the eval
                // stack, GC-tracked. Store it in an R& local (tracked) and massage per flag.
                var tmpR = il.DeclareLocal(returnType.MakeByRefType());
                il.Emit(OpCodes.Stloc, tmpR);
                if (returnFlag == 0)
                {
                    // VoidReturn: discard the byref (stloc already consumed it).
                    // r = default; return ref r;
                    il.Emit(OpCodes.Ldarg, 1);
                    il.Emit(OpCodes.Initobj, returnType);
                    il.Emit(OpCodes.Ldarg, 1);
                    il.Emit(OpCodes.Ret);
                }
                else if (returnFlag == 2)
                {
                    // ByRefParam: out r gets the snapshot copy of the byref's target
                    // address (r = ConvertRef<R, IntPtr>(r) = ConvertRefToAddress(in callresult));
                    // the ORIGINAL stack byref is returned directly — no IntPtr transit.
                    il.Emit(OpCodes.Ldarg, 1);
                    il.Emit(OpCodes.Initobj, returnType);        // r = default
                    il.Emit(OpCodes.Ldarg, 1);
                    il.Emit(OpCodes.Ldloc, tmpR);
                    il.Emit(OpCodes.Conv_U);                     // target address (native int)
                    il.Emit(OpCodes.Call, s_convertParam1.MakeGenericMethod(typeof(IntPtr), returnType));
                    il.Emit(OpCodes.Stobj, returnType);           // r = ByRefParam{_Address = target}
                    il.Emit(OpCodes.Ldloc, tmpR);                // the ORIGINAL byref (tracked)
                    il.Emit(OpCodes.Ret);
                }
                else
                {
                    // Other: deref the byref into out r; return ref r.
                    il.Emit(OpCodes.Ldarg, 1);
                    il.Emit(OpCodes.Ldloc, tmpR);
                    il.Emit(OpCodes.Ldobj, returnType);          // r = *callresult
                    il.Emit(OpCodes.Stobj, returnType);
                    il.Emit(OpCodes.Ldarg, 1);
                    il.Emit(OpCodes.Ret);
                }
            }
            else if (is_action || returnFlag == 0)
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
    public sealed class FreeFunc<R> : FreeInvokable<R>, IPlainInvokableFunc<R>, IFreeInvokableFunc<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag; 
        public FreeFunc(Func<R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByRef;
        }
        public R Invoke()
        {
            if (_Del is Func<R> func)
            {
                return func();
            }
            else if (_Del is Action act)
            {
                act();
            }
            else if (_Del is RefFunc<R> rfunc)
            {
                return rfunc();
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action → null ref; Func → callresult bits as an address;
        // RefFunc → the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn()
        {
            if (_Del is RefFunc<R> rfunc)
            {
                return ref rfunc();
            }
            else if (_Del is Func<R> func)
            {
                var callresult = func();
                return ref ConvertAddressToRef(callresult);
            }
            else // Action
            {
                ((Action)_Del)();
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), Array.Empty<Type>(), (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke(out R r)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action), _DelReturnFlag);
            if (del == null)
            {
                return ref InvokeFallback(out r);
            }
            else
            {
                return ref del(_Del, out r);
            }
        }
        private ref R InvokeFallback(out R r)
        {
            if (_ReturnCategory == ParamFlag.ByRef)
            {
                ref R result = ref InvokeRefReturn();
                r = default;
                ConvertRef<R, IntPtr>(in r) = ConvertRefToAddress(in result);
                return ref result;
            }
            else
            {
                r = Invoke();
                return ref r;
            }
        }
        public FreeFunc<R> Clone()
        {
            return MemberwiseClone() as FreeFunc<R>;
        }
    }
    public sealed class FreeFunc<R, U1> : FreeInvokable<R, U1>, IPlainInvokableFunc<R, U1>, IFreeInvokableFunc1<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByRef;
        }
        public R Invoke(U1 p1)
        {
            if (_Del is Func<U1, R> func)
            {
                return func(p1);
            }
            else if (_Del is Action<U1> act)
            {
                act(p1);
            }
            else if (_Del is RefFunc<U1, R> rfunc)
            {
                return rfunc(p1);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action → null ref; Func → callresult bits as an address;
        // RefFunc → the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1)
        {
            if (_Del is RefFunc<U1, R> rfunc)
            {
                return ref rfunc(u1);
            }
            else if (_Del is Func<U1, R> func)
            {
                var callresult = func(u1);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1>
            {
                ((Action<U1>)_Del)(u1);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1>(out R r, in P1 p1)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1>(in P1 p1)
        {
            return Invoke(out var r, in p1);
        }

        public FreeFunc<R, U1> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1>;
        }
    }
    public sealed class FreeFunc<R, U1, U2> : FreeInvokable<R, U1, U2>, IPlainInvokableFunc<R, U1, U2>, IFreeInvokableFunc2<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2)
        {
            if (_Del is Func<U1, U2, R> func)
            {
                return func(p1, p2);
            }
            else if (_Del is Action<U1, U2> act)
            {
                act(p1, p2);
            }
            else if (_Del is RefFunc<U1, U2, R> rfunc)
            {
                return rfunc(p1, p2);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2)
        {
            if (_Del is RefFunc<U1, U2, R> rfunc)
            {
                return ref rfunc(u1, u2);
            }
            else if (_Del is Func<U1, U2, R> func)
            {
                var callresult = func(u1, u2);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2>
            {
                ((Action<U1, U2>)_Del)(u1, u2);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            return Invoke(out var r, in p1, in p2);
        }
        public FreeFunc<R, U1, U2> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3> : FreeInvokable<R, U1, U2, U3>, IPlainInvokableFunc<R, U1, U2, U3>, IFreeInvokableFunc3<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3)
        {
            if (_Del is Func<U1, U2, U3, R> func)
            {
                return func(p1, p2, p3);
            }
            else if (_Del is Action<U1, U2, U3> act)
            {
                act(p1, p2, p3);
            }
            else if (_Del is RefFunc<U1, U2, U3, R> rfunc)
            {
                return rfunc(p1, p2, p3);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3)
        {
            if (_Del is RefFunc<U1, U2, U3, R> rfunc)
            {
                return ref rfunc(u1, u2, u3);
            }
            else if (_Del is Func<U1, U2, U3, R> func)
            {
                var callresult = func(u1, u2, u3);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3>
            {
                ((Action<U1, U2, U3>)_Del)(u1, u2, u3);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            return Invoke(out var r, in p1, in p2, in p3);
        }
        public FreeFunc<R, U1, U2, U3> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4> : FreeInvokable<R, U1, U2, U3, U4>, IPlainInvokableFunc<R, U1, U2, U3, U4>, IFreeInvokableFunc4<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4)
        {
            if (_Del is Func<U1, U2, U3, U4, R> func)
            {
                return func(p1, p2, p3, p4);
            }
            else if (_Del is Action<U1, U2, U3, U4> act)
            {
                act(p1, p2, p3, p4);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4);
            }
            else if (_Del is Func<U1, U2, U3, U4, R> func)
            {
                var callresult = func(u1, u2, u3, u4);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4>
            {
                ((Action<U1, U2, U3, U4>)_Del)(u1, u2, u3, u4);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4);
        }
        public FreeFunc<R, U1, U2, U3, U4> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5> : FreeInvokable<R, U1, U2, U3, U4, U5>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5>, IFreeInvokableFunc5<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, R> func)
            {
                return func(p1, p2, p3, p4, p5);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5> act)
            {
                act(p1, p2, p3, p4, p5);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5>
            {
                ((Action<U1, U2, U3, U4, U5>)_Del)(u1, u2, u3, u4, u5);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6> : FreeInvokable<R, U1, U2, U3, U4, U5, U6>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6>, IFreeInvokableFunc6<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6> act)
            {
                act(p1, p2, p3, p4, p5, p6);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6>
            {
                ((Action<U1, U2, U3, U4, U5, U6>)_Del)(u1, u2, u3, u4, u5, u6);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7>, IFreeInvokableFunc7<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7>)_Del)(u1, u2, u3, u4, u5, u6, u7);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>, IFreeInvokableFunc8<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>, IFreeInvokableFunc9<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>, IFreeInvokableFunc10<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>, IFreeInvokableFunc11<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>, IFreeInvokableFunc12<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>, IFreeInvokableFunc13<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>, IFreeInvokableFunc14<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>, IFreeInvokableFunc15<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Clone()
        {
            return MemberwiseClone() as FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>;
        }
    }
    public sealed class FreeFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> : FreeInvokable<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>, IPlainInvokableFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>, IFreeInvokableFunc16<R>
    {
        private Delegate _Del;
        private ParamFlag _DelReturnFlag;
        public FreeFunc(Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public FreeFunc(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.Void;
        }
        public FreeFunc(RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> del)
        {
            _Del = del;
            _DelReturnFlag = ParamFlag.ByValue;
        }
        public R Invoke(U1 p1, U2 p2, U3 p3, U4 p4, U5 p5, U6 p6, U7 p7, U8 p8, U9 p9, U10 p10, U11 p11, U12 p12, U13 p13, U14 p14, U15 p15, U16 p16)
        {
            if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> func)
            {
                return func(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
            else if (_Del is Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> act)
            {
                act(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
            else if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> rfunc)
            {
                return rfunc(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
            }
            return default;
        }
        // Ref-return path for InvokeFallback (_ReturnCategory == 2): branches on the
        // delegate kind. Action -> null ref; Func -> callresult bits as an address;
        // RefFunc -> the byref returned by the delegate passes straight through.
        private ref R InvokeRefReturn(U1 u1, U2 u2, U3 u3, U4 u4, U5 u5, U6 u6, U7 u7, U8 u8, U9 u9, U10 u10, U11 u11, U12 u12, U13 u13, U14 u14, U15 u15, U16 u16)
        {
            if (_Del is RefFunc<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> rfunc)
            {
                return ref rfunc(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15, u16);
            }
            else if (_Del is Func<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, R> func)
            {
                var callresult = func(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15, u16);
                return ref ConvertAddressToRef(callresult);
            }
            else // Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>
            {
                ((Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>)_Del)(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, u11, u12, u13, u14, u15, u16);
                return ref ConvertAddressToRef<R>(IntPtr.Zero);
            }
        }
        private delegate ref R DynamicInvoker(Delegate del, out R r, in U1 u1, in U2 u2, in U3 u3, in U4 u4, in U5 u5, in U6 u6, in U7 u7, in U8 u8, in U9 u9, in U10 u10, in U11 u11, in U12 u12, in U13 u13, in U14 u14, in U15 u15, in U16 u16);
        private static DynamicInvoker _DynamicInvokerAction;
        private static DynamicInvoker _DynamicInvokerFunc;
        private static DynamicInvoker _DynamicInvokerRefFunc;
        private static DynamicInvoker GetOrCreateDynamicInvoker(Type delType, ParamFlag delFlag)
        {
            ref DynamicInvoker invoker = ref _DynamicInvokerAction;
            if (delFlag == ParamFlag.ByValue)
            {
                invoker = ref _DynamicInvokerFunc;
            }
            else if (delFlag == ParamFlag.ByRef)
            {
                invoker = ref _DynamicInvokerRefFunc;
            }
            if (!_IsDynamicCodeDisabled && invoker == null)
            {
                try
                {
                    var dm = FreeFuncEmit.EmitDynamicInvoker(delType, typeof(R), new[] { typeof(U1), typeof(U2), typeof(U3), typeof(U4), typeof(U5), typeof(U6), typeof(U7), typeof(U8), typeof(U9), typeof(U10), typeof(U11), typeof(U12), typeof(U13), typeof(U14), typeof(U15), typeof(U16) }, (int)_ReturnCategory, _ParamFlags);
                    DynamicInvoker del = CreateDelegate<DynamicInvoker>(dm);
                    if (del == null) throw new InvalidOperationException("CreateDelegate returned null (signature mismatch on this runtime)");
                    return invoker = del;
                }
                catch (Exception)
                {
                    _IsDynamicCodeDisabled = true;
                }
            }
            return invoker;
        }
        public ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            DynamicInvoker del = GetOrCreateDynamicInvoker(_Del.GetType() ?? typeof(Action<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>), _DelReturnFlag);
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
        private ref R InvokeFallback(out R r, in U1 p1, in U2 p2, in U3 p3, in U4 p4, in U5 p5, in U6 p6, in U7 p7, in U8 p8, in U9 p9, in U10 p10, in U11 p11, in U12 p12, in U13 p13, in U14 p14, in U15 p15, in U16 p16)
        {
            r = default;
            return ref r;
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return Invoke(out var r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
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
