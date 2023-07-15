using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleGradient : ModuleBase
    {
        private double m_gx1, m_gy1, m_gz1, m_gw1, m_gu1, m_gv1;
        private double m_gx2, m_gy2, m_gz2, m_gw2, m_gu2, m_gv2;
        private double m_x, m_y, m_z, m_w, m_u, m_v;
        private double m_vlen;

        public ModuleGradient SetX(double x1, double x2) { m_gx1 = x1; m_gx2 = x2; return this; }
        public ModuleGradient SetY(double y1, double y2) { m_gy1 = y1; m_gy2 = y2; return this; }
        public ModuleGradient SetZ(double z1, double z2) { m_gz1 = z1; m_gz2 = z2; return this; }
        public ModuleGradient SetW(double w1, double w2) { m_gw1 = w1; m_gw2 = w2; return this; }
        public ModuleGradient SetU(double u1, double u2) { m_gu1 = u1; m_gu2 = u2; return this; }
        public ModuleGradient SetV(double v1, double v2) { m_gv1 = v1; m_gv2 = v2; return this; }
        public ModuleGradient Build()
        {
            m_x = m_gx2 - m_gx1;
            m_y = m_gy2 - m_gy1;
            m_z = m_gz2 - m_gz1;
            m_w = m_gw2 - m_gw1;
            m_u = m_gu2 - m_gu1;
            m_v = m_gv2 - m_gv1;
            m_vlen = m_x * m_x + m_y * m_y + m_z * m_z + m_w * m_w + m_u * m_u + m_v * m_v;
            return this;
        }

        private DGradient GetData()
        {
            return new DGradient(m_gx1, m_gy1, m_gz1, m_gw1, m_gu1, m_gv1,
                m_x, m_y, m_z, m_w, m_u, m_v, m_vlen);
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DGradient data = GetData();
            return Gradient2Job.JobHandle(inputs, data, outputs, dependsOn);
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DGradient data = GetData();
            return Gradient3Job.JobHandle(inputs, data, outputs, dependsOn);
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DGradient data = GetData();
            return Gradient4Job.JobHandle(inputs, data, outputs, dependsOn);
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DGradient data = GetData();
            return Gradient6Job.JobHandle(inputs, data, outputs, dependsOn);
        }
    }

    public struct DGradient
    {
        public double Gx, Gy, Gz, Gw, Gu, Gv;
        public double X, Y, Z, W, U, V;
        public double Vlen;

        public DGradient(double gx, double gy, double gz, double gw, double gu, double gv, double x, double y, double z, double w, double u, double v, double vlen)
        {
            Gx = gx;
            Gy = gy;
            Gz = gz;
            Gw = gw;
            Gu = gu;
            Gv = gv;
            X = x;
            Y = y;
            Z = z;
            W = w;
            U = u;
            V = v;
            Vlen = vlen;
        }
    }
}