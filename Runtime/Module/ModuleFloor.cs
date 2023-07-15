using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleFloor : ModuleBase
    {
        private ModuleBase m_source;
        private FunctionPointer<algorithm_a1> m_floor_fun_ptr;

        public ModuleFloor SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleFloor SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleFloor Build()
        {
            unsafe { m_floor_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a1>(amath.floor); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AlgorithmA1Job.JobHandle(cache, outputs, m_floor_fun_ptr, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AlgorithmA1Job.JobHandle(cache, outputs, m_floor_fun_ptr, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AlgorithmA1Job.JobHandle(cache, outputs, m_floor_fun_ptr, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AlgorithmA1Job.JobHandle(cache, outputs, m_floor_fun_ptr, sourcejob);
            DisposeCache(job, cache);
            return job;
        }
    }
}