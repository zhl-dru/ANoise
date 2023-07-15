using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct Gradient2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public DGradient Data;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Gradient(count, inputPtr, outputPtr);
        }

        private unsafe void Gradient(int count, double2* c, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (
                    (c[i].x - Data.Gx) * Data.X +
                    (c[i].y - Data.Gy) * Data.Y
                    ) / Data.Vlen;
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, DGradient data,
            NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Gradient2Job()
            {
                Inputs = inputs,
                Data = data,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Gradient3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public DGradient Data;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Gradient(count, inputPtr, outputPtr);
        }

        private unsafe void Gradient(int count, double3* c, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (
                    (c[i].x - Data.Gx) * Data.X +
                    (c[i].y - Data.Gy) * Data.Y +
                    (c[i].z - Data.Gz) * Data.Z
                    ) / Data.Vlen;
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, DGradient data,
            NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Gradient3Job()
            {
                Inputs = inputs,
                Data = data,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Gradient4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public DGradient Data;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Gradient(count, inputPtr, outputPtr);
        }

        private unsafe void Gradient(int count, double4* c, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (
                    (c[i].x - Data.Gx) * Data.X +
                    (c[i].y - Data.Gy) * Data.Y +
                    (c[i].z - Data.Gz) * Data.Z +
                    (c[i].w - Data.Gw) * Data.W
                    ) / Data.Vlen;
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, DGradient data,
            NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Gradient4Job()
            {
                Inputs = inputs,
                Data = data,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Gradient6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public DGradient Data;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Gradient(count, inputPtr, outputPtr);
        }

        private unsafe void Gradient(int count, double6* c, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (
                    (c[i].x - Data.Gx) * Data.X +
                    (c[i].y - Data.Gy) * Data.Y +
                    (c[i].z - Data.Gz) * Data.Z +
                    (c[i].w - Data.Gw) * Data.W +
                    (c[i].u - Data.Gu) * Data.U +
                    (c[i].v - Data.Gv) * Data.V
                    ) / Data.Vlen;
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, DGradient data,
            NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Gradient6Job()
            {
                Inputs = inputs,
                Data = data,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}