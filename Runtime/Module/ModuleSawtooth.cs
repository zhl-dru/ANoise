using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleSawtooth : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_period;
        private FunctionPointer<algorithm_a2> m_sawtooth_fun_ptr;

        public ModuleSawtooth SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleSawtooth SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleSawtooth SetPeriod(ModuleBase period) { m_period = period; return this; }
        public ModuleSawtooth SetPeriod(double period) { m_period = new ModuleConstant().SetValue(period).Build(); return this; }
        public ModuleSawtooth Build()
        {
            unsafe { m_sawtooth_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a2>(amath.sawtooth); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_sawtooth_fun_ptr, periodjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_sawtooth_fun_ptr, periodjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_sawtooth_fun_ptr, periodjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var periodjob = m_period.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_sawtooth_fun_ptr, periodjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }
    }
}