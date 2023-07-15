using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModulePow : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_power;
        private FunctionPointer<algorithm_a2> m_pow_fun_ptr;

        public ModulePow SetSource(ModuleBase source) { m_source = source; return this; }
        public ModulePow SetPower(ModuleBase power) { m_power = power; return this; }
        public ModulePow SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModulePow SetPower(double power) { m_power = new ModuleConstant().SetValue(power).Build(); return this; }
        public ModulePow Build()
        {
            unsafe { m_pow_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a2>(amath.pow); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var powerjob = m_power.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_pow_fun_ptr, powerjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var powerjob = m_power.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_pow_fun_ptr, powerjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var powerjob = m_power.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_pow_fun_ptr, powerjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var powerjob = m_power.Get(inputs, cache2, sourcejob);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_pow_fun_ptr, powerjob);
            DisposeCache(job, cache1, cache2);
            return job;
        }
    }
}