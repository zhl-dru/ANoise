using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    public struct FunctionGradientModifyCoords2Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double2> Inputs;
        [ReadOnly] public double Spacing;
        [ReadOnly] public EFunctionGradientAxis Axis;

        [WriteOnly] public NativeArray<double2> Outputs1;
        [WriteOnly] public NativeArray<double2> Outputs2;


        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double2*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var output1Ptr = (double2*)Outputs1.GetUnsafePtr() + startIndex;
            var output2Ptr = (double2*)Outputs2.GetUnsafePtr() + startIndex;
            ModifyCoords(count, inputPtr, output1Ptr, output2Ptr);
        }

        private unsafe void ModifyCoords(int count, double2* c, double2* r1, double2* r2)
        {
            for (int i = 0; i < count; i++)
            {
                switch (Axis)
                {
                    case EFunctionGradientAxis.X_AXIS:
                        r1[i] = new double2(c[i].x - Spacing, c[i].y);
                        r2[i] = new double2(c[i].x + Spacing, c[i].y);
                        break;
                    case EFunctionGradientAxis.Y_AXIS:
                        r1[i] = new double2(c[i].x, c[i].y - Spacing);
                        r2[i] = new double2(c[i].x, c[i].y + Spacing);
                        break;
                    default:
                        r1[i] = new double2();
                        r2[i] = new double2();
                        break;
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double2> inputs, double spacing, EFunctionGradientAxis axis,
            NativeArray<double2> outputs1, NativeArray<double2> outputs2, JobHandle dependsOn)
        {
            return new FunctionGradientModifyCoords2Job()
            {
                Inputs = inputs,
                Spacing = spacing,
                Axis = axis,
                Outputs1 = outputs1,
                Outputs2 = outputs2
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct FunctionGradientModifyCoords3Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double3> Inputs;
        [ReadOnly] public double Spacing;
        [ReadOnly] public EFunctionGradientAxis Axis;

        [WriteOnly] public NativeArray<double3> Outputs1;
        [WriteOnly] public NativeArray<double3> Outputs2;


        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double3*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var output1Ptr = (double3*)Outputs1.GetUnsafePtr() + startIndex;
            var output2Ptr = (double3*)Outputs2.GetUnsafePtr() + startIndex;
            ModifyCoords(count, inputPtr, output1Ptr, output2Ptr);
        }

        private unsafe void ModifyCoords(int count, double3* c, double3* r1, double3* r2)
        {
            for (int i = 0; i < count; i++)
            {
                switch (Axis)
                {
                    case EFunctionGradientAxis.X_AXIS:
                        r1[i] = new double3(c[i].x - Spacing, c[i].y, c[i].z);
                        r2[i] = new double3(c[i].x + Spacing, c[i].y, c[i].z);
                        break;
                    case EFunctionGradientAxis.Y_AXIS:
                        r1[i] = new double3(c[i].x, c[i].y - Spacing, c[i].z);
                        r2[i] = new double3(c[i].x, c[i].y + Spacing, c[i].z);
                        break;
                    case EFunctionGradientAxis.Z_AXIS:
                        r1[i] = new double3(c[i].x, c[i].y, c[i].z - Spacing);
                        r2[i] = new double3(c[i].x, c[i].y, c[i].z + Spacing);
                        break;
                    default:
                        r1[i] = new double3();
                        r2[i] = new double3();
                        break;
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double3> inputs, double spacing, EFunctionGradientAxis axis,
            NativeArray<double3> outputs1, NativeArray<double3> outputs2, JobHandle dependsOn)
        {
            return new FunctionGradientModifyCoords3Job()
            {
                Inputs = inputs,
                Spacing = spacing,
                Axis = axis,
                Outputs1 = outputs1,
                Outputs2 = outputs2
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct FunctionGradientModifyCoords4Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double4> Inputs;
        [ReadOnly] public double Spacing;
        [ReadOnly] public EFunctionGradientAxis Axis;

        [WriteOnly] public NativeArray<double4> Outputs1;
        [WriteOnly] public NativeArray<double4> Outputs2;


        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double4*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var output1Ptr = (double4*)Outputs1.GetUnsafePtr() + startIndex;
            var output2Ptr = (double4*)Outputs2.GetUnsafePtr() + startIndex;
            ModifyCoords(count, inputPtr, output1Ptr, output2Ptr);
        }

        private unsafe void ModifyCoords(int count, double4* c, double4* r1, double4* r2)
        {
            for (int i = 0; i < count; i++)
            {
                switch (Axis)
                {
                    case EFunctionGradientAxis.X_AXIS:
                        r1[i] = new double4(c[i].x - Spacing, c[i].y, c[i].z, c[i].w);
                        r2[i] = new double4(c[i].x + Spacing, c[i].y, c[i].z, c[i].w);
                        break;
                    case EFunctionGradientAxis.Y_AXIS:
                        r1[i] = new double4(c[i].x, c[i].y - Spacing, c[i].z, c[i].w);
                        r2[i] = new double4(c[i].x, c[i].y + Spacing, c[i].z, c[i].w);
                        break;
                    case EFunctionGradientAxis.Z_AXIS:
                        r1[i] = new double4(c[i].x, c[i].y, c[i].z - Spacing, c[i].w);
                        r2[i] = new double4(c[i].x, c[i].y, c[i].z + Spacing, c[i].w);
                        break;
                    case EFunctionGradientAxis.W_AXIS:
                        r1[i] = new double4(c[i].x, c[i].y, c[i].z, c[i].w - Spacing);
                        r2[i] = new double4(c[i].x, c[i].y, c[i].z, c[i].w + Spacing);
                        break;
                    default:
                        r1[i] = new double4();
                        r2[i] = new double4();
                        break;
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double4> inputs, double spacing, EFunctionGradientAxis axis,
            NativeArray<double4> outputs1, NativeArray<double4> outputs2, JobHandle dependsOn)
        {
            return new FunctionGradientModifyCoords4Job()
            {
                Inputs = inputs,
                Spacing = spacing,
                Axis = axis,
                Outputs1 = outputs1,
                Outputs2 = outputs2
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }
    [BurstCompile]
    public struct FunctionGradientModifyCoords6Job : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double6> Inputs;
        [ReadOnly] public double Spacing;
        [ReadOnly] public EFunctionGradientAxis Axis;

        [WriteOnly] public NativeArray<double6> Outputs1;
        [WriteOnly] public NativeArray<double6> Outputs2;


        public unsafe void Execute(int startIndex, int count)
        {
            var inputPtr = (double6*)Inputs.GetUnsafeReadOnlyPtr() + startIndex;
            var output1Ptr = (double6*)Outputs1.GetUnsafePtr() + startIndex;
            var output2Ptr = (double6*)Outputs2.GetUnsafePtr() + startIndex;
            ModifyCoords(count, inputPtr, output1Ptr, output2Ptr);
        }

        private unsafe void ModifyCoords(int count, double6* c, double6* r1, double6* r2)
        {
            for (int i = 0; i < count; i++)
            {
                switch (Axis)
                {
                    case EFunctionGradientAxis.X_AXIS:
                        r1[i] = new double6(c[i].x - Spacing, c[i].y, c[i].z, c[i].w, c[i].u, c[i].v);
                        r2[i] = new double6(c[i].x + Spacing, c[i].y, c[i].z, c[i].w, c[i].u, c[i].v);
                        break;
                    case EFunctionGradientAxis.Y_AXIS:
                        r1[i] = new double6(c[i].x, c[i].y - Spacing, c[i].z, c[i].w, c[i].u, c[i].v);
                        r2[i] = new double6(c[i].x, c[i].y + Spacing, c[i].z, c[i].w, c[i].u, c[i].v);
                        break;
                    case EFunctionGradientAxis.Z_AXIS:
                        r1[i] = new double6(c[i].x, c[i].y, c[i].z - Spacing, c[i].w, c[i].u, c[i].v);
                        r2[i] = new double6(c[i].x, c[i].y, c[i].z + Spacing, c[i].w, c[i].u, c[i].v);
                        break;
                    case EFunctionGradientAxis.W_AXIS:
                        r1[i] = new double6(c[i].x, c[i].y, c[i].z, c[i].w - Spacing, c[i].u, c[i].v);
                        r2[i] = new double6(c[i].x, c[i].y, c[i].z, c[i].w + Spacing, c[i].u, c[i].v);
                        break;
                    case EFunctionGradientAxis.U_AXIS:
                        r1[i] = new double6(c[i].x, c[i].y, c[i].z, c[i].w, c[i].u - Spacing, c[i].v);
                        r2[i] = new double6(c[i].x, c[i].y, c[i].z, c[i].w, c[i].u + Spacing, c[i].v);
                        break;
                    case EFunctionGradientAxis.V_AXIS:
                        r1[i] = new double6(c[i].x, c[i].y, c[i].z, c[i].w, c[i].u, c[i].v - Spacing);
                        r2[i] = new double6(c[i].x, c[i].y, c[i].z, c[i].w, c[i].u, c[i].v + Spacing);
                        break;
                    default:
                        r1[i] = new double6();
                        r2[i] = new double6();
                        break;
                }
            }
        }

        public static JobHandle JobHandle(NativeArray<double6> inputs, double spacing, EFunctionGradientAxis axis,
            NativeArray<double6> outputs1, NativeArray<double6> outputs2, JobHandle dependsOn)
        {
            return new FunctionGradientModifyCoords6Job()
            {
                Inputs = inputs,
                Spacing = spacing,
                Axis = axis,
                Outputs1 = outputs1,
                Outputs2 = outputs2
            }.ScheduleBatch(inputs.Length, inputs.Length / Constant.JobBatchCount, dependsOn);
        }
    }


    [BurstCompile]

    public struct FunctionGradientJob : IJobParallelForBatch
    {
        [ReadOnly] public NativeArray<double> Inputs1;
        [ReadOnly] public NativeArray<double> Inputs2;
        [ReadOnly] public double Spacing;

        [WriteOnly] public NativeArray<double> Outputs;

        public unsafe void Execute(int startIndex, int count)
        {
            var input1Ptr = (double*)Inputs1.GetUnsafeReadOnlyPtr() + startIndex;
            var input2Ptr = (double*)Inputs2.GetUnsafeReadOnlyPtr() + startIndex;
            var outputPtr = (double*)Outputs.GetUnsafePtr() + startIndex;
            FunctionGradient(count, input1Ptr, input2Ptr, outputPtr);
        }

        private unsafe void FunctionGradient(int count, double* v1, double* v2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (v1[i] - v2[i]) * (1 / Spacing);
            }
        }

        public static JobHandle JobHandle(NativeArray<double> inputs1, NativeArray<double> inputs2,
            double spacing, NativeArray<double> outputs, JobHandle dependsOn)
        {
            return new FunctionGradientJob()
            {
                Inputs1 = inputs1,
                Inputs2 = inputs2,
                Spacing = spacing,
                Outputs = outputs,
            }.ScheduleBatch(inputs1.Length, inputs1.Length / Constant.JobBatchCount, dependsOn);
        }
    }
}