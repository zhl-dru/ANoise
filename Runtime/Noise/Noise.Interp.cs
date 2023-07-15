using System.Runtime.CompilerServices;

namespace ANoise
{
    internal static partial class Noise
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double noInterp(double t)
        {
            return 0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double linearInterp(double t)
        {
            return t;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double hermiteInterp(double t)
        {
            return t * t * (3 - 2 * t);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double quinticInterp(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Interp(double t, EInterpTypes interpTypes)
        {
            switch (interpTypes)
            {
                case EInterpTypes.NONE:
                    return noInterp(t);
                case EInterpTypes.LINEAR:
                    return linearInterp(t);
                case EInterpTypes.CUBIC:
                    return hermiteInterp(t);
                default:
                    return quinticInterp(t);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_X_2(double x, double y, double xs, int x0, int x1, int iy, uint seed, worker_noise noisefunc)
        {
            double v1 = 0, v2 = 0;

            switch (noisefunc)
            {
                case worker_noise.gradient:
                    v1 = grad_noise_2(x, y, x0, iy, seed);
                    v2 = grad_noise_2(x, y, x1, iy, seed);
                    break;
                case worker_noise.value:
                    v1 = value_noise_2(x, y, x0, iy, seed);
                    v2 = value_noise_2(x, y, x1, iy, seed);
                    break;
            }
            return lerp(xs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XY_2(double x, double y, double xs, double ys, int x0, int x1, int y0, int y1, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_X_2(x, y, xs, x0, x1, y0, seed, noisefunc);
            double v2 = interp_X_2(x, y, xs, x0, x1, y1, seed, noisefunc);
            return lerp(ys, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_X_3(double x, double y, double z, double xs, int x0, int x1, int iy, int iz, uint seed, worker_noise noisefunc)
        {
            double v1 = 0, v2 = 0;

            switch (noisefunc)
            {
                case worker_noise.gradient:
                    v1 = grad_noise_3(x, y, z, x0, iy, iz, seed);
                    v2 = grad_noise_3(x, y, z, x1, iy, iz, seed);
                    break;
                case worker_noise.value:
                    v1 = value_noise_3(x, y, z, x0, iy, iz, seed);
                    v2 = value_noise_3(x, y, z, x1, iy, iz, seed);
                    break;
            }

            return lerp(xs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XY_3(double x, double y, double z, double xs, double ys, int x0, int x1, int y0, int y1, int iz, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_X_3(x, y, z, xs, x0, x1, y0, iz, seed, noisefunc);
            double v2 = interp_X_3(x, y, z, xs, x0, x1, y1, iz, seed, noisefunc);
            return lerp(ys, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZ_3(double x, double y, double z, double xs, double ys, double zs, int x0, int x1, int y0, int y1, int z0, int z1, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_XY_3(x, y, z, xs, ys, x0, x1, y0, y1, z0, seed, noisefunc);
            double v2 = interp_XY_3(x, y, z, xs, ys, x0, x1, y0, y1, z1, seed, noisefunc);
            return lerp(zs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_X_4(double x, double y, double z, double w, double xs, int x0, int x1, int iy, int iz, int iw, uint seed, worker_noise noisefunc)
        {
            double v1 = 0, v2 = 0;

            switch (noisefunc)
            {
                case worker_noise.gradient:
                    v1 = grad_noise_4(x, y, z, w, x0, iy, iz, iw, seed);
                    v2 = grad_noise_4(x, y, z, w, x1, iy, iz, iw, seed);
                    break;
                case worker_noise.value:
                    v1 = value_noise_4(x, y, z, w, x0, iy, iz, iw, seed);
                    v2 = value_noise_4(x, y, z, w, x1, iy, iz, iw, seed);
                    break;
            }
            return lerp(xs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XY_4(double x, double y, double z, double w, double xs, double ys, int x0, int x1, int y0, int y1, int iz, int iw, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_X_4(x, y, z, w, xs, x0, x1, y0, iz, iw, seed, noisefunc);
            double v2 = interp_X_4(x, y, z, w, xs, x0, x1, y1, iz, iw, seed, noisefunc);
            return lerp(ys, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZ_4(double x, double y, double z, double w, double xs, double ys, double zs, int x0, int x1, int y0, int y1, int z0, int z1, int iw, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_XY_4(x, y, z, w, xs, ys, x0, x1, y0, y1, z0, iw, seed, noisefunc);
            double v2 = interp_XY_4(x, y, z, w, xs, ys, x0, x1, y0, y1, z1, iw, seed, noisefunc);
            return lerp(zs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZW_4(double x, double y, double z, double w, double xs, double ys, double zs, double ws, int x0, int x1, int y0, int y1, int z0, int z1, int w0, int w1, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_XYZ_4(x, y, z, w, xs, ys, zs, x0, x1, y0, y1, z0, z1, w0, seed, noisefunc);
            double v2 = interp_XYZ_4(x, y, z, w, xs, ys, zs, x0, x1, y0, y1, z0, z1, w1, seed, noisefunc);
            return lerp(ws, v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_X_6(double x, double y, double z, double w, double u, double v, double xs, int x0, int x1, int iy, int iz, int iw, int iu, int iv, uint seed, worker_noise noisefunc)
        {
            double v1 = 0, v2 = 0;

            switch (noisefunc)
            {
                case worker_noise.gradient:
                    v1 = grad_noise_6(x, y, z, w, u, v, x0, iy, iz, iw, iu, iv, seed);
                    v2 = grad_noise_6(x, y, z, w, u, v, x1, iy, iz, iw, iu, iv, seed);
                    break;
                case worker_noise.value:
                    v1 = value_noise_6(x, y, z, w, u, v, x0, iy, iz, iw, iu, iv, seed);
                    v2 = value_noise_6(x, y, z, w, u, v, x1, iy, iz, iw, iu, iv, seed);
                    break;
            }
            return lerp(xs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XY_6(double x, double y, double z, double w, double u, double v, double xs, double ys, int x0, int x1, int y0, int y1, int iz, int iw, int iu, int iv, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_X_6(x, y, z, w, u, v, xs, x0, x1, y0, iz, iw, iu, iv, seed, noisefunc);
            double v2 = interp_X_6(x, y, z, w, u, v, xs, x0, x1, y1, iz, iw, iu, iv, seed, noisefunc);
            return lerp(ys, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZ_6(double x, double y, double z, double w, double u, double v, double xs, double ys, double zs, int x0, int x1, int y0, int y1, int z0, int z1, int iw, int iu, int iv, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_XY_6(x, y, z, w, u, v, xs, ys, x0, x1, y0, y1, z0, iw, iu, iv, seed, noisefunc);
            double v2 = interp_XY_6(x, y, z, w, u, v, xs, ys, x0, x1, y0, y1, z1, iw, iu, iv, seed, noisefunc);
            return lerp(zs, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZW_6(double x, double y, double z, double w, double u, double v, double xs, double ys, double zs, double ws, int x0, int x1, int y0, int y1, int z0, int z1, int w0, int w1, int iu, int iv, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_XYZ_6(x, y, z, w, u, v, xs, ys, zs, x0, x1, y0, y1, z0, z1, w0, iu, iv, seed, noisefunc);
            double v2 = interp_XYZ_6(x, y, z, w, u, v, xs, ys, zs, x0, x1, y0, y1, z0, z1, w1, iu, iv, seed, noisefunc);
            return lerp(ws, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZWU_6(double x, double y, double z, double w, double u, double v, double xs, double ys, double zs, double ws, double us, int x0, int x1, int y0, int y1, int z0, int z1, int w0, int w1, int u0, int u1, int iv, uint seed, worker_noise noisefunc)
        {
            double v1 = interp_XYZW_6(x, y, z, w, u, v, xs, ys, zs, ws, x0, x1, y0, y1, z0, z1, w0, w1, u0, iv, seed, noisefunc);
            double v2 = interp_XYZW_6(x, y, z, w, u, v, xs, ys, zs, ws, x0, x1, y0, y1, z0, z1, w0, w1, u1, iv, seed, noisefunc);
            return lerp(us, v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double interp_XYZWUV_6(double x, double y, double z, double w, double u, double v, double xs, double ys, double zs, double ws, double us, double vs, int x0, int x1, int y0, int y1, int z0, int z1, int w0, int w1, int u0, int u1, int v0, int v1, uint seed, worker_noise noisefunc)
        {
            double val1 = interp_XYZWU_6(x, y, z, w, u, v, xs, ys, zs, ws, us, x0, x1, y0, y1, z0, z1, w0, w1, u0, u1, v0, seed, noisefunc);
            double val2 = interp_XYZWU_6(x, y, z, w, u, v, xs, ys, zs, ws, us, x0, x1, y0, y1, z0, z1, w0, w1, u0, u1, v1, seed, noisefunc);
            return lerp(vs, val1, val2);
        }
    }
}