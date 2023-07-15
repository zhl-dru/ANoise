using Unity.Jobs;
using Unity.Collections;
using UnityEngine;
using Unity.Burst;

namespace ANoise
{
    public static class Map2DHelper
    {
        //ͨ�����������ֵת��Ϊ��ɫ,Ĭ�ϵ���ɫ������color(n,n,n,1)
        public static JobHandle GetColors(NativeArray<double> inputs, NativeArray<Color> outputs, JobHandle dependsOn = default)
        {
            FunctionPointer<noise2color> noise2color_fun_ptr;
            unsafe { noise2color_fun_ptr = BurstCompiler.CompileFunctionPointer<noise2color>(amath.defaultnoise2color); }
            return Noise2ColorJob.JobHandle(inputs, noise2color_fun_ptr, outputs, dependsOn);
        }

        //ͨ�����������ֵת��Ϊ��ɫ,ʹ���Զ��巽��,��Ҫ����һ���Զ�����ɫת�������ĺ���ָ��
        public static JobHandle GetColors(NativeArray<double> inputs, NativeArray<Color> outputs, FunctionPointer<noise2color> noise2color_fun_ptr, JobHandle dependsOn = default)
        {
            return Noise2ColorJob.JobHandle(inputs, noise2color_fun_ptr, outputs, dependsOn);
        }
    }
}