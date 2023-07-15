using Unity.Jobs;
using Unity.Collections;
using UnityEngine;
using Unity.Burst;

namespace ANoise
{
    public static class Map2DHelper
    {
        //通过传入的噪声值转化为颜色,默认的颜色方法是color(n,n,n,1)
        public static JobHandle GetColors(NativeArray<double> inputs, NativeArray<Color> outputs, JobHandle dependsOn = default)
        {
            FunctionPointer<noise2color> noise2color_fun_ptr;
            unsafe { noise2color_fun_ptr = BurstCompiler.CompileFunctionPointer<noise2color>(amath.defaultnoise2color); }
            return Noise2ColorJob.JobHandle(inputs, noise2color_fun_ptr, outputs, dependsOn);
        }

        //通过传入的噪声值转化为颜色,使用自定义方法,需要传入一个自定义颜色转换方法的函数指针
        public static JobHandle GetColors(NativeArray<double> inputs, NativeArray<Color> outputs, FunctionPointer<noise2color> noise2color_fun_ptr, JobHandle dependsOn = default)
        {
            return Noise2ColorJob.JobHandle(inputs, noise2color_fun_ptr, outputs, dependsOn);
        }
    }
}