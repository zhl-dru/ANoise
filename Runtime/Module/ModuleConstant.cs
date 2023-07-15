using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;

namespace ANoise
{
    public class ModuleConstant : ModuleBase
    {
        private double m_value;
        private FunctionPointer<constant> m_constant_fun_ptr;

        public ModuleConstant SetValue(double value) { m_value = value; return this; }
        public ModuleConstant Build()
        {
            unsafe { m_constant_fun_ptr = BurstCompiler.CompileFunctionPointer<constant>(amath.constant); }
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return ConstantJob.JobHandle(m_value, outputs, m_constant_fun_ptr, dependsOn);
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return ConstantJob.JobHandle(m_value, outputs, m_constant_fun_ptr, dependsOn);
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return ConstantJob.JobHandle(m_value, outputs, m_constant_fun_ptr, dependsOn);
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return ConstantJob.JobHandle(m_value, outputs, m_constant_fun_ptr, dependsOn);
        }
    }
}