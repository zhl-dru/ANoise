using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct Fractal2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public DFractal Data;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FractalGet(count, inputPtr, outputPtr);
        }
        private unsafe void FractalGet(int count, double2* inputs, double* outputs)
        {
            switch (Data.FractalType)
            {
                case EFractalTypes.FBM: FBMGet(count, inputs, outputs); break;
                case EFractalTypes.RIDGEDMULTI: RidgedMultiGet(count, inputs, outputs); break;
                case EFractalTypes.BILLOW: BillowGet(count, inputs, outputs); break;
                case EFractalTypes.MULTI: MultiGet(count, inputs, outputs); break;
                case EFractalTypes.HYBRIDMULTI: HybridMultiGet(count, inputs, outputs); break;
                case EFractalTypes.DECARPENTIERSWISS: DeCarpentierSwissGet(count, inputs, outputs); break;
                default: FBMGet(count, inputs, outputs); break;
            }
        }
        private unsafe void FBMGet(int count, double2* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;

                x *= Data.Frequency;
                y *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, i);
                    sum += n * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void MultiGet(int count, double2* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                x *= Data.Frequency;
                y *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    value *= BaseNoiseGet(x, y, i) * Data.MagicNumbers[i * 14] + 1.0;
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                }

                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }

        }
        private unsafe void BillowGet(int count, double2* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0.0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                x *= Data.Frequency;
                y *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, i);
                    sum += (2.0 * math.abs(n) - 1.0) * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void RidgedMultiGet(int count, double2* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double result = 0.0, signal;
                double x = inputs[c].x;
                double y = inputs[c].y;
                x *= Data.Frequency;
                y *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    signal = BaseNoiseGet(x, y, i);
                    signal = Data.Offset - math.abs(signal);
                    signal *= signal;
                    result += signal * Data.MagicNumbers[i * 14];

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                }
                outputs[c] = result * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void HybridMultiGet(int count, double2* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 0.0, signal, weight = 0.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                x *= Data.Frequency;
                y *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    if (i == 0)
                    {
                        value = BaseNoiseGet(x, y, 0) + Data.Offset;
                        weight = Data.Gain * value;
                    }
                    else
                    {
                        if (weight > 1.0) weight = 1.0;
                        signal = (BaseNoiseGet(x, y, i) + Data.Offset) * Data.MagicNumbers[i * 14];
                        value += weight * signal;
                        weight *= Data.Gain * signal;
                    }
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                }
                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void DeCarpentierSwissGet(int count, double2* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;

                double dx_sum = 0;
                double dy_sum = 0;

                double x = inputs[c].x;
                double y = inputs[c].y;

                x *= Data.Frequency;
                y *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, i);
                    double dx = BaseNoiseGetDx(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, i);
                    double dy = BaseNoiseGetDy(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, i);
                    sum += amp * (1.0 - math.abs(n));
                    dx_sum += amp * dx * -n;
                    dy_sum += amp * dy * -n;
                    amp *= Data.Gain * math.clamp(sum, 0.0, 1.0);
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }

        private double BaseNoiseGet(double x, double y, int index)
        {
            double nx, ny;
            double cos2d = Data.MagicNumbers[index * 14 + 3];
            double sin2d = Data.MagicNumbers[index * 14 + 4];
            nx = x * cos2d - y * sin2d;
            ny = y * cos2d + x * sin2d;
            uint seed = (uint)(Data.Seed + index * 300);
            EInterpTypes interp = Data.InterpType;
            switch (Data.BasisType)
            {
                case EBasisTypes.VALUE: return Noise.value_noise2D(nx, ny, seed, interp);
                case EBasisTypes.GRADIENT: return Noise.gradient_noise2D(nx, ny, seed, interp);
                case EBasisTypes.GRADVAL: return Noise.gradval_noise2D(nx, ny, seed, interp);
                case EBasisTypes.WHITE: return Noise.white_noise2D(nx, ny, seed);
                case EBasisTypes.SIMPLEX: return Noise.simplex_noise2D(nx, ny, seed);
                default: return Noise.gradient_noise2D(nx, ny, seed, interp);
            }
        }

        private double BaseNoiseGetDx(double x, double y, int index)
        {
            return (BaseNoiseGet(x - Data.Spacing, y, index) - BaseNoiseGet(x + Data.Spacing, y, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDy(double x, double y, int index)
        {
            return (BaseNoiseGet(x, y - Data.Spacing, index) - BaseNoiseGet(x, y + Data.Spacing, index)) / Data.Spacing;
        }


        public static JobHandle JobHandle(NativeArray<double2> inputs, NativeArray<double> outputs, DFractal data,
            JobHandle dependsOn)
        {
            return new Fractal2Job()
            {
                Inputs = inputs,
                Outputs = outputs,
                Data = data
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }

    [BurstCompile]
    public struct Fractal3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public DFractal Data;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FractalGet(count, inputPtr, outputPtr);
        }
        private unsafe void FractalGet(int count, double3* inputs, double* outputs)
        {
            switch (Data.FractalType)
            {
                case EFractalTypes.FBM: FBMGet(count, inputs, outputs); break;
                case EFractalTypes.RIDGEDMULTI: RidgedMultiGet(count, inputs, outputs); break;
                case EFractalTypes.BILLOW: BillowGet(count, inputs, outputs); break;
                case EFractalTypes.MULTI: MultiGet(count, inputs, outputs); break;
                case EFractalTypes.HYBRIDMULTI: HybridMultiGet(count, inputs, outputs); break;
                case EFractalTypes.DECARPENTIERSWISS: DeCarpentierSwissGet(count, inputs, outputs); break;
                default: FBMGet(count, inputs, outputs); break;
            }
        }
        private unsafe void FBMGet(int count, double3* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, z, i);
                    sum += n * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void MultiGet(int count, double3* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    value *= BaseNoiseGet(x, y, z, i) * Data.MagicNumbers[i * 14] + 1.0;
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                }

                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }

        }
        private unsafe void BillowGet(int count, double3* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0.0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, z, i);
                    sum += (2.0 * math.abs(n) - 1.0) * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void RidgedMultiGet(int count, double3* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double result = 0.0, signal;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    signal = BaseNoiseGet(x, y, z, i);
                    signal = Data.Offset - math.abs(signal);
                    signal *= signal;
                    result += signal * Data.MagicNumbers[i * 14];

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                }
                outputs[c] = result * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void HybridMultiGet(int count, double3* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 0.0, signal, weight = 0.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    if (i == 0)
                    {
                        value = BaseNoiseGet(x, y, z, 0) + Data.Offset;
                        weight = Data.Gain * value;
                    }
                    else
                    {
                        if (weight > 1.0) weight = 1.0;
                        signal = (BaseNoiseGet(x, y, z, i) + Data.Offset) * Data.MagicNumbers[i * 14];
                        value += weight * signal;
                        weight *= Data.Gain * signal;
                    }
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                }
                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void DeCarpentierSwissGet(int count, double3* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;

                double dx_sum = 0;
                double dy_sum = 0;
                double dz_sum = 0;

                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, i);
                    double dx = BaseNoiseGetDx(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, i);
                    double dy = BaseNoiseGetDy(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, i);
                    double dz = BaseNoiseGetDz(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, i);
                    sum += amp * (1.0 - math.abs(n));
                    dx_sum += amp * dx * -n;
                    dy_sum += amp * dy * -n;
                    dz_sum += amp * dz * -n;
                    amp *= Data.Gain * math.clamp(sum, 0.0, 1.0);
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }

        private double BaseNoiseGet(double x, double y, double z, int index)
        {
            double nx, ny, nz;
            nx = (Data.MagicNumbers[index * 14 + 5] * x) + (Data.MagicNumbers[index * 14 + 6] * y) + (Data.MagicNumbers[index * 14 + 7] * z);
            ny = (Data.MagicNumbers[index * 14 + 8] * x) + (Data.MagicNumbers[index * 14 + 9] * y) + (Data.MagicNumbers[index * 14 + 10] * z);
            nz = (Data.MagicNumbers[index * 14 + 11] * x) + (Data.MagicNumbers[index * 14 + 12] * y) + (Data.MagicNumbers[index * 14 + 13] * z);
            uint seed = (uint)(Data.Seed + index * 300);
            switch (Data.BasisType)
            {
                case EBasisTypes.VALUE: return Noise.value_noise3D(nx, ny, nz, seed, Data.InterpType);
                case EBasisTypes.GRADIENT: return Noise.gradient_noise3D(nx, ny, nz, seed, Data.InterpType);
                case EBasisTypes.GRADVAL: return Noise.gradval_noise3D(nx, ny, nz, seed, Data.InterpType);
                case EBasisTypes.WHITE: return Noise.white_noise3D(nx, ny, nz, seed);
                case EBasisTypes.SIMPLEX: return Noise.simplex_noise3D(nx, ny, nz, seed);
                default: return Noise.gradient_noise3D(nx, ny, nz, seed, Data.InterpType);
            }
        }

        private double BaseNoiseGetDx(double x, double y, double z, int index)
        {
            return (BaseNoiseGet(x - Data.Spacing, y, z, index) - BaseNoiseGet(x + Data.Spacing, y, z, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDy(double x, double y, double z, int index)
        {
            return (BaseNoiseGet(x, y - Data.Spacing, z, index) - BaseNoiseGet(x, y + Data.Spacing, z, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDz(double x, double y, double z, int index)
        {
            return (BaseNoiseGet(x, y, z - Data.Spacing, index) - BaseNoiseGet(x, y, z + Data.Spacing, index)) / Data.Spacing;
        }


        public static JobHandle JobHandle(NativeArray<double3> inputs, NativeArray<double> outputs, DFractal data,
            JobHandle dependsOn)
        {
            return new Fractal3Job()
            {
                Inputs = inputs,
                Outputs = outputs,
                Data = data
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }

    [BurstCompile]
    public struct Fractal4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public DFractal Data;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FractalGet(count, inputPtr, outputPtr);
        }
        private unsafe void FractalGet(int count, double4* inputs, double* outputs)
        {
            switch (Data.FractalType)
            {
                case EFractalTypes.FBM: FBMGet(count, inputs, outputs); break;
                case EFractalTypes.RIDGEDMULTI: RidgedMultiGet(count, inputs, outputs); break;
                case EFractalTypes.BILLOW: BillowGet(count, inputs, outputs); break;
                case EFractalTypes.MULTI: MultiGet(count, inputs, outputs); break;
                case EFractalTypes.HYBRIDMULTI: HybridMultiGet(count, inputs, outputs); break;
                case EFractalTypes.DECARPENTIERSWISS: DeCarpentierSwissGet(count, inputs, outputs); break;
                default: FBMGet(count, inputs, outputs); break;
            }
        }
        private unsafe void FBMGet(int count, double4* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, z, w, i);
                    sum += n * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void MultiGet(int count, double4* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    value *= BaseNoiseGet(x, y, z, w, i) * Data.MagicNumbers[i * 14] + 1.0;
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                }

                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }

        }
        private unsafe void BillowGet(int count, double4* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0.0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, z, w, i);
                    sum += (2.0 * math.abs(n) - 1.0) * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void RidgedMultiGet(int count, double4* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double result = 0.0, signal;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    signal = BaseNoiseGet(x, y, z, w, i);
                    signal = Data.Offset - math.abs(signal);
                    signal *= signal;
                    result += signal * Data.MagicNumbers[i * 14];

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                }
                outputs[c] = result * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void HybridMultiGet(int count, double4* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 0.0, signal, weight = 0.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    if (i == 0)
                    {
                        value = BaseNoiseGet(x, y, z, w, 0) + Data.Offset;
                        weight = Data.Gain * value;
                    }
                    else
                    {
                        if (weight > 1.0) weight = 1.0;
                        signal = (BaseNoiseGet(x, y, z, w, i) + Data.Offset) * Data.MagicNumbers[i * 14];
                        value += weight * signal;
                        weight *= Data.Gain * signal;
                    }
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                }
                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void DeCarpentierSwissGet(int count, double4* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;

                double dx_sum = 0;
                double dy_sum = 0;
                double dz_sum = 0;
                double dw_sum = 0;

                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, i);
                    double dx = BaseNoiseGetDx(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, i);
                    double dy = BaseNoiseGetDy(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, i);
                    double dz = BaseNoiseGetDz(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, i);
                    double dw = BaseNoiseGetDw(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, i);
                    sum += amp * (1.0 - math.abs(n));
                    dx_sum += amp * dx * -n;
                    dy_sum += amp * dy * -n;
                    dz_sum += amp * dz * -n;
                    dw_sum += amp * dw * -n;
                    amp *= Data.Gain * math.clamp(sum, 0.0, 1.0);
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }

        private double BaseNoiseGet(double x, double y, double z, double w, int index)
        {
            double nx, ny, nz;
            nx = (Data.MagicNumbers[index * 14 + 5] * x) + (Data.MagicNumbers[index * 14 + 6] * y) + (Data.MagicNumbers[index * 14 + 7] * z);
            ny = (Data.MagicNumbers[index * 14 + 8] * x) + (Data.MagicNumbers[index * 14 + 9] * y) + (Data.MagicNumbers[index * 14 + 10] * z);
            nz = (Data.MagicNumbers[index * 14 + 11] * x) + (Data.MagicNumbers[index * 14 + 12] * y) + (Data.MagicNumbers[index * 14 + 13] * z);
            uint seed = (uint)(Data.Seed + index * 300);
            switch (Data.BasisType)
            {
                case EBasisTypes.VALUE: return Noise.value_noise4D(nx, ny, nz, w, seed, Data.InterpType);
                case EBasisTypes.GRADIENT: return Noise.gradient_noise4D(nx, ny, nz, w, seed, Data.InterpType);
                case EBasisTypes.GRADVAL: return Noise.gradval_noise4D(nx, ny, nz, w, seed, Data.InterpType);
                case EBasisTypes.WHITE: return Noise.white_noise4D(nx, ny, nz, w, seed);
                case EBasisTypes.SIMPLEX: return Noise.simplex_noise4D(nx, ny, nz, w, seed);
                default: return Noise.gradient_noise4D(nx, ny, nz, w, seed, Data.InterpType);
            }
        }

        private double BaseNoiseGetDx(double x, double y, double z, double w, int index)
        {
            return (BaseNoiseGet(x - Data.Spacing, y, z, w, index) - BaseNoiseGet(x + Data.Spacing, y, z, w, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDy(double x, double y, double z, double w, int index)
        {
            return (BaseNoiseGet(x, y - Data.Spacing, z, w, index) - BaseNoiseGet(x, y + Data.Spacing, z, w, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDz(double x, double y, double z, double w, int index)
        {
            return (BaseNoiseGet(x, y, z - Data.Spacing, w, index) - BaseNoiseGet(x, y, z + Data.Spacing, w, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDw(double x, double y, double z, double w, int index)
        {
            return (BaseNoiseGet(x, y, z, w - Data.Spacing, index) - BaseNoiseGet(x, y, z, w + Data.Spacing, index)) / Data.Spacing;
        }


        public static JobHandle JobHandle(NativeArray<double4> inputs, NativeArray<double> outputs, DFractal data,
            JobHandle dependsOn)
        {
            return new Fractal4Job()
            {
                Inputs = inputs,
                Outputs = outputs,
                Data = data
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }

    [BurstCompile]
    public struct Fractal6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public DFractal Data;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FractalGet(count, inputPtr, outputPtr);
        }
        private unsafe void FractalGet(int count, double6* inputs, double* outputs)
        {
            switch (Data.FractalType)
            {
                case EFractalTypes.FBM: FBMGet(count, inputs, outputs); break;
                case EFractalTypes.RIDGEDMULTI: RidgedMultiGet(count, inputs, outputs); break;
                case EFractalTypes.BILLOW: BillowGet(count, inputs, outputs); break;
                case EFractalTypes.MULTI: MultiGet(count, inputs, outputs); break;
                case EFractalTypes.HYBRIDMULTI: HybridMultiGet(count, inputs, outputs); break;
                case EFractalTypes.DECARPENTIERSWISS: DeCarpentierSwissGet(count, inputs, outputs); break;
                default: FBMGet(count, inputs, outputs); break;
            }
        }
        private unsafe void FBMGet(int count, double6* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;
                double u = inputs[c].u;
                double v = inputs[c].v;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;
                u *= Data.Frequency;
                v *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, z, w, u, v, i);
                    sum += n * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                    u *= Data.Lacunarity;
                    v *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void MultiGet(int count, double6* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;
                double u = inputs[c].u;
                double v = inputs[c].v;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;
                u *= Data.Frequency;
                v *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    value *= BaseNoiseGet(x, y, z, w, u, v, i) * Data.MagicNumbers[i * 14] + 1.0;
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                    u *= Data.Lacunarity;
                    v *= Data.Lacunarity;
                }

                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }

        }
        private unsafe void BillowGet(int count, double6* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0.0;
                double amp = 1.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;
                double u = inputs[c].u;
                double v = inputs[c].v;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;
                u *= Data.Frequency;
                v *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x, y, z, w, u, v, i);
                    sum += (2.0 * math.abs(n) - 1.0) * amp;
                    amp *= Data.Gain;

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                    u *= Data.Lacunarity;
                    v *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }
        private unsafe void RidgedMultiGet(int count, double6* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double result = 0.0, signal;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;
                double u = inputs[c].u;
                double v = inputs[c].v;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;
                u *= Data.Frequency;
                v *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    signal = BaseNoiseGet(x, y, z, w, u, v, i);
                    signal = Data.Offset - math.abs(signal);
                    signal *= signal;
                    result += signal * Data.MagicNumbers[i * 14];

                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                    u *= Data.Lacunarity;
                    v *= Data.Lacunarity;
                }
                outputs[c] = result * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void HybridMultiGet(int count, double6* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double value = 0.0, signal, weight = 0.0;
                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;
                double u = inputs[c].u;
                double v = inputs[c].v;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;
                u *= Data.Frequency;
                v *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    if (i == 0)
                    {
                        value = BaseNoiseGet(x, y, z, w, u, v, 0) + Data.Offset;
                        weight = Data.Gain * value;
                    }
                    else
                    {
                        if (weight > 1.0) weight = 1.0;
                        signal = (BaseNoiseGet(x, y, z, w, u, v, i) + Data.Offset) * Data.MagicNumbers[i * 14];
                        value += weight * signal;
                        weight *= Data.Gain * signal;
                    }
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                    u *= Data.Lacunarity;
                    v *= Data.Lacunarity;
                }
                outputs[c] = value * Data.MagicNumbers[(Data.Octaves - 1) * 14 + 1] + Data.MagicNumbers[(Data.Octaves - 1) * 14 + 2];
            }
        }
        private unsafe void DeCarpentierSwissGet(int count, double6* inputs, double* outputs)
        {
            for (int c = 0; c < count; c++)
            {
                double sum = 0;
                double amp = 1.0;

                double dx_sum = 0;
                double dy_sum = 0;
                double dz_sum = 0;
                double dw_sum = 0;
                double du_sum = 0;
                double dv_sum = 0;

                double x = inputs[c].x;
                double y = inputs[c].y;
                double z = inputs[c].z;
                double w = inputs[c].w;
                double u = inputs[c].u;
                double v = inputs[c].v;

                x *= Data.Frequency;
                y *= Data.Frequency;
                z *= Data.Frequency;
                w *= Data.Frequency;
                u *= Data.Frequency;
                v *= Data.Frequency;

                for (int i = 0; i < Data.Octaves; ++i)
                {
                    double n = BaseNoiseGet(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    double dx = BaseNoiseGetDx(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dx_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    double dy = BaseNoiseGetDy(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    double dz = BaseNoiseGetDz(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    double dw = BaseNoiseGetDw(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    double du = BaseNoiseGetDu(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    double dv = BaseNoiseGetDv(x + Data.Offset * dx_sum, y + Data.Offset * dy_sum, z + Data.Offset * dz_sum, w + Data.Offset * dw_sum, u + Data.Offset * du_sum, v + Data.Offset * dv_sum, i);
                    sum += amp * (1.0 - math.abs(n));
                    dx_sum += amp * dx * -n;
                    dy_sum += amp * dy * -n;
                    dz_sum += amp * dz * -n;
                    dw_sum += amp * dw * -n;
                    du_sum += amp * du * -n;
                    dv_sum += amp * dv * -n;
                    amp *= Data.Gain * math.clamp(sum, 0.0, 1.0);
                    x *= Data.Lacunarity;
                    y *= Data.Lacunarity;
                    z *= Data.Lacunarity;
                    w *= Data.Lacunarity;
                    u *= Data.Lacunarity;
                    v *= Data.Lacunarity;
                }
                outputs[c] = sum;
            }
        }

        private double BaseNoiseGet(double x, double y, double z, double w, double u, double v, int index)
        {
            double nx, ny, nz;
            nx = (Data.MagicNumbers[index * 14 + 5] * x) + (Data.MagicNumbers[index * 14 + 6] * y) + (Data.MagicNumbers[index * 14 + 7] * z);
            ny = (Data.MagicNumbers[index * 14 + 8] * x) + (Data.MagicNumbers[index * 14 + 9] * y) + (Data.MagicNumbers[index * 14 + 10] * z);
            nz = (Data.MagicNumbers[index * 14 + 11] * x) + (Data.MagicNumbers[index * 14 + 12] * y) + (Data.MagicNumbers[index * 14 + 13] * z);
            uint seed = (uint)(Data.Seed + index * 300);
            switch (Data.BasisType)
            {
                case EBasisTypes.VALUE: return Noise.value_noise6D(nx, ny, nz, w, u, v, seed, Data.InterpType);
                case EBasisTypes.GRADIENT: return Noise.gradient_noise6D(nx, ny, nz, w, u, v, seed, Data.InterpType);
                case EBasisTypes.GRADVAL: return Noise.gradval_noise6D(nx, ny, nz, w, u, v, seed, Data.InterpType);
                case EBasisTypes.WHITE: return Noise.white_noise6D(nx, ny, nz, w, u, v, seed);
                case EBasisTypes.SIMPLEX: return Noise.simplex_noise6D(nx, ny, nz, w, u, v, seed);
                default: return Noise.gradient_noise6D(nx, ny, nz, w, u, v, seed, Data.InterpType);
            }
        }

        private double BaseNoiseGetDx(double x, double y, double z, double w, double u, double v, int index)
        {
            return (BaseNoiseGet(x - Data.Spacing, y, z, w, u, v, index) - BaseNoiseGet(x + Data.Spacing, y, z, w, u, v, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDy(double x, double y, double z, double w, double u, double v, int index)
        {
            return (BaseNoiseGet(x, y - Data.Spacing, z, w, u, v, index) - BaseNoiseGet(x, y + Data.Spacing, z, w, u, v, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDz(double x, double y, double z, double w, double u, double v, int index)
        {
            return (BaseNoiseGet(x, y, z - Data.Spacing, w, u, v, index) - BaseNoiseGet(x, y, z + Data.Spacing, w, u, v, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDw(double x, double y, double z, double w, double u, double v, int index)
        {
            return (BaseNoiseGet(x, y, z, w - Data.Spacing, u, v, index) - BaseNoiseGet(x, y, z, w + Data.Spacing, u, v, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDu(double x, double y, double z, double w, double u, double v, int index)
        {
            return (BaseNoiseGet(x, y, z, w, u - Data.Spacing, v, index) - BaseNoiseGet(x, y, z, w, u + Data.Spacing, v, index)) / Data.Spacing;
        }
        private double BaseNoiseGetDv(double x, double y, double z, double w, double u, double v, int index)
        {
            return (BaseNoiseGet(x, y, z, w, u, v - Data.Spacing, index) - BaseNoiseGet(x, y, z, w, u, v + Data.Spacing, index)) / Data.Spacing;
        }


        public static JobHandle JobHandle(NativeArray<double6> inputs, NativeArray<double> outputs, DFractal data,
            JobHandle dependsOn)
        {
            return new Fractal6Job()
            {
                Inputs = inputs,
                Outputs = outputs,
                Data = data
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}