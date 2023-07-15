using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleScaleDomain : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_sx, m_sy, m_sz, m_sw, m_su, m_sv;

        public ModuleScaleDomain SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleScaleDomain SetSource(double source) { m_source = m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleScaleDomain SetScaleX(ModuleBase x) { m_sx = x; return this; }
        public ModuleScaleDomain SetScaleY(ModuleBase y) { m_sy = y; return this; }
        public ModuleScaleDomain SetScaleZ(ModuleBase z) { m_sz = z; return this; }
        public ModuleScaleDomain SetScaleW(ModuleBase w) { m_sw = w; return this; }
        public ModuleScaleDomain SetScaleU(ModuleBase u) { m_su = u; return this; }
        public ModuleScaleDomain SetScaleV(ModuleBase v) { m_sv = v; return this; }
        public ModuleScaleDomain SetScaleX(double x) { m_sx = new ModuleConstant().SetValue(x).Build(); return this; }
        public ModuleScaleDomain SetScaleY(double y) { m_sy = new ModuleConstant().SetValue(y).Build(); return this; }
        public ModuleScaleDomain SetScaleZ(double z) { m_sz = new ModuleConstant().SetValue(z).Build(); return this; }
        public ModuleScaleDomain SetScaleW(double w) { m_sw = new ModuleConstant().SetValue(w).Build(); return this; }
        public ModuleScaleDomain SetScaleU(double u) { m_su = new ModuleConstant().SetValue(u).Build(); return this; }
        public ModuleScaleDomain SetScaleV(double v) { m_sv = new ModuleConstant().SetValue(v).Build(); return this; }
        public ModuleScaleDomain Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var coords = CreateCache<double2>(length);

            var xjob = m_sx.Get(inputs, cacheX, dependsOn);
            var yjob = m_sy.Get(inputs, cacheY, xjob);

            var scaledomainjob = ScaleDomain2Job.JobHandle(inputs, cacheX, cacheY, coords, yjob);

            var job = m_source.Get(coords, outputs, scaledomainjob);
            DisposeCache(job, cacheX, cacheY);
            DisposeCache(job, coords);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var cacheZ = CreateCache<double>(length);
            var coords = CreateCache<double3>(length);

            var xjob = m_sx.Get(inputs, cacheX, dependsOn);
            var yjob = m_sy.Get(inputs, cacheY, xjob);
            var zjob = m_sz.Get(inputs, cacheZ, yjob);

            var scaledomainjob = ScaleDomain3Job.JobHandle(inputs, cacheX, cacheY, cacheZ, coords, zjob);

            var job = m_source.Get(coords, outputs, scaledomainjob);
            DisposeCache(job, cacheX, cacheY, cacheZ);
            DisposeCache(job, coords);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var cacheZ = CreateCache<double>(length);
            var cacheW = CreateCache<double>(length);
            var coords = CreateCache<double4>(length);

            var xjob = m_sx.Get(inputs, cacheX, dependsOn);
            var yjob = m_sy.Get(inputs, cacheY, xjob);
            var zjob = m_sz.Get(inputs, cacheZ, yjob);
            var wjob = m_sw.Get(inputs, cacheW, zjob);

            var scaledomainjob = ScaleDomain4Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheW, coords, wjob);

            var job = m_source.Get(coords, outputs, scaledomainjob);
            DisposeCache(job, cacheX, cacheY, cacheZ, cacheW);
            DisposeCache(job, coords);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var cacheZ = CreateCache<double>(length);
            var cacheW = CreateCache<double>(length);
            var cacheU = CreateCache<double>(length);
            var cacheV = CreateCache<double>(length);
            var coords = CreateCache<double6>(length);

            var xjob = m_sx.Get(inputs, cacheX, dependsOn);
            var yjob = m_sy.Get(inputs, cacheY, xjob);
            var zjob = m_sz.Get(inputs, cacheZ, yjob);
            var wjob = m_sw.Get(inputs, cacheW, zjob);
            var ujob = m_su.Get(inputs, cacheU, wjob);
            var vjob = m_sv.Get(inputs, cacheV, ujob);

            var scaledomainjob = ScaleDomain6Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheW, cacheU, cacheV, coords, vjob);

            var job = m_source.Get(coords, outputs, scaledomainjob);
            DisposeCache(job, cacheX, cacheY, cacheZ, cacheW, cacheU, cacheV);
            DisposeCache(job, coords);
            return job;
        }
    }


}