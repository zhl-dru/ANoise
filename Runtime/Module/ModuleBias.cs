using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleBias : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_bias;
        private FunctionPointer<algorithm_a2> m_bias_fun_ptr;

        public ModuleBias SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleBias SetBias(ModuleBase bias) { m_bias = bias; return this; }
        public ModuleBias SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleBias SetBias(double bias) { m_bias = new ModuleConstant().SetValue(bias).Build(); return this; }
        public ModuleBias Build()
        {
            unsafe { m_bias_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a2>(amath.bias); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var biasjob = m_bias.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_bias_fun_ptr, biasjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var biasjob = m_bias.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_bias_fun_ptr, biasjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var biasjob = m_bias.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_bias_fun_ptr, biasjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var biasjob = m_bias.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_bias_fun_ptr, biasjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }
    }


}