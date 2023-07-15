using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;

namespace ANoise
{
    public class ModuleMagnitude : ModuleBase
    {
        ModuleBase m_x, m_y, m_z, m_w, m_u, m_v;
        FunctionPointer<algorithm_a2> m_magnitude2_fun_ptr;
        FunctionPointer<algorithm_a3> m_magnitude3_fun_ptr;
        FunctionPointer<algorithm_a4> m_magnitude4_fun_ptr;
        FunctionPointer<algorithm_a6> m_magnitude6_fun_ptr;

        public ModuleMagnitude SetX(ModuleBase x) { m_x = x; return this; }
        public ModuleMagnitude SetY(ModuleBase y) { m_y = y; return this; }
        public ModuleMagnitude SetZ(ModuleBase z) { m_z = z; return this; }
        public ModuleMagnitude SetW(ModuleBase w) { m_w = w; return this; }
        public ModuleMagnitude SetU(ModuleBase u) { m_u = u; return this; }
        public ModuleMagnitude SetV(ModuleBase v) { m_v = v; return this; }
        public ModuleMagnitude SetX(double x) { m_x = new ModuleConstant().SetValue(x).Build(); return this; }
        public ModuleMagnitude SetY(double y) { m_y = new ModuleConstant().SetValue(y).Build(); return this; }
        public ModuleMagnitude SetZ(double z) { m_z = new ModuleConstant().SetValue(z).Build(); return this; }
        public ModuleMagnitude SetW(double w) { m_w = new ModuleConstant().SetValue(w).Build(); return this; }
        public ModuleMagnitude SetU(double u) { m_u = new ModuleConstant().SetValue(u).Build(); return this; }
        public ModuleMagnitude SetV(double v) { m_v = new ModuleConstant().SetValue(v).Build(); return this; }
        public ModuleMagnitude Build()
        {
            unsafe
            {
                m_magnitude2_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a2>(amath.magnitude);
                m_magnitude3_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a3>(amath.magnitude);
                m_magnitude4_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a4>(amath.magnitude);
                m_magnitude6_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a6>(amath.magnitude);
            }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var xjob = m_x.Get(inputs, cache1, dependsOn);
            var yjob = m_y.Get(inputs, cache2, xjob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_magnitude2_fun_ptr, yjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var xjob = m_x.Get(inputs, cache1, dependsOn);
            var yjob = m_y.Get(inputs, cache2, xjob);
            var zjob = m_z.Get(inputs, cache3, yjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_magnitude3_fun_ptr, zjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);

            var xjob = m_x.Get(inputs, cache1, dependsOn);
            var yjob = m_y.Get(inputs, cache2, xjob);
            var zjob = m_z.Get(inputs, cache3, yjob);
            var wjob = m_w.Get(inputs, cache4, zjob);

            var job = AlgorithmA4Job.JobHandle(cache1, cache2, cache3, cache4, outputs, m_magnitude4_fun_ptr, wjob);
            DisposeCache(job, cache1, cache2, cache3, cache4);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);
            var cache5 = CreateCache<double>(length);
            var cache6 = CreateCache<double>(length);

            var xjob = m_x.Get(inputs, cache1, dependsOn);
            var yjob = m_y.Get(inputs, cache2, xjob);
            var zjob = m_z.Get(inputs, cache3, yjob);
            var wjob = m_w.Get(inputs, cache4, zjob);
            var ujob = m_u.Get(inputs, cache5, wjob);
            var vjob = m_v.Get(inputs, cache6, ujob);

            var job = AlgorithmA6Job.JobHandle(cache1, cache2, cache3, cache4, cache5, cache6, outputs, m_magnitude6_fun_ptr, vjob);
            DisposeCache(job, cache1, cache2, cache3, cache4, cache5, cache6);
            return job;
        }
    }
}