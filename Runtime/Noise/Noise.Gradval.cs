using System.Runtime.CompilerServices;

namespace ANoise
{
    internal static partial class Noise
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double gradval_noise2D(double x, double y, uint seed, EInterpTypes interpTypes)
        {
            return value_noise2D(x, y, seed, interpTypes) + gradient_noise2D(x, y, seed, interpTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double gradval_noise3D(double x, double y, double z, uint seed, EInterpTypes interpTypes)
        {
            return value_noise3D(x, y, z, seed, interpTypes) + gradient_noise3D(x, y, z, seed, interpTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double gradval_noise4D(double x, double y, double z, double w, uint seed, EInterpTypes interpTypes)
        {
            return value_noise4D(x, y, z, w, seed, interpTypes) + gradient_noise4D(x, y, z, w, seed, interpTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double gradval_noise6D(double x, double y, double z, double w, double u, double v, uint seed, EInterpTypes interpTypes)
        {
            return value_noise6D(x, y, z, w, u, v, seed, interpTypes) + gradient_noise6D(x, y, z, w, u, v, seed, interpTypes);
        }
    }
}