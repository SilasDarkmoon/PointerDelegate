# PointerDelegate
Add function pointer to .NET Standard 2.0 and Generic Delegates like ```Action<ref int>```

## What's this project for
In C# 9, we got a new feature: function pointers ```(deldgate*<...>)```

But we can not use them when developing apps running on old .NET runtime. (e.g. .NET standard 2.0)

And also, sometimes, especially when doing reflection, we need a delegate. But this is at run time, we cannot define a new delegate type.
In this condition, we normally use ```Action<T1, T2, ...> ``` generic delegate type. But what could we use when there is a "ref XXX" parameter?

## How to get it

1) Download and Copy the PointerDelegate.dll to your project.
2) Add reference to the PointerDelegate.dll
3) Use it.

## How to use it

1) ```using Mod.LowLevel;```

2) Get an IntPtr (nint) to hold the function pointer. Getting a pointer of managed method is something like that:
```C#
            var mi1 = typeof(Program).GetMethod("TestFunc1");
            RuntimeHelpers.PrepareMethod(mi1.MethodHandle);
            var fn1 = mi1.MethodHandle.GetFunctionPointer();
```

3) Make an instance of PointerDelegate

There are 16 "PointerFunc"s. Unlike System.Func, the first generic parameter of PointerFunc is the return value.

All of them are generic. The generic parameters you provide must be compatiable with the target function's signature. Use ByRefParam to indicate the parameter or return value to be a "ref XXX". Use VoidReturn to indicate the func will not return value to caller. 

For example, for "static ref int TestFunc1(ref int p)" we should choose ```PointerFunc<ByRefParam, ByRefParam>```. For "static void TestFunc1(IntPtr p)" we should choose ```PointerFunc<VoidReturn, IntPtr>```

Instead of function pointer (e.g. on Unity IL2CPP, it is impossible to get a func pointer), you can also simply wrap a system delegate using constructor ```PointerFunc<IntPtr, IntPtr>(Func<IntPtr, IntPtr> del)```

4) ```Invoke(...)``` and ```Invoke<...>(...)```

Like normal delegates there is an Invoke method to call the func. (But there is no "del(...)" syntactic sugar).

When there is no ref parameters, you can use the non-generic Invoke, with strictly type-matching when passing the arguments.

When there are ref parameters, or you want to pass a argument of the type different from the generic parameter when creating the PointerDelegate, you'll find the generic ```Invoke<...>(...)``` useful - you can provide a new type parameter for each argument!
But be careful, the parameters must be compatiable, e.g. int - enum, object - realclass, ref - IntPtr.

5) What is the underlayer of it? How does it convert parameters.

In IL, the Invoke(...) just:

pushes the args one by one onto the stack

and then pushes the function pointer

and then do a "calli"

and then return.

```
.method public hidebysig instance void Invoke(!U1 p1, !U2 p2, !U3 p3, !U4 p4) cil managed
{
    .maxstack 8
    L_0000: ldarg p1
    L_0004: ldarg p2
    L_0008: ldarg p3
    L_000c: ldarg p4
    L_0010: ldarg.0 
    L_0011: ldfld native int Mod.LowLevel.PointerDelegateBase::_Pfn
    L_0016: calli method void *(!U1, !U2, !U3, !U4)
    L_001b: ret 
}
```

It is slightly complicated for ```Invoke<>```. All its parameters are passed by ref ("in" ref). When a argument is "!IsRefParam(...)" (meaning we need its value), we do a ldobj after ldarg, while when a argument is "IsRefParam(...)" (meaning we need its ref), we just leave ldarg alone (because the arg is a ref already).

# 中文说明

为.NET Standard 2.0添加函数指针类似功能。并能实现类似```Action<ref int>```的泛型。

## 这个工程是做什么的？
在 C# 9 中，我们获得了一个新特性：函数指针 ```(delegate*<...>)```

但我们无法在基于旧版 .NET 运行时的应用中使用它。（例如 .NET Standard 2.0）

同时，有时候，尤其是在做反射的时候，我们需要一个委托。但这是在运行时，我们无法定义新的委托类型。
在这种情况下，我们通常使用 ```Action<T1, T2, ...>``` 泛型委托。但当参数中有 "ref XXX" 时，我们该用什么？

## 如何获取

1) 下载并将 PointerDelegate.dll 复制到你的项目中。
2) 添加对 PointerDelegate.dll 的引用。
3) 使用它。

## 如何使用

1) ```using Mod.LowLevel;```

2) 获取一个 IntPtr（nint）来持有函数指针。获取托管方法的指针的过程大致如下：
```C#
            var mi1 = typeof(Program).GetMethod("TestFunc1");
            RuntimeHelpers.PrepareMethod(mi1.MethodHandle);
            var fn1 = mi1.MethodHandle.GetFunctionPointer();
```

3) 创建 PointerDelegate 实例

PointerDelegate 子类有16个 PointerFunc。与 System.Func 不同，PointerFunc 的第一个泛型参数是返回值类型。

它们都是泛型的。你提供的泛型参数必须与目标函数的签名兼容。使用 ByRefParam 来指定一个参数或者返回值是一个引用（ref XXX）。使用 VoidReturn 来指定函数没有返回值。 

例如，对于"static ref int TestFunc1(ref int p)"，我们应当选择 ```PointerFunc<ByRefParam, ByRefParam>```。 对于"static void TestFunc1(IntPtr p)"，我们应当选择 ```PointerFunc<VoidReturn, IntPtr>```

如果不方便拿到指针（比如Unity IL2CPP平台），你可以简单包装一个系统的委托对象，例如使用如下的构造函数：```PointerFunc<IntPtr, IntPtr>(Func<IntPtr, IntPtr> del)```

4) ```Invoke(...)``` 和 ```Invoke<...>(...)```

和普通委托一样，有一个 Invoke 方法来调用函数。（但没有 "del(...)" 的语法糖）。

当没有 ref 参数时，你可以使用非泛型的 Invoke，传参时严格类型匹配。

当有 ref 参数时，或者你想在调用时传入与创建 PointerDelegate 时不同的类型参数，你会发现泛型的 ```Invoke<...>(...)``` 很有用——你可以为每个参数提供一个新的类型参数！
但要注意，参数必须兼容，例如 int - enum，object - realclass，ref - IntPtr。

5) 它的底层原理是什么？它是如何转换参数的。

在 IL 层面，Invoke(...) 只是：

逐个将参数压入栈

然后压入函数指针

然后执行 "calli"

然后返回。

```
.method public hidebysig instance void Invoke(!U1 p1, !U2 p2, !U3 p3, !U4 p4) cil managed
{
    .maxstack 8
    L_0000: ldarg p1
    L_0004: ldarg p2
    L_0008: ldarg p3
    L_000c: ldarg p4
    L_0010: ldarg.0 
    L_0011: ldfld native int Mod.LowLevel.PointerDelegateBase::_Pfn
    L_0016: calli method void *(!U1, !U2, !U3, !U4)
    L_001b: ret 
}
```

对于 ```Invoke<>``` 则稍微复杂一些。它的所有参数都以引用方式传递（"in" 引用）。当某个参数 "!IsRefParam(...)"（即我们需要它的值）时，我们在 ldarg 之后执行 ldobj；而当某个参数 "IsRefParam(...)"（即我们需要它的引用）时，我们只保留 ldarg 不变（因为参数本身已经是引用了）。
