using System;
using System.Collections.Generic;
using System.Text;

namespace Mod.LowLevel
{
    public interface IFreeInvokable { }
    #region IFreeInvokableFunc
    public interface IFreeInvokableFunc<R> : IFreeInvokable
    {
        R Invoke();
    }
    public interface IFreeInvokableFunc1<R> : IFreeInvokable
    {
        R Invoke<P1>(in P1 p1);
    }
    public interface IFreeInvokableFunc2<R> : IFreeInvokable
    {
        R Invoke<P1, P2>(in P1 p1, in P2 p2);
    }
    public interface IFreeInvokableFunc3<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3);
    }
    public interface IFreeInvokableFunc4<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4);
    }
    public interface IFreeInvokableFunc5<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
    }
    public interface IFreeInvokableFunc6<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
    }
    public interface IFreeInvokableFunc7<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
    }
    public interface IFreeInvokableFunc8<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
    }
    public interface IFreeInvokableFunc9<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
    }
    public interface IFreeInvokableFunc10<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
    }
    public interface IFreeInvokableFunc11<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
    }
    public interface IFreeInvokableFunc12<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
    }
    public interface IFreeInvokableFunc13<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
    }
    public interface IFreeInvokableFunc14<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
    }
    public interface IFreeInvokableFunc15<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
    }
    public interface IFreeInvokableFunc16<R> : IFreeInvokable
    {
        R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
    }
    #endregion

    public interface IUnifiedInvoker { }
    #region IUnifiedFuncInvoker
    public interface IUnifiedFuncInvoker<R> : IUnifiedInvoker
    {
        R Invoke();
    }
    public interface IUnifiedFuncInvoker<R, P1> : IUnifiedInvoker
    {
        R Invoke(in P1 p1);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
    }
    public interface IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : IUnifiedInvoker
    {
        R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
    }
    #endregion

    public abstract class UnifiedInvoker : IUnifiedInvoker
    {
        protected IFreeInvokable _FreeInvokable;
        protected UnifiedInvoker() { }
        protected UnifiedInvoker(IFreeInvokable invokable) { _FreeInvokable = invokable; }
    }

    //public class UnifiedFuncInvoker<R> : UnifiedInvoker, IUnifiedFuncInvoker<R>
    //{
    //    public IFreeInvokableFunc<R> Inner => (IFreeInvokableFunc<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc<R> invokable) : base(invokable) { }
    //    public R Invoke() => Inner.Invoke();
    //}
    //public class UnifiedFuncInvoker<R, P1> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1>
    //{
    //    public IFreeInvokableFunc1<R> Inner => (IFreeInvokableFunc1<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc1<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1) => Inner.Invoke(in p1);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2>
    //{
    //    public IFreeInvokableFunc2<R> Inner => (IFreeInvokableFunc2<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc2<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2) => Inner.Invoke(in p1, in p2);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3>
    //{
    //    public IFreeInvokableFunc3<R> Inner => (IFreeInvokableFunc3<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc3<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3) => Inner.Invoke(in p1, in p2, in p3);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4>
    //{
    //    public IFreeInvokableFunc4<R> Inner => (IFreeInvokableFunc4<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc4<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => Inner.Invoke(in p1, in p2, in p3, in p4);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5>
    //{
    //    public IFreeInvokableFunc5<R> Inner => (IFreeInvokableFunc5<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc5<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6>
    //{
    //    public IFreeInvokableFunc6<R> Inner => (IFreeInvokableFunc6<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc6<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7>
    //{
    //    public IFreeInvokableFunc7<R> Inner => (IFreeInvokableFunc7<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc7<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8>
    //{
    //    public IFreeInvokableFunc8<R> Inner => (IFreeInvokableFunc8<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc8<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9>
    //{
    //    public IFreeInvokableFunc9<R> Inner => (IFreeInvokableFunc9<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc9<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>
    //{
    //    public IFreeInvokableFunc10<R> Inner => (IFreeInvokableFunc10<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc10<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>
    //{
    //    public IFreeInvokableFunc11<R> Inner => (IFreeInvokableFunc11<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc11<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>
    //{
    //    public IFreeInvokableFunc12<R> Inner => (IFreeInvokableFunc12<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc12<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>
    //{
    //    public IFreeInvokableFunc13<R> Inner => (IFreeInvokableFunc13<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc13<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>
    //{
    //    public IFreeInvokableFunc14<R> Inner => (IFreeInvokableFunc14<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc14<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>
    //{
    //    public IFreeInvokableFunc15<R> Inner => (IFreeInvokableFunc15<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc15<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    //}
    //public class UnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : UnifiedInvoker, IUnifiedFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>
    //{
    //    public IFreeInvokableFunc16<R> Inner => (IFreeInvokableFunc16<R>)_FreeInvokable;
    //    public UnifiedFuncInvoker(IFreeInvokableFunc16<R> invokable) : base(invokable) { }
    //    public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    //}
}