using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace ANoise
{
    /*
     * 这个Job用于建立缓存,它们几乎不做任何事,
     * 只是将值从一个数组复制到另一个,需要这些Job是为了保证作业链的连续,
     */


    [BurstCompile]
    public struct CacheJob : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs;
        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            amath.cache(count, inputPtr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs, NativeArray<double> outputs,
            JobHandle dependsOn)
        {
            return new CacheJob()
            {
                Inputs = inputs,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}