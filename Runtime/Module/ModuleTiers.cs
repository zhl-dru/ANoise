using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleTiers : ModuleBase
    {
        private ModuleBase m_source;
        private int m_numtiers;
        private bool m_smooth;

        public ModuleTiers SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleTiers SetSource(double source) { m_source = m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleTiers SetNumTiers(int numtiers) { m_numtiers = numtiers; return this; }
        public ModuleTiers SetSmooth(bool smooth) { m_smooth = smooth; return this; }
        public ModuleTiers Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = TiersJob.JobHandle(cache, m_numtiers, m_smooth, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = TiersJob.JobHandle(cache, m_numtiers, m_smooth, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = TiersJob.JobHandle(cache, m_numtiers, m_smooth, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = TiersJob.JobHandle(cache, m_numtiers, m_smooth, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }
    }
}