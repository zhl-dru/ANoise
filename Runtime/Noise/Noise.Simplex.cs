using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace ANoise
{
    internal static partial class Noise
    {
        private const double F2 = 0.36602540378443864676372317075294;
        private const double G2 = 0.21132486540518711774542560974902;
        private const double F3 = 1.0 / 3.0;
        private const double G3 = 1.0 / 6.0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double simplex_noise2D(double x, double y, uint seed)
        {
            double s = (x + y) * F2;
            int i = fast_floor(x + s);
            int j = fast_floor(y + s);

            double t = (i + j) * G2;
            double X0 = i - t;
            double Y0 = j - t;
            double x0 = x - X0;
            double y0 = y - Y0;

            int i1, j1;
            if (x0 > y0)
            {
                i1 = 1; j1 = 0;
            }
            else
            {
                i1 = 0; j1 = 1;
            }

            double x1 = x0 - i1 + G2;
            double y1 = y0 - j1 + G2;
            double x2 = x0 - 1.0 + 2.0 * G2;
            double y2 = y0 - 1.0 + 2.0 * G2;

            // Hash the triangle coordinates to index the gradient table
            int h0 = (int)hash2(i, j, seed);
            int h1 = (int)hash2(i + i1, j + j1, seed);
            int h2 = (int)hash2(i + 1, j + 1, seed);

            // Now, index the tables

            double g00 = gradient2D_lut[h0 * 2];
            double g01 = gradient2D_lut[h0 * 2 + 1];
            double g10 = gradient2D_lut[h1 * 2];
            double g11 = gradient2D_lut[h1 * 2 + 1];
            double g20 = gradient2D_lut[h2 * 2];
            double g21 = gradient2D_lut[h2 * 2 + 1];

            double n0, n1, n2;
            // Calculate the contributions from the 3 corners
            double t0 = 0.5 - x0 * x0 - y0 * y0;
            if (t0 < 0) n0 = 0;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * array_dot2(g00, g01, x0, y0);
            }

            double t1 = 0.5 - x1 * x1 - y1 * y1;
            if (t1 < 0) n1 = 0;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * array_dot2(g10, g11, x1, y1);
            }

            double t2 = 0.5 - x2 * x2 - y2 * y2;
            if (t2 < 0) n2 = 0;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * array_dot2(g20, g21, x2, y2);
            }

            // Add contributions together
            return 70.0 * (n0 + n1 + n2) * 1.42188695 + 0.001054489;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double simplex_noise3D(double x, double y, double z, uint seed)
        {
            //static double F3 = 1.0/3.0;
            //static double G3 = 1.0/6.0;
            double n0, n1, n2, n3;

            double s = (x + y + z) * F3;
            int i = fast_floor(x + s);
            int j = fast_floor(y + s);
            int k = fast_floor(z + s);

            double t = (i + j + k) * G3;
            double X0 = i - t;
            double Y0 = j - t;
            double Z0 = k - t;

            double x0 = x - X0;
            double y0 = y - Y0;
            double z0 = z - Z0;

            int i1, j1, k1;
            int i2, j2, k2;

            if (x0 >= y0)
            {
                if (y0 >= z0)
                {
                    i1 = 1; j1 = 0; k1 = 0; i2 = 1; j2 = 1; k2 = 0;
                }
                else if (x0 >= z0)
                {
                    i1 = 1; j1 = 0; k1 = 0; i2 = 1; j2 = 0; k2 = 1;
                }
                else
                {
                    i1 = 0; j1 = 0; k1 = 1; i2 = 1; j2 = 0; k2 = 1;
                }
            }
            else
            {
                if (y0 < z0)
                {
                    i1 = 0; j1 = 0; k1 = 1; i2 = 0; j2 = 1; k2 = 1;
                }
                else if (x0 < z0)
                {
                    i1 = 0; j1 = 1; k1 = 0; i2 = 0; j2 = 1; k2 = 1;
                }
                else
                {
                    i1 = 0; j1 = 1; k1 = 0; i2 = 1; j2 = 1; k2 = 0;
                }
            }

            double x1 = x0 - i1 + G3;
            double y1 = y0 - j1 + G3;
            double z1 = z0 - k1 + G3;
            double x2 = x0 - i2 + 2.0 * G3;
            double y2 = y0 - j2 + 2.0 * G3;
            double z2 = z0 - k2 + 2.0 * G3;
            double x3 = x0 - 1.0 + 3.0 * G3;
            double y3 = y0 - 1.0 + 3.0 * G3;
            double z3 = z0 - 1.0 + 3.0 * G3;

            uint h0, h1, h2, h3;

            h0 = hash3(i, j, k, seed);
            h1 = hash3(i + i1, j + j1, k + k1, seed);
            h2 = hash3(i + i2, j + j2, k + k2, seed);
            h3 = hash3(i + 1, j + 1, k + 1, seed);

            double g00 = gradient3D_lut[h0 * 3];
            double g01 = gradient3D_lut[h0 * 3 + 1];
            double g02 = gradient3D_lut[h0 * 3 + 2];
            double g10 = gradient3D_lut[h1 * 3];
            double g11 = gradient3D_lut[h1 * 3 + 1];
            double g12 = gradient3D_lut[h1 * 3 + 2];
            double g20 = gradient3D_lut[h2 * 3];
            double g21 = gradient3D_lut[h2 * 3 + 1];
            double g22 = gradient3D_lut[h2 * 3 + 2];
            double g30 = gradient3D_lut[h3 * 3];
            double g31 = gradient3D_lut[h3 * 3 + 1];
            double g32 = gradient3D_lut[h3 * 3 + 2];

            double t0 = 0.6 - x0 * x0 - y0 * y0 - z0 * z0;
            if (t0 < 0.0) n0 = 0.0;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * array_dot3(g00, g01, g02, x0, y0, z0);
            }

            double t1 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
            if (t1 < 0.0) n1 = 0.0;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * array_dot3(g10, g11, g12, x1, y1, z1);
            }

            double t2 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
            if (t2 < 0) n2 = 0.0;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * array_dot3(g20, g21, g22, x2, y2, z2);
            }

            double t3 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
            if (t3 < 0) n3 = 0.0;
            else
            {
                t3 *= t3;
                n3 = t3 * t3 * array_dot3(g30, g31, g32, x3, y3, z3);
            }

            return 32.0 * (n0 + n1 + n2 + n3) * 1.25086885 + 0.0003194984;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double simplex_noise4D(double x, double y, double z, double w, uint seed)
        {
            double F4 = (math.sqrt(5.0) - 1.0) / 4.0;
            double G4 = (5.0 - math.sqrt(5.0)) / 20.0;
            double n0, n1, n2, n3, n4; // Noise contributions from the five corners
                                       // Skew the (x,y,z,w) space to determine which cell of 24 simplices we're in
            double s = (x + y + z + w) * F4; // Factor for 4D skewing
            int i = fast_floor(x + s);
            int j = fast_floor(y + s);
            int k = fast_floor(z + s);
            int l = fast_floor(w + s);
            double t = (i + j + k + l) * G4; // Factor for 4D unskewing
            double X0 = i - t; // Unskew the cell origin back to (x,y,z,w) space
            double Y0 = j - t;
            double Z0 = k - t;
            double W0 = l - t;
            double x0 = x - X0; // The x,y,z,w distances from the cell origin
            double y0 = y - Y0;
            double z0 = z - Z0;
            double w0 = w - W0;
            // For the 4D case, the simplex is a 4D shape I won't even try to describe.
            // To find out which of the 24 possible simplices we're in, we need to
            // determine the magnitude ordering of x0, y0, z0 and w0.
            // The method below is a good way of finding the ordering of x,y,z,w and
            // then find the correct traversal order for the simplex we’re in.
            // First, six pair-wise comparisons are performed between each possible pair
            // of the four coordinates, and the results are used to add up binary bits
            // for an integer index.
            int c1 = (x0 > y0) ? 32 : 0;
            int c2 = (x0 > z0) ? 16 : 0;
            int c3 = (y0 > z0) ? 8 : 0;
            int c4 = (x0 > w0) ? 4 : 0;
            int c5 = (y0 > w0) ? 2 : 0;
            int c6 = (z0 > w0) ? 1 : 0;
            int c = c1 + c2 + c3 + c4 + c5 + c6;
            int i1, j1, k1, l1; // The integer offsets for the second simplex corner
            int i2, j2, k2, l2; // The integer offsets for the third simplex corner
            int i3, j3, k3, l3; // The integer offsets for the fourth simplex corner
                                // simplex[c] is a 4-vector with the numbers 0, 1, 2 and 3 in some order.
                                // Many values of c will never occur, since e.g. x>y>z>w makes x<z, y<w and x<w
                                // impossible. Only the 24 indices which have non-zero entries make any sense.
                                // We use a thresholding to set the coordinates in turn from the largest magnitude.
                                // The number 3 in the "simplex" array is at the position of the largest coordinate.
            i1 = simplex[c * 4 + 0] >= 3 ? 1 : 0;
            j1 = simplex[c * 4 + 1] >= 3 ? 1 : 0;
            k1 = simplex[c * 4 + 2] >= 3 ? 1 : 0;
            l1 = simplex[c * 4 + 3] >= 3 ? 1 : 0;
            // The number 2 in the "simplex" array is at the second largest coordinate.
            i2 = simplex[c * 4 + 0] >= 2 ? 1 : 0;
            j2 = simplex[c * 4 + 1] >= 2 ? 1 : 0;
            k2 = simplex[c * 4 + 2] >= 2 ? 1 : 0;
            l2 = simplex[c * 4 + 3] >= 2 ? 1 : 0;
            // The number 1 in the "simplex" array is at the second smallest coordinate.
            i3 = simplex[c * 4 + 0] >= 1 ? 1 : 0;
            j3 = simplex[c * 4 + 1] >= 1 ? 1 : 0;
            k3 = simplex[c * 4 + 2] >= 1 ? 1 : 0;
            l3 = simplex[c * 4 + 3] >= 1 ? 1 : 0;
            // The fifth corner has all coordinate offsets = 1, so no need to look that up.
            double x1 = x0 - i1 + G4; // Offsets for second corner in (x,y,z,w) coords
            double y1 = y0 - j1 + G4;
            double z1 = z0 - k1 + G4;
            double w1 = w0 - l1 + G4;
            double x2 = x0 - i2 + 2.0 * G4; // Offsets for third corner in (x,y,z,w) coords
            double y2 = y0 - j2 + 2.0 * G4;
            double z2 = z0 - k2 + 2.0 * G4;
            double w2 = w0 - l2 + 2.0 * G4;
            double x3 = x0 - i3 + 3.0 * G4; // Offsets for fourth corner in (x,y,z,w) coords
            double y3 = y0 - j3 + 3.0 * G4;
            double z3 = z0 - k3 + 3.0 * G4;
            double w3 = w0 - l3 + 3.0 * G4;
            double x4 = x0 - 1.0 + 4.0 * G4; // Offsets for last corner in (x,y,z,w) coords
            double y4 = y0 - 1.0 + 4.0 * G4;
            double z4 = z0 - 1.0 + 4.0 * G4;
            double w4 = w0 - 1.0 + 4.0 * G4;
            // Work out the hashed gradient indices of the five simplex corners
            uint h0, h1, h2, h3, h4;
            h0 = hash4(i, j, k, l, seed);
            h1 = hash4(i + i1, j + j1, k + k1, l + l1, seed);
            h2 = hash4(i + i2, j + j2, k + k2, l + l2, seed);
            h3 = hash4(i + i3, j + j3, k + k3, l + l3, seed);
            h4 = hash4(i + 1, j + 1, k + 1, l + 1, seed);

            double g00 = gradient4D_lut[h0 * 4];
            double g01 = gradient4D_lut[h0 * 4 + 1];
            double g02 = gradient4D_lut[h0 * 4 + 2];
            double g03 = gradient4D_lut[h0 * 4 + 3];
            double g10 = gradient4D_lut[h1 * 4];
            double g11 = gradient4D_lut[h1 * 4 + 1];
            double g12 = gradient4D_lut[h1 * 4 + 2];
            double g13 = gradient4D_lut[h1 * 4 + 3];
            double g20 = gradient4D_lut[h2 * 4];
            double g21 = gradient4D_lut[h2 * 4 + 1];
            double g22 = gradient4D_lut[h2 * 4 + 2];
            double g23 = gradient4D_lut[h2 * 4 + 3];
            double g30 = gradient4D_lut[h3 * 4];
            double g31 = gradient4D_lut[h3 * 4 + 1];
            double g32 = gradient4D_lut[h3 * 4 + 2];
            double g33 = gradient4D_lut[h3 * 4 + 3];
            double g40 = gradient4D_lut[h4 * 4];
            double g41 = gradient4D_lut[h4 * 4 + 1];
            double g42 = gradient4D_lut[h4 * 4 + 2];
            double g43 = gradient4D_lut[h4 * 4 + 3];

            // Calculate the contribution from the five corners
            double t0 = 0.6 - x0 * x0 - y0 * y0 - z0 * z0 - w0 * w0;
            if (t0 < 0) n0 = 0.0;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * array_dot4(g00, g01, g02, g03, x0, y0, z0, w0);
            }
            double t1 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1 - w1 * w1;
            if (t1 < 0) n1 = 0.0;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * array_dot4(g10, g11, g12, g13, x1, y1, z1, w1);
            }
            double t2 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2 - w2 * w2;
            if (t2 < 0) n2 = 0.0;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * array_dot4(g20, g21, g22, g23, x2, y2, z2, w2);
            }
            double t3 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3 - w3 * w3;
            if (t3 < 0) n3 = 0.0;
            else
            {
                t3 *= t3;
                n3 = t3 * t3 * array_dot4(g30, g31, g32, g33, x3, y3, z3, w3);
            }
            double t4 = 0.6 - x4 * x4 - y4 * y4 - z4 * z4 - w4 * w4;
            if (t4 < 0) n4 = 0.0;
            else
            {
                t4 *= t4;
                n4 = t4 * t4 * array_dot4(g40, g41, g42, g43, x4, y4, z4, w4);
            }

            // Sum up and scale the result to cover the range [-1,1]
            return 27.0 * (n0 + n1 + n2 + n3 + n4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int6 sortBy_6(double6 data)
        {
            int6 distOrder = new int6(0, 1, 2, 3, 4, 5);
            double temp = 0.0;
            int tempIndex = 0;
            int index = 0;
            for (int i = 0; i < 6; ++i)
            {
                index = i;
                temp = data[i];
                tempIndex = distOrder[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (temp < data[j])
                    {
                        data[j + 1] = data[j];
                        index = j;
                    }
                }
                data[index] = temp;
                distOrder[index] = tempIndex;
            }
            return distOrder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        internal static double simplex_noise6D(double x, double y, double z, double w, double u, double v, uint seed)
        {
            // Skew
            //self.f = ((self.d + 1) ** .5 - 1) / self.d

            double F4 = (math.sqrt(7.0) - 1.0) / 6.0; //(sqrt(5.0)-1.0)/4.0;

            // Unskew
            // self.g=self.f/(1+self.d*self.f)
            double G4 = F4 / (1.0 + 6.0 * F4);

            double sideLength = math.sqrt(6.0) / (6.0 * F4 + 1.0);
            double a = math.sqrt((sideLength * sideLength) - ((sideLength / 2.0) * (sideLength / 2.0)));
            double cornerFace = math.sqrt(a * a + a / 2.0 * (a / 2.0));

            double cornerFaceSqrd = cornerFace * cornerFace;

            //self.valueScaler=(self.d-1)**-.5
            double valueScaler = math.pow(5.0, -0.5);
            valueScaler *= math.pow(5.0, -3.5) * 100 + 13;

            double6 loc = new double6(x, y, z, w, u, v);
            double s = 0;
            for (int c = 0; c < 6; ++c) s += loc[c];
            s *= F4;

            double6 skewLoc = new double6();
            skewLoc.x = fast_floor(x + s);
            skewLoc.y = fast_floor(y + s);
            skewLoc.z = fast_floor(z + s);
            skewLoc.w = fast_floor(w + s);
            skewLoc.u = fast_floor(u + s);
            skewLoc.v = fast_floor(v + s);

            double6 intLoc = new double6(skewLoc.x, skewLoc.y, skewLoc.z, skewLoc.w, skewLoc.u, skewLoc.z);
            double unskew = 0.0;
            for (int c = 0; c < 6; ++c) unskew += skewLoc[c];
            unskew *= G4;
            double6 cellDist = new double6();
            cellDist.x = loc[0] - skewLoc[0] + unskew;
            cellDist.y = loc[1] - skewLoc[1] + unskew;
            cellDist.z = loc[2] - skewLoc[2] + unskew;
            cellDist.w = loc[3] - skewLoc[3] + unskew;
            cellDist.u = loc[4] - skewLoc[4] + unskew;
            cellDist.v = loc[5] - skewLoc[5] + unskew;

            int6 distOrder = sortBy_6(cellDist);

            double n = 0.0;
            double skewOffset = 0.0;

            double6 uu = new double6();
            for (int c = 0; c < 7; ++c)
            {
                int i = c == 0 ? -1 : distOrder[c - 1];
                if (i != -1) intLoc[i] += 1;

                for (int d = 0; d < 6; ++d)
                {
                    uu[d] = cellDist[d] - (intLoc[d] - skewLoc[d]) + skewOffset;
                }

                double t = cornerFaceSqrd;

                for (int d = 0; d < 6; ++d)
                {
                    t -= uu[d] * uu[d];
                }

                if (t > 0.0)
                {
                    uint h = hash6(intLoc[0], intLoc[1], intLoc[2], intLoc[3], intLoc[4], intLoc[5], seed);
                    double gr = 0.0;
                    for (int d = 0; d < 6; ++d)
                    {
                        gr += gradient6D_lut[h * 6 + d] * uu[d];
                    }

                    n += gr * t * t * t * t;
                }
                skewOffset += G4;
            }
            n *= valueScaler;

            return n;
        }
    }
}