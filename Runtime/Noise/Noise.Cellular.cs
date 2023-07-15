using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    internal static partial class Noise
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void cellular_function2D(double x, double y, uint seed, ref double4 f, ref double4 disp, ECellularDistanceFunction distype)
        {
            int xInt = fast_floor(x);
            int yInt = fast_floor(y);

            for (int c = 0; c < 4; ++c) { f[c] = 99999.0; disp[c] = 0.0; }

            for (int ycur = yInt - 3; ycur <= yInt + 3; ++ycur)
            {
                for (int xcur = xInt - 3; xcur <= xInt + 3; ++xcur)
                {
                    double xpos = xcur + value_noise_2(x, y, xcur, ycur, seed);
                    double ypos = ycur + value_noise_2(x, y, xcur, ycur, seed + 1);
                    double xdist = xpos - x;
                    double ydist = ypos - y;
                    double dist = 0.0;
                    switch (distype)
                    {
                        case ECellularDistanceFunction.Euclidean:
                            dist = math.sqrt(xdist * xdist + ydist * ydist);
                            break;
                        case ECellularDistanceFunction.EuclideanSq:
                            dist = xdist * xdist + ydist * ydist;
                            break;
                        case ECellularDistanceFunction.Manhattan:
                            dist = math.abs(xdist) + math.abs(ydist);
                            break;
                        case ECellularDistanceFunction.Hybrid:
                            dist = math.abs(xdist) + math.abs(ydist) + xdist * xdist + ydist * ydist;
                            break;
                    }
                    int xval = fast_floor(xpos);
                    int yval = fast_floor(ypos);
                    double dsp = value_noise_2(x, y, xval, yval, seed + 3);
                    add_dist(ref f, ref disp, dist, dsp);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void cellular_function3D(double x, double y, double z, uint seed, ref double4 f, ref double4 disp, ECellularDistanceFunction distype)
        {
            int xInt = fast_floor(x);
            int yInt = fast_floor(y);
            int zInt = fast_floor(z);

            for (int c = 0; c < 4; ++c) { f[c] = 99999.0; disp[c] = 0.0; }

            for (int zcur = zInt - 2; zcur <= zInt + 2; ++zcur)
            {
                for (int ycur = yInt - 2; ycur <= yInt + 2; ++ycur)
                {
                    for (int xcur = xInt - 2; xcur <= xInt + 2; ++xcur)
                    {
                        double xpos = xcur + value_noise_3(x, y, z, xcur, ycur, zcur, seed);
                        double ypos = ycur + value_noise_3(x, y, z, xcur, ycur, zcur, seed + 1);
                        double zpos = zcur + value_noise_3(x, y, z, xcur, ycur, zcur, seed + 2);
                        double xdist = xpos - x;
                        double ydist = ypos - y;
                        double zdist = zpos - z;
                        double dist = 0.0;
                        switch (distype)
                        {
                            case ECellularDistanceFunction.Euclidean:
                                dist = math.sqrt(xdist * xdist + ydist * ydist + zdist * zdist);
                                break;
                            case ECellularDistanceFunction.EuclideanSq:
                                dist = xdist * xdist + ydist * ydist + zdist * zdist;
                                break;
                            case ECellularDistanceFunction.Manhattan:
                                dist = math.abs(xdist) + math.abs(ydist) + math.abs(zdist);
                                break;
                            case ECellularDistanceFunction.Hybrid:
                                dist = math.abs(xdist) + math.abs(ydist) + math.abs(zdist) + xdist * xdist + ydist * ydist + zdist * zdist;
                                break;
                        }
                        int xval = fast_floor(xpos);
                        int yval = fast_floor(ypos);
                        int zval = fast_floor(zpos);
                        double dsp = value_noise_3(x, y, z, xval, yval, zval, seed + 3);
                        add_dist(ref f, ref disp, dist, dsp);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void cellular_function4D(double x, double y, double z, double w, uint seed, ref double4 f, ref double4 disp, ECellularDistanceFunction distype)
        {
            int xInt = fast_floor(x);
            int yInt = fast_floor(y);
            int zInt = fast_floor(z);
            int wInt = fast_floor(w);

            for (int c = 0; c < 4; ++c) { f[c] = 99999.0; disp[c] = 0.0; }

            for (int wcur = wInt - 2; wcur <= wInt + 2; ++wcur)
            {
                for (int zcur = zInt - 2; zcur <= zInt + 2; ++zcur)
                {
                    for (int ycur = yInt - 2; ycur <= yInt + 2; ++ycur)
                    {
                        for (int xcur = xInt - 2; xcur <= xInt + 2; ++xcur)
                        {
                            double xpos = xcur + value_noise_4(x, y, z, w, xcur, ycur, zcur, wcur, seed);
                            double ypos = ycur + value_noise_4(x, y, z, w, xcur, ycur, zcur, wcur, seed + 1);
                            double zpos = zcur + value_noise_4(x, y, z, w, xcur, ycur, zcur, wcur, seed + 2);
                            double wpos = wcur + value_noise_4(x, y, z, w, xcur, ycur, zcur, wcur, seed + 3);
                            double xdist = xpos - x;
                            double ydist = ypos - y;
                            double zdist = zpos - z;
                            double wdist = wpos - w;
                            double dist = 0.0;
                            switch (distype)
                            {
                                case ECellularDistanceFunction.Euclidean:
                                    dist = math.sqrt(xdist * xdist + ydist * ydist + zdist * zdist + wdist * wdist);
                                    break;
                                case ECellularDistanceFunction.EuclideanSq:
                                    dist = xdist * xdist + ydist * ydist + zdist * zdist + wdist * wdist;
                                    break;
                                case ECellularDistanceFunction.Manhattan:
                                    dist = math.abs(xdist) + math.abs(ydist) + math.abs(zdist) + math.abs(wdist);
                                    break;
                                case ECellularDistanceFunction.Hybrid:
                                    dist = math.abs(xdist) + math.abs(ydist) + math.abs(zdist) + math.abs(wdist) + xdist * xdist + ydist * ydist + zdist * zdist + wdist * wdist;
                                    break;
                            }
                            int xval = fast_floor(xpos);
                            int yval = fast_floor(ypos);
                            int zval = fast_floor(zpos);
                            int wval = fast_floor(wpos);
                            double dsp = value_noise_4(x, y, z, w, xval, yval, zval, wval, seed + 3);
                            add_dist(ref f, ref disp, dist, dsp);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void cellular_function6D(double x, double y, double z, double w, double u, double v, uint seed, ref double4 f, ref double4 disp, ECellularDistanceFunction distype)
        {
            int xInt = fast_floor(x);
            int yInt = fast_floor(y);
            int zInt = fast_floor(z);
            int wInt = fast_floor(w);
            int uInt = fast_floor(u);
            int vInt = fast_floor(v);

            for (int c = 0; c < 4; ++c) { f[c] = 99999.0; disp[c] = 0.0; }

            for (int vcur = vInt - 1; vcur <= vInt + 1; ++vcur)
            {
                for (int ucur = uInt - 1; ucur <= uInt + 1; ++ucur)
                {
                    for (int wcur = wInt - 2; wcur <= wInt + 2; ++wcur)
                    {
                        for (int zcur = zInt - 2; zcur <= zInt + 2; ++zcur)
                        {
                            for (int ycur = yInt - 2; ycur <= yInt + 2; ++ycur)
                            {
                                for (int xcur = xInt - 2; xcur <= xInt + 2; ++xcur)
                                {
                                    double xpos = xcur + value_noise_6(x, y, z, w, u, v, xcur, ycur, zcur, wcur, ucur, vcur, seed);
                                    double ypos = ycur + value_noise_6(x, y, z, w, u, v, xcur, ycur, zcur, wcur, ucur, vcur, seed + 1);
                                    double zpos = zcur + value_noise_6(x, y, z, w, u, v, xcur, ycur, zcur, wcur, ucur, vcur, seed + 2);
                                    double wpos = wcur + value_noise_6(x, y, z, w, u, v, xcur, ycur, zcur, wcur, ucur, vcur, seed + 3);
                                    double upos = ucur + value_noise_6(x, y, z, w, u, v, xcur, ycur, zcur, wcur, ucur, vcur, seed + 4);
                                    double vpos = vcur + value_noise_6(x, y, z, w, u, v, xcur, ycur, zcur, wcur, ucur, vcur, seed + 5);
                                    double xdist = xpos - x;
                                    double ydist = ypos - y;
                                    double zdist = zpos - z;
                                    double wdist = wpos - w;
                                    double udist = upos - u;
                                    double vdist = vpos - v;
                                    double dist = 0.0;
                                    switch (distype)
                                    {
                                        case ECellularDistanceFunction.Euclidean:
                                            dist = math.sqrt(xdist * xdist + ydist * ydist + zdist * zdist + wdist * wdist + udist * udist + vdist * vdist);
                                            break;
                                        case ECellularDistanceFunction.EuclideanSq:
                                            dist = xdist * xdist + ydist * ydist + zdist * zdist + wdist * wdist + udist * udist + vdist * vdist;
                                            break;
                                        case ECellularDistanceFunction.Manhattan:
                                            dist = math.abs(xdist) + math.abs(ydist) + math.abs(zdist) + math.abs(wdist) + math.abs(udist) + math.abs(vdist);
                                            break;
                                        case ECellularDistanceFunction.Hybrid:
                                            dist = math.abs(xdist) + math.abs(ydist) + math.abs(zdist) + math.abs(wdist) + math.abs(udist) + math.abs(vdist) + xdist * xdist + ydist * ydist + zdist * zdist + wdist * wdist + udist * udist + vdist * vdist;
                                            break;
                                    }
                                    int xval = fast_floor(xpos);
                                    int yval = fast_floor(ypos);
                                    int zval = fast_floor(zpos);
                                    int wval = fast_floor(wpos);
                                    int uval = fast_floor(upos);
                                    int vval = fast_floor(vpos);
                                    double dsp = value_noise_6(x, y, z, w, u, v, xval, yval, zval, wval, uval, vval, seed + 6);
                                    add_dist(ref f, ref disp, dist, dsp);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}