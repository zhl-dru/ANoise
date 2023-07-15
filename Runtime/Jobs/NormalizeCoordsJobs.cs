using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct NormalizeCoords2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public NativeArray<double> Length;

        [WriteOnly] public NativeArray<double2> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var lengthPtr = (double*)Length.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double2*)Outputs.GetUnsafePtr() + startIndex;
            NormalizeCoords(count, inputPtr, lengthPtr, outputPtr);
        }

        private unsafe void NormalizeCoords(int count, double2* c, double* l, double2* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, lv = l[i];
                if (x == 0 && y == 0)
                {
                    r[i] = new double2();
                }
                else
                {
                    double len = math.sqrt(x * x + y * y);
                    r[i] = new double2(x / len * lv, y / len * lv);
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, NativeArray<double> length, NativeArray<double2> outputs,
            JobHandle dependsOn)
        {
            return new NormalizeCoords2Job()
            {
                Inputs = inputs,
                Length = length,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct NormalizeCoords3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public NativeArray<double> Length;

        [WriteOnly] public NativeArray<double3> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var lengthPtr = (double*)Length.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double3*)Outputs.GetUnsafePtr() + startIndex;
            NormalizeCoords(count, inputPtr, lengthPtr, outputPtr);
        }

        private unsafe void NormalizeCoords(int count, double3* c, double* l, double3* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, z = c[i].z, lv = l[i];
                if (x == 0 && y == 0 && z == 0)
                {
                    r[i] = new double3();
                }
                else
                {
                    double len = math.sqrt(x * x + y * y + z * z);
                    r[i] = new double3(x / len * lv, y / len * lv, z / len * lv);
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, NativeArray<double> length, NativeArray<double3> outputs,
            JobHandle dependsOn)
        {
            return new NormalizeCoords3Job()
            {
                Inputs = inputs,
                Length = length,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct NormalizeCoords4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public NativeArray<double> Length;

        [WriteOnly] public NativeArray<double4> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var lengthPtr = (double*)Length.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double4*)Outputs.GetUnsafePtr() + startIndex;
            NormalizeCoords(count, inputPtr, lengthPtr, outputPtr);
        }

        private unsafe void NormalizeCoords(int count, double4* c, double* l, double4* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, z = c[i].z, w = c[i].w, lv = l[i];
                if (x == 0 && y == 0 && z == 0 && w == 0)
                {
                    r[i] = new double4();
                }
                else
                {
                    double len = math.sqrt(x * x + y * y + z * z + w * w);
                    r[i] = new double4(x / len * lv, y / len * lv, z / len * lv, w / len * lv);
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, NativeArray<double> length, NativeArray<double4> outputs,
            JobHandle dependsOn)
        {
            return new NormalizeCoords4Job()
            {
                Inputs = inputs,
                Length = length,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct NormalizeCoords6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public NativeArray<double> Length;

        [WriteOnly] public NativeArray<double6> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var lengthPtr = (double*)Length.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double6*)Outputs.GetUnsafePtr() + startIndex;
            NormalizeCoords(count, inputPtr, lengthPtr, outputPtr);
        }

        private unsafe void NormalizeCoords(int count, double6* c, double* l, double6* r)
        {
            for (int i = 0; i < count; i++)
            {
                double x = c[i].x, y = c[i].y, z = c[i].z, w = c[i].w, u = c[i].u, v = c[i].v, lv = l[i];
                if (x == 0 && y == 0 && z == 0 && w == 0 && u == 0 && v == 0)
                {
                    r[i] = new double6();
                }
                else
                {
                    double len = math.sqrt(x * x + y * y + z * z + w * w + u * u + v * v);
                    r[i] = new double6(x / len * lv, y / len * lv, z / len * lv, w / len * lv, u / len * lv, v / len * lv);
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, NativeArray<double> length, NativeArray<double6> outputs,
            JobHandle dependsOn)
        {
            return new NormalizeCoords6Job()
            {
                Inputs = inputs,
                Length = length,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}