using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleBrightContrast : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_bright, m_threshold, m_factor;
        private FunctionPointer<algorithm_a4> m_contrast_fun_ptr;

        public ModuleBrightContrast SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleBrightContrast SetBright(ModuleBase bright) { m_bright = bright; return this; }
        public ModuleBrightContrast SetThreshold(ModuleBase threshold) { m_threshold = threshold; return this; }
        public ModuleBrightContrast SetFactor(ModuleBase factor) { m_factor = factor; return this; }
        public ModuleBrightContrast SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleBrightContrast SetBright(double bright) { m_bright = new ModuleConstant().SetValue(bright).Build(); return this; }
        public ModuleBrightContrast SetThreshold(double threshold) { m_threshold = new ModuleConstant().SetValue(threshold).Build(); return this; }
        public ModuleBrightContrast SetFactor(double factor) { m_factor = new ModuleConstant().SetValue(factor).Build(); return this; }
        public ModuleBrightContrast Build()
        {
            unsafe { m_contrast_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a4>(amath.contrast); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var brightjob = m_bright.Get(inputs, cache2, sourcejob);
            var thresholdjob = m_threshold.Get(inputs, cache3, brightjob);
            var factorjob = m_factor.Get(inputs, cache4, thresholdjob);

            var job = AlgorithmA4Job.JobHandle(cache1, cache2, cache3, cache4, outputs, m_contrast_fun_ptr, factorjob);
            DisposeCache(job, cache1, cache2, cache3, cache4);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var brightjob = m_bright.Get(inputs, cache2, sourcejob);
            var thresholdjob = m_threshold.Get(inputs, cache3, brightjob);
            var factorjob = m_factor.Get(inputs, cache4, thresholdjob);

            var job = AlgorithmA4Job.JobHandle(cache1, cache2, cache3, cache4, outputs, m_contrast_fun_ptr, factorjob);
            DisposeCache(job, cache1, cache2, cache3, cache4);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var brightjob = m_bright.Get(inputs, cache2, sourcejob);
            var thresholdjob = m_threshold.Get(inputs, cache3, brightjob);
            var factorjob = m_factor.Get(inputs, cache4, thresholdjob);

            var job = AlgorithmA4Job.JobHandle(cache1, cache2, cache3, cache4, outputs, m_contrast_fun_ptr, factorjob);
            DisposeCache(job, cache1, cache2, cache3, cache4);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);

            var sourcejob = m_source.Get(inputs, cache1, dependsOn);
            var brightjob = m_bright.Get(inputs, cache2, sourcejob);
            var thresholdjob = m_threshold.Get(inputs, cache3, brightjob);
            var factorjob = m_factor.Get(inputs, cache4, thresholdjob);

            var job = AlgorithmA4Job.JobHandle(cache1, cache2, cache3, cache4, outputs, m_contrast_fun_ptr, factorjob);
            DisposeCache(job, cache1, cache2, cache3, cache4);
            return job;
        }
    }
}