using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct TranslateDomain2Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double2> Inputs;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;

        [WriteOnly] public NativeArray<double2> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double2*)Outputs.GetUnsafePtr() + startIndex;
            TranslateDomain(count, inputPtr, sxPtr, syPtr, outputPtr);
        }

        private unsafe void TranslateDomain(int count, double2* c, double* axs, double* ays, double2* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double2(c[i].x + axs[i], c[i].y + ays[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, NativeArray<double> ax, NativeArray<double> ay,
            NativeArray<double2> outputs, JobHandle dependsOn)
        {
            return new TranslateDomain2Job()
            {
                Inputs = inputs,
                Axs = ax,
                Ays = ay,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct TranslateDomain3Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double3> Inputs;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;
        [ReadOnly] public NativeArray<double> Azs;

        [WriteOnly] public NativeArray<double3> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var szPtr = (double*)Azs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double3*)Outputs.GetUnsafePtr() + startIndex;
            TranslateDomain(count, inputPtr, sxPtr, syPtr, szPtr, outputPtr);
        }

        private unsafe void TranslateDomain(int count, double3* c, double* axs, double* ays, double* azs, double3* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double3(c[i].x + axs[i], c[i].y + ays[i], c[i].z + azs[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, NativeArray<double> ax, NativeArray<double> ay,
            NativeArray<double> az, NativeArray<double3> outputs, JobHandle dependsOn)
        {
            return new TranslateDomain3Job()
            {
                Inputs = inputs,
                Axs = ax,
                Ays = ay,
                Azs = az,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct TranslateDomain4Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double4> Inputs;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;
        [ReadOnly] public NativeArray<double> Azs;
        [ReadOnly] public NativeArray<double> Aws;

        [WriteOnly] public NativeArray<double4> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var szPtr = (double*)Azs.GetUnsafeReadOnlyPtr() + startIndex;
            var swPtr = (double*)Aws.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double4*)Outputs.GetUnsafePtr() + startIndex;
            TranslateDomain(count, inputPtr, sxPtr, syPtr, szPtr, swPtr, outputPtr);
        }

        private unsafe void TranslateDomain(int count, double4* c, double* axs, double* ays, double* azs, double* aws, double4* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double4(c[i].x + axs[i], c[i].y + ays[i], c[i].z + azs[i], c[i].w + aws[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, NativeArray<double> ax, NativeArray<double> ay,
            NativeArray<double> az, NativeArray<double> aw, NativeArray<double4> outputs, JobHandle dependsOn)
        {
            return new TranslateDomain4Job()
            {
                Inputs = inputs,
                Axs = ax,
                Ays = ay,
                Azs = az,
                Aws = aw,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct TranslateDomain6Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double6> Inputs;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;
        [ReadOnly] public NativeArray<double> Azs;
        [ReadOnly] public NativeArray<double> Aws;
        [ReadOnly] public NativeArray<double> Aus;
        [ReadOnly] public NativeArray<double> Avs;

        [WriteOnly] public NativeArray<double6> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var szPtr = (double*)Azs.GetUnsafeReadOnlyPtr() + startIndex;
            var swPtr = (double*)Aws.GetUnsafeReadOnlyPtr() + startIndex;
            var suPtr = (double*)Aus.GetUnsafeReadOnlyPtr() + startIndex;
            var svPtr = (double*)Avs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double6*)Outputs.GetUnsafePtr() + startIndex;
            TranslateDomain(count, inputPtr, sxPtr, syPtr, szPtr, swPtr, suPtr, svPtr, outputPtr);
        }

        private unsafe void TranslateDomain(int count, double6* c, double* axs, double* ays, double* azs, double* aws, double* aus, double* avs, double6* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double6(c[i].x + axs[i], c[i].y + ays[i], c[i].z + azs[i], c[i].w + aws[i], c[i].u + aus[i], c[i].v + avs[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, NativeArray<double> ax, NativeArray<double> ay,
            NativeArray<double> az, NativeArray<double> aw, NativeArray<double> au, NativeArray<double> av,
            NativeArray<double6> outputs, JobHandle dependsOn)
        {
            return new TranslateDomain6Job()
            {
                Inputs = inputs,
                Axs = ax,
                Ays = ay,
                Azs = az,
                Aws = aw,
                Aus = au,
                Avs = av,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}