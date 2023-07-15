using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;

namespace ANoise
{
    public class ModuleSelect : ModuleBase
    {
        private ModuleBase m_low, m_high, m_control;
        private ModuleBase m_threshold, m_falloff;
        private FunctionPointer<algorithm_a5> m_select_fun_ptr;

        public ModuleSelect SetLowSource(ModuleBase low) { m_low = low; return this; }
        public ModuleSelect SetHighSource(ModuleBase high) { m_high = high; return this; }
        public ModuleSelect SetControlSource(ModuleBase control) { m_control = control; return this; }
        public ModuleSelect SetThreshold(ModuleBase threshold) { m_threshold = threshold; return this; }
        public ModuleSelect SetFalloff(ModuleBase falloff) { m_falloff = falloff; return this; }
        public ModuleSelect SetLowSource(double low) { m_low = new ModuleConstant().SetValue(low).Build(); return this; }
        public ModuleSelect SetHighSource(double high) { m_high = new ModuleConstant().SetValue(high).Build(); return this; }
        public ModuleSelect SetControlSource(double control) { m_control = new ModuleConstant().SetValue(control).Build(); return this; }
        public ModuleSelect SetThreshold(double threshold) { m_threshold = new ModuleConstant().SetValue(threshold).Build(); return this; }
        public ModuleSelect SetFalloff(double falloff) { m_falloff = new ModuleConstant().SetValue(falloff).Build(); return this; }
        public ModuleSelect Build()
        {
            unsafe { m_select_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a5>(amath.select); }
            return this;
        }


        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);
            var cache5 = CreateCache<double>(length);

            var lowjob = m_low.Get(inputs, cache1, dependsOn);
            var highjob = m_high.Get(inputs, cache2, lowjob);
            var controljob = m_control.Get(inputs, cache3, highjob);
            var thresholdjob = m_threshold.Get(inputs, cache4, controljob);
            var falloffjob = m_falloff.Get(inputs, cache5, thresholdjob);

            var job = AlgorithmA5Job.JobHandle(cache1, cache2, cache3, cache4, cache5, outputs, m_select_fun_ptr, falloffjob);
            DisposeCache(job, cache1, cache2, cache3, cache4, cache5);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);
            var cache5 = CreateCache<double>(length);

            var lowjob = m_low.Get(inputs, cache1, dependsOn);
            var highjob = m_high.Get(inputs, cache2, lowjob);
            var controljob = m_control.Get(inputs, cache3, highjob);
            var thresholdjob = m_threshold.Get(inputs, cache4, controljob);
            var falloffjob = m_falloff.Get(inputs, cache5, thresholdjob);

            var job = AlgorithmA5Job.JobHandle(cache1, cache2, cache3, cache4, cache5, outputs, m_select_fun_ptr, falloffjob);
            DisposeCache(job, cache1, cache2, cache3, cache4, cache5);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);
            var cache5 = CreateCache<double>(length);

            var lowjob = m_low.Get(inputs, cache1, dependsOn);
            var highjob = m_high.Get(inputs, cache2, lowjob);
            var controljob = m_control.Get(inputs, cache3, highjob);
            var thresholdjob = m_threshold.Get(inputs, cache4, controljob);
            var falloffjob = m_falloff.Get(inputs, cache5, thresholdjob);

            var job = AlgorithmA5Job.JobHandle(cache1, cache2, cache3, cache4, cache5, outputs, m_select_fun_ptr, falloffjob);
            DisposeCache(job, cache1, cache2, cache3, cache4, cache5);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);
            var cache4 = CreateCache<double>(length);
            var cache5 = CreateCache<double>(length);

            var lowjob = m_low.Get(inputs, cache1, dependsOn);
            var highjob = m_high.Get(inputs, cache2, lowjob);
            var controljob = m_control.Get(inputs, cache3, highjob);
            var thresholdjob = m_threshold.Get(inputs, cache4, controljob);
            var falloffjob = m_falloff.Get(inputs, cache5, thresholdjob);

            var job = AlgorithmA5Job.JobHandle(cache1, cache2, cache3, cache4, cache5, outputs, m_select_fun_ptr, falloffjob);
            DisposeCache(job, cache1, cache2, cache3, cache4, cache5);
            return job;
        }
    }
}