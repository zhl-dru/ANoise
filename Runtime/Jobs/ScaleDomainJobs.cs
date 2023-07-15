using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct ScaleDomain2Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double2> Inputs;
        [ReadOnly] public NativeArray<double> Sxs;
        [ReadOnly] public NativeArray<double> Sys;

        [WriteOnly] public NativeArray<double2> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Sxs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Sys.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double2*)Outputs.GetUnsafePtr() + startIndex;
            ScaleDomain(count, inputPtr, sxPtr, syPtr, outputPtr);
        }

        private unsafe void ScaleDomain(int count, double2* c, double* sxs, double* sys, double2* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double2(c[i].x * sxs[i], c[i].y * sys[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, NativeArray<double> sx, NativeArray<double> sy,
            NativeArray<double2> outputs, JobHandle dependsOn)
        {
            return new ScaleDomain2Job()
            {
                Inputs = inputs,
                Sxs = sx,
                Sys = sy,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct ScaleDomain3Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double3> Inputs;
        [ReadOnly] public NativeArray<double> Sxs;
        [ReadOnly] public NativeArray<double> Sys;
        [ReadOnly] public NativeArray<double> Szs;

        [WriteOnly] public NativeArray<double3> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Sxs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Sys.GetUnsafeReadOnlyPtr() + startIndex;
            var szPtr = (double*)Szs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double3*)Outputs.GetUnsafePtr() + startIndex;
            ScaleDomain(count, inputPtr, sxPtr, syPtr, szPtr, outputPtr);
        }

        private unsafe void ScaleDomain(int count, double3* c, double* sxs, double* sys, double* szs, double3* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double3(c[i].x * sxs[i], c[i].y * sys[i], c[i].z * szs[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, NativeArray<double> sx, NativeArray<double> sy,
            NativeArray<double> sz, NativeArray<double3> outputs, JobHandle dependsOn)
        {
            return new ScaleDomain3Job()
            {
                Inputs = inputs,
                Sxs = sx,
                Sys = sy,
                Szs = sz,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct ScaleDomain4Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double4> Inputs;
        [ReadOnly] public NativeArray<double> Sxs;
        [ReadOnly] public NativeArray<double> Sys;
        [ReadOnly] public NativeArray<double> Szs;
        [ReadOnly] public NativeArray<double> Sws;

        [WriteOnly] public NativeArray<double4> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Sxs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Sys.GetUnsafeReadOnlyPtr() + startIndex;
            var szPtr = (double*)Szs.GetUnsafeReadOnlyPtr() + startIndex;
            var swPtr = (double*)Sws.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double4*)Outputs.GetUnsafePtr() + startIndex;
            ScaleDomain(count, inputPtr, sxPtr, syPtr, szPtr, swPtr, outputPtr);
        }

        private unsafe void ScaleDomain(int count, double4* c, double* sxs, double* sys, double* szs, double* sws, double4* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double4(c[i].x * sxs[i], c[i].y * sys[i], c[i].z * szs[i], c[i].w * sws[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, NativeArray<double> sx, NativeArray<double> sy,
            NativeArray<double> sz, NativeArray<double> sw, NativeArray<double4> outputs, JobHandle dependsOn)
        {
            return new ScaleDomain4Job()
            {
                Inputs = inputs,
                Sxs = sx,
                Sys = sy,
                Szs = sz,
                Sws = sw,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct ScaleDomain6Job : IJobParallelForBatch
    {
        [ReadOnly]
        public NativeArray<double6> Inputs;
        [ReadOnly] public NativeArray<double> Sxs;
        [ReadOnly] public NativeArray<double> Sys;
        [ReadOnly] public NativeArray<double> Szs;
        [ReadOnly] public NativeArray<double> Sws;
        [ReadOnly] public NativeArray<double> Sus;
        [ReadOnly] public NativeArray<double> Svs;

        [WriteOnly] public NativeArray<double6> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var sxPtr = (double*)Sxs.GetUnsafeReadOnlyPtr() + startIndex;
            var syPtr = (double*)Sys.GetUnsafeReadOnlyPtr() + startIndex;
            var szPtr = (double*)Szs.GetUnsafeReadOnlyPtr() + startIndex;
            var swPtr = (double*)Sws.GetUnsafeReadOnlyPtr() + startIndex;
            var suPtr = (double*)Sus.GetUnsafeReadOnlyPtr() + startIndex;
            var svPtr = (double*)Svs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double6*)Outputs.GetUnsafePtr() + startIndex;
            ScaleDomain(count, inputPtr, sxPtr, syPtr, szPtr, swPtr, suPtr, svPtr, outputPtr);
        }

        private unsafe void ScaleDomain(int count, double6* c, double* sxs, double* sys, double* szs, double* sws, double* sus, double* svs, double6* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = new double6(c[i].x * sxs[i], c[i].y * sys[i], c[i].z * szs[i], c[i].w * sws[i], c[i].u * sus[i], c[i].v * svs[i]);
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, NativeArray<double> sx, NativeArray<double> sy,
            NativeArray<double> sz, NativeArray<double> sw, NativeArray<double> su, NativeArray<double> sv,
            NativeArray<double6> outputs, JobHandle dependsOn)
        {
            return new ScaleDomain6Job()
            {
                Inputs = inputs,
                Sxs = sx,
                Sys = sy,
                Szs = sz,
                Sws = sw,
                Sus = su,
                Svs = sv,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}