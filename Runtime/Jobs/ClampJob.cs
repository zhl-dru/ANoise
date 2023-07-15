using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct ClampJob : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Source;
        public double Low;
        public double High;

        public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var sourcePtr = (double*)Source.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Clamp(count, sourcePtr, outputPtr);
        }

        private unsafe void Clamp(int count, double* s, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.clamp(s[i], Low, High);
            }
        }

        public static JobHandle JobHandle(NativeArray<double> source, double low, double high, NativeArray<double> outputs,
            JobHandle dependsOn)
        {
            return new ClampJob()
            {
                Source = source,
                Low = low,
                High = high,
                Outputs = outputs
            }.ScheduleBatch(source.Length, source.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}