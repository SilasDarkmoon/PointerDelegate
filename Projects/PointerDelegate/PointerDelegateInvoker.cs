using System;
using System.Collections.Generic;
using System.Text;

namespace Mod.LowLevel
{
    public abstract class PointerDelegateInvoker
    {
        protected PointerDelegate _PDel;
        public PointerDelegateInvoker(PointerDelegate pdel) { _PDel = pdel; }
    }
    public class PointerActionInvoker : PointerDelegateInvoker
    {
        public PointerAction Inner => (PointerAction)_PDel;
        public PointerActionInvoker(PointerAction pdel) : base(pdel) { }
        public void Invoke() => Inner.Invoke();
    }
    public class PointerActionInvoker<U1, P1> : PointerDelegateInvoker
    {
        public PointerAction<U1> Inner => (PointerAction<U1>)_PDel;
        public PointerActionInvoker(PointerAction<U1> pdel) : base(pdel) { }
        public void Invoke(in P1 p1) => Inner.Invoke(in p1);
    }
    public class PointerActionInvoker<U1, U2, P1, P2> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2> Inner => (PointerAction<U1, U2>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2) => Inner.Invoke(in p1, in p2);
    }
    public class PointerActionInvoker<U1, U2, U3, P1, P2, P3> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3> Inner => (PointerAction<U1, U2, U3>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3) => Inner.Invoke(in p1, in p2, in p3);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, P1, P2, P3, P4> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4> Inner => (PointerAction<U1, U2, U3, U4>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => Inner.Invoke(in p1, in p2, in p3, in p4);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, P1, P2, P3, P4, P5> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5> Inner => (PointerAction<U1, U2, U3, U4, U5>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, P1, P2, P3, P4, P5, P6> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6> Inner => (PointerAction<U1, U2, U3, U4, U5, U6>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, P1, P2, P3, P4, P5, P6, P7> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, P1, P2, P3, P4, P5, P6, P7, P8> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, P1, P2, P3, P4, P5, P6, P7, P8, P9> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    }
    public class PointerActionInvoker<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : PointerDelegateInvoker
    {
        public PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Inner => (PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>)_PDel;
        public PointerActionInvoker(PointerAction<U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> pdel) : base(pdel) { }
        public void Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    }

    public class PointerFuncInvoker<R> : PointerDelegateInvoker
    {
        public PointerFunc<R> Inner => (PointerFunc<R>)_PDel;
        public PointerFuncInvoker(PointerFunc<R> pdel) : base(pdel) { }
        public R Invoke() => Inner.Invoke();
    }
    public class PointerFuncInvoker<R, U1, P1> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1> Inner => (PointerFunc<R, U1>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1> pdel) : base(pdel) { }
        public R Invoke(in P1 p1) => Inner.Invoke(in p1);
    }
    public class PointerFuncInvoker<R, U1, U2, P1, P2> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2> Inner => (PointerFunc<R, U1, U2>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2) => Inner.Invoke(in p1, in p2);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, P1, P2, P3> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3> Inner => (PointerFunc<R, U1, U2, U3>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3) => Inner.Invoke(in p1, in p2, in p3);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, P1, P2, P3, P4> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4> Inner => (PointerFunc<R, U1, U2, U3, U4>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => Inner.Invoke(in p1, in p2, in p3, in p4);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, P1, P2, P3, P4, P5> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5> Inner => (PointerFunc<R, U1, U2, U3, U4, U5>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, P1, P2, P3, P4, P5, P6> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, P1, P2, P3, P4, P5, P6, P7> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, P1, P2, P3, P4, P5, P6, P7, P8> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, P1, P2, P3, P4, P5, P6, P7, P8, P9> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    }
    public class PointerFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : PointerDelegateInvoker
    {
        public PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Inner => (PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>)_PDel;
        public PointerFuncInvoker(PointerFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> pdel) : base(pdel) { }
        public R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    }

    public class PointerRefFuncInvoker<R> : PointerDelegateInvoker
    {
        public PointerRefFunc<R> Inner => (PointerRefFunc<R>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R> pdel) : base(pdel) { }
        public ref R Invoke() => ref Inner.Invoke();
    }
    public class PointerRefFuncInvoker<R, U1, P1> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1> Inner => (PointerRefFunc<R, U1>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1) => ref Inner.Invoke(in p1);
    }
    public class PointerRefFuncInvoker<R, U1, U2, P1, P2> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2> Inner => (PointerRefFunc<R, U1, U2>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2) => ref Inner.Invoke(in p1, in p2);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, P1, P2, P3> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3> Inner => (PointerRefFunc<R, U1, U2, U3>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3) => ref Inner.Invoke(in p1, in p2, in p3);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, P1, P2, P3, P4> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4> Inner => (PointerRefFunc<R, U1, U2, U3, U4>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4) => ref Inner.Invoke(in p1, in p2, in p3, in p4);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, P1, P2, P3, P4, P5> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, P1, P2, P3, P4, P5, P6> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, P1, P2, P3, P4, P5, P6, P7> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, P1, P2, P3, P4, P5, P6, P7, P8> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, P1, P2, P3, P4, P5, P6, P7, P8, P9> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
    }
    public class PointerRefFuncInvoker<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : PointerDelegateInvoker
    {
        public PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> Inner => (PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16>)_PDel;
        public PointerRefFuncInvoker(PointerRefFunc<R, U1, U2, U3, U4, U5, U6, U7, U8, U9, U10, U11, U12, U13, U14, U15, U16> pdel) : base(pdel) { }
        public ref R Invoke(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16) => ref Inner.Invoke(in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
    }
}