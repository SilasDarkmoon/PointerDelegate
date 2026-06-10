using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Data;

namespace Generator
{
    public static class MonoCecilExtensions
    {
        internal static MethodReference GetReference(this MethodDefinition method, GenericInstanceType type)
        {
            MethodReference mref = new MethodReference(method.Name, method.ReturnType, type);
            foreach (var par in method.Parameters)
            {
                mref.Parameters.Add(par);
            }
            if (!method.IsStatic)
            {
                mref.HasThis = true;
            }
            return mref;
        }
        internal static MethodReference GetReference(this MethodDefinition method)
        {
            MethodReference mref = new MethodReference(method.Name, method.ReturnType, method.DeclaringType);
            foreach (var par in method.Parameters)
            {
                mref.Parameters.Add(par);
            }
            if (!method.IsStatic)
            {
                mref.HasThis = true;
            }
            return mref;
        }
        internal static MethodReference GetReference(this MethodDefinition method, GenericInstanceType type, ModuleDefinition inModule)
        {
            MethodReference mref = inModule.ImportReference(method);
            mref.DeclaringType = inModule.ImportReference(type);
            if (!method.IsStatic)
            {
                mref.HasThis = true;
            }
            return mref;
        }
        internal static MethodReference GetReference(this MethodDefinition method, ModuleDefinition inModule)
        {
            MethodReference mref = inModule.ImportReference(method);
            if (!method.IsStatic)
            {
                mref.HasThis = true;
            }
            return mref;
        }
        internal static List<MethodDefinition> GetMethods(this TypeDefinition type, string name)
        {
            List<MethodDefinition> list = new List<MethodDefinition>();
            foreach (var method in type.Methods)
            {
                if (method.Name == name)
                {
                    list.Add(method);
                }
            }
            return list;
        }
        internal static MethodDefinition GetMethod(this TypeDefinition type, string name)
        {
            var methods = GetMethods(type, name);
            if (methods.Count > 0)
            {
                return methods[0];
            }
            return null;
        }
        internal static MethodDefinition GetMethod(this TypeDefinition type, string name, int paramCnt)
        {
            foreach (var method in type.Methods)
            {
                if (method.Name == name && method.Parameters.Count == paramCnt)
                {
                    return method;
                }
            }
            return null;
        }
        internal static MethodDefinition GetMethod(this TypeDefinition type, string name, params TypeReference[] pars)
        {
            pars = pars ?? new TypeReference[0];
            foreach (var method in type.Methods)
            {
                if (method.Name == name)
                {
                    if (method.Parameters.Count == pars.Length)
                    {
                        bool match = true;
                        for (int i = 0; i < pars.Length; ++i)
                        {
                            if (pars[i] != method.Parameters[i].ParameterType)
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            return method;
                        }
                    }
                }
            }
            return null;
        }
        internal static FieldDefinition GetField(this TypeDefinition type, string name)
        {
            foreach (var field in type.Fields)
            {
                if (field.Name == name)
                {
                    return field;
                }
            }
            return null;
        }
        internal static PropertyDefinition GetProperty(this TypeDefinition type, string name)
        {
            foreach (var prop in type.Properties)
            {
                if (prop.Name == name)
                {
                    return prop;
                }
            }
            return null;
        }
        internal static TypeDefinition GetNestedType(this TypeDefinition type, string name)
        {
            foreach (var ntype in type.NestedTypes)
            {
                if (ntype.Name == name)
                {
                    return ntype;
                }
            }
            return null;
        }
        internal static void AddRange<T>(this Mono.Collections.Generic.Collection<T> collection, IEnumerable<T> values)
        {
            foreach (var val in values)
            {
                collection.Add(val);
            }
        }
        internal static void AddRange<T>(this Mono.Collections.Generic.Collection<T> collection, params T[] values)
        {
            AddRange(collection, (IEnumerable<T>)values);
        }
        internal static void InsertRange<T>(this Mono.Collections.Generic.Collection<T> collection, int index, IEnumerable<T> values)
        {
            foreach (var val in values)
            {
                collection.Insert(index++, val);
            }
        }
        internal static void InsertRange<T>(this Mono.Collections.Generic.Collection<T> collection, int index, params T[] values)
        {
            InsertRange(collection, index, (IEnumerable<T>)values);
        }
    }

    class Program
    {
        enum DelegateCategory
        {
            Ignore = 0,
            Pointer,
            Func,
        }

        static void Main(string[] args)
        {
            var baseDir = AppContext.BaseDirectory;
            var root = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, "../../../../../"));
            var srcDll = System.IO.Path.Combine(baseDir, "PointerDelegate.dll");
            var tar = System.IO.Path.Combine(root, "PointerDelegate.dll");

            var asm = AssemblyDefinition.ReadAssembly(srcDll);
            var module = asm.MainModule;

            var baseType = module.GetType("Mod.LowLevel.FreeInvokable");
            var retcField = baseType.GetField("_ReturnCategory");
            var getRefParamFlagRef = module.ImportReference(baseType.GetMethod("GetRefParamFlag"));

            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;

                var category = GetCategory(type.Name);

                if (category == DelegateCategory.Pointer)
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.Name != "Invoke") continue;

                        bool isGenericMethod = method.GenericParameters.Count > 0;

                        if (isGenericMethod)
                        {
                            InjectPointerFuncGenericInvoke(method, type, retcField, module, getRefParamFlagRef);
                        }
                        else
                        {
                            InjectPointerFuncNonGenericInvoke(method, type, retcField, module);
                        }
                        RemoveNop(method);
                    }
                }
                else if (category == DelegateCategory.Func)
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.Name != "Invoke") continue;

                        bool isGenericMethod = method.GenericParameters.Count > 0;

                        if (isGenericMethod)
                        {
                            InjectFreeFuncGenericInvoke(method, type, retcField, module, getRefParamFlagRef);
                            RemoveNop(method);
                        }
                    }
                }
            }

            InjectFakeConvert(module);

            asm.Write(tar);
            asm.Dispose();

            Console.WriteLine("Injection completed: " + tar);
        }

        static void RemoveNop(MethodDefinition method)
        {
            var body = method.Body;
            var instructions = body.Instructions;

            var nopMap = new Dictionary<Instruction, Instruction>();
            for (int i = 0; i < instructions.Count; i++)
            {
                var inst = instructions[i];
                if (inst.OpCode != OpCodes.Nop) continue;
                int j = i + 1;
                while (j < instructions.Count && instructions[j].OpCode == OpCodes.Nop)
                    j++;
                if (j < instructions.Count)
                    nopMap[inst] = instructions[j];
            }

            foreach (var inst in instructions)
            {
                if (inst.Operand is Instruction target && nopMap.TryGetValue(target, out var newTarget))
                    inst.Operand = newTarget;
            }

            for (int i = instructions.Count - 1; i >= 0; i--)
            {
                if (instructions[i].OpCode == OpCodes.Nop)
                    instructions.RemoveAt(i);
            }
        }

        static DelegateCategory GetCategory(string typeName)
        {
            if (typeName.StartsWith("PointerFunc")) return DelegateCategory.Pointer;
            if (typeName.StartsWith("FreeFunc")) return DelegateCategory.Func;
            return DelegateCategory.Ignore;
        }

        static TypeReference GetCallSiteReturnType(TypeDefinition type)
        {
            return type.GenericParameters[0];
        }

        static List<TypeReference> GetCallSiteParams(TypeDefinition type)
        {
            int startIndex = 1;
            var result = new List<TypeReference>();
            for (int i = startIndex; i < type.GenericParameters.Count; i++)
            {
                result.Add(type.GenericParameters[i]);
            }
            return result;
        }

        static TypeReference GetFuncTypeFromCtor(TypeDefinition type)
        {
            foreach (var ctor in type.Methods)
            {
                if (!ctor.IsConstructor || ctor.Parameters.Count == 0) continue;
                var paramType = ctor.Parameters[0].ParameterType;
                var name = paramType.FullName;
                if (name.StartsWith("System.Func"))
                    return paramType;
            }
            return null;
        }
        static TypeReference GetActionTypeFromCtor(TypeDefinition type)
        {
            foreach (var ctor in type.Methods)
            {
                if (!ctor.IsConstructor || ctor.Parameters.Count == 0) continue;
                var paramType = ctor.Parameters[0].ParameterType;
                var name = paramType.FullName;
                if (name.StartsWith("System.Action"))
                    return paramType;
            }
            return null;
        }

        static MethodReference CreateFuncInvokeRef(TypeDefinition type, ModuleDefinition module)
        {
            var delTypeRef = GetFuncTypeFromCtor(type);
            if (delTypeRef is GenericInstanceType git)
            {
                var delTypeDef = git.ElementType.Resolve();
                var invokeMethod = delTypeDef.GetMethod("Invoke");
                var invokeRef = new MethodReference("Invoke", invokeMethod.ReturnType, delTypeRef);
                invokeRef.HasThis = invokeMethod.HasThis;
                foreach (var p in invokeMethod.Parameters)
                    invokeRef.Parameters.Add(new ParameterDefinition(p.ParameterType));
                return invokeRef;
            }
            else
            {
                var delTypeDef = delTypeRef.Resolve();
                var invokeMethod = delTypeDef.GetMethod("Invoke");
                var invokeRef = new MethodReference("Invoke", invokeMethod.ReturnType, delTypeRef);
                invokeRef.HasThis = invokeMethod.HasThis;
                foreach (var p in invokeMethod.Parameters)
                    invokeRef.Parameters.Add(new ParameterDefinition(p.ParameterType));
                return invokeRef;
            }
        }
        static MethodReference CreateActionInvokeRef(TypeDefinition type, ModuleDefinition module)
        {
            var delTypeRef = GetActionTypeFromCtor(type);
            if (delTypeRef is GenericInstanceType git)
            {
                var delTypeDef = git.ElementType.Resolve();
                var invokeMethod = delTypeDef.GetMethod("Invoke");
                var invokeRef = new MethodReference("Invoke", invokeMethod.ReturnType, delTypeRef);
                invokeRef.HasThis = invokeMethod.HasThis;
                foreach (var p in invokeMethod.Parameters)
                    invokeRef.Parameters.Add(new ParameterDefinition(p.ParameterType));
                return invokeRef;
            }
            else
            {
                var delTypeDef = delTypeRef.Resolve();
                var invokeMethod = delTypeDef.GetMethod("Invoke");
                var invokeRef = new MethodReference("Invoke", invokeMethod.ReturnType, delTypeRef);
                invokeRef.HasThis = invokeMethod.HasThis;
                foreach (var p in invokeMethod.Parameters)
                    invokeRef.Parameters.Add(new ParameterDefinition(p.ParameterType));
                return invokeRef;
            }
        }

        static void InjectPointerFuncNonGenericInvoke(MethodDefinition method, TypeDefinition type, FieldDefinition retcField, ModuleDefinition module)
        {
            var pfnField = type.GetField("_Pfn");

            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();
            var emitter = method.Body.GetILProcessor();

            var returnType = GetCallSiteReturnType(type);
            VariableDefinition retValLocal = new VariableDefinition(returnType);
            method.Body.Variables.Add(retValLocal);

            var callvoidfnLabel = emitter.Create(OpCodes.Nop);

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, retcField);
            emitter.Emit(OpCodes.Brfalse, callvoidfnLabel);

            var callSite = new CallSite(returnType);
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);
            emitter.Emit(OpCodes.Ret);

            emitter.Append(callvoidfnLabel);
            var callSiteVoid = new CallSite(module.TypeSystem.Void);
            callSiteVoid.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type))
            {
                callSiteVoid.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSiteVoid);
            emitter.Emit(OpCodes.Ldloca, retValLocal);
            emitter.Emit(OpCodes.Initobj, returnType);
            emitter.Emit(OpCodes.Ldloc, retValLocal);
            emitter.Emit(OpCodes.Ret);
        }

        static void InjectPointerFuncGenericInvoke(MethodDefinition method, TypeDefinition type, FieldDefinition retcField, ModuleDefinition module, MethodReference getRefParamFlagRef)
        {
            var pfnField = type.GetField("_Pfn");

            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var emitter = method.Body.GetILProcessor();
            var funcTypeRef = GetFuncTypeFromCtor(type);

            int paramCount = method.Parameters.Count;

            var refcatenLocals = new VariableDefinition[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                refcatenLocals[i] = new VariableDefinition(module.TypeSystem.Boolean);
                method.Body.Variables.Add(refcatenLocals[i]);
            }

            var returnType = GetCallSiteReturnType(type);
            VariableDefinition retValLocal = new VariableDefinition(returnType);
            method.Body.Variables.Add(retValLocal);

            for (int i = 0; i < paramCount; i++)
            {
                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Ldc_I4, i);
                emitter.Emit(OpCodes.Call, getRefParamFlagRef);
                emitter.Emit(OpCodes.Stloc, refcatenLocals[i]);
            }

            var callvoidfnLabel = emitter.Create(OpCodes.Nop);

            for (int i = 0; i < paramCount; i++)
            {
                var ux = type.GenericParameters[i + 1];
                var category0Label = emitter.Create(OpCodes.Nop);
                var doneLabel = emitter.Create(OpCodes.Nop);

                emitter.Emit(OpCodes.Ldloc, refcatenLocals[i]);
                emitter.Emit(OpCodes.Brfalse, category0Label);

                emitter.Emit(OpCodes.Ldarga, method.Parameters[i]);
                emitter.Emit(OpCodes.Ldind_Ref);
                emitter.Emit(OpCodes.Br, doneLabel);

                emitter.Append(category0Label);
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
                emitter.Emit(OpCodes.Ldobj, ux);

                emitter.Append(doneLabel);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, retcField);
            emitter.Emit(OpCodes.Brfalse, callvoidfnLabel);

            var callSite = new CallSite(returnType);
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);
            emitter.Emit(OpCodes.Ret);

            emitter.Append(callvoidfnLabel);
            var callSiteVoid = new CallSite(module.TypeSystem.Void);
            callSiteVoid.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type))
            {
                callSiteVoid.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSiteVoid);
            emitter.Emit(OpCodes.Ldloca, retValLocal);
            emitter.Emit(OpCodes.Initobj, returnType);
            emitter.Emit(OpCodes.Ldloc, retValLocal);
            emitter.Emit(OpCodes.Ret);
        }

        static void InjectFreeFuncGenericInvoke(MethodDefinition method, TypeDefinition type, FieldDefinition retField, ModuleDefinition module, MethodReference getRefParamFlagRef)
        {
            var delField = type.GetField("_Del");

            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var emitter = method.Body.GetILProcessor();
            var funcTypeRef = GetFuncTypeFromCtor(type);

            int paramCount = method.Parameters.Count;

            var refcatenLocals = new VariableDefinition[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                refcatenLocals[i] = new VariableDefinition(module.TypeSystem.Boolean);
                method.Body.Variables.Add(refcatenLocals[i]);
            }

            var returnType = GetCallSiteReturnType(type);

            for (int i = 0; i < paramCount; i++)
            {
                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Ldc_I4, i);
                emitter.Emit(OpCodes.Call, getRefParamFlagRef);
                emitter.Emit(OpCodes.Stloc, refcatenLocals[i]);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, delField);

            for (int i = 0; i < paramCount; i++)
            {
                var ux = type.GenericParameters[i + 1];
                var category0Label = emitter.Create(OpCodes.Nop);
                var doneLabel = emitter.Create(OpCodes.Nop);

                emitter.Emit(OpCodes.Ldloc, refcatenLocals[i]);
                emitter.Emit(OpCodes.Brfalse, category0Label);

                emitter.Emit(OpCodes.Ldarga, method.Parameters[i]);
                emitter.Emit(OpCodes.Ldind_Ref);
                emitter.Emit(OpCodes.Br, doneLabel);

                emitter.Append(category0Label);
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
                emitter.Emit(OpCodes.Ldobj, ux);

                emitter.Append(doneLabel);
            }

            emitter.Emit(OpCodes.Callvirt, CreateFuncInvokeRef(type, module));
            emitter.Emit(OpCodes.Ret);
        }

        static void InjectFakeConvert(ModuleDefinition module)
        {
            var type = module.GetType("Mod.LowLevel.PointerDelegateExtensions");
            var mtoref = type.GetMethod("ToRef");
            mtoref.Body.Variables.Clear();
            mtoref.Body.Instructions.Clear();
            {
                var vrv = new VariableDefinition(mtoref.ReturnType);
                mtoref.Body.Variables.Add(vrv);
                var emitter = mtoref.Body.GetILProcessor();
                emitter.Emit(OpCodes.Ldloca, 0);
                emitter.Emit(OpCodes.Ldarga, 0);
                emitter.Emit(OpCodes.Ldind_Ref);
                emitter.Emit(OpCodes.Stind_Ref);
                emitter.Emit(OpCodes.Ldloc_0);
                emitter.Emit(OpCodes.Ret);
            }
            var mtofake = type.GetMethod("ToFakeRefObj");
            var faketype = module.GetType("Mod.LowLevel.ByRefParam");
            mtofake.Body.Instructions.Clear();
            mtofake.Body.Variables.Clear();
            {
                var vrv = new VariableDefinition(faketype);
                mtofake.Body.Variables.Add(vrv);
                var emitter = mtofake.Body.GetILProcessor();
                emitter.Emit(OpCodes.Ldloca, 0);
                emitter.Emit(OpCodes.Ldarga, 0);
                emitter.Emit(OpCodes.Ldind_Ref);
                emitter.Emit(OpCodes.Stind_Ref);
                emitter.Emit(OpCodes.Ldloc_0);
                emitter.Emit(OpCodes.Ret);
            }
        }
    }
}
