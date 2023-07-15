using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleRotateDomain : ModuleBase
    {
        private ModuleBase m_source;
        private ModuleBase m_ax, m_ay, m_az, m_angledeg;

        public ModuleRotateDomain SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleRotateDomain SetSource(double source) { m_source = new ModuleConstant().SetValue(source).Build(); return this; }
        public ModuleRotateDomain SetAxis(ModuleBase ax, ModuleBase ay, ModuleBase az) { m_ax = ax; m_ay = ay; m_az = az; return this; }
        public ModuleRotateDomain SetAxis(double ax, double ay, double az)
        {
            m_ax = new ModuleConstant().SetValue(ax).Build();
            m_ay = new ModuleConstant().SetValue(ay).Build();
            m_az = new ModuleConstant().SetValue(az).Build();
            return this;
        }
        public ModuleRotateDomain SetAxisX(ModuleBase ax) { m_ax = ax; return this; }
        public ModuleRotateDomain SetAxisY(ModuleBase ay) { m_ay = ay; return this; }
        public ModuleRotateDomain SetAxisZ(ModuleBase az) { m_az = az; return this; }
        public ModuleRotateDomain SetAxisX(double ax) { m_ax = new ModuleConstant().SetValue(ax).Build(); return this; }
        public ModuleRotateDomain SetAxisY(double ay) { m_ay = new ModuleConstant().SetValue(ay).Build(); return this; }
        public ModuleRotateDomain SetAxisZ(double az) { m_az = new ModuleConstant().SetValue(az).Build(); return this; }
        public ModuleRotateDomain SetAngle(ModuleBase angle) { m_angledeg = angle; return this; }
        public ModuleRotateDomain SetAngle(double angle) { m_angledeg = new ModuleConstant().SetValue(angle).Build(); return this; }
        public ModuleRotateDomain Build() { return this; }



        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheangle = CreateCache<double>(length);
            var coords = CreateCache<double2>(length);

            var anglejob = m_angledeg.Get(inputs, cacheangle, dependsOn);
            var rotatedomainjob = RotateDomain2Job.JobHandle(inputs, cacheangle, coords, anglejob);

            var job = m_source.Get(coords, outputs, rotatedomainjob);
            DisposeCache(job, cacheangle);
            DisposeCache(job, coords);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheangle = CreateCache<double>(length);
            var cacheax = CreateCache<double>(length);
            var cacheay = CreateCache<double>(length);
            var cacheaz = CreateCache<double>(length);
            var coords = CreateCache<double3>(length);

            var anglejob = m_angledeg.Get(inputs, cacheangle, dependsOn);
            var axjob = m_ax.Get(inputs, cacheax, anglejob);
            var ayjob = m_ay.Get(inputs, cacheay, axjob);
            var azjob = m_az.Get(inputs, cacheaz, ayjob);
            var rotatedomainjob = RotateDomain3Job.JobHandle(inputs, cacheangle, cacheax, cacheay, cacheaz, coords, azjob);

            var job = m_source.Get(coords, outputs, rotatedomainjob);
            DisposeCache(job, cacheangle, cacheax, cacheay, cacheaz);
            DisposeCache(job, coords);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheangle = CreateCache<double>(length);
            var cacheax = CreateCache<double>(length);
            var cacheay = CreateCache<double>(length);
            var cacheaz = CreateCache<double>(length);
            var coords = CreateCache<double4>(length);

            var anglejob = m_angledeg.Get(inputs, cacheangle, dependsOn);
            var axjob = m_ax.Get(inputs, cacheax, anglejob);
            var ayjob = m_ay.Get(inputs, cacheay, axjob);
            var azjob = m_az.Get(inputs, cacheaz, ayjob);
            var rotatedomainjob = RotateDomain4Job.JobHandle(inputs, cacheangle, cacheax, cacheay, cacheaz, coords, azjob);

            var job = m_source.Get(coords, outputs, rotatedomainjob);
            DisposeCache(job, cacheangle, cacheax, cacheay, cacheaz);
            DisposeCache(job, coords);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cacheangle = CreateCache<double>(length);
            var cacheax = CreateCache<double>(length);
            var cacheay = CreateCache<double>(length);
            var cacheaz = CreateCache<double>(length);
            var coords = CreateCache<double6>(length);

            var anglejob = m_angledeg.Get(inputs, cacheangle, dependsOn);
            var axjob = m_ax.Get(inputs, cacheax, anglejob);
            var ayjob = m_ay.Get(inputs, cacheay, axjob);
            var azjob = m_az.Get(inputs, cacheaz, ayjob);
            var rotatedomainjob = RotateDomain6Job.JobHandle(inputs, cacheangle, cacheax, cacheay, cacheaz, coords, azjob);

            var job = m_source.Get(coords, outputs, rotatedomainjob);
            DisposeCache(job, cacheangle, cacheax, cacheay, cacheaz);
            DisposeCache(job, coords);
            return job;
        }
    }
}