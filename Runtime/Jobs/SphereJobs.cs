using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct Sphere2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public NativeArray<double> Cxs;
        [ReadOnly] public NativeArray<double> Cys;
        [ReadOnly] public NativeArray<double> Radius;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var cxPtr = (double*)Cxs.GetUnsafeReadOnlyPtr() + startIndex;
            var cyPtr = (double*)Cys.GetUnsafeReadOnlyPtr() + startIndex;
            var rPtr = (double*)Radius.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Sphere(count, inputPtr, cxPtr, cyPtr, rPtr, outputPtr);
        }

        private unsafe void Sphere(int count, double2* c, double* cxs, double* cys, double* rads, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                double dx = c[i].x - cxs[i];
                double dy = c[i].y - cys[i];
                double len = math.sqrt(dx * dx + dy * dy);
                double rad = rads[i];
                double ii = (rad - len) / rad;
                if (ii < 0) ii = 0;
                if (ii > 1) ii = 1;
                r[i] = ii;
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, NativeArray<double> cx, NativeArray<double> cy,
            NativeArray<double> rad, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Sphere2Job()
            {
                Inputs = inputs,
                Cxs = cx,
                Cys = cy,
                Radius = rad,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Sphere3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public NativeArray<double> Cxs;
        [ReadOnly] public NativeArray<double> Cys;
        [ReadOnly] public NativeArray<double> Czs;
        [ReadOnly] public NativeArray<double> Radius;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var cxPtr = (double*)Cxs.GetUnsafeReadOnlyPtr() + startIndex;
            var cyPtr = (double*)Cys.GetUnsafeReadOnlyPtr() + startIndex;
            var czPtr = (double*)Czs.GetUnsafeReadOnlyPtr() + startIndex;
            var rPtr = (double*)Radius.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Sphere(count, inputPtr, cxPtr, cyPtr, czPtr, rPtr, outputPtr);
        }

        private unsafe void Sphere(int count, double3* c, double* cxs, double* cys, double* czs, double* rads, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                double dx = c[i].x - cxs[i];
                double dy = c[i].y - cys[i];
                double dz = c[i].z - czs[i];
                double len = math.sqrt(dx * dx + dy * dy + dz * dz);
                double rad = rads[i];
                double ii = (rad - len) / rad;
                if (ii < 0) ii = 0;
                if (ii > 1) ii = 1;
                r[i] = ii;
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, NativeArray<double> cx, NativeArray<double> cy,
            NativeArray<double> cz, NativeArray<double> rad, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Sphere3Job()
            {
                Inputs = inputs,
                Cxs = cx,
                Cys = cy,
                Czs = cz,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Sphere4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public NativeArray<double> Cxs;
        [ReadOnly] public NativeArray<double> Cys;
        [ReadOnly] public NativeArray<double> Czs;
        [ReadOnly] public NativeArray<double> Cws;
        [ReadOnly] public NativeArray<double> Radius;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var cxPtr = (double*)Cxs.GetUnsafeReadOnlyPtr() + startIndex;
            var cyPtr = (double*)Cys.GetUnsafeReadOnlyPtr() + startIndex;
            var czPtr = (double*)Czs.GetUnsafeReadOnlyPtr() + startIndex;
            var cwPtr = (double*)Cws.GetUnsafeReadOnlyPtr() + startIndex;
            var rPtr = (double*)Radius.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Sphere(count, inputPtr, cxPtr, cyPtr, czPtr, cwPtr, rPtr, outputPtr);
        }

        private unsafe void Sphere(int count, double4* c, double* cxs, double* cys, double* czs, double* cws, double* rads, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                double dx = c[i].x - cxs[i];
                double dy = c[i].y - cys[i];
                double dz = c[i].z - czs[i];
                double dw = c[i].w - cws[i];
                double len = math.sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
                double rad = rads[i];
                double ii = (rad - len) / rad;
                if (ii < 0) ii = 0;
                if (ii > 1) ii = 1;
                r[i] = ii;
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, NativeArray<double> cx, NativeArray<double> cy,
            NativeArray<double> cz, NativeArray<double> cw, NativeArray<double> rad, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Sphere4Job()
            {
                Inputs = inputs,
                Cxs = cx,
                Cys = cy,
                Czs = cz,
                Cws = cw,
                Radius = rad,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Sphere6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public NativeArray<double> Cxs;
        [ReadOnly] public NativeArray<double> Cys;
        [ReadOnly] public NativeArray<double> Czs;
        [ReadOnly] public NativeArray<double> Cws;
        [ReadOnly] public NativeArray<double> Cus;
        [ReadOnly] public NativeArray<double> Cvs;
        [ReadOnly] public NativeArray<double> Radius;

        [WriteOnly] public NativeArray<double> Outputs;
        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var cxPtr = (double*)Cxs.GetUnsafeReadOnlyPtr() + startIndex;
            var cyPtr = (double*)Cys.GetUnsafeReadOnlyPtr() + startIndex;
            var czPtr = (double*)Czs.GetUnsafeReadOnlyPtr() + startIndex;
            var cwPtr = (double*)Cws.GetUnsafeReadOnlyPtr() + startIndex;
            var cuPtr = (double*)Cus.GetUnsafeReadOnlyPtr() + startIndex;
            var cvPtr = (double*)Cvs.GetUnsafeReadOnlyPtr() + startIndex;
            var rPtr = (double*)Radius.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            Sphere(count, inputPtr, cxPtr, cyPtr, czPtr, cwPtr, cuPtr, cvPtr, rPtr, outputPtr);
        }

        private unsafe void Sphere(int count, double6* c, double* cxs, double* cys, double* czs, double* cws, double* cus, double* cvs, double* rads, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                double dx = c[i].x - cxs[i];
                double dy = c[i].y - cys[i];
                double dz = c[i].z - czs[i];
                double dw = c[i].w - cws[i];
                double du = c[i].u - cus[i];
                double dv = c[i].v - cvs[i];
                double len = math.sqrt(dx * dx + dy * dy + dz * dz + dw * dw + du * du + dv * dv);
                double rad = rads[i];
                double ii = (rad - len) / rad;
                if (ii < 0) ii = 0;
                if (ii > 1) ii = 1;
                r[i] = ii;
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, NativeArray<double> cx, NativeArray<double> cy,
            NativeArray<double> cz, NativeArray<double> cw, NativeArray<double> cu, NativeArray<double> cv, NativeArray<double> rad,
            NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Sphere6Job()
            {
                Inputs = inputs,
                Cxs = cx,
                Cys = cy,
                Czs = cz,
                Cws = cw,
                Cus = cu,
                Cvs = cv,
                Radius = rad,
                Outputs = outputs,
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}
