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
                        if (method.Name != "InvokePlain") continue;

                        InjectPointerFuncNonGenericInvoke(method, type, retcField, module, unmonly);
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

            // InvokeFallback ByRefLocalSlot fixup: before the non-generic Invoke(u1..un) call,
            // each ux local gets FixupByRefLocalSlot<!Ux>(ref ux, ldarga pX) — when Ux is
            // ByRefLocalSlot, ux.Ref2Ref.Address is overwritten with the address of
            // InvokeFallback's own `in Ux pX` parameter slot (a GC-tracked byref the
            // collector keeps updated), so the ByRefLocalSlot the callee receives keeps
            // tracking heap compaction on the fallback path. NOTE: the fixup helper
            // calls ConvertRefToAddress/ConvertAddressToRef, so the prim section above
            // must run too (it does under "all").
            if (mode == "all" || mode == "lref")
            {
                InjectInvokeFallbackByRefLocalSlotFixup(asm);
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

            // locate the non-generic Invoke(u1..un) call
            Instruction invokeCall = null;
            foreach (var inst in body.Instructions)
            {
                if (inst.OpCode != OpCodes.Call && inst.OpCode != OpCodes.Callvirt) continue;
                if (!(inst.Operand is MethodReference mref)) continue;
                if (mref.Name != "Invoke" || mref.HasGenericParameters) continue;
                if (mref.Parameters.Count != n) continue;
                invokeCall = inst;
                break;
            }
            if (invokeCall == null) return false;

            // walk back over the [ldloc un]...[ldloc u1] argument pushes
            var locals = new VariableDefinition[n];
            var cursor = invokeCall.Previous;
            for (int i = n - 1; i >= 0; i--)
            {
                if (cursor == null || !TryGetLdlocLocal(cursor, body, out locals[i])) return false;
                cursor = cursor.Previous;
            }
            // cursor must now be the receiver push (ldarg.0 / ldarg.s 0)
            if (cursor == null || !IsLdarg0(cursor)) return false;

            // insert the fixups before the receiver push
            var il = body.GetILProcessor();
            for (int i = 0; i < n; i++)
            {
                var gim = new GenericInstanceMethod(fixup);
                gim.GenericArguments.Add(type.GenericParameters[1 + i]);    // U(i+1)
                il.InsertBefore(cursor, il.Create(OpCodes.Ldloca, locals[i]));
                il.InsertBefore(cursor, il.Create(OpCodes.Ldarga, method.Parameters[1 + i]));
                il.InsertBefore(cursor, il.Create(OpCodes.Conv_U));
                il.InsertBefore(cursor, il.Create(OpCodes.Call, gim));
            }
            return true;
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
