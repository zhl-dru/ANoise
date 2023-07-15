using System.Runtime.CompilerServices;

namespace ANoise
{
    internal static partial class Noise
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double white_noise2D(double x, double y, uint seed)
        {
            uint hash = hash2(x, y, seed);
            return whitenoise_lut[hash];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double white_noise3D(double x, double y, double z, uint seed)
        {
            uint hash = hash3(x, y, z, seed);
            return whitenoise_lut[hash];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double white_noise4D(double x, double y, double z, double w, uint seed)
        {
            uint hash = hash4(x, y, z, w, seed);
            return whitenoise_lut[hash];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double white_noise6D(double x, double y, double z, double w, double u, double v, uint seed)
        {
            uint hash = hash6(x, y, z, w, u, v, seed);
            return whitenoise_lut[hash];
        }
    }
}