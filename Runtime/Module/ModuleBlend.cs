using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleBlend : ModuleBase
    {
        private ModuleBase m_control;
        private ModuleBase m_low;
        private ModuleBase m_high;
        private FunctionPointer<algorithm_a3> m_blend_fun_ptr;

        public ModuleBlend SetControlSource(ModuleBase control) { m_control = control; return this; }
        public ModuleBlend SetLowSource(ModuleBase low) { m_low = low; return this; }
        public ModuleBlend SetHighSource(ModuleBase high) { m_high = high; return this; }
        public ModuleBlend SetControlSource(double control) { m_control = new ModuleConstant().SetValue(control).Build(); return this; }
        public ModuleBlend SetLowSource(double low) { m_low = new ModuleConstant().SetValue(low).Build(); return this; }
        public ModuleBlend SetHighSource(double high) { m_high = new ModuleConstant().SetValue(high).Build(); return this; }
        public ModuleBlend Build()
        {
            unsafe { m_blend_fun_ptr = BurstCompiler.CompileFunctionPointer<algorithm_a3>(amath.blend); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var controljob = m_control.Get(inputs, cache1, dependsOn);
            var lowjob = m_low.Get(inputs, cache2, controljob);
            var highjob = m_high.Get(inputs, cache3, lowjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_blend_fun_ptr, highjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var controljob = m_control.Get(inputs, cache1, dependsOn);
            var lowjob = m_low.Get(inputs, cache2, controljob);
            var highjob = m_high.Get(inputs, cache3, lowjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_blend_fun_ptr, highjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var controljob = m_control.Get(inputs, cache1, dependsOn);
            var lowjob = m_low.Get(inputs, cache2, controljob);
            var highjob = m_high.Get(inputs, cache3, lowjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_blend_fun_ptr, highjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache1 = CreateCache<double>(length);
            var cache2 = CreateCache<double>(length);
            var cache3 = CreateCache<double>(length);

            var controljob = m_control.Get(inputs, cache1, dependsOn);
            var lowjob = m_low.Get(inputs, cache2, controljob);
            var highjob = m_high.Get(inputs, cache3, lowjob);

            var job = AlgorithmA3Job.JobHandle(cache1, cache2, cache3, outputs, m_blend_fun_ptr, highjob);
            DisposeCache(job, cache1, cache2, cache3);
            return job;
        }
    }
}