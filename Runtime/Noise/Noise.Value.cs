using System.Runtime.CompilerServices;

namespace ANoise
{
    internal static partial class Noise
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double value_noise_2(double x, double y, int ix, int iy, uint seed)
        {
            uint n = hash2(ix, iy, seed);
            double noise = n / 255.0;
            return noise * 2.0 - 1.0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double value_noise_3(double x, double y, double z, int ix, int iy, int iz, uint seed)
        {
            uint n = hash3(ix, iy, iz, seed);
            double noise = n / (255.0);
            return noise * 2.0 - 1.0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double value_noise_4(double x, double y, double z, double w, int ix, int iy, int iz, int iw, uint seed)
        {
            uint n = hash4(ix, iy, iz, iw, seed);
            double noise = n / 255.0;
            return noise * 2.0 - 1.0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double value_noise_6(double x, double y, double z, double w, double u, double v, int ix, int iy, int iz, int iw, int iu, int iv, uint seed)
        {
            uint n = hash6(ix, iy, iz, iw, iu, iv, seed);
            double noise = n / 255.0;
            return noise * 2.0 - 1.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double value_noise2D(double x, double y, uint seed, EInterpTypes interpTypes)
        {
            int x0 = fast_floor(x);
            int y0 = fast_floor(y);

            int x1 = x0 + 1;
            int y1 = y0 + 1;

            double xs = Interp(x - x0, interpTypes);
            double ys = Interp(y - y0, interpTypes);

            return interp_XY_2(x, y, xs, ys, x0, x1, y0, y1, seed, worker_noise.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double value_noise3D(double x, double y, double z, uint seed, EInterpTypes interpTypes)
        {
            int x0 = fast_floor(x);
            int y0 = fast_floor(y);
            int z0 = fast_floor(z);
            int x1 = x0 + 1;
            int y1 = y0 + 1;
            int z1 = z0 + 1;

            double xs = Interp(x - x0, interpTypes);
            double ys = Interp(y - y0, interpTypes);
            double zs = Interp(z - z0, interpTypes);

            return interp_XYZ_3(x, y, z, xs, ys, zs, x0, x1, y0, y1, z0, z1, seed, worker_noise.value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double value_noise4D(double x, double y, double z, double w, uint seed, EInterpTypes interpTypes)
        {
            int x0 = fast_floor(x);
            int y0 = fast_floor(y);
            int z0 = fast_floor(z);
            int w0 = fast_floor(w);

            int x1 = x0 + 1;
            int y1 = y0 + 1;
            int z1 = z0 + 1;
            int w1 = w0 + 1;

            double xs = Interp(x - x0, interpTypes);
            double ys = Interp(y - y0, interpTypes);
            double zs = Interp(z - z0, interpTypes);
            double ws = Interp(w - w0, interpTypes);

            return interp_XYZW_4(x, y, z, w, xs, ys, zs, ws, x0, x1, y0, y1, z0, z1, w0, w1, seed, worker_noise.value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double value_noise6D(double x, double y, double z, double w, double u, double v, uint seed, EInterpTypes interpTypes)
        {
            int x0 = fast_floor(x);
            int y0 = fast_floor(y);
            int z0 = fast_floor(z);
            int w0 = fast_floor(w);
            int u0 = fast_floor(u);
            int v0 = fast_floor(v);

            int x1 = x0 + 1;
            int y1 = y0 + 1;
            int z1 = z0 + 1;
            int w1 = w0 + 1;
            int u1 = u0 + 1;
            int v1 = v0 + 1;

            double xs = Interp(x - x0, interpTypes);
            double ys = Interp(y - y0, interpTypes);
            double zs = Interp(z - z0, interpTypes);
            double ws = Interp(w - w0, interpTypes);
            double us = Interp(u - u0, interpTypes);
            double vs = Interp(v - v0, interpTypes);

            return interp_XYZWUV_6(x, y, z, w, u, v, xs, ys, zs, ws, us, vs, x0, x1, y0, y1, z0, z1, w0, w1, u0, u1, v0, v1, seed, worker_noise.value);
        }
    }
}