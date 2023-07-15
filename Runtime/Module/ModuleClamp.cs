using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleClamp : ModuleBase
    {
        private ModuleBase m_source;
        private double m_low, m_high;

        public ModuleClamp SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleClamp SetRange(double low, double high) { m_low = low; m_high = high; return this; }
        public ModuleClamp Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = ClampJob.JobHandle(cache, m_low, m_high, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = ClampJob.JobHandle(cache, m_low, m_high, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = ClampJob.JobHandle(cache, m_low, m_high, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = ClampJob.JobHandle(cache, m_low, m_high, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }
    }
}