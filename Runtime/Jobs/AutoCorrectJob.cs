using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct AutoCorrectJob : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs;
        public double Low;
        public double High;
        public double Scale;
        public double Offset;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            AutoCorrect(count, inputPtr, outputPtr);
        }

        private unsafe void AutoCorrect(int count, double* s, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.max(Low, math.min(High, s[i] * Scale + Offset));
            }
        }

        public static JobHandle JobHandle(NativeArray<double> inputs, double low,
            double high, double scale, double offset, NativeArray<double> outputs,
            JobHandle dependsOn)
        {
            return new AutoCorrectJob()
            {
                Inputs = inputs,
                Outputs = outputs,
                Low = low,
                High = high,
                Scale = scale,
                Offset = offset
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}