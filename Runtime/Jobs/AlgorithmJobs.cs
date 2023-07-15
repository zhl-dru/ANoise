using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace ANoise
{
    [BurstCompile]
    public struct AlgorithmA1Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs;
        [ReadOnly] public FunctionPointer<algorithm_a1> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, inputPtr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs,
            NativeArray<double> outputs, FunctionPointer<algorithm_a1> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA1Job()
            {
                Inputs = inputs,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct AlgorithmA2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public FunctionPointer<algorithm_a2> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, input1Ptr, input2Ptr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            NativeArray<double> outputs, FunctionPointer<algorithm_a2> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA2Job()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct AlgorithmA3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public NativeArray<double> Inputs3;
        [ReadOnly] public FunctionPointer<algorithm_a3> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var input3Ptr = (double*)Inputs3.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, input1Ptr, input2Ptr, input3Ptr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            NativeArray<double> inputs3, NativeArray<double> outputs, FunctionPointer<algorithm_a3> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA3Job()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Inputs3 = inputs3,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct AlgorithmA4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public NativeArray<double> Inputs3;
        [ReadOnly] public NativeArray<double> Inputs4;
        [ReadOnly] public FunctionPointer<algorithm_a4> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var input3Ptr = (double*)Inputs3.GetUnsafeReadOnlyPtr() + startIndex;
            var input4Ptr = (double*)Inputs4.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, input1Ptr, input2Ptr, input3Ptr, input4Ptr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            NativeArray<double> inputs3, NativeArray<double> inputs4, NativeArray<double> outputs,
            FunctionPointer<algorithm_a4> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA4Job()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Inputs3 = inputs3,
                Inputs4 = inputs4,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct AlgorithmA5Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public NativeArray<double> Inputs3;
        [ReadOnly] public NativeArray<double> Inputs4;
        [ReadOnly] public NativeArray<double> Inputs5;
        [ReadOnly] public FunctionPointer<algorithm_a5> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var input3Ptr = (double*)Inputs3.GetUnsafeReadOnlyPtr() + startIndex;
            var input4Ptr = (double*)Inputs4.GetUnsafeReadOnlyPtr() + startIndex;
            var input5Ptr = (double*)Inputs5.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, input1Ptr, input2Ptr, input3Ptr, input4Ptr, input5Ptr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            NativeArray<double> inputs3, NativeArray<double> inputs4, NativeArray<double> inputs5,
            NativeArray<double> outputs, FunctionPointer<algorithm_a5> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA5Job()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Inputs3 = inputs3,
                Inputs4 = inputs4,
                Inputs5 = inputs5,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct AlgorithmA6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public NativeArray<double> Inputs3;
        [ReadOnly] public NativeArray<double> Inputs4;
        [ReadOnly] public NativeArray<double> Inputs5;
        [ReadOnly] public NativeArray<double> Inputs6;
        [ReadOnly] public FunctionPointer<algorithm_a6> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var input3Ptr = (double*)Inputs3.GetUnsafeReadOnlyPtr() + startIndex;
            var input4Ptr = (double*)Inputs4.GetUnsafeReadOnlyPtr() + startIndex;
            var input5Ptr = (double*)Inputs5.GetUnsafeReadOnlyPtr() + startIndex;
            var input6Ptr = (double*)Inputs6.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, input1Ptr, input2Ptr, input3Ptr, input4Ptr, input5Ptr, input6Ptr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            NativeArray<double> inputs3, NativeArray<double> inputs4, NativeArray<double> inputs5,
            NativeArray<double> inputs6, NativeArray<double> outputs,
            FunctionPointer<algorithm_a6> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA6Job()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Inputs3 = inputs3,
                Inputs4 = inputs4,
                Inputs5 = inputs5,
                Inputs6 = inputs6,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct AlgorithmA7Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public NativeArray<double> Inputs3;
        [ReadOnly] public NativeArray<double> Inputs4;
        [ReadOnly] public NativeArray<double> Inputs5;
        [ReadOnly] public NativeArray<double> Inputs6;
        [ReadOnly] public NativeArray<double> Inputs7;
        [ReadOnly] public FunctionPointer<algorithm_a7> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;


        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var input3Ptr = (double*)Inputs3.GetUnsafeReadOnlyPtr() + startIndex;
            var input4Ptr = (double*)Inputs4.GetUnsafeReadOnlyPtr() + startIndex;
            var input5Ptr = (double*)Inputs5.GetUnsafeReadOnlyPtr() + startIndex;
            var input6Ptr = (double*)Inputs6.GetUnsafeReadOnlyPtr() + startIndex;
            var input7Ptr = (double*)Inputs7.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, input1Ptr, input2Ptr, input3Ptr, input4Ptr, input5Ptr, input6Ptr, input7Ptr, outputPtr);
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            NativeArray<double> inputs3, NativeArray<double> inputs4, NativeArray<double> inputs5,
            NativeArray<double> inputs6, NativeArray<double> inputs7, NativeArray<double> outputs,
            FunctionPointer<algorithm_a7> algorithm,
            JobHandle dependsOn)
        {
            return new AlgorithmA7Job()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Inputs3 = inputs3,
                Inputs4 = inputs4,
                Inputs5 = inputs5,
                Inputs6 = inputs6,
                Inputs7 = inputs7,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }



    /*
     * 这个Job用于常量,它们几乎不做任何事,
     * 只是为输出数组填充相同的值,需要这些Job是为了保证作业链的连续,
     */


    [BurstCompile]
    public struct ConstantJob : IJobParallelForBatch
    {
        [ReadOnly] public double Parameter;
        [ReadOnly] public FunctionPointer<constant> FunPtr;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunPtr.Invoke(count, Parameter, outputPtr);
        }

        public static JobHandle JobHandle(double parameter,
            NativeArray<double> outputs, FunctionPointer<constant> algorithm,
            JobHandle dependsOn)
        {
            return new ConstantJob()
            {
                Parameter = parameter,
                Outputs = outputs,
                FunPtr = algorithm
            }.ScheduleBatch(outputs.Length, outputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }

}