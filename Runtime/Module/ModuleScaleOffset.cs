using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;

namespace ANoise
{
    public class ModuleScaleOffset : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_scale, m_offset;
        private FunctionPointer<algorithm_a3> m_scaleoffset_fun_ptr;


        public ModuleScaleOffset SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleScaleOffset SetScale(ModuleBase scale) { m_scale = scale; return this; }
        public ModuleScaleOffset SetOffset(ModuleBase offset) { m_offset = offset; return this; }
        public ModuleScaleOffset SetSource(double source) { m_source = m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleScaleOffset SetScale(double scale) { m_scale = new ModuleConstant().SetValue(scale).Build(); return this; }
        public ModuleScaleOffset SetOffset(double offset) { m_offset = new ModuleConstant().SetValue(offset).Build(); return this; }
        public ModuleScaleOffset Build()
        {
            unsafe { m_scaleoffset_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a3>(amath.scaleoffset); }
            return this;
        }


        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var scalejob = m_scale.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, scalejob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_scaleoffset_fun_ptr, offsetjob);
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
            var scalejob = m_scale.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, scalejob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_scaleoffset_fun_ptr, offsetjob);
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
            var scalejob = m_scale.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, scalejob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_scaleoffset_fun_ptr, offsetjob);
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
            var scalejob = m_scale.Get(inputs, cache2, sourcejob);
            var offsetjob = m_offset.Get(inputs, cache3, scalejob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_scaleoffset_fun_ptr, offsetjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }
    }
}