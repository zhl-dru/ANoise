using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace ANoise
{
    /*
     * ���Job���ڽ�������,���Ǽ��������κ���,
     * ֻ�ǽ�ֵ��һ�����鸴�Ƶ���һ��,��Ҫ��ЩJob��Ϊ�˱�֤��ҵ��������,
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