using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct Cellular2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public uint Seed;
        [ReadOnly] public ECellularDistanceFunction DistanceType;
        [ReadOnly] public double F1, F2, F3, F4;
        [ReadOnly] public double Frequency;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            CellularGet(count, inputPtr, outputPtr);
        }

        private unsafe void CellularGet(int count, double2* inputs, double* outputs)
        {
            double4 f = new double4();
            double4 disp = new double4();
            for (int i = 0; i < count; i++)
            {
                double x = inputs[i].x;
                double y = inputs[i].y;
                x *= Frequency;
                y *= Frequency;

                Noise.cellular_function2D(x, y, Seed, ref f, ref disp, DistanceType);
                outputs[i] = f[0] * F1 + f[1] * F2 + f[2] * F3 + f[3] * F4;
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, uint seed, ECellularDistanceFunction distype,
            double f1, double f2, double f3, double f4, double frequency, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Cellular2Job()
            {
                Inputs = inputs,
                Seed = seed,
                DistanceType = distype,
                F1 = f1,
                F2 = f2,
                F3 = f3,
                F4 = f4,
                Frequency = frequency,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Cellular3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public uint Seed;
        [ReadOnly] public ECellularDistanceFunction DistanceType;
        [ReadOnly] public double F1, F2, F3, F4;
        [ReadOnly] public double Frequency;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            CellularGet(count, inputPtr, outputPtr);
        }

        private unsafe void CellularGet(int count, double3* inputs, double* outputs)
        {
            double4 f = new double4();
            double4 disp = new double4();
            for (int i = 0; i < count; i++)
            {
                double x = inputs[i].x;
                double y = inputs[i].y;
                double z = inputs[i].z;
                x *= Frequency;
                y *= Frequency;
                z *= Frequency;

                Noise.cellular_function3D(x, y, z, Seed, ref f, ref disp, DistanceType);
                outputs[i] = f[0] * F1 + f[1] * F2 + f[2] * F3 + f[3] * F4;
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, uint seed, ECellularDistanceFunction distype,
            double f1, double f2, double f3, double f4, double frequency, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Cellular3Job()
            {
                Inputs = inputs,
                Seed = seed,
                DistanceType = distype,
                F1 = f1,
                F2 = f2,
                F3 = f3,
                F4 = f4,
                Frequency = frequency,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Cellular4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public uint Seed;
        [ReadOnly] public ECellularDistanceFunction DistanceType;
        [ReadOnly] public double F1, F2, F3, F4;
        [ReadOnly] public double Frequency;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            CellularGet(count, inputPtr, outputPtr);
        }

        private unsafe void CellularGet(int count, double4* inputs, double* outputs)
        {
            double4 f = new double4();
            double4 disp = new double4();
            for (int i = 0; i < count; i++)
            {
                double x = inputs[i].x;
                double y = inputs[i].y;
                double z = inputs[i].z;
                double w = inputs[i].w;
                x *= Frequency;
                y *= Frequency;
                z *= Frequency;
                w *= Frequency;

                Noise.cellular_function4D(x, y, z, w, Seed, ref f, ref disp, DistanceType);
                outputs[i] = f[0] * F1 + f[1] * F2 + f[2] * F3 + f[3] * F4;
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, uint seed, ECellularDistanceFunction distype,
            double f1, double f2, double f3, double f4, double frequency, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Cellular4Job()
            {
                Inputs = inputs,
                Seed = seed,
                DistanceType = distype,
                F1 = f1,
                F2 = f2,
                F3 = f3,
                F4 = f4,
                Frequency = frequency,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct Cellular6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public uint Seed;
        [ReadOnly] public ECellularDistanceFunction DistanceType;
        [ReadOnly] public double F1, F2, F3, F4;
        [ReadOnly] public double Frequency;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            CellularGet(count, inputPtr, outputPtr);
        }

        private unsafe void CellularGet(int count, double6* inputs, double* outputs)
        {
            double4 f = new double4();
            double4 disp = new double4();
            for (int i = 0; i < count; i++)
            {
                double x = inputs[i].x;
                double y = inputs[i].y;
                double z = inputs[i].z;
                double w = inputs[i].w;
                double u = inputs[i].u;
                double v = inputs[i].v;
                x *= Frequency;
                y *= Frequency;
                z *= Frequency;
                w *= Frequency;
                u *= Frequency;
                v *= Frequency;

                Noise.cellular_function6D(x, y, z, w, u, v, Seed, ref f, ref disp, DistanceType);
                outputs[i] = f[0] * F1 + f[1] * F2 + f[2] * F3 + f[3] * F4;
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, uint seed, ECellularDistanceFunction distype,
            double f1, double f2, double f3, double f4, double frequency, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new Cellular6Job()
            {
                Inputs = inputs,
                Seed = seed,
                DistanceType = distype,
                F1 = f1,
                F2 = f2,
                F3 = f3,
                F4 = f4,
                Frequency = frequency,
                Outputs = outputs
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}