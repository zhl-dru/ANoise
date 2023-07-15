using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleCellular : ModuleBase
    {
        private uint m_seed;
        private double m_frequency;
        private ECellularDistanceFunction m_cellulardistancetype;
        private double m_f1, m_f2, m_f3, m_f4;

        public ModuleCellular SetSeed(int seed) { m_seed = (uint)seed; return this; }
        public ModuleCellular SetFrequency(double frequency) { m_frequency = frequency; return this; }
        public ModuleCellular SetDistanceType(ECellularDistanceFunction distype) { m_cellulardistancetype = distype; return this; }
        public ModuleCellular SetF1(double f1) { m_f1 = f1; return this; }
        public ModuleCellular SetF2(double f2) { m_f2 = f2; return this; }
        public ModuleCellular SetF3(double f3) { m_f3 = f3; return this; }
        public ModuleCellular SetF4(double f4) { m_f4 = f4; return this; }
        public ModuleCellular SetCoefficients(double f1, double f2, double f3, double f4) { m_f1 = f1; m_f2 = f2; m_f3 = f3; m_f4 = f4; return this; }
        public ModuleCellular Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return Cellular2Job.JobHandle(inputs, m_seed, m_cellulardistancetype, m_f1, m_f2, m_f3, m_f4, m_frequency, outputs, dependsOn);
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return Cellular3Job.JobHandle(inputs, m_seed, m_cellulardistancetype, m_f1, m_f2, m_f3, m_f4, m_frequency, outputs, dependsOn);
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return Cellular4Job.JobHandle(inputs, m_seed, m_cellulardistancetype, m_f1, m_f2, m_f3, m_f4, m_frequency, outputs, dependsOn);
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            return Cellular6Job.JobHandle(inputs, m_seed, m_cellulardistancetype, m_f1, m_f2, m_f3, m_f4, m_frequency, outputs, dependsOn);
        }
    }
}