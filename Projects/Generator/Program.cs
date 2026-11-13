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
            // _ReturnCategory/_ParamFlags are now STATIC fields on the per-arity
            // FreeInvokable`n+1 base classes (no longer instance fields on
            // FreeInvokableBase) — resolve them per type via GetStaticFieldRef.

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
                    foreach (var method in type.Methods.ToArray())
                    {
                        // The non-generic Invoke is now a plain-C# trampoline (runtime-Emit
                        // PlainInvoker first, InvokePlain fallback). InvokePlain — plain R
                        // return, calli to Pfn — is the only woven method left in PointerFunc.
                        if (method.Name == "InvokePlain")
                        {

                            InjectPointerFuncNonGenericInvoke(method, type, module, unmonly);
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

            // RefInvoker core-Invoke rebuild (ActionRefInvoker/FuncRefInvoker/
            // RefFuncRefInvoker — slot-indirect wrappers over a typed delegate
            // field). Completely separate from the PointerFunc/FreeFunc passes:
            // this pass's type filter matches only the three RefInvoker family
            // names, and none of the other passes' filters (PointerFunc* /
            // FreeFunc* prefixes) ever match them.
            if (mode == "all" || mode == "refinvoker")
            {
                InjectRefInvokerBodies(asm);
            }

            EmitRefSafetyRulesAttribute(asm);
            EmitUnscopedRefOnRefReturningMethods(asm);
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
        /// <summary>
        /// Resolves a STATIC field (_ReturnCategory / _ParamFlags) that now lives on the
        /// per-arity FreeInvokable`n+1 base class. type.BaseType is the generic
        /// instantiation FreeInvokable&lt;!R, !U1..!Un&gt; — its generic arguments ARE the
        /// declaring type's own generic parameters — so a FieldReference over it is the
        /// correct open-generic static field reference (the exact shape Roslyn emits
        /// for base-class static field access from a generic derived class).
        /// </summary>
        static FieldReference GetStaticFieldRef(TypeDefinition type, string name)
        {
            var baseDef = type.BaseType.Resolve();
            var fd = baseDef.GetField(name);
            if (fd == null) return null;
            return new FieldReference(fd.Name, fd.FieldType, type.BaseType);
        }

        /// <summary>
        /// Resolves a static accessor method (GetReturnCategory() / GetParamFlag(int))
        /// on the per-arity FreeInvokable`n+1 base class, referenced through the
        /// declaring type's own generic instantiation of that base. The woven IL
        /// uses the cached flags through these accessors without Type comparisons.
        /// </summary>
        static MethodReference GetStaticMethodRef(TypeDefinition type, string name, int paramCount)
        {
            var baseDef = type.BaseType.Resolve();
            MethodDefinition md = null;
            foreach (var m in baseDef.Methods)
            {
                if (m.Name == name && !m.HasGenericParameters
                    && m.Parameters.Count == paramCount && m.IsStatic)
                {
                    md = m;
                    break;
                }
            }
            if (md == null) return null;
            return md.GetReference((GenericInstanceType)type.BaseType);
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
        /// <summary>
        /// Injects [UnscopedRef] on every STATIC ref-returning method of Mod.LowLevel
        /// (the woven InvokeFallback placeholders, the generic Invoke family,
        /// InvokeRefReturn, ConvertRef/ConvertAddressToRef bridges, ...). The woven
        /// IL returns refs that escape to the caller's frame (the out r slot, the
        /// delegate byref), but the C#-level throw placeholders have no return-ref
        /// flow for Roslyn to infer the escape scope from, so consumers get
        /// CS8347/CS8156 ("result may expose variables referred to by r").
        /// UnscopedRefAttribute declares the calling-method escape scope the real
        /// IL provides. The attribute type is a shim (ns2.0 lacks it); Roslyn
        /// recognizes these compiler-reserved attributes by full name alone.
        /// Idempotent: skips methods that already carry it.
        ///
        /// Static-only is deliberate and matches ByRefUtils' Generator: under
        /// rules-11 [UnscopedRef] pulls all ref-like arguments INCLUDING the
        /// receiver back into the result's escape-min. This assembly's ref-
        /// returning surface is all static, so behavior is unchanged today, but
        /// the guard keeps any future struct-instance ref-returning member
        /// (non-readonly this) from being silently TIGHTENED by the attribute.
        /// </summary>
        static void EmitUnscopedRefOnRefReturningMethods(AssemblyDefinition asm)
        {
            const string ns = "System.Diagnostics.CodeAnalysis";
            var module = asm.MainModule;

            // UnscopedRefAttribute shim (ns2.0 lacks it)
            var shim = module.GetType($"{ns}.UnscopedRefAttribute");
            if (shim == null)
            {
                shim = new TypeDefinition(ns, "UnscopedRefAttribute",
                    TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                    GetSystemType(module, "Attribute"));

                // NOTE: no [AttributeUsage] on the shim - see the note in
                // EmitScopedRefOnInParameters for why (MemberRef resolution).

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

            int annotated = 0;
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;
                foreach (var method in type.Methods)
                {
                    if (!method.ReturnType.IsByReference) continue;      // ref-returning methods
                    if (!method.IsStatic) continue;                      // see doc: receiver must stay out of escape-min
                    if (method.CustomAttributes.Any(a => a.AttributeType.FullName == $"{ns}.UnscopedRefAttribute")) continue;
                    method.CustomAttributes.Add(new CustomAttribute(shimCtor));
                    annotated++;
                }
            }
            Console.WriteLine($"[UnscopedRef] annotated {annotated} ref-returning methods");
        }

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

        static void InjectPointerFuncNonGenericInvoke(MethodDefinition method, TypeDefinition type, ModuleDefinition module, bool unmanagedOnly)
        {
            var pfnField = type.GetField("_Pfn");
            var unmanagedField = type.GetField("_IsUnmanaged");
            // The caller supplies the cached return-category flag.
            var retcParam = method.Parameters[method.Parameters.Count - 1];

            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();
            var emitter = method.Body.GetILProcessor();

            var returnType = GetCallSiteReturnType(type);
            VariableDefinition retValLocal = new VariableDefinition(returnType);
            method.Body.Variables.Add(retValLocal);

            var callvoidfnLabel = emitter.Create(OpCodes.Nop);
            var callvoidfnLabelU = emitter.Create(OpCodes.Nop);

            if (!unmanagedOnly)
            {
                // Keep unmanaged generic calli signatures out of the managed body.
                // Desktop CLR resolves their native sizes even on untaken branches.
                var unmanagedMethod = new MethodDefinition("InvokePlainUnmanaged",
                    Mono.Cecil.MethodAttributes.Private | Mono.Cecil.MethodAttributes.HideBySig,
                    method.ReturnType);
                unmanagedMethod.ImplAttributes = Mono.Cecil.MethodImplAttributes.NoInlining;
                foreach (var parameter in method.Parameters)
                    unmanagedMethod.Parameters.Add(new ParameterDefinition(parameter.Name, parameter.Attributes, parameter.ParameterType));
                type.Methods.Add(unmanagedMethod);
                InjectPointerFuncNonGenericInvoke(unmanagedMethod, type, module, true);
                var self = new GenericInstanceType(type);
                foreach (var parameter in type.GenericParameters) self.GenericArguments.Add(parameter);
                var managedStart = emitter.Create(OpCodes.Nop);
                emitter.Emit(OpCodes.Ldarg_0);
                emitter.Emit(OpCodes.Ldfld, new FieldReference(unmanagedField.Name, unmanagedField.FieldType, self));
                emitter.Emit(OpCodes.Brfalse, managedStart);
                emitter.Emit(OpCodes.Ldarg_0);
                foreach (var parameter in method.Parameters) emitter.Emit(OpCodes.Ldarg, parameter);
                emitter.Emit(OpCodes.Call, unmanagedMethod.GetReference(self));
                emitter.Emit(OpCodes.Ret);
                emitter.Append(managedStart);
            }

            for (int i = 0; i < method.Parameters.Count - 1; i++)   // exclude trailing retc
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

            if (!unmanagedOnly)
            {

            // === managed path ===
            emitter.Emit(OpCodes.Ldarg, retcParam);
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
            return;
            }

            // === unmanaged path (the only path in unmonly mode) ===
            emitter.Emit(OpCodes.Ldarg, retcParam);
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
            // GetRefParamFlag(ulong flags, int paramIndex) — the static flag decoder on
            // FreeInvokableBase; the woven branches key on its ParamFlag result instead
            // of typeof comparisons.
            var getRefParamFlag = baseType.Methods.Single(m =>
                m.Name == "GetRefParamFlag" && m.Parameters.Count == 2).GetReference();

            // NOTE: never ImportReference(typeof(...)) from the net10 Generator here —
            // Cecil 0.10 flushes every imported reference into the output module, which
            // adds an extern System.Private.CoreLib that net48 cannot load
            // (BadImageFormatException / FileNotFoundException at JIT time).

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
                        getRefParamFlag, intPtrType, intPtrOpExplicit)) patched++;
                    else skipped++;
                }
            }
            Console.WriteLine($"InvokeFallback rebuild: {patched} methods rebuilt, {skipped} skipped");
        }

        static bool RebuildInvokeFallbackBody(MethodDefinition method, TypeDefinition type, ModuleDefinition module,
            MethodReference convertParam, MethodReference getRefParamFlag,
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

            // Read the cached per-arity flags through the static base accessors.
            var retcGetter = GetStaticMethodRef(type, "GetReturnCategory", 0);
            var paramFlagGetter = GetStaticMethodRef(type, "GetParamFlag", 1);
            if (retcGetter == null || paramFlagGetter == null) return false;

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

            // Emit a flag comparison: GetParamFlag(i) == expected. Returns the bne.un
            // placeholder (Cecil 0.10 rejects null targets, so the previous
            // instruction is the temporary target, overwritten later).
            Instruction EmitFlagCheck(int paramIndex, int expected)
            {
                Emit(OpCodes.Ldc_I4, paramIndex);
                Emit(OpCodes.Call, paramFlagGetter);
                Emit(OpCodes.Ldc_I4, expected);
                var bne = Instruction.Create(OpCodes.Bne_Un, seq[seq.Count - 1]);
                seq.Add(bne);
                return bne;
            }

            // ---- per-parameter conversion ----
            // brDone targets are backfilled after the loop (each points at the first
            // instruction of the next parameter's segment, or at the branch test).
            var brDoneFixups = new List<(Instruction br, int paramIndex)>();
            var segStarts = new Instruction[n + 1];
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

                // if (GetRefParamFlag(_ParamFlags, i) == ParamFlag.ByRef) — pin the
                // byref target (ByRefParam), snapshot its address. ldarg + stloc
                // mirrors Roslyn's fixed prologue; ldloc + conv.u + op_Explicit
                // bridges the pinned byref bits into the IntPtr struct.
                int segStartIdx = seq.Count;   // the ldsfld lands here
                var brf1 = EmitFlagCheck(i, 2 /* ParamFlag.ByRef */);
                segStarts[i] = seq[segStartIdx];
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

                // notBrp anchor: flag == ParamFlag.Ref2Ref (ByRefLocalSlot) — slot indirect.
                var notBrpAnchor = Instruction.Create(OpCodes.Ldc_I4, i);
                seq.Add(notBrpAnchor);
                brf1.Operand = notBrpAnchor;
                Emit(OpCodes.Call, paramFlagGetter);
                Emit(OpCodes.Ldc_I4, 3 /* ParamFlag.Ref2Ref */);
                var brf2 = Instruction.Create(OpCodes.Bne_Un, seq[seq.Count - 1]);
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

            // ---- _ReturnCategory == ParamFlag.ByRef (2) branch test ----
            var branchTest = Instruction.Create(OpCodes.Call, retcGetter);
            seq.Add(branchTest);
            // Backfill the brDone targets: segment i ends right where segment i+1
            // starts (or at the branch test for the last segment).
            if (brDoneFixups.Count > 0)
            {
                segStarts[n] = branchTest;
                foreach (var (br, paramIndex) in brDoneFixups)
                {
                    br.Operand = segStarts[paramIndex + 1];
                }
            }

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
        /// Rebuilds the private generic core Invoke&lt;P1..Pn&gt;(out R r, in P1 p1, ..., in Pn pn)
        /// of every ActionRefInvoker/FuncRefInvoker/RefFuncRefInvoker (EX and non-EX
        /// families). The woven body:
        ///   - prologue: ldarg.0; ldfld _Del (the typed delegate field);
        ///   - every slot parameter: ldarga pX; conv.u; IntPtr::op_Explicit(void*);
        ///     newobj ByRefLocalSlot::.ctor(IntPtr) — a slot-indirect wrapper over this
        ///     frame's own argument slot (the byref inside the slot stays GC-tracked,
        ///     so the callee keeps following heap compaction — no address snapshot,
        ///     no pin);
        ///   - the EX family's last parameter: ldarg; ldobj; ConvertParam&lt;!Pex, !EX&gt; —
        ///     bit-pattern reinterpretation of the method generic parameter into the
        ///     class's EX (identical types at every wrapper call site, so a plain
        ///     value copy that satisfies the verifier);
        ///   - callvirt the delegate's Invoke;
        ///   - epilogue: Action → r = default; return ref r. Func → r = result;
        ///     return ref r. RefFunc → r = *resultRef; return the delegate's byref.
        /// Idempotent (the body is cleared and re-emitted, so re-running the
        /// Generator on an already-woven dll is safe).
        /// </summary>
        static void InjectRefInvokerBodies(AssemblyDefinition asm)
        {
            var module = asm.MainModule;
            var baseType = module.GetType("Mod.LowLevel.FreeInvokableBase");

            // ConvertParam<P, U>(P p) — the 1-arg bit-reinterpretation overload.
            var convertParam = baseType.Methods.Single(m =>
                m.Name == "ConvertParam" && m.GenericParameters.Count == 2
                && m.Parameters.Count == 1);

            // IntPtr::op_Explicit(void*) — reuse the C#-compiler-written reference
            // from ConvertRefToAddress's IL (never ImportReference(typeof(...)) —
            // see the note in RebuildInvokeFallbackBodies).
            MethodReference intPtrOpExplicit = null;
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

            // ByRefLocalSlot::.ctor(IntPtr) — read straight from the compiled dll
            // (the parameter's IntPtr reference is Roslyn-written, so it is safe
            // for every target runtime).
            var slotType = module.GetType("Mod.LowLevel.ByRefLocalSlot");
            var slotCtor = slotType.Methods.Single(m =>
                m.IsConstructor && m.Parameters.Count == 1);

            int patched = 0, skipped = 0;
            var invokeTemplates = new Dictionary<string, MethodReference>();
            foreach (var type in module.Types)
            {
                if (type.Namespace != "Mod.LowLevel") continue;
                if (!type.Name.StartsWith("ActionRefInvoker")
                    && !type.Name.StartsWith("FuncRefInvoker")
                    && !type.Name.StartsWith("RefFuncRefInvoker")) continue;
                foreach (var method in type.Methods)
                {
                    // The private generic core Invoke — NOT the public wrappers
                    // (public, plain returns) and NOT the explicit interface
                    // implementations (method.Overrides is non-empty).
                    if (method.Name != "Invoke") continue;
                    if (!method.IsPrivate || method.Overrides.Count > 0) continue;
                    if (!method.HasBody || !method.HasGenericParameters) continue;
                    if (!method.ReturnType.IsByReference) continue;
                    if (method.Parameters.Count < 2) continue;
                    if (InjectRefInvokerBody(method, type, module, convertParam,
                        intPtrOpExplicit, slotCtor, invokeTemplates)) patched++;
                    else skipped++;
                }
            }
            Console.WriteLine($"RefInvoker rebuild: {patched} methods rebuilt, {skipped} skipped");
        }

        static bool InjectRefInvokerBody(MethodDefinition method, TypeDefinition type,
            ModuleDefinition module, MethodDefinition convertParam,
            MethodReference intPtrOpExplicit, MethodReference slotCtor,
            Dictionary<string, MethodReference> invokeTemplates)
        {
            var body = method.Body;
            int n = method.Parameters.Count - 1;    // in-param count (param 0 = `out R r`)
            var rOut = method.Parameters[0];

            // EX family: the class's LAST generic parameter is EX
            // (ActionRefInvoker1<EX> / FuncRefInvoker1<R, EX> / RefFuncRefInvoker1<R, EX>);
            // the non-EX families have no EX parameter (their last class param is R,
            // or they have none). The EX parameter is the method's last `in` param.
            bool isExFamily = type.GenericParameters.Count > 0
                && type.GenericParameters[type.GenericParameters.Count - 1].Name == "EX";
            int slotCount = isExFamily ? n - 1 : n;
            if (slotCount < 0) return false;

            // The typed delegate field and its Invoke reference.
            var delField = type.GetField("_Del");
            if (delField == null) return false;
            if (!(delField.FieldType is GenericInstanceType delType)) return false;
            var delInvokeRef = MakeDelegateInvokeRef(module, delType, invokeTemplates);
            if (delInvokeRef == null) return false;

            bool isActionFamily = type.Name.StartsWith("ActionRefInvoker");
            bool isRefFuncFamily = type.Name.StartsWith("RefFuncRefInvoker");
            var typeR = ((ByReferenceType)method.ReturnType).ElementType;

            // Clear and rebuild — idempotent by construction.
            body.Instructions.Clear();
            body.Variables.Clear();
            body.ExceptionHandlers.Clear();

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
                else inst = Instruction.Create(op, (int)operand);
                seq.Add(inst);
            }

            // this._Del. For fields declared on a generic wrapper, the ldfld operand
            // must be a MemberRef whose declaring type is the open self TypeSpec
            // FuncRefInvokerN<!R> / RefFuncRefInvokerN<!R>. A raw FieldDef works on
            // the JIT but makes NativeAOT/ILC fail to import the generic type.
            Emit(OpCodes.Ldarg_0);
            FieldReference delFieldRef = delField;
            if (type.HasGenericParameters)
            {
                var openSelf = new GenericInstanceType(type);
                foreach (var gp in type.GenericParameters)
                    openSelf.GenericArguments.Add(gp);
                delFieldRef = new FieldReference(delField.Name, delField.FieldType, openSelf);
            }
            Emit(OpCodes.Ldfld, delFieldRef);

            // Slot params: ldarga pX; conv.u; op_Explicit(void*); newobj ByRefLocalSlot(IntPtr).
            for (int i = 0; i < slotCount; i++)
            {
                Emit(OpCodes.Ldarga, method.Parameters[1 + i]);
                Emit(OpCodes.Conv_U);
                Emit(OpCodes.Call, intPtrOpExplicit);
                Emit(OpCodes.Newobj, slotCtor);
            }

            // EX param: ldarg; ldobj; ConvertParam<!Pex, !EX>. The generic param is
            // taken from the method's own generic parameter list (the last one —
            // the parameter types of `in` params carry modreqs that Cecil 0.10
            // does not model, so they must not be inspected). At every wrapper
            // call site Pex == EX, so the bit reinterpretation is a plain copy.
            if (isExFamily)
            {
                var pEx = method.Parameters[method.Parameters.Count - 1];
                var gpEx = method.GenericParameters[method.GenericParameters.Count - 1];
                var exParam = type.GenericParameters[type.GenericParameters.Count - 1];
                Emit(OpCodes.Ldarg, pEx);
                Emit(OpCodes.Ldobj, gpEx);
                var gim = new GenericInstanceMethod(convertParam);
                gim.GenericArguments.Add(gpEx);
                gim.GenericArguments.Add(exParam);
                Emit(OpCodes.Call, gim);
            }

            // callvirt _Del.Invoke(slots..., [EX])
            Emit(OpCodes.Callvirt, delInvokeRef);

            // Epilogue per family.
            if (isActionFamily)
            {
                // r = default; return ref r
                Emit(OpCodes.Ldarg, rOut);
                Emit(OpCodes.Initobj, typeR);
                Emit(OpCodes.Ldarg, rOut);
                Emit(OpCodes.Ret);
            }
            else if (isRefFuncFamily)
            {
                // resRef = the delegate's byref; r = *resRef; return resRef
                var resRef = new VariableDefinition(new ByReferenceType(typeR));
                body.Variables.Add(resRef);
                Emit(OpCodes.Stloc, resRef);
                Emit(OpCodes.Ldarg, rOut);
                Emit(OpCodes.Ldloc, resRef);
                Emit(OpCodes.Ldobj, typeR);
                Emit(OpCodes.Stobj, typeR);
                Emit(OpCodes.Ldloc, resRef);
                Emit(OpCodes.Ret);
            }
            else
            {
                // res = the delegate result; r = res; return ref r
                var res = new VariableDefinition(typeR);
                body.Variables.Add(res);
                Emit(OpCodes.Stloc, res);
                Emit(OpCodes.Ldarg, rOut);
                Emit(OpCodes.Ldloc, res);
                Emit(OpCodes.Stobj, typeR);
                Emit(OpCodes.Ldarg, rOut);
                Emit(OpCodes.Ret);
            }

            body.MaxStackSize = Math.Max(body.MaxStackSize, slotCount + 3);
            var il = body.GetILProcessor();
            foreach (var inst in seq) il.Append(inst);
            return true;
        }

        /// <summary>
        /// Builds the callvirt Invoke reference for a typed delegate field
        /// (e.g. Action&lt;ByRefLocalSlot, !EX&gt;) without resolving any netstandard
        /// type: the dll's own FreeFunc Invoke bodies already contain callvirt
        /// sites on every Action`n / Func`n / RefFunc`n arity, and a MemberRef's
        /// signature is expressed in the DELEGATE's own generic parameters —
        /// identical for every instantiation — so an existing site is reused as
        /// the template and only the declaring instantiation is replaced.
        /// </summary>
        static MethodReference MakeDelegateInvokeRef(ModuleDefinition module,
            GenericInstanceType delType, Dictionary<string, MethodReference> invokeTemplates)
        {
            var elemName = delType.ElementType.FullName;
            var key = elemName + "/" + delType.GenericArguments.Count;
            if (invokeTemplates.TryGetValue(key, out var template))
            {
                return CloneInvokeRef(template, delType);
            }
            foreach (var type in module.Types)
            {
                foreach (var method in type.Methods)
                {
                    if (!method.HasBody) continue;
                    foreach (var inst in method.Body.Instructions)
                    {
                        if (inst.OpCode != OpCodes.Callvirt) continue;
                        if (!(inst.Operand is MethodReference mr) || mr.Name != "Invoke") continue;
                        if (!(mr.DeclaringType is GenericInstanceType git)) continue;
                        if (git.ElementType.FullName != elemName) continue;
                        if (git.GenericArguments.Count != delType.GenericArguments.Count) continue;
                        invokeTemplates[key] = mr;
                        return CloneInvokeRef(mr, delType);
                    }
                }
            }
            return null;
        }

        static MethodReference CloneInvokeRef(MethodReference template, GenericInstanceType delType)
        {
            var mref = new MethodReference(template.Name, template.ReturnType, delType) { HasThis = true };
            foreach (var p in template.Parameters) mref.Parameters.Add(p);
            return mref;
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
