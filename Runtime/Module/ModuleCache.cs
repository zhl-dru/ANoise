using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleCache : ModuleBase
    {
        public double[] m_cache;


        public ModuleCache SetCache(NativeArray<double> values) { m_cache = values.ToArray(); return this; }
        public ModuleCache Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            var cache = new NativeArray<double>(m_cache, Allocator.TempJob);
            var job = CacheJob.JobHandle(cache, outputs, dependsOn);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            var cache = new NativeArray<double>(m_cache, Allocator.TempJob);
            var job = CacheJob.JobHandle(cache, outputs, dependsOn);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            var cache = new NativeArray<double>(m_cache, Allocator.TempJob);
            var job = CacheJob.JobHandle(cache, outputs, dependsOn);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            var cache = new NativeArray<double>(m_cache, Allocator.TempJob);
            var job = CacheJob.JobHandle(cache, outputs, dependsOn);
            DisposeCache(job, cache);
            return job;
        }
    }
}