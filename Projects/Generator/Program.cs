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

            var refParamType = module.GetType("Mod.LowLevel.RefParam");
            var baseType = module.GetType("Mod.LowLevel.PointerDelegate");
            var pfnField = baseType.GetField("_Pfn");
            var isRefParamMethod = baseType.GetMethod("IsRefParam").GetReference(module);

            var getTypeFromHandle = module.ImportReference(
                typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }));

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
                        InjectGenericInvoke(method, type, category.Value, pfnField, module, getTypeFromHandle, isRefParamMethod);
                    else
                        InjectNonGenericInvoke(method, type, category.Value, pfnField, module);
                }
            }

            asm.Write(tar);
            asm.Dispose();

            Console.WriteLine("Injection completed: " + tar);
        }

        static DelegateCategory? GetCategory(string typeName)
        {
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

        static void InjectNonGenericInvoke(MethodDefinition method, TypeDefinition type,
            DelegateCategory category, FieldDefinition pfnField, ModuleDefinition module)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var emitter = method.Body.GetILProcessor();

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);

            var callSite = new CallSite(GetCallSiteReturnType(type, category));
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type, category))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);

            emitter.Emit(OpCodes.Ret);
        }

        static void InjectGenericInvoke(MethodDefinition method, TypeDefinition type,
            DelegateCategory category, FieldDefinition pfnField,
            ModuleDefinition module, MethodReference getTypeFromHandle, MethodReference isRefParamMethod)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var emitter = method.Body.GetILProcessor();

            int startIndex = category == DelegateCategory.Action ? 0 : 1;

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                var ux = type.GenericParameters[startIndex + i];
                var px = method.GenericParameters[i];

                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);

                var skipLabel = emitter.Create(OpCodes.Nop);

                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Ldc_I4, i);
                emitter.Emit(OpCodes.Ldtoken, ux);
                emitter.Emit(OpCodes.Call, getTypeFromHandle);
                emitter.Emit(OpCodes.Ldtoken, px);
                emitter.Emit(OpCodes.Call, getTypeFromHandle);
                emitter.Emit(OpCodes.Callvirt, isRefParamMethod);
                emitter.Emit(OpCodes.Brtrue, skipLabel);

                emitter.Emit(OpCodes.Ldobj, px);

                emitter.Append(skipLabel);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, pfnField);

            var callSite = new CallSite(GetCallSiteReturnType(type, category));
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type, category))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);

            emitter.Emit(OpCodes.Ret);
        }
    }
}
