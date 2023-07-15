using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct RotateDomain2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public NativeArray<double> Angles;

        [WriteOnly] public NativeArray<double2> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var anglesPtr = (double*)Angles.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double2*)Outputs.GetUnsafePtr() + startIndex;
            CalculateRotMatrix(count, inputPtr, anglesPtr, outputPtr);
        }

        private unsafe void CalculateRotMatrix(int count, double2* c, double* ans, double2* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y;
                double angle = ans[i] * 360.0 * 3.14159265 / 180.0;
                double cosangle = math.cos(angle);
                double sinangle = math.sin(angle);

                double nx = x * cosangle + y * sinangle;
                double ny = x * sinangle + y * cosangle;

                r[i] = new double2(nx, ny);
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, NativeArray<double> angles, NativeArray<double2> outputs,
            JobHandle dependsOn)
        {
            return new RotateDomain2Job()
            {
                Inputs = inputs,
                Angles = angles,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct RotateDomain3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public NativeArray<double> Angles;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;
        [ReadOnly] public NativeArray<double> Azs;

        [WriteOnly] public NativeArray<double3> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var anglesPtr = (double*)Angles.GetUnsafeReadOnlyPtr() + startIndex;
            var axsPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var aysPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var azsPtr = (double*)Azs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double3*)Outputs.GetUnsafePtr() + startIndex;
            CalculateRotMatrix(count, inputPtr, anglesPtr, axsPtr, aysPtr, azsPtr, outputPtr);
        }

        private unsafe void CalculateRotMatrix(int count, double3* c, double* ans, double* axs, double* ays, double* azs, double3* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, z = c[i].z;
                double ax = axs[i], ay = ays[i], az = azs[i];
                double angle = ans[i] * 360.0 * 3.14159265 / 180.0;
                double cosangle = math.cos(angle);
                double sinangle = math.sin(angle);

                double rm0 = 1.0 + (1.0 - cosangle) * (ax * ax - 1.0);
                double rm1 = -az * sinangle + (1.0 - cosangle) * ax * ay;
                double rm2 = ay * sinangle + (1.0 - cosangle) * ax * az;

                double rm3 = az * sinangle + (1.0 - cosangle) * ax * ay;
                double rm4 = 1.0 + (1.0 - cosangle) * (ay * ay - 1.0);
                double rm5 = -ax * sinangle + (1.0 - cosangle) * ay * az;

                double rm6 = -ay * sinangle + (1.0 - cosangle) * ax * az;
                double rm7 = ax * sinangle + (1.0 - cosangle) * ay * az;
                double rm8 = 1.0 + (1.0 - cosangle) * (az * az - 1.0);

                double nx = x * rm0 + y * rm1 + z * rm2;
                double ny = x * rm3 + y * rm4 + z * rm5;
                double nz = x * rm6 + y * rm7 + z * rm8;

                r[i] = new double3(nx, ny, nz);
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, NativeArray<double> angles,
            NativeArray<double> axs, NativeArray<double> ays, NativeArray<double> azs,
            NativeArray<double3> outputs,
            JobHandle dependsOn)
        {
            return new RotateDomain3Job()
            {
                Inputs = inputs,
                Angles = angles,
                Axs = axs,
                Ays = ays,
                Azs = azs,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct RotateDomain4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public NativeArray<double> Angles;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;
        [ReadOnly] public NativeArray<double> Azs;

        [WriteOnly] public NativeArray<double4> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var anglesPtr = (double*)Angles.GetUnsafeReadOnlyPtr() + startIndex;
            var axsPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var aysPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var azsPtr = (double*)Azs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double4*)Outputs.GetUnsafePtr() + startIndex;
            CalculateRotMatrix(count, inputPtr, anglesPtr, axsPtr, aysPtr, azsPtr, outputPtr);
        }

        private unsafe void CalculateRotMatrix(int count, double4* c, double* ans, double* axs, double* ays, double* azs, double4* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, z = c[i].z, w = c[i].w;
                double ax = axs[i], ay = ays[i], az = azs[i];
                double angle = ans[i] * 360.0 * 3.14159265 / 180.0;
                double cosangle = math.cos(angle);
                double sinangle = math.sin(angle);

                double rm0 = 1.0 + (1.0 - cosangle) * (ax * ax - 1.0);
                double rm1 = -az * sinangle + (1.0 - cosangle) * ax * ay;
                double rm2 = ay * sinangle + (1.0 - cosangle) * ax * az;

                double rm3 = az * sinangle + (1.0 - cosangle) * ax * ay;
                double rm4 = 1.0 + (1.0 - cosangle) * (ay * ay - 1.0);
                double rm5 = -ax * sinangle + (1.0 - cosangle) * ay * az;

                double rm6 = -ay * sinangle + (1.0 - cosangle) * ax * az;
                double rm7 = ax * sinangle + (1.0 - cosangle) * ay * az;
                double rm8 = 1.0 + (1.0 - cosangle) * (az * az - 1.0);

                double nx = x * rm0 + y * rm1 + z * rm2;
                double ny = x * rm3 + y * rm4 + z * rm5;
                double nz = x * rm6 + y * rm7 + z * rm8;

                r[i] = new double4(nx, ny, nz, w);
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, NativeArray<double> angles,
            NativeArray<double> axs, NativeArray<double> ays, NativeArray<double> azs,
            NativeArray<double4> outputs,
            JobHandle dependsOn)
        {
            return new RotateDomain4Job()
            {
                Inputs = inputs,
                Angles = angles,
                Axs = axs,
                Ays = ays,
                Azs = azs,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct RotateDomain6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public NativeArray<double> Angles;
        [ReadOnly] public NativeArray<double> Axs;
        [ReadOnly] public NativeArray<double> Ays;
        [ReadOnly] public NativeArray<double> Azs;

        [WriteOnly] public NativeArray<double6> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var anglesPtr = (double*)Angles.GetUnsafeReadOnlyPtr() + startIndex;
            var axsPtr = (double*)Axs.GetUnsafeReadOnlyPtr() + startIndex;
            var aysPtr = (double*)Ays.GetUnsafeReadOnlyPtr() + startIndex;
            var azsPtr = (double*)Azs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double6*)Outputs.GetUnsafePtr() + startIndex;
            CalculateRotMatrix(count, inputPtr, anglesPtr, axsPtr, aysPtr, azsPtr, outputPtr);
        }

        private unsafe void CalculateRotMatrix(int count, double6* c, double* ans, double* axs, double* ays, double* azs, double6* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, z = c[i].z, w = c[i].w, u = c[i].u, v = c[i].v;
                double ax = axs[i], ay = ays[i], az = azs[i];
                double angle = ans[i] * 360.0 * 3.14159265 / 180.0;
                double cosangle = math.cos(angle);
                double sinangle = math.sin(angle);

                double rm0 = 1.0 + (1.0 - cosangle) * (ax * ax - 1.0);
                double rm1 = -az * sinangle + (1.0 - cosangle) * ax * ay;
                double rm2 = ay * sinangle + (1.0 - cosangle) * ax * az;

                double rm3 = az * sinangle + (1.0 - cosangle) * ax * ay;
                double rm4 = 1.0 + (1.0 - cosangle) * (ay * ay - 1.0);
                double rm5 = -ax * sinangle + (1.0 - cosangle) * ay * az;

                double rm6 = -ay * sinangle + (1.0 - cosangle) * ax * az;
                double rm7 = ax * sinangle + (1.0 - cosangle) * ay * az;
                double rm8 = 1.0 + (1.0 - cosangle) * (az * az - 1.0);

                double nx = x * rm0 + y * rm1 + z * rm2;
                double ny = x * rm3 + y * rm4 + z * rm5;
                double nz = x * rm6 + y * rm7 + z * rm8;

                r[i] = new double6(nx, ny, nz, w, u, v);
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, NativeArray<double> angles,
            NativeArray<double> axs, NativeArray<double> ays, NativeArray<double> azs,
            NativeArray<double6> outputs,
            JobHandle dependsOn)
        {
            return new RotateDomain6Job()
            {
                Inputs = inputs,
                Angles = angles,
                Axs = axs,
                Ays = ays,
                Azs = azs,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}