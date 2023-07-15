using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleNormalizeCoords : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_length;

        public ModuleNormalizeCoords SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleNormalizeCoords SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleNormalizeCoords SetLength(ModuleBase length) { m_length = length; return this; }
        public ModuleNormalizeCoords SetLength(double length) { m_length = new ModuleConstant().SetValue(length).Build(); return this; }
        public ModuleNormalizeCoords Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var coords = CreateCache<double2>(length);
            var cache = CreateCache<double>(length);

            var lenthjob = m_length.Get(inputs, cache, dependsOn);
            var normalizecoordsjob = NormalizeCoords2Job.JobHandle(inputs, cache, coords, lenthjob);
            var job = m_source.Get(coords, outputs, normalizecoordsjob);
            DisposeCache(job, coords);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var coords = CreateCache<double3>(length);
            var cache = CreateCache<double>(length);

            var lenthjob = m_length.Get(inputs, cache, dependsOn);
            var normalizecoordsjob = NormalizeCoords3Job.JobHandle(inputs, cache, coords, lenthjob);
            var job = m_source.Get(coords, outputs, normalizecoordsjob);
            DisposeCache(job, coords);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var coords = CreateCache<double4>(length);
            var cache = CreateCache<double>(length);

            var lenthjob = m_length.Get(inputs, cache, dependsOn);
            var normalizecoordsjob = NormalizeCoords4Job.JobHandle(inputs, cache, coords, lenthjob);
            var job = m_source.Get(coords, outputs, normalizecoordsjob);
            DisposeCache(job, coords);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var coords = CreateCache<double6>(length);
            var cache = CreateCache<double>(length);

            var lenthjob = m_length.Get(inputs, cache, dependsOn);
            var normalizecoordsjob = NormalizeCoords6Job.JobHandle(inputs, cache, coords, lenthjob);
            var job = m_source.Get(coords, outputs, normalizecoordsjob);
            DisposeCache(job, coords);
            DisposeCache(job, cache);
            return job;
        }
    }
}