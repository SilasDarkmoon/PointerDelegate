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

            // Bisect switch for ILC debugging: all | pf | prim | lref | none | unmonly.
            var mode = Environment.GetEnvironmentVariable("PD_INJ_MODE") ?? "all";
            // InvokeRefReturn arity cap for ILC-crash bisection (PD_INJ_MAXU=1 → only U1).
            var maxU = int.TryParse(Environment.GetEnvironmentVariable("PD_INJ_MAXU"), out var mu) ? mu : int.MaxValue;
            // unmonly (2026-09-28 experiment): InvokePlain woven with ONLY the unmanaged
            // calli (no _IsUnmanaged branch, no managed calli sites). Tests whether the
            // mixed-convention branching is what makes IL2CPP translate the unmanaged
            // calli as a RuntimeMethod* dereference in shared-generic bodies.
            var unmonly = mode == "unmonly";

            if (mode == "all" || mode == "pf" || unmonly)
            {
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;

                var category = GetCategory(type.Name);

                if (category == DelegateCategory.Pointer)
                {
                    foreach (var method in type.Methods)
                    {
                        // The non-generic Invoke is now a plain-C# trampoline (runtime-Emit
                        // PlainInvoker first, InvokePlain fallback). InvokePlain — plain R
                        // return, calli to Pfn — is the only woven method left in PointerFunc.
                        if (method.Name == "InvokePlain")
                        {

                            InjectPointerFuncNonGenericInvoke(method, type, retcField, module, unmonly);
                            RemoveNop(method);
                        }
                        // InvokeRefReturn — ref R return, managed calli with a ref-R
                        // callsite return, params pushed as-is (in Ux byref bits) —
                        // the GC-safe ref-return path for InvokeFallback (no IntPtr
                        // transit, unlike ConvertAddressToRef).
                        else if (method.Name == "InvokeRefReturn")
                        {
                            if (type.GenericParameters.Count - 1 > maxU) continue;
                            InjectPointerFuncInvokeRefReturn(method, type, module);
                            RemoveNop(method);
                        }
                    }
                }
            }
            }

            //// ConvertAddressToRef<T>(IntPtr) -> ref T and ConvertRefToAddress<T>(in T) -> IntPtr
            //// are identity passthroughs: ldarg.0; ret.
            //if (mode == "all" || mode == "prim")
            //{
            //var convertAddressToRef = baseType.Methods.FirstOrDefault(m =>
            //    m.Name == "ConvertAddressToRef" && m.Parameters.Count == 1
            //    && m.Parameters[0].ParameterType.MetadataType == MetadataType.IntPtr
            //    && m.ReturnType.IsByReference);
            //if (convertAddressToRef != null)
            //{
            //    InjectIdentityPassthrough(convertAddressToRef);
            //}
            //var convertRefToAddress = baseType.Methods.FirstOrDefault(m =>
            //    m.Name == "ConvertRefToAddress" && m.Parameters.Count == 1
            //    && m.Parameters[0].ParameterType.IsByReference
            //    && m.ReturnType.MetadataType == MetadataType.IntPtr);
            //if (convertRefToAddress != null)
            //{
            //    InjectIdentityPassthrough(convertRefToAddress);
            //}
            //}

            // Here to inject ConvertRef<F, T>
            var convertRef = baseType.Methods.Single(m =>
                m.Name == "ConvertRef" && m.GenericParameters.Count == 2
                && m.Parameters.Count == 1);
            InjectIdentityPassthrough(convertRef);

            // InvokeFallback full rebuild (idempotent: the body is cleared and re-emitted,
            // so re-running the Generator on an already-woven dll is safe). The per-parameter
            // ConvertParam chain is replaced by a three-branch sequence keyed on typeof(Ux):
            //   ByRefParam     → ldarg pX into a PINNED local (pins the byref target for the
            //                    whole call window), then snapshot the address into ux — the
            //                    pinned slot is reported by GCInfo, so ux._Address stays
            //                    valid across every safepoint until the wrapper returns;
            //   ByRefLocalSlot → ldarga pX (this method's parameter slot address) into ux —
            //                    slot-indirect, the callee derefs the GC-tracked slot;
            //   otherwise      → ldarg pX + ldobj (plain value copy).
            // The _ReturnCategory branch (InvokeRefReturn ref-return path / Invoke value
            // path) and the pin release before every ret are re-emitted as well.
            if (mode == "all" || mode == "lref")
            {
                if (Environment.GetEnvironmentVariable("PD_INJ_LEGACY") == "1")
                {
                    InjectInvokeFallbackByRefLocalSlotFixup(asm);
                }
                else
                {
                    RebuildInvokeFallbackBodies(asm);
                }
            }

            EmitRefSafetyRulesAttribute(asm);
            EmitScopedRefOnInParameters(asm);

            asm.Write(tar);
            asm.Dispose();

            Console.WriteLine("Injection completed: " + tar);
        }

        /// <summary>
        /// Builds a System.<name> type reference in the TARGET module's own reference
        /// context (the assembly providing its object type - netstandard for ns2.0).
        /// Never import from the injector's runtime: typeof(System.Attribute) here is
        /// System.Private.CoreLib's, and that assembly reference makes the produced
        /// dll unloadable on Unity/Mono ("Unable to resolve reference
        /// 'System.Private.CoreLib'").
        /// </summary>
        static TypeReference GetSystemType(ModuleDefinition module, string name)
        {
            var scope = module.TypeSystem.Object.Scope;
            return new TypeReference("System", name, module, scope);
        }
        static MethodReference GetSystemCtor(ModuleDefinition module, TypeReference type, params TypeReference[] pars)
        {
            var ctor = new MethodReference(".ctor", module.TypeSystem.Void, type) { HasThis = true };
            foreach (var p in pars) ctor.Parameters.Add(new ParameterDefinition(p));
            return ctor;
        }

        /// <summary>
        /// Adds [ScopedRef] to every `in` parameter of the generic ref-returning Invoke
        /// methods (ref R Invoke&lt;P1..&gt;(out R r, in P1 p1, ...)). The scoped promise
        /// removes those parameters from the consumer's escape-min entirely, so ANY
        /// argument shape (constants included) can be passed while returning the result
        /// by ref: `return ref pf.Invoke(out r, 41)` compiles. Without it the in
        /// parameter participates in the min and demands a ref-returnable argument.
        /// The metadata shape mirrors what C# 11 emits for `scoped in`: the parameter
        /// carries [In, ScopedRef, IsReadOnly] - In/IsReadOnly are already present on
        /// plain `in`, so only ScopedRef is added here. Idempotent per parameter.
        /// NOTE: Cecil's IsByReference returns false for `P1& modreq(InAttribute)`
        /// (the exact shape C# emits for `in`), so parameters are matched by the
        /// '&' suffix in their type name instead.
        /// </summary>
        static void EmitScopedRefOnInParameters(AssemblyDefinition asm)
        {
            const string ns = "System.Runtime.CompilerServices";
            var module = asm.MainModule;

            // ScopedRefAttribute shim (ns2.0 lacks it)
            var shim = module.GetType($"{ns}.ScopedRefAttribute");
            if (shim == null)
            {
                shim = new TypeDefinition(ns, "ScopedRefAttribute",
                    TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                    GetSystemType(module, "Attribute"));

                // NOTE: no [AttributeUsage] on the shim. The parameterized
                // AttributeUsageAttribute ctor MemberRef built from raw TypeRefs
                // fails to resolve under Mono/Unity reflection ("Method not
                // found: AttributeUsageAttribute..ctor(AttributeTargets)"),
                // which makes Unity's TypeCache declare the whole assembly
                // broken. Roslyn recognizes these compiler-reserved attributes
                // by full name alone; the default AttributeUsage (All) still
                // permits every target we need.

                var ctor = new MethodDefinition(".ctor",
                    MethodAttributes.Public | MethodAttributes.HideBySig |
                    MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                    module.TypeSystem.Void);
                var baseCtor = GetSystemCtor(module, GetSystemType(module, "Attribute"));
                var il = ctor.Body.GetILProcessor();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Call, baseCtor);
                il.Emit(OpCodes.Ret);
                shim.Methods.Add(ctor);
                module.Types.Add(shim);
            }
            var shimCtor = shim.GetMethod(".ctor");

            // annotate every `in` parameter of every ref-returning method
            // (the generic Invoke family AND helpers like ConvertRef<F, T>(in F f))
            int annotated = 0;
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;
                foreach (var method in type.Methods)
                {
                    if (!method.ReturnType.IsByReference) continue;      // ref-returning methods
                    foreach (var p in method.Parameters)
                    {
                        // Cecil quirk: IsByReference is false for `P1& modreq(InAttribute)`
                        bool isByRefLike = p.ParameterType.Name.Contains("&");
                        if (isByRefLike && !p.IsOut)
                        {
                            if (!p.CustomAttributes.Any(a => a.AttributeType.FullName == $"{ns}.ScopedRefAttribute"))
                            {
                                p.CustomAttributes.Add(new CustomAttribute(shimCtor));
                                annotated++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"[ScopedRef] annotated {annotated} in-parameters");
        }

        /// <summary>
        /// Injects [module: RefSafetyRules(version)] so that C# 11+ consumers apply the
        /// updated ref-safety escape rules to this module's APIs (out-parameter passthrough
        /// bridges like PointerFunc's generic Invoke become usable in `return ref`). The
        /// attribute type is compiler-reserved (CS8335) and cannot be attached from source,
        /// so it has to be emitted here at the metadata level. Idempotent: skips if already
        /// present.
        /// </summary>
        static void EmitRefSafetyRulesAttribute(AssemblyDefinition asm, int version = 11)
        {
            const string attrNamespace = "System.Runtime.CompilerServices";
            const string attrFullName = attrNamespace + ".RefSafetyRulesAttribute";
            var module = asm.MainModule;

            // already attached to the module? nothing to do
            foreach (var ca in module.CustomAttributes)
            {
                if (ca.AttributeType.FullName == attrFullName)
                {
                    return;
                }
            }

            // find or create the shim attribute type (old assemblies don't contain it)
            var attrType = module.GetType(attrFullName);
            if (attrType == null)
            {
                attrType = new TypeDefinition(
                    attrNamespace,
                    "RefSafetyRulesAttribute",
                    TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                    GetSystemType(module, "Attribute"));

                // NOTE: no [AttributeUsage] on the shim - see the note in
                // EmitScopedRefOnInParameters for why (MemberRef resolution).

                // public readonly int Version;
                var versionField = new FieldDefinition("Version",
                    FieldAttributes.Public | FieldAttributes.InitOnly,
                    module.TypeSystem.Int32);

                // public RefSafetyRulesAttribute(int version) { Version = version; }
                var ctor = new MethodDefinition(".ctor",
                    MethodAttributes.Public | MethodAttributes.HideBySig |
                    MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                    module.TypeSystem.Void);
                ctor.Parameters.Add(new ParameterDefinition("version", ParameterAttributes.None, module.TypeSystem.Int32));

                // System.Attribute's own constructor is protected - reflect with NonPublic
                var baseCtor = GetSystemCtor(module, GetSystemType(module, "Attribute"));

                var emitter = ctor.Body.GetILProcessor();
                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Call, baseCtor);
                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Ldarg_1);
                emitter.Emit(OpCodes.Stfld, versionField);
                emitter.Emit(OpCodes.Ret);

                attrType.Fields.Add(versionField);
                attrType.Methods.Add(ctor);
                module.Types.Add(attrType);
            }

            // [module: RefSafetyRules(version)]
            var attrCtor = attrType.GetMethod(".ctor");
            var attribute = new CustomAttribute(attrCtor);
            attribute.ConstructorArguments.Add(new CustomAttributeArgument(module.TypeSystem.Int32, version));
            module.CustomAttributes.Add(attribute);
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

        static void InjectPointerFuncNonGenericInvoke(MethodDefinition method, TypeDefinition type, FieldDefinition retcField, ModuleDefinition module, bool unmanagedOnly)
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
            if (!unmanagedOnly)
            {
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
            emitter.Append(unmanagedLabel);
            }

            // === unmanaged path (the only path in unmonly mode) ===
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Ldfld, retcField);
            emitter.Emit(OpCodes.Brfalse, callvoidfnLabelU);

            var callSiteU = new CallSite(returnType);
            // 2026-09-28 fix: use IMAGE_CEE_CS_CALLCONV_C (0x01) — the value Roslyn
            // actually emits for delegate* unmanaged[Cdecl] (verified by Cecil-dumping
            // MCallUnmanagedNoArg in TestAssembly: CallingConvention = 1 (C), no modopt).
            // The previous 0x09 (IMAGE_CEE_CS_CALLCONV_UNMANAGED) made IL2CPP translate
            // the calli as a managed indirect call (RuntimeMethod* dereference) in EVERY
            // body — shared or concrete (unmonly + Faster-runtime experiments proved the
            // translation ignores sharing/branching; only the conv byte matters).
            callSiteU.CallingConvention = MethodCallingConvention.C;
            foreach (var p in GetCallSiteParams(type))
            {
                callSiteU.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSiteU);
            emitter.Emit(OpCodes.Ret);

            emitter.Append(callvoidfnLabelU);
            var callSiteVoidU = new CallSite(module.TypeSystem.Void);
            callSiteVoidU.CallingConvention = MethodCallingConvention.C;
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

        /// <summary>
        /// Injects InvokeRefReturn(U1 u1, ..., Un un) -> ref R for PointerFunc`n:
        /// pushes the VALUE parameters as-is (ldarg — the same push InvokePlain uses;
        /// the u1..un arguments are the ConvertParam-converted values from
        /// InvokeFallback), pushes _Pfn, then a single managed calli whose CALLSITE
        /// RETURN is ref R and whose callsite params are U1..Un (values, matching
        /// the ldarg pushes), and returns the calli result directly — the byref
        /// stays on the eval stack the whole time, GC-tracked, with NO IntPtr
        /// transit (unlike the old ConvertAddressToRef snapshot path). The out r
        /// parameter is intentionally NOT filled here — InvokeFallback fills it
        /// with the snapshot copy after the call.
        /// </summary>
        static void InjectPointerFuncInvokeRefReturn(MethodDefinition method, TypeDefinition type, ModuleDefinition module)
        {
            var pfnField = type.GetField("_Pfn");

            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();
            var emitter = method.Body.GetILProcessor();

            var returnType = GetCallSiteReturnType(type);          // R (generic param 0)
            var retByRef = new ByReferenceType(returnType);      // ref R

            // push params as-is: u1..un (VALUE parameters — ldarg loads the value,
            // the same push InvokePlain uses; the values are the ConvertParam
            // conversions done by InvokeFallback before calling here).
            for (int i = 0; i < method.Parameters.Count; i++)
            {
                emitter.Emit(OpCodes.Ldarg, method.Parameters[i]);
            }

            // push pfn: ldarg.0; ldfld _Pfn through the OPEN generic instantiation
            // (TypeSpec + MemberRef) — the ILC-safe shape (see the note in
            // InjectPointerFuncNonGenericInvoke).
            emitter.Emit(OpCodes.Ldarg_0);
            var openSelf = new GenericInstanceType(type);
            foreach (var gp in type.GenericParameters)
                openSelf.GenericArguments.Add(gp);
            var pfnRef = new FieldReference(pfnField.Name, pfnField.FieldType, openSelf);
            emitter.Emit(OpCodes.Ldfld, pfnRef);

            // calli ref R(U1, ..., Un) — managed convention; callsite params are the
            // VALUE types U1..Un (matching the ldarg pushes, same as InvokePlain's
            // callsite); only the return is byref.
            var callSite = new CallSite(retByRef);
            callSite.CallingConvention = MethodCallingConvention.Default;
            foreach (var p in GetCallSiteParams(type))
            {
                callSite.Parameters.Add(new ParameterDefinition(p));
            }
            emitter.Emit(OpCodes.Calli, callSite);
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

        /// <summary>
        /// Rebuilds every parameterized InvokeFallback(out R r, in U1 p1, ..., in Un pn) body of
        /// PointerFunc`n / FreeFunc`n from scratch. Idempotent: the body (instructions, locals,
        /// exception handlers) is cleared first, so re-running the Generator on an already-woven
        /// dll produces the same result. The C#-emitted ConvertParam chain is replaced by a
        /// per-parameter three-branch sequence keyed on typeof(Ux):
        ///   ByRefParam     → ldarg pX stored into a PINNED local (the pinned slot is reported
        ///                    by GCInfo, so the byref target object cannot be relocated while
        ///                    the address snapshot copied into ux._Address is in flight), then
        ///                    ldloc + conv.u + ConvertParam&lt;IntPtr, Ux&gt; into ux;
        ///   ByRefLocalSlot → ldarga pX (this method's own `in Ux` parameter slot address) +
        ///                    conv.u + ConvertParam&lt;IntPtr, Ux&gt; into ux — slot-indirect,
        ///                    the callee reads the GC-updated current address through it;
        ///   otherwise      → ldarg pX + ldobj Ux (plain value copy).
        /// The _ReturnCategory == 2 branch is re-emitted (ref-return path via InvokeRefReturn
        /// with the out-r address snapshot; value path via Invoke), and every ret is preceded
        /// by the pin-release sequence (ldc.i4.0; conv.u; stloc pinned) so each pinned local's
        /// liveness covers the wrapper-call safepoints.
        /// </summary>
        static void RebuildInvokeFallbackBodies(AssemblyDefinition asm)
        {
            var module = asm.MainModule;
            var baseType = module.GetType("Mod.LowLevel.FreeInvokableBase");

            // ConvertParam<P, U>(P p) — the single-parameter bit-reinterpretation overload.
            var convertParam = baseType.Methods.Single(m =>
                m.Name == "ConvertParam" && m.GenericParameters.Count == 2
                && m.Parameters.Count == 1);
            var byRefParamType = module.GetType("Mod.LowLevel.ByRefParam");
            var byRefLocalSlotType = module.GetType("Mod.LowLevel.ByRefLocalSlot");
            var retcField = baseType.GetField("_ReturnCategory");

            var getTypeFromHandle = module.ImportReference(
                typeof(Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }));
            var typeOpEquality = module.ImportReference(
                typeof(Type).GetMethod("op_Equality", new Type[] { typeof(Type), typeof(Type) }));
            // Cecil 0.10 maps System.IntPtr to ELEMENT_TYPE_I (native int) both via
            // TypeSystem.IntPtr and ImportReference(typeof(IntPtr)) — and native int is
            // NOT a legal generic argument. Reuse the C#-compiler-written IntPtr
            // reference found in the two-arg ConvertParam overload's IL (its isref
            // branch calls ConvertParam<IntPtr, U>), and the op_Explicit(void*) bridge
            // from ConvertRefToAddress's IL.
            TypeReference intPtrType = null;
            MethodReference intPtrOpExplicit = null;
            var convertParam2 = baseType.Methods.Single(m =>
                m.Name == "ConvertParam" && m.GenericParameters.Count == 2
                && m.Parameters.Count == 2);
            foreach (var inst in convertParam2.Body.Instructions)
            {
                if (inst.OpCode == OpCodes.Call
                    && inst.Operand is GenericInstanceMethod gim
                    && gim.GenericArguments.Count == 2)
                {
                    intPtrType = gim.GenericArguments[0];
                    break;
                }
            }
            var convRefToAddress = baseType.Methods.Single(m =>
                m.Name == "ConvertRefToAddress" && m.GenericParameters.Count == 1
                && m.Parameters.Count == 1);
            foreach (var inst in convRefToAddress.Body.Instructions)
            {
                if (inst.OpCode == OpCodes.Call
                    && inst.Operand is MethodReference mr && mr.Name == "op_Explicit")
                {
                    intPtrOpExplicit = mr;
                    break;
                }
            }

            int patched = 0, skipped = 0;
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;
                if (!type.Name.StartsWith("PointerFunc") && !type.Name.StartsWith("FreeFunc")) continue;
                foreach (var method in type.Methods)
                {
                    if (method.Name != "InvokeFallback") continue;
                    if (!method.HasBody || method.Parameters.Count < 2) continue;
                    if (RebuildInvokeFallbackBody(method, type, module, convertParam,
                        byRefParamType, byRefLocalSlotType, retcField,
                        getTypeFromHandle, typeOpEquality, intPtrType, intPtrOpExplicit)) patched++;
                    else skipped++;
                }
            }
            Console.WriteLine($"InvokeFallback rebuild: {patched} methods rebuilt, {skipped} skipped");
        }

        static bool RebuildInvokeFallbackBody(MethodDefinition method, TypeDefinition type, ModuleDefinition module,
            MethodReference convertParam, TypeReference byRefParamType, TypeReference byRefLocalSlotType,
            FieldReference retcField, MethodReference getTypeFromHandle, MethodReference typeOpEquality,
            TypeReference intPtrType, MethodReference intPtrOpExplicit)
        {
            var body = method.Body;
            int n = method.Parameters.Count - 1;    // Ux count (param 0 = `out R r`)
            var gpR = type.GenericParameters[0];    // !R
            var rOut = method.Parameters[0];        // out R r

            // Bisect switch for IPE debugging: 0 = plain value-copy params only (no
            // typeof checks, no pinning, not even pinned local declarations), 1 = +
            // typeof three-branch (no pin), 2 = full.
            var step = int.TryParse(Environment.GetEnvironmentVariable("PD_INJ_STEP"), out var s) ? s : 2;

            // Locate this type's non-generic Invoke(U1..Un) and InvokeRefReturn(U1..Un).
            MethodDefinition invoke = null, invokeRefReturn = null;
            foreach (var m in type.Methods)
            {
                if (m.HasGenericParameters || m.Parameters.Count != n) continue;
                if (m.Name == "Invoke") invoke = m;
                else if (m.Name == "InvokeRefReturn") invokeRefReturn = m;
            }
            if (invoke == null || invokeRefReturn == null) return false;

            // Same-type method calls must spell the OPEN generic instantiation
            // (PointerFunc`n<!R, !U1..!Un>) as the declaring type — a bare
            // TypeDefinition declaring type is exactly what makes RyuJIT throw
            // InvalidProgramException on the woven body.
            var openSelf = new GenericInstanceType(type);
            foreach (var gp2 in type.GenericParameters) openSelf.GenericArguments.Add(gp2);
            var invokeRef = invokeRefReturn.GetReference(openSelf);
            var invokeSelfRef = invoke.GetReference(openSelf);

            // Clear and rebuild — idempotent by construction.
            body.Instructions.Clear();
            body.Variables.Clear();
            body.ExceptionHandlers.Clear();

            // Locals: per param ux (Ux value) + pinned pux (pinned Ux& — the exact shape
            // Roslyn emits for `fixed (Ux* p = &x)` over a generic Ux: a pinned BYREF local,
            // not a pinned pointer; RyuJIT rejects pinned generic-pointer locals), then
            // result (R&) and tmp (R). Cecil 0.10 expresses the pinned flag by wrapping
            // the variable type in PinnedType.
            var uLocals = new VariableDefinition[n];
            var pinnedLocals = new VariableDefinition[n];
            for (int i = 0; i < n; i++)
            {
                var gp = type.GenericParameters[1 + i];
                uLocals[i] = new VariableDefinition(gp);
                body.Variables.Add(uLocals[i]);
                if (step >= 2)
                {
                    pinnedLocals[i] = new VariableDefinition(new PinnedType(new ByReferenceType(gp)));
                    body.Variables.Add(pinnedLocals[i]);
                }
            }
            var resultLocal = new VariableDefinition(new ByReferenceType(gpR));
            body.Variables.Add(resultLocal);
            var tmpLocal = new VariableDefinition(gpR);
            body.Variables.Add(tmpLocal);
            body.MaxStackSize = Math.Max(body.MaxStackSize, 8);

            var seq = new List<Instruction>();
            void Emit(OpCode op, object operand = null)
            {
                Instruction inst;
                if (operand == null) inst = Instruction.Create(op);
                else if (operand is ParameterDefinition p) inst = Instruction.Create(op, p);
                else if (operand is VariableDefinition v) inst = Instruction.Create(op, v);
                else if (operand is TypeReference t) inst = Instruction.Create(op, t);
                else if (operand is MethodReference m) inst = Instruction.Create(op, m);
                else if (operand is FieldReference f) inst = Instruction.Create(op, f);
                else if (operand is Instruction i) inst = Instruction.Create(op, i);
                else inst = Instruction.Create(op, (int)operand);
                seq.Add(inst);
            }
            MethodReference ConvParam(int i)
            {
                var gim = new GenericInstanceMethod(convertParam);
                gim.GenericArguments.Add(intPtrType);
                gim.GenericArguments.Add(type.GenericParameters[1 + i]);
                return gim;
            }

            // Emit a typeof(X) == typeof(Y) comparison; returns the brfalse placeholder
            // (Cecil 0.10 rejects null targets, so the previous instruction is used as
            // a temporary target and overwritten by the caller).
            Instruction EmitTypeCheck(TypeReference genericArg, TypeReference concrete)
            {
                Emit(OpCodes.Ldtoken, genericArg);
                Emit(OpCodes.Call, getTypeFromHandle);
                Emit(OpCodes.Ldtoken, concrete);
                Emit(OpCodes.Call, getTypeFromHandle);
                Emit(OpCodes.Call, typeOpEquality);
                var brfalse = Instruction.Create(OpCodes.Brfalse, seq[seq.Count - 1]);
                seq.Add(brfalse);
                return brfalse;
            }

            // ---- per-parameter conversion ----
            // brDone targets are backfilled after the loop (each points at the first
            // instruction of the next parameter's segment, or at the branch test).
            var brDoneFixups = new List<(Instruction br, int paramIndex)>();
            for (int i = 0; i < n; i++)
            {
                var gp = type.GenericParameters[1 + i];
                var pX = method.Parameters[1 + i];

                if (step == 0)
                {
                    // plain value copy only
                    Emit(OpCodes.Ldarg, pX);
                    Emit(OpCodes.Ldobj, gp);
                    Emit(OpCodes.Stloc, uLocals[i]);
                    continue;
                }

                // if (typeof(Ux) == typeof(ByRefParam)) — pin the byref target, snapshot its address.
                // ldarg + stloc mirrors Roslyn's fixed prologue; ldloc + conv.u + op_Explicit
                // bridges the pinned byref bits into the IntPtr struct ConvertParam expects.
                var brf1 = EmitTypeCheck(gp, byRefParamType);
                if (step >= 2)
                {
                    Emit(OpCodes.Ldarg, pX);
                    Emit(OpCodes.Stloc, pinnedLocals[i]);
                    Emit(OpCodes.Ldloc, pinnedLocals[i]);
                }
                else
                {
                    Emit(OpCodes.Ldarg, pX);
                }
                Emit(OpCodes.Conv_U);
                Emit(OpCodes.Call, intPtrOpExplicit);
                Emit(OpCodes.Call, ConvParam(i));
                Emit(OpCodes.Stloc, uLocals[i]);
                var br1 = Instruction.Create(OpCodes.Br, seq[seq.Count - 1]);
                seq.Add(br1);

                // notBrp anchor: if (typeof(Ux) == typeof(ByRefLocalSlot)) — slot indirect.
                var notBrpAnchor = Instruction.Create(OpCodes.Ldtoken, gp);
                seq.Add(notBrpAnchor);
                brf1.Operand = notBrpAnchor;
                Emit(OpCodes.Call, getTypeFromHandle);
                Emit(OpCodes.Ldtoken, byRefLocalSlotType);
                Emit(OpCodes.Call, getTypeFromHandle);
                Emit(OpCodes.Call, typeOpEquality);
                var brf2 = Instruction.Create(OpCodes.Brfalse, seq[seq.Count - 1]);
                seq.Add(brf2);
                Emit(OpCodes.Ldarga, pX);
                Emit(OpCodes.Conv_U);
                Emit(OpCodes.Call, intPtrOpExplicit);
                Emit(OpCodes.Call, ConvParam(i));
                Emit(OpCodes.Stloc, uLocals[i]);
                var br2 = Instruction.Create(OpCodes.Br, seq[seq.Count - 1]);
                seq.Add(br2);

                // value branch anchor: else — plain value copy.
                var valueAnchor = Instruction.Create(OpCodes.Ldarg, pX);
                seq.Add(valueAnchor);
                brf2.Operand = valueAnchor;
                Emit(OpCodes.Ldobj, gp);
                Emit(OpCodes.Stloc, uLocals[i]);

                brDoneFixups.Add((br1, i));
                brDoneFixups.Add((br2, i));
            }

            // ---- _ReturnCategory == 2 branch test ----
            var branchTest = Instruction.Create(OpCodes.Ldarg_0);
            seq.Add(branchTest);
            // Backfill the brDone targets: segment i ends right before segment i+1 starts.
            if (brDoneFixups.Count > 0)
            {
                int[] segStarts = new int[n + 1];
                segStarts[0] = 0;
                {
                    int seg = 1, idx = 1;
                    while (seg < n && idx < seq.Count)
                    {
                        // each segment i>0 opens with ldtoken !Ui of its own typeof check
                        if (seq[idx].OpCode == OpCodes.Ldtoken
                            && seq[idx].Operand == type.GenericParameters[1 + seg])
                        {
                            segStarts[seg++] = idx;
                        }
                        idx++;
                    }
                }
                segStarts[n] = seq.IndexOf(branchTest);
                foreach (var (br, paramIndex) in brDoneFixups)
                {
                    br.Operand = seq[segStarts[paramIndex + 1]];
                }
            }

            Emit(OpCodes.Ldfld, retcField);
            Emit(OpCodes.Ldc_I4_2);
            var bne = Instruction.Create(OpCodes.Bne_Un, seq[seq.Count - 1]);
            seq.Add(bne);

            // ---- ref-return path: result = InvokeRefReturn(u1..un); r = default;
            //      r._Address = ConvertParam<IntPtr, R>(result); return ref result ----
            Emit(OpCodes.Ldarg_0);
            for (int i = 0; i < n; i++) Emit(OpCodes.Ldloc, uLocals[i]);
            Emit(OpCodes.Call, invokeRef);
            Emit(OpCodes.Stloc, resultLocal);
            Emit(OpCodes.Ldarg, rOut);
            Emit(OpCodes.Initobj, gpR);
            Emit(OpCodes.Ldarg, rOut);
            Emit(OpCodes.Ldloc, resultLocal);
            Emit(OpCodes.Conv_U);
            Emit(OpCodes.Call, intPtrOpExplicit);
            var convToR = new GenericInstanceMethod(convertParam);
            convToR.GenericArguments.Add(intPtrType);
            convToR.GenericArguments.Add(gpR);
            Emit(OpCodes.Call, convToR);
            Emit(OpCodes.Stobj, gpR);
            if (step >= 2)
            {
                for (int i = 0; i < n; i++)
                {
                    Emit(OpCodes.Ldc_I4_0);
                    Emit(OpCodes.Conv_U);
                    Emit(OpCodes.Stloc, pinnedLocals[i]);
                }
            }
            Emit(OpCodes.Ldloc, resultLocal);
            Emit(OpCodes.Ret);

            // ---- value path: r = Invoke(u1..un); return ref r ----
            var elseAnchor = Instruction.Create(OpCodes.Ldarg_0);
            seq.Add(elseAnchor);
            bne.Operand = elseAnchor;
            for (int i = 0; i < n; i++) Emit(OpCodes.Ldloc, uLocals[i]);
            Emit(OpCodes.Call, invokeSelfRef);
            Emit(OpCodes.Stloc, tmpLocal);
            Emit(OpCodes.Ldarg, rOut);
            Emit(OpCodes.Ldloc, tmpLocal);
            Emit(OpCodes.Stobj, gpR);
            if (step >= 2)
            {
                for (int i = 0; i < n; i++)
                {
                    Emit(OpCodes.Ldc_I4_0);
                    Emit(OpCodes.Conv_U);
                    Emit(OpCodes.Stloc, pinnedLocals[i]);
                }
            }
            Emit(OpCodes.Ldarg, rOut);
            Emit(OpCodes.Ret);

            // Append everything in order.
            var il = body.GetILProcessor();
            foreach (var inst in seq) il.Append(inst);
            return true;
        }

        /// <summary>
        /// Injects the ByRefLocalSlot fixup into every InvokeFallback(out R r, in U1 p1, ..., in Un pn)
        /// of PointerFunc`n / FreeFunc`n: right before the non-generic Invoke(u1..un) call, each
        /// ux local gets
        ///   ldloca ux; ldarga pX; conv.u; call FixupByRefLocalSlot&lt;!Ux&gt;(ref !Ux, native int)
        /// (X = the Ux argument slot: this=0, r=1, p1=2, ...). At runtime the helper checks
        /// typeof(Ux) == typeof(ByRefLocalSlot) and, if so, overwrites ux.Ref2Ref.Address with the
        /// address of InvokeFallback's own `in Ux pX` parameter slot — a GC-tracked byref the
        /// collector keeps updated across compacting GCs — so the ByRefLocalSlot the callee receives
        /// keeps tracking heap movement. Pattern-matched on the Roslyn-emitted shape:
        ///   [ldarg.0][ldloc u1]...[ldloc un][callvirt Invoke(U1..Un)]
        /// </summary>
        static void InjectInvokeFallbackByRefLocalSlotFixup(AssemblyDefinition asm)
        {
            var module = asm.MainModule;
            var baseType = module.GetType("Mod.LowLevel.FreeInvokableBase");
            var fixup = baseType.Methods.FirstOrDefault(m => m.Name == "FixupByRefLocalSlot");
            if (fixup == null)
            {
                Console.WriteLine("InvokeFallback fixup: FixupByRefLocalSlot not found — skipped");
                return;
            }
            int patched = 0, skipped = 0;
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;
                if (!type.Name.StartsWith("PointerFunc") && !type.Name.StartsWith("FreeFunc")) continue;
                foreach (var method in type.Methods)
                {
                    if (method.Name != "InvokeFallback") continue;
                    if (!method.HasBody || method.Parameters.Count < 2) continue;
                    if (InjectFixupsIntoInvokeFallback(method, type, fixup)) patched++;
                    else skipped++;
                }
            }
            Console.WriteLine($"InvokeFallback fixup: {patched} methods patched, {skipped} skipped");
        }

        static bool InjectFixupsIntoInvokeFallback(MethodDefinition method, TypeDefinition type, MethodDefinition fixup)
        {
            var body = method.Body;
            int n = method.Parameters.Count - 1;    // Ux param count (param 0 = `out R r`)

            // 1) locate each ConvertParam<Ux,Ux>(P&, bool) call and the stloc that
            //    immediately follows it → the local slot holding the converted uX.
            var locals = new VariableDefinition[n];
            int found = 0;
            foreach (var inst in body.Instructions)
            {
                if (found == n) break;
                if (inst.OpCode != OpCodes.Call) continue;
                if (!(inst.Operand is MethodReference mr)) continue;
                // generic instantiation calls report HasGenericParameters == false
                // in Cecil (type args are resolved into the GenericInstanceMethod),
                // so match on the (element) method name only.
                if (mr.Name != "ConvertParam") continue;
                int idx = GetStlocIndex(inst.Next);
                if (idx < 0 || idx >= body.Variables.Count) return false;
                locals[found++] = body.Variables[idx];
            }
            if (found != n) return false;

            // 2) insertion point: BEFORE the `_ReturnCategory == 2` branch test
            //    (ldarg.0; ldfld _ReturnCategory; ldc.i4.2; bne.un ...) so BOTH
            //    branches receive fixed-up slots. Previously the fixups were only
            //    inserted before the non-generic Invoke call (value-return path);
            //    the ref-return path (call InvokeRefReturn) ran with raw
            //    ConvertParam output — under ilc ConvertRefToAddress compiles to
            //    one-level ldarg semantics and the ByRefLocalSlot lost GC tracking
            //    (RefInvoke NullReferenceException on NativeAOT, ffH/ffJ probes).
            Instruction insertionPoint = null;
            for (int i = 0; i < body.Instructions.Count - 3; i++)
            {
                var a = body.Instructions[i];
                var b = body.Instructions[i + 1];
                var c = body.Instructions[i + 2];
                var d = body.Instructions[i + 3];
                if (IsLdarg0(a)
                    && b.OpCode == OpCodes.Ldfld && (b.Operand as FieldReference)?.Name == "_ReturnCategory"
                    && c.OpCode == OpCodes.Ldc_I4_2
                    && (d.OpCode == OpCodes.Bne_Un || d.OpCode == OpCodes.Bne_Un_S))
                {
                    insertionPoint = a;
                    break;
                }
            }
            if (insertionPoint == null) return false;   // unexpected shape — don't guess

            var il = body.GetILProcessor();
            for (int i = 0; i < n; i++)
            {
                var gim = new GenericInstanceMethod(fixup);
                gim.GenericArguments.Add(type.GenericParameters[1 + i]);    // U(i+1)
                il.InsertBefore(insertionPoint, il.Create(OpCodes.Ldloca, locals[i]));
                il.InsertBefore(insertionPoint, il.Create(OpCodes.Ldarga, method.Parameters[1 + i]));
                il.InsertBefore(insertionPoint, il.Create(OpCodes.Conv_U));
                il.InsertBefore(insertionPoint, il.Create(OpCodes.Call, gim));
            }
            return true;
        }

        static int GetStlocIndex(Instruction inst)
        {
            if (inst == null) return -1;
            var op = inst.OpCode;
            if (op == OpCodes.Stloc || op == OpCodes.Stloc_S)
            {
                if (inst.Operand is VariableDefinition vd) return vd.Index;
            }
            if (op == OpCodes.Stloc_0) return 0;
            if (op == OpCodes.Stloc_1) return 1;
            if (op == OpCodes.Stloc_2) return 2;
            if (op == OpCodes.Stloc_3) return 3;
            return -1;
        }

        static bool TryGetLdlocLocal(Instruction inst, MethodBody body, out VariableDefinition local)
        {
            local = null;
            var op = inst.OpCode;
            if (op == OpCodes.Ldloc || op == OpCodes.Ldloc_S)
            {
                local = inst.Operand as VariableDefinition;
                return local != null;
            }
            int idx = -1;
            if (op == OpCodes.Ldloc_0) idx = 0;
            else if (op == OpCodes.Ldloc_1) idx = 1;
            else if (op == OpCodes.Ldloc_2) idx = 2;
            else if (op == OpCodes.Ldloc_3) idx = 3;
            if (idx >= 0 && idx < body.Variables.Count)
            {
                local = body.Variables[idx];
                return true;
            }
            return false;
        }

        static bool IsLdarg0(Instruction inst)
        {
            return inst.OpCode == OpCodes.Ldarg_0
                || (inst.OpCode == OpCodes.Ldarg_S && (inst.Operand as ParameterDefinition)?.Index == 0);
        }
    }
}
