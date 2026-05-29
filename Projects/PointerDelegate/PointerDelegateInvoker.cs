using System;
using System.Collections.Generic;
using System.Text;

namespace Mod.LowLevel
{
    public interface IFreeInvokable { }
    #region IFreeInvokableAction
    public interface IFreeInvokableAction : IFreeInvokable
    {
        void Invoke();
    }
    public interface IFreeInvokableAction1 : IFreeInvokable
    {
        void Invoke<P1>(in P1 p1);
    }
    public interface IFreeInvokableAction2 : IFreeInvokable
    {
        void Invoke<P1, P2>(in P1 p1, in P2 p2);
    }
    public interface IFreeInvokableAction3 : IFreeInvokable
    {
        void Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3);
    }
    public interface IFreeInvokableAction4 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4);
    }
    public interface IFreeInvokableAction5 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
    }
    public interface IFreeInvokableAction6 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
    }
    public interface IFreeInvokableAction7 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
    }
    public interface IFreeInvokableAction8 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
    }
    public interface IFreeInvokableAction9 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
    }
    public interface IFreeInvokableAction10 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
    }
    public interface IFreeInvokableAction11 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
    }
    public interface IFreeInvokableAction12 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
    }
    public interface IFreeInvokableAction13 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
    }
    public interface IFreeInvokableAction14 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
    }
    public interface IFreeInvokableAction15 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
    }
    public interface IFreeInvokableAction16 : IFreeInvokable
    {
        void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
    }
    #endregion
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
    #region IFreeInvokableRefFunc
    public interface IFreeInvokableRefFunc<R> : IFreeInvokable
    {
        ref R Invoke();
    }
    public interface IFreeInvokableRefFunc1<R> : IFreeInvokable
    {
        ref R Invoke<P1>(in P1 p1);
    }
    public interface IFreeInvokableRefFunc2<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2>(in P1 p1, in P2 p2);
    }
    public interface IFreeInvokableRefFunc3<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3);
    }
    public interface IFreeInvokableRefFunc4<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4);
    }
    public interface IFreeInvokableRefFunc5<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5);
    }
    public interface IFreeInvokableRefFunc6<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6);
    }
    public interface IFreeInvokableRefFunc7<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7);
    }
    public interface IFreeInvokableRefFunc8<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8);
    }
    public interface IFreeInvokableRefFunc9<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9);
    }
    public interface IFreeInvokableRefFunc10<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10);
    }
    public interface IFreeInvokableRefFunc11<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11);
    }
    public interface IFreeInvokableRefFunc12<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12);
    }
    public interface IFreeInvokableRefFunc13<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13);
    }
    public interface IFreeInvokableRefFunc14<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14);
    }
    public interface IFreeInvokableRefFunc15<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15);
    }
    public interface IFreeInvokableRefFunc16<R> : IFreeInvokable
    {
        ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16);
    }
    #endregion

    public abstract class FreeInvokableStrictInvoker
    {
        protected IFreeInvokable _FreeInvokable;
        public FreeInvokableStrictInvoker(IFreeInvokable invokable) { _FreeInvokable = invokable; }
    }
    public class FreeActionInvoker : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction Inner => (IFreeInvokableAction)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction invokable) : base(invokable) { }
        public void Invoke() => Inner.Invoke();
    }
    public class FreeActionInvoker<P1> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction1 Inner => (IFreeInvokableAction1)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction1 invokable) : base(invokable) { }
        public void Invoke(in P1 p1) => Inner.Invoke(in p1);
    }
    public class FreeActionInvoker<P1, P2> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction2 Inner => (IFreeInvokableAction2)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction2 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2) => Inner.Invoke(in p1, in p2);
    }
    public class FreeActionInvoker<P1, P2, P3> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction3 Inner => (IFreeInvokableAction3)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction3 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3) => Inner.Invoke(in p1, in p2, in p3);
    }
    public class FreeActionInvoker<P1, P2, P3, P4> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction4 Inner => (IFreeInvokableAction4)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction4 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => Inner.Invoke(in p1, in p2, in p3, in p4);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction5 Inner => (IFreeInvokableAction5)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction5 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction6 Inner => (IFreeInvokableAction6)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction6 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction7 Inner => (IFreeInvokableAction7)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction7 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction8 Inner => (IFreeInvokableAction8)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction8 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction9 Inner => (IFreeInvokableAction9)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction9 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction10 Inner => (IFreeInvokableAction10)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction10 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction11 Inner => (IFreeInvokableAction11)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction11 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction12 Inner => (IFreeInvokableAction12)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction12 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction13 Inner => (IFreeInvokableAction13)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction13 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction14 Inner => (IFreeInvokableAction14)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction14 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction15 Inner => (IFreeInvokableAction15)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction15 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    }
    public class FreeActionInvoker<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableAction16 Inner => (IFreeInvokableAction16)_FreeInvokable;
        public FreeActionInvoker(IFreeInvokableAction16 invokable) : base(invokable) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    }

    public class FreeFuncInvoker<R> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc<R> Inner => (IFreeInvokableFunc<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc<R> invokable) : base(invokable) { }
        public R Invoke() => Inner.Invoke();
    }
    public class FreeFuncInvoker<R, P1> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc1<R> Inner => (IFreeInvokableFunc1<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc1<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1) => Inner.Invoke(in p1);
    }
    public class FreeFuncInvoker<R, P1, P2> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc2<R> Inner => (IFreeInvokableFunc2<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc2<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2) => Inner.Invoke(in p1, in p2);
    }
    public class FreeFuncInvoker<R, P1, P2, P3> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc3<R> Inner => (IFreeInvokableFunc3<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc3<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3) => Inner.Invoke(in p1, in p2, in p3);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc4<R> Inner => (IFreeInvokableFunc4<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc4<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => Inner.Invoke(in p1, in p2, in p3, in p4);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc5<R> Inner => (IFreeInvokableFunc5<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc5<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc6<R> Inner => (IFreeInvokableFunc6<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc6<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc7<R> Inner => (IFreeInvokableFunc7<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc7<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc8<R> Inner => (IFreeInvokableFunc8<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc8<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc9<R> Inner => (IFreeInvokableFunc9<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc9<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc10<R> Inner => (IFreeInvokableFunc10<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc10<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc11<R> Inner => (IFreeInvokableFunc11<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc11<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc12<R> Inner => (IFreeInvokableFunc12<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc12<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc13<R> Inner => (IFreeInvokableFunc13<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc13<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc14<R> Inner => (IFreeInvokableFunc14<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc14<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc15<R> Inner => (IFreeInvokableFunc15<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc15<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    }
    public class FreeFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableFunc16<R> Inner => (IFreeInvokableFunc16<R>)_FreeInvokable;
        public FreeFuncInvoker(IFreeInvokableFunc16<R> invokable) : base(invokable) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    }

    public class FreeRefFuncInvoker<R> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc<R> Inner => (IFreeInvokableRefFunc<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc<R> invokable) : base(invokable) { }
        public ref R Invoke() => ref Inner.Invoke();
    }
    public class FreeRefFuncInvoker<R, P1> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc1<R> Inner => (IFreeInvokableRefFunc1<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc1<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1) => ref Inner.Invoke(in p1);
    }
    public class FreeRefFuncInvoker<R, P1, P2> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc2<R> Inner => (IFreeInvokableRefFunc2<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc2<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2) => ref Inner.Invoke(in p1, in p2);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc3<R> Inner => (IFreeInvokableRefFunc3<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc3<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3) => ref Inner.Invoke(in p1, in p2, in p3);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc4<R> Inner => (IFreeInvokableRefFunc4<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc4<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => ref Inner.Invoke(in p1, in p2, in p3, in p4);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc5<R> Inner => (IFreeInvokableRefFunc5<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc5<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc6<R> Inner => (IFreeInvokableRefFunc6<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc6<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc7<R> Inner => (IFreeInvokableRefFunc7<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc7<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc8<R> Inner => (IFreeInvokableRefFunc8<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc8<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc9<R> Inner => (IFreeInvokableRefFunc9<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc9<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc10<R> Inner => (IFreeInvokableRefFunc10<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc10<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc11<R> Inner => (IFreeInvokableRefFunc11<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc11<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc12<R> Inner => (IFreeInvokableRefFunc12<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc12<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc13<R> Inner => (IFreeInvokableRefFunc13<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc13<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc14<R> Inner => (IFreeInvokableRefFunc14<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc14<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc15<R> Inner => (IFreeInvokableRefFunc15<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc15<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    }
    public class FreeRefFuncInvoker<R, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : FreeInvokableStrictInvoker
    {
        public IFreeInvokableRefFunc16<R> Inner => (IFreeInvokableRefFunc16<R>)_FreeInvokable;
        public FreeRefFuncInvoker(IFreeInvokableRefFunc16<R> invokable) : base(invokable) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    }
}