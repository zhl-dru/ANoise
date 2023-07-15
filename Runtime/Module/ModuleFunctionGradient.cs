using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleFunctionGradient : ModuleBase
    {
        private ModuleBase m_source;
        private double m_spacing = 0.01;
        private EFunctionGradientAxis m_axis;

        public ModuleFunctionGradient SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleFunctionGradient SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleFunctionGradient SetSpacing(double spacing) { m_spacing = spacing; return this; }
        public ModuleFunctionGradient SetAxis(EFunctionGradientAxis axis) { m_axis = axis; return this; }
        public ModuleFunctionGradient Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;

            var coords1 = CreateCache<double2>(length);
            var coords2 = CreateCache<double2>(length);

            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var coordjob = FunctionGradientModifyCoords2Job.JobHandle(inputs, m_spacing, m_axis, coords1, coords2, dependsOn);

            var job1 = m_source.Get(coords1, cache1, coordjob);
            var job2 = m_source.Get(coords2, cache2, job1);

            var job = FunctionGradientJob.JobHandle(cache1, cache2, m_spacing, outputs, job2);
            DisposeCache(job, cache1, cache2);
            DisposeCache(job, coords1, coords2);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;

            var coords1 = CreateCache<double3>(length);
            var coords2 = CreateCache<double3>(length);

            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var coordjob = FunctionGradientModifyCoords3Job.JobHandle(inputs, m_spacing, m_axis, coords1, coords2, dependsOn);
            var job1 = m_source.Get(coords1, cache1, coordjob);
            var job2 = m_source.Get(coords2, cache2, job1);

            var job = FunctionGradientJob.JobHandle(cache1, cache2, m_spacing, outputs, job2);
            DisposeCache(job, cache1, cache2);
            DisposeCache(job, coords1, coords2);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;

            var coords1 = CreateCache<double4>(length);
            var coords2 = CreateCache<double4>(length);

            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var coordjob = FunctionGradientModifyCoords4Job.JobHandle(inputs, m_spacing, m_axis, coords1, coords2, dependsOn);
            var job1 = m_source.Get(coords1, cache1, coordjob);
            var job2 = m_source.Get(coords2, cache2, job1);

            var job = FunctionGradientJob.JobHandle(cache1, cache2, m_spacing, outputs, job2);
            DisposeCache(job, cache1, cache2);
            DisposeCache(job, coords1, coords2);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;

            var coords1 = CreateCache<double6>(length);
            var coords2 = CreateCache<double6>(length);

            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);

            var coordjob = FunctionGradientModifyCoords6Job.JobHandle(inputs, m_spacing, m_axis, coords1, coords2, dependsOn);
            var job1 = m_source.Get(coords1, cache1, coordjob);
            var job2 = m_source.Get(coords2, cache2, job1);

            var job = FunctionGradientJob.JobHandle(cache1, cache2, m_spacing, outputs, job2);
            DisposeCache(job, cache1, cache2);
            DisposeCache(job, coords1, coords2);
            return job;
        }
    }


}