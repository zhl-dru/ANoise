using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ANoise
{
    public class ModuleAutoCorrect : ModuleBase
    {
        private ModuleBase m_source;
        private int m_seed;
        private double m_low, m_high;
        private double m_scale2, m_offset2;
        private double m_scale3, m_offset3;
        private double m_scale4, m_offset4;
        private double m_scale6, m_offset6;

        public ModuleAutoCorrect SetSource(ModuleBase source) { m_source = source; return this; }
        public ModuleAutoCorrect SetSeed(int seed) { m_seed = seed; return this; }
        public ModuleAutoCorrect SetRange(double low, double high) { m_low = low; m_high = high; return this; }
        public ModuleAutoCorrect Build()
        {
            var random = new Random((uint)m_seed);

            double mn, mx, v;

            var coords2d = new NativeArray<double2>(10000, Allocator.TempJob);
            var coords3d = new NativeArray<double3>(10000, Allocator.TempJob);
            var coords4d = new NativeArray<double4>(10000, Allocator.TempJob);
            var coords6d = new NativeArray<double6>(10000, Allocator.TempJob);

            var result2d = new NativeArray<double>(10000, Allocator.TempJob);
            var result3d = new NativeArray<double>(10000, Allocator.TempJob);
            var result4d = new NativeArray<double>(10000, Allocator.TempJob);
            var result6d = new NativeArray<double>(10000, Allocator.TempJob);

            for (int c = 0; c < 10000; ++c)
            {
                double nx = random.NextDouble() * 4.0 - 2.0;
                double ny = random.NextDouble() * 4.0 - 2.0;
                double nz = random.NextDouble() * 4.0 - 2.0;
                double nw = random.NextDouble() * 4.0 - 2.0;
                double nu = random.NextDouble() * 4.0 - 2.0;
                double nv = random.NextDouble() * 4.0 - 2.0;

                coords2d[c] = new double2(nx, ny);
                coords3d[c] = new double3(nx, ny, nz);
                coords4d[c] = new double4(nx, ny, nz, nw);
                coords6d[c] = new double6(nx, ny, nz, nw, nu, nv);
            }

            var job2d = m_source.Get(coords2d, result2d);
            var job3d = m_source.Get(coords3d, result3d);
            var job4d = m_source.Get(coords4d, result4d);
            var job6d = m_source.Get(coords6d, result6d);

            job2d.Complete(); job3d.Complete(); job4d.Complete(); job6d.Complete();

            coords2d.Dispose(); coords3d.Dispose(); coords4d.Dispose(); coords6d.Dispose();

            result2d.SortJob().Schedule().Complete();
            result3d.SortJob().Schedule().Complete();
            result4d.SortJob().Schedule().Complete();
            result6d.SortJob().Schedule().Complete();

            // Calculate 2D
            mn = 10000.0; mx = -10000.0;
            v = result2d[0]; if (v < mn) mn = v;
            v = result2d[9999]; if (v > mx) mx = v;
            m_scale2 = (m_high - m_low) / (mx - mn);
            m_offset2 = m_low - mn * m_scale2;


            // Calculate 3D
            mn = 10000.0; mx = -10000.0;
            v = result3d[0]; if (v < mn) mn = v;
            v = result3d[9999]; if (v > mx) mx = v;
            m_scale3 = (m_high - m_low) / (mx - mn);
            m_offset3 = m_low - mn * m_scale3;

            // Calculate 4D
            mn = 10000.0; mx = -10000.0;
            v = result4d[0]; if (v < mn) mn = v;
            v = result4d[9999]; if (v > mx) mx = v;
            m_scale4 = (m_high - m_low) / (mx - mn);
            m_offset4 = m_low - mn * m_scale4;

            // Calculate 6D
            mn = 10000.0; mx = -10000.0;
            v = result6d[0]; if (v < mn) mn = v;
            v = result6d[9999]; if (v > mx) mx = v;
            m_scale6 = (m_high - m_low) / (mx - mn);
            m_offset6 = m_low - mn * m_scale6;

            result2d.Dispose(); result3d.Dispose(); result4d.Dispose(); result6d.Dispose();
            return this;
        }

        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AutoCorrectJob.JobHandle(cache, m_low, m_high, m_scale2, m_offset2, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AutoCorrectJob.JobHandle(cache, m_low, m_high, m_scale2, m_offset2, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AutoCorrectJob.JobHandle(cache, m_low, m_high, m_scale2, m_offset2, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }

        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            int length = inputs.Length;
            var cache = CreateCache<double>(length);
            var sourcejob = m_source.Get(inputs, cache, dependsOn);
            var job = AutoCorrectJob.JobHandle(cache, m_low, m_high, m_scale2, m_offset2, outputs, sourcejob);
            DisposeCache(job, cache);
            return job;
        }
    }
}