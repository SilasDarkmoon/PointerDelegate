using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;

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
            Action,
            Func,
            RefFunc
        }

        static void Main(string[] args)
        {
            var baseDir = AppContext.BaseDirectory;
            var root = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, "../../../../../"));
            var srcDll = System.IO.Path.Combine(baseDir, "PointerDelegate.dll");
            var tar = System.IO.Path.Combine(root, "PointerDelegate.dll");

            var asm = AssemblyDefinition.ReadAssembly(srcDll);
            var module = asm.MainModule;

            var baseType = module.GetType("Mod.LowLevel.PointerDelegate");
            var pfnField = baseType.GetField("_Pfn");
            var delField = baseType.GetField("_Del");
            var getRefParamCategoryRef = module.ImportReference(baseType.GetMethod("GetRefParamCategory"));

            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;

                var category = GetCategory(type.Name);
                if (category == null) continue;

                foreach (var method in type.Methods)
                {
                    if (method.Name != "Invoke") continue;

                    bool isGenericMethod = method.GenericParameters.Count > 0;

                    if (isGenericMethod)
                        InjectGenericInvoke(method, type, category.Value, pfnField, delField, module, getRefParamCategoryRef);
                    else
                        InjectNonGenericInvoke(method, type, category.Value, pfnField, delField, module);
                }
            }

            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;
                if (GetCategory(type.Name) == null) continue;

                foreach (var method in type.Methods)
                {
                    if (method.Name != "Invoke") continue;
                    RemoveNop(method);
                }
            }

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

        static DelegateCategory? GetCategory(string typeName)
        {
            if (typeName.StartsWith("PointerRefFuncInvoker")) return null;
            if (typeName.StartsWith("PointerFuncInvoker")) return null;
            if (typeName.StartsWith("PointerActionInvoker")) return null;
            if (typeName.StartsWith("PointerRefFunc")) return DelegateCategory.RefFunc;
            if (typeName.StartsWith("PointerFunc")) return DelegateCategory.Func;
            if (typeName.StartsWith("PointerAction")) return DelegateCategory.Action;
            return null;
        }

        static TypeReference GetCallSiteReturnType(TypeDefinition type, DelegateCategory category)
        {
            switch (category)
            {
                case DelegateCategory.Action:
                    return type.Module.TypeSystem.Void;
                case DelegateCategory.Func:
                    return type.GenericParameters[0];
                case DelegateCategory.RefFunc:
                    return new ByReferenceType(type.GenericParameters[0]);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        static List<TypeReference> GetCallSiteParams(TypeDefinition type, DelegateCategory category)
        {
            int startIndex = category == DelegateCategory.Action ? 0 : 1;
            var result = new List<TypeReference>();
            for (int i = startIndex; i < type.GenericParameters.Count; i++)
            {
                result.Add(type.GenericParameters[i]);
            }
            return result;
        }

        static TypeReference GetDelegateTypeFromCtor(TypeDefinition type)
        {
            foreach (var ctor in type.Methods)
            {
                if (!ctor.IsConstructor || ctor.Parameters.Count == 0) continue;
                var paramType = ctor.Parameters[0].ParameterType;
                var name = paramType.FullName;
                if (name.StartsWith("System.Action") || name.StartsWith("System.Func") || name.StartsWith("Mod.LowLevel.RefFunc"))
                    return paramType;
            }
            return null;
        }

        static MethodReference CreateDelegateInvokeRef(TypeDefinition type, DelegateCategory category, ModuleDefinition module)
        {
            var delTypeRef = GetDelegateTypeFromCtor(type);
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

        static void InjectNonGenericInvoke(MethodDefinition method, TypeDefinition type,
            DelegateCategory category, FieldDefinition pfnField, FieldDefinition delField, ModuleDefinition module)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var emitter = method.Body.GetILProcessor();
            var delTypeRef = GetDelegateTypeFromCtor(type);
            bool hasReturn = category != DelegateCategory.Action;

            VariableDefinition retValLocal = null;
            if (hasReturn)
            {
                retValLocal = new VariableDefinition(GetCallSiteReturnType(type, category));
                method.Body.Variables.Add(retValLocal);
            }

            var loadDelegateLabel = emitter.Create(OpCodes.Nop);
            var afterObjLabel = emitter.Create(OpCodes.Nop);
            var callvirtLabel = emitter.Create(OpCodes.Nop);

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);
            emitter.Emit(OpCodes.Brfalse, loadDelegateLabel);
            emitter.Emit(OpCodes.Ldnull);
            emitter.Emit(OpCodes.Br, afterObjLabel);

            emitter.Append(loadDelegateLabel);
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, delField);
            emitter.Emit(OpCodes.Castclass, delTypeRef);

            emitter.Append(afterObjLabel);

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);
            emitter.Emit(OpCodes.Brfalse, callvirtLabel);

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);

            var callSite = new CallSite(GetCallSiteReturnType(type, category));
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type, category))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);
            if (hasReturn)
            {
                emitter.Emit(OpCodes.Stloc, retValLocal);
                emitter.Emit(OpCodes.Pop);
                emitter.Emit(OpCodes.Ldloc, retValLocal);
            }
            else
            {
                emitter.Emit(OpCodes.Pop);
            }
            emitter.Emit(OpCodes.Ret);

            emitter.Append(callvirtLabel);
            emitter.Emit(OpCodes.Callvirt, CreateDelegateInvokeRef(type, category, module));
            emitter.Emit(OpCodes.Ret);
        }

        static void InjectGenericInvoke(MethodDefinition method, TypeDefinition type,
            DelegateCategory category, FieldDefinition pfnField, FieldDefinition delField,
            ModuleDefinition module, MethodReference getRefParamCategoryRef)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var emitter = method.Body.GetILProcessor();
            var delTypeRef = GetDelegateTypeFromCtor(type);
            bool hasReturn = category != DelegateCategory.Action;

            int startIndex = category == DelegateCategory.Action ? 0 : 1;
            int paramCount = method.Parameters.Count;

            var refcatenLocals = new VariableDefinition[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                refcatenLocals[i] = new VariableDefinition(module.TypeSystem.Int32);
                method.Body.Variables.Add(refcatenLocals[i]);
            }

            VariableDefinition retValLocal = null;
            if (hasReturn)
            {
                retValLocal = new VariableDefinition(GetCallSiteReturnType(type, category));
                method.Body.Variables.Add(retValLocal);
            }

            for (int i = 0; i < paramCount; i++)
            {
                var ux = type.GenericParameters[startIndex + i];
                var px = method.GenericParameters[i];

                var gim = new GenericInstanceMethod(getRefParamCategoryRef);
                gim.GenericArguments.Add(ux);
                gim.GenericArguments.Add(px);

                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Ldc_I4, i);
                emitter.Emit(OpCodes.Call, gim);
                emitter.Emit(OpCodes.Stloc, refcatenLocals[i]);
            }

            var loadDelegateLabel = emitter.Create(OpCodes.Nop);
            var afterObjLabel = emitter.Create(OpCodes.Nop);
            var callvirtLabel = emitter.Create(OpCodes.Nop);

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);
            emitter.Emit(OpCodes.Brfalse, loadDelegateLabel);
            emitter.Emit(OpCodes.Ldnull);
            emitter.Emit(OpCodes.Br, afterObjLabel);

            emitter.Append(loadDelegateLabel);
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, delField);
            emitter.Emit(OpCodes.Castclass, delTypeRef);

            emitter.Append(afterObjLabel);

            for (int i = 0; i < paramCount; i++)
            {
                var ux = type.GenericParameters[startIndex + i];
                var category0Label = emitter.Create(OpCodes.Nop);
                var category1Label = emitter.Create(OpCodes.Nop);
                var doneLabel = emitter.Create(OpCodes.Nop);

                emitter.Emit(OpCodes.Ldloc, refcatenLocals[i]);
                emitter.Emit(OpCodes.Brfalse, category0Label);

                emitter.Emit(OpCodes.Ldloc, refcatenLocals[i]);
                emitter.Emit(OpCodes.Ldc_I4_1);
                emitter.Emit(OpCodes.Beq, category1Label);

                emitter.Emit(OpCodes.Ldarga, method.Parameters[i]);
                emitter.Emit(OpCodes.Ldind_Ref);
                emitter.Emit(OpCodes.Br, doneLabel);

                emitter.Append(category1Label);
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
                emitter.Emit(OpCodes.Br, doneLabel);

                emitter.Append(category0Label);
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
                emitter.Emit(OpCodes.Ldobj, ux);

                emitter.Append(doneLabel);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);
            emitter.Emit(OpCodes.Brfalse, callvirtLabel);

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);

            var callSite = new CallSite(GetCallSiteReturnType(type, category));
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type, category))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);
            if (hasReturn)
            {
                emitter.Emit(OpCodes.Stloc, retValLocal);
                emitter.Emit(OpCodes.Pop);
                emitter.Emit(OpCodes.Ldloc, retValLocal);
            }
            else
            {
                emitter.Emit(OpCodes.Pop);
            }
            emitter.Emit(OpCodes.Ret);

            emitter.Append(callvirtLabel);
            emitter.Emit(OpCodes.Callvirt, CreateDelegateInvokeRef(type, category, module));
            emitter.Emit(OpCodes.Ret);
        }
    }
}
