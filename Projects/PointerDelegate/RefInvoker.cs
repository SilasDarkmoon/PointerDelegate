using System;
using System.Collections.Generic;
using System.Text;

namespace Mod.LowLevel
{
    public sealed class ActionRefInvoker1<EX> : FreeInvokableBase, IFreeInvokableFunc2<VoidReturn>
    {
        public Action<ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker1(Action<ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2>(out VoidReturn r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc2<VoidReturn>.Invoke<P1, P2>(out VoidReturn r, in P1 p1, in P2 p2)
        {
            return ref Invoke(out r, in p1, in p2);
        }
        public void Invoke<P1>(in P1 p1, EX ex)
        {
            Invoke(out _, in p1, in ex);
        }
    }
    public sealed class ActionRefInvoker2<EX> : FreeInvokableBase, IFreeInvokableFunc3<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker2(Action<ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc3<VoidReturn>.Invoke<P1, P2, P3>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref Invoke(out r, in p1, in p2, in p3);
        }
        public void Invoke<P1, P2>(in P1 p1, in P2 p2, EX ex)
        {
            Invoke(out _, in p1, in p2, in ex);
        }
    }
    public sealed class ActionRefInvoker3<EX> : FreeInvokableBase, IFreeInvokableFunc4<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker3(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc4<VoidReturn>.Invoke<P1, P2, P3, P4>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public void Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in ex);
        }
    }
    public sealed class ActionRefInvoker4<EX> : FreeInvokableBase, IFreeInvokableFunc5<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker4(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc5<VoidReturn>.Invoke<P1, P2, P3, P4, P5>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public void Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in ex);
        }
    }
    public sealed class ActionRefInvoker5<EX> : FreeInvokableBase, IFreeInvokableFunc6<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker5(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc6<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public void Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in ex);
        }
    }
    public sealed class ActionRefInvoker6<EX> : FreeInvokableBase, IFreeInvokableFunc7<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker6(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc7<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in ex);
        }
    }
    public sealed class ActionRefInvoker7<EX> : FreeInvokableBase, IFreeInvokableFunc8<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker7(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc8<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in ex);
        }
    }
    public sealed class ActionRefInvoker8<EX> : FreeInvokableBase, IFreeInvokableFunc9<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker8(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc9<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in ex);
        }
    }
    public sealed class ActionRefInvoker9<EX> : FreeInvokableBase, IFreeInvokableFunc10<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker9(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc10<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in ex);
        }
    }
    public sealed class ActionRefInvoker10<EX> : FreeInvokableBase, IFreeInvokableFunc11<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker10(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc11<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in ex);
        }
    }
    public sealed class ActionRefInvoker11<EX> : FreeInvokableBase, IFreeInvokableFunc12<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker11(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc12<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in ex);
        }
    }
    public sealed class ActionRefInvoker12<EX> : FreeInvokableBase, IFreeInvokableFunc13<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker12(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc13<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in ex);
        }
    }
    public sealed class ActionRefInvoker13<EX> : FreeInvokableBase, IFreeInvokableFunc14<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker13(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc14<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in ex);
        }
    }
    public sealed class ActionRefInvoker14<EX> : FreeInvokableBase, IFreeInvokableFunc15<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker14(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc15<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in ex);
        }
    }
    public sealed class ActionRefInvoker15<EX> : FreeInvokableBase, IFreeInvokableFunc16<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> _Del;
        public ActionRefInvoker15(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc16<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, EX ex)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in ex);
        }
    }
    public sealed class ActionRefInvoker1 : FreeInvokableBase, IFreeInvokableFunc1<VoidReturn>
    {
        public Action<ByRefLocalSlot> _Del;
        public ActionRefInvoker1(Action<ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1>(out VoidReturn r, in P1 p1)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc1<VoidReturn>.Invoke<P1>(out VoidReturn r, in P1 p1)
        {
            return ref Invoke(out r, in p1);
        }
        public void Invoke<P1>(in P1 p1)
        {
            Invoke(out _, in p1);
        }
    }
    public sealed class ActionRefInvoker2 : FreeInvokableBase, IFreeInvokableFunc2<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker2(Action<ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2>(out VoidReturn r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc2<VoidReturn>.Invoke<P1, P2>(out VoidReturn r, in P1 p1, in P2 p2)
        {
            return ref Invoke(out r, in p1, in p2);
        }
        public void Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            Invoke(out _, in p1, in p2);
        }
    }
    public sealed class ActionRefInvoker3 : FreeInvokableBase, IFreeInvokableFunc3<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker3(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc3<VoidReturn>.Invoke<P1, P2, P3>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref Invoke(out r, in p1, in p2, in p3);
        }
        public void Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            Invoke(out _, in p1, in p2, in p3);
        }
    }
    public sealed class ActionRefInvoker4 : FreeInvokableBase, IFreeInvokableFunc4<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker4(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc4<VoidReturn>.Invoke<P1, P2, P3, P4>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public void Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            Invoke(out _, in p1, in p2, in p3, in p4);
        }
    }
    public sealed class ActionRefInvoker5 : FreeInvokableBase, IFreeInvokableFunc5<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker5(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc5<VoidReturn>.Invoke<P1, P2, P3, P4, P5>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public void Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5);
        }
    }
    public sealed class ActionRefInvoker6 : FreeInvokableBase, IFreeInvokableFunc6<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker6(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc6<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6);
        }
    }
    public sealed class ActionRefInvoker7 : FreeInvokableBase, IFreeInvokableFunc7<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker7(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc7<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
    }
    public sealed class ActionRefInvoker8 : FreeInvokableBase, IFreeInvokableFunc8<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker8(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc8<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
    }
    public sealed class ActionRefInvoker9 : FreeInvokableBase, IFreeInvokableFunc9<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker9(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc9<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
    }
    public sealed class ActionRefInvoker10 : FreeInvokableBase, IFreeInvokableFunc10<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker10(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc10<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
    }
    public sealed class ActionRefInvoker11 : FreeInvokableBase, IFreeInvokableFunc11<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker11(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc11<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
    }
    public sealed class ActionRefInvoker12 : FreeInvokableBase, IFreeInvokableFunc12<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker12(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc12<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
    }
    public sealed class ActionRefInvoker13 : FreeInvokableBase, IFreeInvokableFunc13<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker13(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc13<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
    }
    public sealed class ActionRefInvoker14 : FreeInvokableBase, IFreeInvokableFunc14<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker14(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc14<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
    }
    public sealed class ActionRefInvoker15 : FreeInvokableBase, IFreeInvokableFunc15<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker15(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc15<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
    }
    public sealed class ActionRefInvoker16 : FreeInvokableBase, IFreeInvokableFunc16<VoidReturn>
    {
        public Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> _Del;
        public ActionRefInvoker16(Action<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot> del)
        {
            _Del = del;
        }

        private ref VoidReturn Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        ref VoidReturn IFreeInvokableFunc16<VoidReturn>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out VoidReturn r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
        public void Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
    }
    public sealed class FuncRefInvoker1<R, EX> : FreeInvokableBase, IFreeInvokableFunc2<R>
    {
        public Func<ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker1(Func<ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc2<R>.Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            return ref Invoke(out r, in p1, in p2);
        }
        public R Invoke<P1>(in P1 p1, EX ex)
        {
            return Invoke(out _, in p1, in ex);
        }
    }
    public sealed class FuncRefInvoker2<R, EX> : FreeInvokableBase, IFreeInvokableFunc3<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker2(Func<ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc3<R>.Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref Invoke(out r, in p1, in p2, in p3);
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2, EX ex)
        {
            return Invoke(out _, in p1, in p2, in ex);
        }
    }
    public sealed class FuncRefInvoker3<R, EX> : FreeInvokableBase, IFreeInvokableFunc4<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker3(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc4<R>.Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in ex);
        }
    }
    public sealed class FuncRefInvoker4<R, EX> : FreeInvokableBase, IFreeInvokableFunc5<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker4(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc5<R>.Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in ex);
        }
    }
    public sealed class FuncRefInvoker5<R, EX> : FreeInvokableBase, IFreeInvokableFunc6<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker5(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc6<R>.Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in ex);
        }
    }
    public sealed class FuncRefInvoker6<R, EX> : FreeInvokableBase, IFreeInvokableFunc7<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker6(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc7<R>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in ex);
        }
    }
    public sealed class FuncRefInvoker7<R, EX> : FreeInvokableBase, IFreeInvokableFunc8<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker7(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc8<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in ex);
        }
    }
    public sealed class FuncRefInvoker8<R, EX> : FreeInvokableBase, IFreeInvokableFunc9<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker8(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc9<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in ex);
        }
    }
    public sealed class FuncRefInvoker9<R, EX> : FreeInvokableBase, IFreeInvokableFunc10<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker9(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc10<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in ex);
        }
    }
    public sealed class FuncRefInvoker10<R, EX> : FreeInvokableBase, IFreeInvokableFunc11<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker10(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc11<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in ex);
        }
    }
    public sealed class FuncRefInvoker11<R, EX> : FreeInvokableBase, IFreeInvokableFunc12<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker11(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc12<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in ex);
        }
    }
    public sealed class FuncRefInvoker12<R, EX> : FreeInvokableBase, IFreeInvokableFunc13<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker12(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc13<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in ex);
        }
    }
    public sealed class FuncRefInvoker13<R, EX> : FreeInvokableBase, IFreeInvokableFunc14<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker13(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc14<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in ex);
        }
    }
    public sealed class FuncRefInvoker14<R, EX> : FreeInvokableBase, IFreeInvokableFunc15<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker14(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc15<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in ex);
        }
    }
    public sealed class FuncRefInvoker15<R, EX> : FreeInvokableBase, IFreeInvokableFunc16<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public FuncRefInvoker15(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc16<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in ex);
        }
    }
    public sealed class FuncRefInvoker1<R> : FreeInvokableBase, IFreeInvokableFunc1<R>
    {
        public Func<ByRefLocalSlot, R> _Del;
        public FuncRefInvoker1(Func<ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1>(out R r, in P1 p1)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc1<R>.Invoke<P1>(out R r, in P1 p1)
        {
            return ref Invoke(out r, in p1);
        }
        public R Invoke<P1>(in P1 p1)
        {
            return Invoke(out _, in p1);
        }
    }
    public sealed class FuncRefInvoker2<R> : FreeInvokableBase, IFreeInvokableFunc2<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker2(Func<ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc2<R>.Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            return ref Invoke(out r, in p1, in p2);
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            return Invoke(out _, in p1, in p2);
        }
    }
    public sealed class FuncRefInvoker3<R> : FreeInvokableBase, IFreeInvokableFunc3<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker3(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc3<R>.Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref Invoke(out r, in p1, in p2, in p3);
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            return Invoke(out _, in p1, in p2, in p3);
        }
    }
    public sealed class FuncRefInvoker4<R> : FreeInvokableBase, IFreeInvokableFunc4<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker4(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc4<R>.Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4);
        }
    }
    public sealed class FuncRefInvoker5<R> : FreeInvokableBase, IFreeInvokableFunc5<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker5(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc5<R>.Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5);
        }
    }
    public sealed class FuncRefInvoker6<R> : FreeInvokableBase, IFreeInvokableFunc6<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker6(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc6<R>.Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6);
        }
    }
    public sealed class FuncRefInvoker7<R> : FreeInvokableBase, IFreeInvokableFunc7<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker7(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc7<R>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
    }
    public sealed class FuncRefInvoker8<R> : FreeInvokableBase, IFreeInvokableFunc8<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker8(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc8<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
    }
    public sealed class FuncRefInvoker9<R> : FreeInvokableBase, IFreeInvokableFunc9<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker9(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc9<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
    }
    public sealed class FuncRefInvoker10<R> : FreeInvokableBase, IFreeInvokableFunc10<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker10(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc10<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
    }
    public sealed class FuncRefInvoker11<R> : FreeInvokableBase, IFreeInvokableFunc11<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker11(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc11<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
    }
    public sealed class FuncRefInvoker12<R> : FreeInvokableBase, IFreeInvokableFunc12<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker12(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc12<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
    }
    public sealed class FuncRefInvoker13<R> : FreeInvokableBase, IFreeInvokableFunc13<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker13(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc13<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
    }
    public sealed class FuncRefInvoker14<R> : FreeInvokableBase, IFreeInvokableFunc14<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker14(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc14<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
    }
    public sealed class FuncRefInvoker15<R> : FreeInvokableBase, IFreeInvokableFunc15<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker15(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc15<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
    }
    public sealed class FuncRefInvoker16<R> : FreeInvokableBase, IFreeInvokableFunc16<R>
    {
        public Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public FuncRefInvoker16(Func<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc16<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
    }
    public sealed class RefFuncRefInvoker1<R, EX> : FreeInvokableBase, IFreeInvokableFunc2<R>
    {
        public RefFunc<ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker1(RefFunc<ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc2<R>.Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            return ref Invoke(out r, in p1, in p2);
        }
        public R Invoke<P1>(in P1 p1, EX ex)
        {
            return Invoke(out _, in p1, in ex);
        }
    }
    public sealed class RefFuncRefInvoker2<R, EX> : FreeInvokableBase, IFreeInvokableFunc3<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker2(RefFunc<ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc3<R>.Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref Invoke(out r, in p1, in p2, in p3);
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2, EX ex)
        {
            return Invoke(out _, in p1, in p2, in ex);
        }
    }
    public sealed class RefFuncRefInvoker3<R, EX> : FreeInvokableBase, IFreeInvokableFunc4<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker3(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc4<R>.Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in ex);
        }
    }
    public sealed class RefFuncRefInvoker4<R, EX> : FreeInvokableBase, IFreeInvokableFunc5<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker4(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc5<R>.Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in ex);
        }
    }
    public sealed class RefFuncRefInvoker5<R, EX> : FreeInvokableBase, IFreeInvokableFunc6<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker5(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc6<R>.Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in ex);
        }
    }
    public sealed class RefFuncRefInvoker6<R, EX> : FreeInvokableBase, IFreeInvokableFunc7<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker6(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc7<R>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in ex);
        }
    }
    public sealed class RefFuncRefInvoker7<R, EX> : FreeInvokableBase, IFreeInvokableFunc8<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker7(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc8<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in ex);
        }
    }
    public sealed class RefFuncRefInvoker8<R, EX> : FreeInvokableBase, IFreeInvokableFunc9<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker8(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc9<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in ex);
        }
    }
    public sealed class RefFuncRefInvoker9<R, EX> : FreeInvokableBase, IFreeInvokableFunc10<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker9(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc10<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in ex);
        }
    }
    public sealed class RefFuncRefInvoker10<R, EX> : FreeInvokableBase, IFreeInvokableFunc11<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker10(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc11<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in ex);
        }
    }
    public sealed class RefFuncRefInvoker11<R, EX> : FreeInvokableBase, IFreeInvokableFunc12<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker11(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc12<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in ex);
        }
    }
    public sealed class RefFuncRefInvoker12<R, EX> : FreeInvokableBase, IFreeInvokableFunc13<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker12(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc13<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in ex);
        }
    }
    public sealed class RefFuncRefInvoker13<R, EX> : FreeInvokableBase, IFreeInvokableFunc14<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker13(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc14<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in ex);
        }
    }
    public sealed class RefFuncRefInvoker14<R, EX> : FreeInvokableBase, IFreeInvokableFunc15<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker14(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc15<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in ex);
        }
    }
    public sealed class RefFuncRefInvoker15<R, EX> : FreeInvokableBase, IFreeInvokableFunc16<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> _Del;
        public RefFuncRefInvoker15(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, EX, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc16<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, EX ex)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in ex);
        }
    }
    public sealed class RefFuncRefInvoker1<R> : FreeInvokableBase, IFreeInvokableFunc1<R>
    {
        public RefFunc<ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker1(RefFunc<ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1>(out R r, in P1 p1)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc1<R>.Invoke<P1>(out R r, in P1 p1)
        {
            return ref Invoke(out r, in p1);
        }
        public R Invoke<P1>(in P1 p1)
        {
            return Invoke(out _, in p1);
        }
    }
    public sealed class RefFuncRefInvoker2<R> : FreeInvokableBase, IFreeInvokableFunc2<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker2(RefFunc<ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc2<R>.Invoke<P1, P2>(out R r, in P1 p1, in P2 p2)
        {
            return ref Invoke(out r, in p1, in p2);
        }
        public R Invoke<P1, P2>(in P1 p1, in P2 p2)
        {
            return Invoke(out _, in p1, in p2);
        }
    }
    public sealed class RefFuncRefInvoker3<R> : FreeInvokableBase, IFreeInvokableFunc3<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker3(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc3<R>.Invoke<P1, P2, P3>(out R r, in P1 p1, in P2 p2, in P3 p3)
        {
            return ref Invoke(out r, in p1, in p2, in p3);
        }
        public R Invoke<P1, P2, P3>(in P1 p1, in P2 p2, in P3 p3)
        {
            return Invoke(out _, in p1, in p2, in p3);
        }
    }
    public sealed class RefFuncRefInvoker4<R> : FreeInvokableBase, IFreeInvokableFunc4<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker4(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc4<R>.Invoke<P1, P2, P3, P4>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4);
        }
        public R Invoke<P1, P2, P3, P4>(in P1 p1, in P2 p2, in P3 p3, in P4 p4)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4);
        }
    }
    public sealed class RefFuncRefInvoker5<R> : FreeInvokableBase, IFreeInvokableFunc5<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker5(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc5<R>.Invoke<P1, P2, P3, P4, P5>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5);
        }
        public R Invoke<P1, P2, P3, P4, P5>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5);
        }
    }
    public sealed class RefFuncRefInvoker6<R> : FreeInvokableBase, IFreeInvokableFunc6<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker6(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc6<R>.Invoke<P1, P2, P3, P4, P5, P6>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6);
        }
    }
    public sealed class RefFuncRefInvoker7<R> : FreeInvokableBase, IFreeInvokableFunc7<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker7(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc7<R>.Invoke<P1, P2, P3, P4, P5, P6, P7>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7);
        }
    }
    public sealed class RefFuncRefInvoker8<R> : FreeInvokableBase, IFreeInvokableFunc8<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker8(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc8<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8);
        }
    }
    public sealed class RefFuncRefInvoker9<R> : FreeInvokableBase, IFreeInvokableFunc9<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker9(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc9<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9);
        }
    }
    public sealed class RefFuncRefInvoker10<R> : FreeInvokableBase, IFreeInvokableFunc10<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker10(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc10<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10);
        }
    }
    public sealed class RefFuncRefInvoker11<R> : FreeInvokableBase, IFreeInvokableFunc11<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker11(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc11<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11);
        }
    }
    public sealed class RefFuncRefInvoker12<R> : FreeInvokableBase, IFreeInvokableFunc12<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker12(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc12<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12);
        }
    }
    public sealed class RefFuncRefInvoker13<R> : FreeInvokableBase, IFreeInvokableFunc13<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker13(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc13<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13);
        }
    }
    public sealed class RefFuncRefInvoker14<R> : FreeInvokableBase, IFreeInvokableFunc14<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker14(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc14<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14);
        }
    }
    public sealed class RefFuncRefInvoker15<R> : FreeInvokableBase, IFreeInvokableFunc15<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker15(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc15<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15);
        }
    }
    public sealed class RefFuncRefInvoker16<R> : FreeInvokableBase, IFreeInvokableFunc16<R>
    {
        public RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> _Del;
        public RefFuncRefInvoker16(RefFunc<ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, ByRefLocalSlot, R> del)
        {
            _Del = del;
        }
        private ref R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            throw new NotImplementedException();
        }
        ref R IFreeInvokableFunc16<R>.Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(out R r, in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return ref Invoke(out r, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
        public R Invoke<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(in P1 p1, in P2 p2, in P3 p3, in P4 p4, in P5 p5, in P6 p6, in P7 p7, in P8 p8, in P9 p9, in P10 p10, in P11 p11, in P12 p12, in P13 p13, in P14 p14, in P15 p15, in P16 p16)
        {
            return Invoke(out _, in p1, in p2, in p3, in p4, in p5, in p6, in p7, in p8, in p9, in p10, in p11, in p12, in p13, in p14, in p15, in p16);
        }
    }
}
