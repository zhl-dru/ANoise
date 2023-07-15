using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleTranslateDomain : ModuleBase
    {
        private ModuleBase m_source, m_ax, m_ay, m_az, m_aw, m_au, m_av;

        public ModuleTranslateDomain SetAxisX(ModuleBase x) { m_ax = x; return this; }
        public ModuleTranslateDomain SetAxisY(ModuleBase y) { m_ay = y; return this; }
        public ModuleTranslateDomain SetAxisZ(ModuleBase z) { m_az = z; return this; }
        public ModuleTranslateDomain SetAxisW(ModuleBase w) { m_aw = w; return this; }
        public ModuleTranslateDomain SetAxisU(ModuleBase u) { m_au = u; return this; }
        public ModuleTranslateDomain SetAxisV(ModuleBase v) { m_av = v; return this; }
        public ModuleTranslateDomain SetAxisX(double x) { m_ax = new ModuleConstant().SetValue(x).Build(); return this; }
        public ModuleTranslateDomain SetAxisY(double y) { m_ay = new ModuleConstant().SetValue(y).Build(); return this; }
        public ModuleTranslateDomain SetAxisZ(double z) { m_az = new ModuleConstant().SetValue(z).Build(); return this; }
        public ModuleTranslateDomain SetAxisW(double w) { m_aw = new ModuleConstant().SetValue(w).Build(); return this; }
        public ModuleTranslateDomain SetAxisU(double u) { m_au = new ModuleConstant().SetValue(u).Build(); return this; }
        public ModuleTranslateDomain SetAxisV(double v) { m_av = new ModuleConstant().SetValue(v).Build(); return this; }
        public ModuleTranslateDomain SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleTranslateDomain SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleTranslateDomain Build() { return this; }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var coords = CreateCache<double2>(length);

            var xjob = m_ax.Get(inputs, cacheX, dependsOn);
            var yjob = m_ay.Get(inputs, cacheY, xjob);

            var translatedomainjob = TranslateDomain2Job.JobHandle(inputs, cacheX, cacheY, coords, yjob);

            var job = m_source.Get(coords, outputs, translatedomainjob);
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

            var xjob = m_ax.Get(inputs, cacheX, dependsOn);
            var yjob = m_ay.Get(inputs, cacheY, xjob);
            var zjob = m_az.Get(inputs, cacheZ, yjob);

            var translatedomainjob = TranslateDomain3Job.JobHandle(inputs, cacheX, cacheY, cacheZ, coords, zjob);

            var job = m_source.Get(coords, outputs, translatedomainjob);
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

            var xjob = m_ax.Get(inputs, cacheX, dependsOn);
            var yjob = m_ay.Get(inputs, cacheY, xjob);
            var zjob = m_az.Get(inputs, cacheZ, yjob);
            var wjob = m_aw.Get(inputs, cacheW, zjob);

            var translatedomainjob = TranslateDomain4Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheW, coords, wjob);

            var job = m_source.Get(coords, outputs, translatedomainjob);
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
            

            var xjob = m_ax.Get(inputs, cacheX, dependsOn);
            var yjob = m_ay.Get(inputs, cacheY, xjob);
            var zjob = m_az.Get(inputs, cacheZ, yjob);
            var wjob = m_aw.Get(inputs, cacheW, zjob);
            var ujob = m_au.Get(inputs, cacheU, wjob);
            var vjob = m_av.Get(inputs, cacheV, ujob);

            var translatedomainjob = TranslateDomain6Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheW, cacheU, cacheV, coords, vjob);

            var job = m_source.Get(coords, outputs, translatedomainjob);
            DisposeCache(job, cacheX, cacheY, cacheZ, cacheW, cacheU, cacheV);
            DisposeCache(job, coords);
            return job;
        }
    }
}