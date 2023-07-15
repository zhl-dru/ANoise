using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;

namespace ANoise
{
    public class ModuleTriangle : ModuleBase
    {
        private ModuleBase m_source, m_period, m_offset;
        private FunctionPointer<algorithm_a3> m_triangle_fun_ptr;

        public ModuleTriangle SetSource(ModuleBase s) { m_source = s; return this; }
        public ModuleTriangle SetPeriod(ModuleBase p) { m_period = p; return this; }
        public ModuleTriangle SetOffset(ModuleBase o) { m_offset = o; return this; }
        public ModuleTriangle SetSource(double s) { m_source = new ModuleConstant().SetValue(s).Build(); return this; }
        public ModuleTriangle SetOffset(double o) { m_offset = new ModuleConstant().SetValue(o).Build(); return this; }
        public ModuleTriangle SetPeriod(double p) { m_period = new ModuleConstant().SetValue(p).Build(); return this; }
        public ModuleTriangle Build()
        {
            unsafe { m_triangle_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a3>(amath.triangle); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, periodjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_triangle_fun_ptr, offsetjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, periodjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_triangle_fun_ptr, offsetjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, periodjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_triangle_fun_ptr, offsetjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, periodjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_triangle_fun_ptr, offsetjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }
    }
}