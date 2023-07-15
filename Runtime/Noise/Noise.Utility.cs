using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    [BurstCompile]
    internal static partial class Noise
    {
        private enum worker_noise
        {
            value,
            gradient
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int fast_floor(double t)
        {
            return t > 0 ? (int)t : (int)t - 1;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double array_dot2(double arr0, double arr1, double a, double b)
        {
            return a * arr0 + b * arr1;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double array_dot3(double arr0, double arr1, double arr2, double a, double b, double c)
        {
            return a * arr0 + b * arr1 + c * arr2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double array_dot4(double arr0, double arr1, double arr2, double arr3, double x, double y, double z, double w)
        {
            return x * arr0 + y * arr1 + z * arr2 + w * arr3;
        }

        // Edge/Face/Cube/Hypercube interpolation
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double lerp(double s, double v1, double v2)
        {
            return v1 + s * (v2 - v1);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void add_dist(ref double4 f, ref double4 disp, double testdist, double testdisp)
        {
            int index;
            // Compare the given distance to the ones already in f
            if (testdist < f[3])
            {
                index = 3;
                while (index > 0 && testdist < f[index - 1]) index--;
                for (int i = 3; i-- > index;)
                {
                    f[i + 1] = f[i];
                    disp[i + 1] = disp[i];
                }
                f[index] = testdist;
                disp[index] = testdisp;
            }
        }
    }
}