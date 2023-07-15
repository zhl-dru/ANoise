using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleSphere : ModuleBase
    {
        private ModuleBase m_cx, m_cy, m_cz, m_cw, m_cu, m_cv;
        private ModuleBase m_radius;

        public ModuleSphere SetCenterX(ModuleBase cx) { m_cx = cx; return this; }
        public ModuleSphere SetCenterY(ModuleBase cy) { m_cy = cy; return this; }
        public ModuleSphere SetCenterZ(ModuleBase cz) { m_cz = cz; return this; }
        public ModuleSphere SetCenterW(ModuleBase cw) { m_cw = cw; return this; }
        public ModuleSphere SetCenterU(ModuleBase cu) { m_cu = cu; return this; }
        public ModuleSphere SetCenterV(ModuleBase cv) { m_cv = cv; return this; }
        public ModuleSphere SetCenterX(double cx) { m_cx = new ModuleConstant().SetValue(cx).Build(); return this; }
        public ModuleSphere SetCenterY(double cy) { m_cy = new ModuleConstant().SetValue(cy).Build(); return this; }
        public ModuleSphere SetCenterZ(double cz) { m_cz = new ModuleConstant().SetValue(cz).Build(); return this; }
        public ModuleSphere SetCenterW(double cw) { m_cw = new ModuleConstant().SetValue(cw).Build(); return this; }
        public ModuleSphere SetCenterU(double cu) { m_cu = new ModuleConstant().SetValue(cu).Build(); return this; }
        public ModuleSphere SetCenterV(double cv) { m_cv = new ModuleConstant().SetValue(cv).Build(); return this; }
        public ModuleSphere SetRadius(ModuleBase r) { m_radius = r; return this; }
        public ModuleSphere SetRadius(double r) { m_radius = new ModuleConstant().SetValue(r).Build(); return this; }
        public ModuleSphere Build() { return this; }


        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var cacheRad = CreateCache<double>(length);

            var xjob = m_cx.Get(inputs, cacheX, dependsOn);
            var yjob = m_cy.Get(inputs, cacheY, xjob);
            var rjob = m_radius.Get(inputs, cacheRad, yjob);

            var job = Sphere2Job.JobHandle(inputs, cacheX, cacheY, cacheRad, outputs, rjob);
            DisposeCache(job, cacheX, cacheY, cacheRad);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var cacheZ = CreateCache<double>(length);
            var cacheRad = CreateCache<double>(length);

            var xjob = m_cx.Get(inputs, cacheX, dependsOn);
            var yjob = m_cy.Get(inputs, cacheY, xjob);
            var zjob = m_cz.Get(inputs, cacheZ, yjob);
            var rjob = m_radius.Get(inputs, cacheRad, zjob);

            var job = Sphere3Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheRad, outputs, rjob);
            DisposeCache(job, cacheX, cacheY, cacheZ, cacheRad);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheX = CreateCache<double>(length);
            var cacheY = CreateCache<double>(length);
            var cacheZ = CreateCache<double>(length);
            var cacheW = CreateCache<double>(length);
            var cacheRad = CreateCache<double>(length);

            var xjob = m_cx.Get(inputs, cacheX, dependsOn);
            var yjob = m_cy.Get(inputs, cacheY, xjob);
            var zjob = m_cz.Get(inputs, cacheZ, yjob);
            var wjob = m_cw.Get(inputs, cacheX, zjob);
            var rjob = m_radius.Get(inputs, cacheRad, wjob);

            var job = Sphere4Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheW, cacheRad, outputs, rjob);
            DisposeCache(job, cacheX, cacheY, cacheZ, cacheW, cacheRad);
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
            var cacheRad = CreateCache<double>(length);

            var xjob = m_cx.Get(inputs, cacheX, dependsOn);
            var yjob = m_cy.Get(inputs, cacheY, xjob);
            var zjob = m_cz.Get(inputs, cacheZ, yjob);
            var wjob = m_cw.Get(inputs, cacheW, zjob);
            var ujob = m_cu.Get(inputs, cacheU, wjob);
            var vjob = m_cv.Get(inputs, cacheV, ujob);
            var rjob = m_radius.Get(inputs, cacheRad, vjob);

            var job = Sphere6Job.JobHandle(inputs, cacheX, cacheY, cacheZ, cacheW, cacheU, cacheV, cacheRad, outputs, rjob);
            DisposeCache(job, cacheX, cacheY, cacheZ, cacheW, cacheU, cacheV, cacheRad);
            return job;
        }
    }
}