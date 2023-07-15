using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct TiersJob : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Source;
        [ReadOnly] public int NumTiers;
        [ReadOnly] public bool Smooth;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var sourcePtr = (double*)Source.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Tiers(count, sourcePtr, outputPtr);
        }

        private unsafe void Tiers(int count, double* s, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                int numsteps = NumTiers;
                if (Smooth) --numsteps;
                double val = s[i];
                double tb = math.floor(val * numsteps);
                double tt = tb + 1.0;
                double t = val * numsteps - tb;
                tb /= numsteps;
                tt /= numsteps;
                double u = Smooth ? amath.quintic_blend(t) : 0.0;
                r[i] = tb + u * (tt - tb);
            }
        }

        public static JobHandle JobHandle(NativeArray<double> source, int tiers, bool smooth, NativeArray<double> outputs,
            JobHandle dependsOn)
        {
            return new TiersJob()
            {
                Source = source,
                NumTiers = tiers,
                Smooth = smooth,
                Outputs = outputs
            }.ScheduleBatch(source.Length, source.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}