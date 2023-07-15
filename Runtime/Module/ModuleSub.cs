using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleSub : ModuleBase
    {
        private ModuleBase m_source1;
        private ModuleBase m_source2;
        private FunctionPointer<algorithm_a2> m_subtract_fun_ptr;

        public ModuleSub SetSource1(ModuleBase source1) { m_source1 = source1; return this; }
        public ModuleSub SetSource2(ModuleBase source2) { m_source2 = source2; return this; }
        public ModuleSub SetSource1(double source1) { m_source1 = new ModuleConstant().SetValue(source1).Build(); return this; }
        public ModuleSub SetSource2(double source2) { m_source2 = new ModuleConstant().SetValue(source2).Build(); return this; }
        public ModuleSub Build()
        {
            unsafe { m_subtract_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a2>(amath.subtract); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var source1job = m_source1.Get(inputs, cache1, dependsOn);
            var source2job = m_source2.Get(inputs, cache2, source1job);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_subtract_fun_ptr, source2job);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var source1job = m_source1.Get(inputs, cache1, dependsOn);
            var source2job = m_source2.Get(inputs, cache2, source1job);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_subtract_fun_ptr, source2job);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var source1job = m_source1.Get(inputs, cache1, dependsOn);
            var source2job = m_source2.Get(inputs, cache2, source1job);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_subtract_fun_ptr, source2job);
            DisposeCache(job, cache1, cache2);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var source1job = m_source1.Get(inputs, cache1, dependsOn);
            var source2job = m_source2.Get(inputs, cache2, source1job);

            var job = AlgorithmA2Job.JobHandle(cache1, cache2, outputs, m_subtract_fun_ptr, source2job);
            DisposeCache(job, cache1, cache2);
            return job;
        }
    }
}