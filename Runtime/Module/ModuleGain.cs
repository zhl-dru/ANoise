using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleGain : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_gain;
        private FunctionPointer<algorithm_a2> m_gain_fun_ptr;

        public ModuleGain SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleGain SetGain(ModuleBase gain) { m_gain = gain; return this; }
        public ModuleGain SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleGain SetGain(double gain) { m_gain = new ModuleConstant().SetValue(gain).Build(); return this; }
        public ModuleGain Build()
        {
            unsafe { m_gain_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a2>(amath.gain); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var gainjob = m_gain.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_gain_fun_ptr, gainjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var gainjob = m_gain.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_gain_fun_ptr, gainjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var gainjob = m_gain.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_gain_fun_ptr, gainjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var gainjob = m_gain.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_gain_fun_ptr, gainjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }
    }
}