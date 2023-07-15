using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace ANoise
{
    [BurstCompile]
    public struct Noise2ColorJob : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Noise;
        [ReadOnly] public FunctionPointer<noise2color> Rule;

        [WriteOnly] public NativeArray<Color> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var noisePtr = (double*)Noise.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (Color*)Outputs.GetUnsafePtr() + startIndex;
            Rule.Invoke(count, noisePtr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> noise, FunctionPointer<noise2color> rule, NativeArray<Color> outputs, JobHandle dependsOn)
        {
            return new Noise2ColorJob()
            {
                Noise = noise,
                Rule = rule,
                Outputs = outputs
            }.ScheduleBatch(noise.Length, noise.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}