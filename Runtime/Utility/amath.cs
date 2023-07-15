using AOT;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace ANoise
{
    public unsafe delegate void algorithm_a1(int count, double* v, double* r);
    public unsafe delegate void algorithm_a2(int count, double* v1, double* v2, double* r);
    public unsafe delegate void algorithm_a3(int count, double* v1, double* v2, double* v3, double* r);
    public unsafe delegate void algorithm_a4(int count, double* v1, double* v2, double* v3, double* v4, double* r);
    public unsafe delegate void algorithm_a5(int count, double* v1, double* v2, double* v3, double* v4, double* v5, double* r);
    public unsafe delegate void algorithm_a6(int count, double* v1, double* v2, double* v3, double* v4, double* v5, double* v6, double* r);
    public unsafe delegate void algorithm_a7(int count, double* v1, double* v2, double* v3, double* v4, double* v5, double* v6, double* v7, double* r);

    public unsafe delegate void constant(int count, double v, double* r);

    public unsafe delegate void noise2color(int count, double* v, Color* r);


    [BurstCompile]
    public static class amath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double quintic_blend(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double sawtooth(double x, double p)
        {
            return 2.0 * (x / p - math.floor(0.5 + x / p)) * 0.5 + 0.5;
        }



        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void bias(int count, double* s, double* b, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.pow(s[i], math.log(b[i]) / math.log(0.5));
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void add(int count, double* s1, double* s2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = s1[i] + s2[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void subtract(int count, double* s1, double* s2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = s1[i] - s2[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void mult(int count, double* s1, double* s2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = s1[i] * s2[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void div(int count, double* s1, double* s2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = s1[i] / s2[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void max(int count, double* s1, double* s2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = s1[i] > s2[i] ? s1[i] : s2[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void min(int count, double* s1, double* s2, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = s1[i] < s2[i] ? s1[i] : s2[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a3))]
        public unsafe static void blend(int count, double* c, double* l, double* h, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.lerp(l[i], h[i], c[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a4))]
        public unsafe static void contrast(int count, double* s, double* b, double* t, double* f, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (s[i] + b[i] - t[i]) * f[i] + t[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a1))]
        public unsafe static void cos(int count, double* v, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.cos(v[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a1))]
        public unsafe static void sin(int count, double* v, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.sin(v[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a1))]
        public unsafe static void floor(int count, double* v, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.floor(v[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void gain(int count, double* s, double* g, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                if (s[i] < 0.5)
                {
                    r[i] = math.pow(s[i] * 2.0, math.log(1.0 - g[i]) / math.log(0.5));
                }
                else
                {
                    r[i] = 1.0 - (math.pow(2.0 - (s[i] * 2.0), math.log(1.0 - g[i]) / math.log(0.5)) / 2.0);
                }
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void pow(int count, double* s, double* p, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.pow(s[i], p[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void sawtooth(int count, double* s, double* p, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = (s[i] / p[i] - math.floor(s[i] / p[i] + 0.5)) * 2.0;
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(constant))]
        public unsafe static void constant(int count, double v, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = v;
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a1))]
        public unsafe static void cache(int count, double* v, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                if (v != null)
                    r[i] = v[i];
                else
                    r[i] = 0;
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a2))]
        public unsafe static void magnitude(int count, double* x, double* y, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.sqrt(x[i] * x[i] + y[i] * y[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a3))]
        public unsafe static void magnitude(int count, double* x, double* y, double* z, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.sqrt(x[i] * x[i] + y[i] * y[i] + z[i] * z[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a4))]
        public unsafe static void magnitude(int count, double* x, double* y, double* z, double* w, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.sqrt(x[i] * x[i] + y[i] * y[i] + z[i] * z[i] + w[i] * w[i]);
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a6))]
        public unsafe static void magnitude(int count, double* x, double* y, double* z, double* w, double* u, double* v, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = math.sqrt(x[i] * x[i] + y[i] * y[i] + z[i] * z[i] + w[i] * w[i] + u[i] * u[i] + v[i] * v[i]);
            }
        }

        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a3))]
        public unsafe static void scaleoffset(int count, double* so, double* sc, double* o, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                r[i] = so[i] * sc[i] + o[i];
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a5))]
        public unsafe static void select(int count, double* l, double* h, double* c, double* t, double* f, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                double low = l[i], high = h[i], control = c[i];
                double threshold = t[i], falloff = f[i];
                if (falloff > 0.0)
                {
                    if (control < (threshold - falloff))
                    {
                        r[i] = low;
                    }
                    else if (control > (threshold + falloff))
                    {
                        r[i] = high;
                    }
                    else
                    {
                        double lower = threshold - falloff;
                        double upper = threshold + falloff;
                        double blend = quintic_blend((control - lower) / (upper - lower));
                        r[i] = math.lerp(low, high, blend);
                    }
                }
                else
                {
                    if (control < threshold) r[i] = low;
                    else r[i] = high;
                }
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(algorithm_a3))]
        public unsafe static void triangle(int count, double* s, double* p, double* o, double* r)
        {
            for (int i = 0; i < count; i++)
            {
                double val = s[i];
                double period = p[i];
                double offset = o[i];

                if (offset >= 1) r[i] = sawtooth(val, period);
                else if (offset <= 0) r[i] = 1.0 - sawtooth(val, period);
                else
                {
                    double s1 = (offset - sawtooth(val, period)) >= 0 ? 1.0 : 0.0;
                    double s2 = (1.0 - offset - (sawtooth(-val, period))) >= 0 ? 1.0 : 0.0;
                    r[i] = sawtooth(val, period) * s1 / offset + (sawtooth(-val, period) * s2 / (1.0 - offset));
                }
            }
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(noise2color))]
        public unsafe static void defaultnoise2color(int count, double* v, Color* r)
        {
            for (int i = 0; i < count; i++)
            {
                float val = (float)v[i];
                r[i] = new Color(val, val, val, 1f);
            }
        }
    }


    public struct double6
    {
        public double x;
        public double y;
        public double z;
        public double w;
        public double u;
        public double v;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double6(double x, double y, double z, double w, double u, double v)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            this.u = u;
            this.v = v;
        }

        unsafe public double this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 6)
                    throw new System.ArgumentException("index must be between[0...5]");
#endif
                fixed (double6* array = &this) { return ((double*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 6)
                    throw new System.ArgumentException("index must be between[0...5]");
#endif
                fixed (double* array = &x) { array[index] = value; }
            }
        }
    }
    public struct int6
    {
        public int x;
        public int y;
        public int z;
        public int w;
        public int u;
        public int v;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int6(int x, int y, int z, int w, int u, int v)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            this.u = u;
            this.v = v;
        }

        unsafe public int this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 6)
                    throw new System.ArgumentException("index must be between[0...5]");
#endif
                fixed (int6* array = &this) { return ((int*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 6)
                    throw new System.ArgumentException("index must be between[0...5]");
#endif
                fixed (int* array = &x) { array[index] = value; }
            }
        }
    }
}