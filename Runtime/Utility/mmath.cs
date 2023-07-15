using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Runtime.CompilerServices;

namespace ANoise
{
    [BurstCompile]
    public static class mmath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector3 firstderivative(int x, int y, int w, int h, double mh, double s, double* n)
        {
            double z1 = n[edgesx(x - 1, w) + edgesy(y + 1, h) * w] * mh;
            double z2 = n[edgesx(x + 0, w) + edgesy(y + 1, h) * w] * mh;
            double z3 = n[edgesx(x + 1, w) + edgesy(y + 1, h) * w] * mh;
            double z4 = n[edgesx(x - 1, w) + edgesy(y + 0, h) * w] * mh;
            double z6 = n[edgesx(x + 1, w) + edgesy(y + 0, h) * w] * mh;
            double z7 = n[edgesx(x - 1, w) + edgesy(y - 1, h) * w] * mh;
            double z8 = n[edgesx(x + 0, w) + edgesy(y - 1, h) * w] * mh;
            double z9 = n[edgesx(x + 1, w) + edgesy(y - 1, h) * w] * mh;

            //p, q
            double zx = (z3 + z6 + z9 - z1 - z4 - z7) / (6.0 * s);
            double zy = (z1 + z2 + z3 - z7 - z8 - z9) / (6.0 * s);

            return new Vector3((float)-zx, (float)-zy, 1f);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector3 calculatenormal(int x, int y, int w, int h, double mh, double s, double* n)
        {
            Vector3 normal = firstderivative(x, y, w, h, mh, s, n);
            normal.x = -normal.x * 0.5f + 0.5f;
            normal.y = normal.y * 0.5f + 0.5f;
            normal.Normalize();
            return normal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int edgesx(int x, int w) { return math.clamp(x, 0, w - 1); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int edgesy(int y, int h) { return math.clamp(y, 0, h - 1); }
    }
}