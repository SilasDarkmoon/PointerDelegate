using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        }

        static void Main(string[] args)
        {
            var baseDir = AppContext.BaseDirectory;
            var root = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, "../../../../../"));
            var srcDll = System.IO.Path.Combine(baseDir, "PointerDelegate.dll");
            var tar = System.IO.Path.Combine(root, "PointerDelegate.dll");

            var asm = AssemblyDefinition.ReadAssembly(srcDll);
            var module = asm.MainModule;

            var baseType = module.GetType("Mod.LowLevel.FreeInvokableBase");
            var retcField = baseType.GetField("_ReturnCategory");

            // Bisect switch for ILC debugging: all | pf | prim | none.
            var mode = Environment.GetEnvironmentVariable("PD_INJ_MODE") ?? "all";

            if (mode == "all" || mode == "pf")
            {
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;

                var category = GetCategory(type.Name);

                if (category == DelegateCategory.Pointer)
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.Name != "Invoke") continue;

                        // New-style generic Invoke returns 'ref R' (ReturnType is a byref) —
                        // now a plain-C# trampoline calling the runtime-Emit DynamicInvoker;
                        // do NOT inject it. Only inject the old-style non-generic Invoke
                        // (plain R return, calli to Pfn).
                        if (method.ReturnType.IsByReference) continue;

                        InjectPointerFuncNonGenericInvoke(method, type, retcField, module);
                        RemoveNop(method);
                    }
                }
            }
            }

            // ConvertAddressToRef<T>(IntPtr) -> ref T and ConvertRefToAddress<T>(in T) -> IntPtr
            // are identity passthroughs: ldarg.0; ret.
            if (mode == "all" || mode == "prim")
            {
            var convertAddressToRef = baseType.Methods.FirstOrDefault(m =>
                m.Name == "ConvertAddressToRef" && m.Parameters.Count == 1
                && m.Parameters[0].ParameterType.MetadataType == MetadataType.IntPtr
                && m.ReturnType.IsByReference);
            if (convertAddressToRef != null)
            {
                InjectIdentityPassthrough(convertAddressToRef);
            }
            var convertRefToAddress = baseType.Methods.FirstOrDefault(m =>
                m.Name == "ConvertRefToAddress" && m.Parameters.Count == 1
                && m.Parameters[0].ParameterType.IsByReference
                && m.ReturnType.MetadataType == MetadataType.IntPtr);
            if (convertRefToAddress != null)
            {
                InjectIdentityPassthrough(convertRefToAddress);
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

        static DelegateCategory GetCategory(string typeName)
        {
            if (typeName.StartsWith("PointerFunc")) return DelegateCategory.Pointer;
            // FreeFunc/FreeAction are now pure runtime-Emit (FreeFuncEmit) — no injection.
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

        static void InjectPointerFuncNonGenericInvoke(MethodDefinition method, TypeDefinition type, FieldDefinition retcField, ModuleDefinition module)
        {
            var pfnField = type.GetField("_Pfn");
            var unmanagedField = type.GetField("_IsUnmanaged");

            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();
            var emitter = method.Body.GetILProcessor();

            var returnType = GetCallSiteReturnType(type);
            VariableDefinition retValLocal = new VariableDefinition(returnType);
            method.Body.Variables.Add(retValLocal);

            var unmanagedLabel = emitter.Create(OpCodes.Nop);
            var callvoidfnLabel = emitter.Create(OpCodes.Nop);
            var callvoidfnLabelU = emitter.Create(OpCodes.Nop);

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
            }

            emitter.Emit(OpCodes.Ldarg_0);
            // ldfld must reference Pfn through the OPEN generic instantiation
            // PointerFunc<!0, !1..!n> (TypeSpec + MemberRef) — the shape Roslyn
            // always emits for self-referencing fields in generic classes.
            // A direct FieldDef reference combined with calli trips an ILC
            // (NativeAOT) bug: "Failed to load type" → throw stub. CoreCLR's
            // JIT accepts both shapes; ILC only tolerates the Roslyn shape.
            var openSelf = new GenericInstanceType(type);
            foreach (var gp in type.GenericParameters)
                openSelf.GenericArguments.Add(gp);
            var pfnRef = new FieldReference(pfnField.Name, pfnField.FieldType, openSelf);
            emitter.Emit(OpCodes.Ldfld, pfnRef);

            // Runtime branch on _IsUnmanaged: true → unmanaged calli (0x09, same as
            // delegate* unmanaged), false → managed calli (0x00). brtrue pops the
            // bool; the [params, pfn] stack below is shared by both paths.
            var unmanagedRef = new FieldReference(unmanagedField.Name, unmanagedField.FieldType, openSelf);
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, unmanagedRef);
            emitter.Emit(OpCodes.Brtrue, unmanagedLabel);

            // === managed path ===
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

            // === unmanaged path ===
            emitter.Append(unmanagedLabel);
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, retcField);
            emitter.Emit(OpCodes.Brfalse, callvoidfnLabelU);

            var callSiteU = new CallSite(returnType);
            // Cecil 0.10's MethodCallingConvention lacks an Unmanaged member; 0x09 is
            // IMAGE_CEE_CS_CALLCONV_UNMANAGED (verified against delegate* unmanaged output).
            callSiteU.CallingConvention = (MethodCallingConvention)9;
            foreach (var p in GetCallSiteParams(type))
            {
                callSiteU.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSiteU);
            emitter.Emit(OpCodes.Ret);

            emitter.Append(callvoidfnLabelU);
            var callSiteVoidU = new CallSite(module.TypeSystem.Void);
            callSiteVoidU.CallingConvention = (MethodCallingConvention)9;
            foreach (var p in GetCallSiteParams(type))
            {
                callSiteVoidU.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSiteVoidU);
            emitter.Emit(OpCodes.Ldloca, retValLocal);
            emitter.Emit(OpCodes.Initobj, returnType);
            emitter.Emit(OpCodes.Ldloc, retValLocal);
            emitter.Emit(OpCodes.Ret);
        }

        // Identity passthrough: ldarg.0; ret — used for ConvertAddressToRef<T>(IntPtr) -> ref T
        // and ConvertRefToAddress<T>(in T) -> IntPtr (the byref parameter is already the address).
        static void InjectIdentityPassthrough(MethodDefinition method)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();
            var emitter = method.Body.GetILProcessor();
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ret);
        }
    }
}
