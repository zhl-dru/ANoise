using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Random = Unity.Mathematics.Random;

namespace ANoise
{
    public class ModuleFractal : ModuleBase
    {
        private const int MaxSources = 20;
        private const int SeedOffset = 300;


        private double m_offset, m_gain, m_h;
        private double m_frequency, m_lacunarity = 2.0;
        private int m_octaves;
        private EFractalTypes m_fractaltype;

        private readonly double[] m_exparray = new double[MaxSources];
        private readonly double[] m_correct = new double[MaxSources * 2];

        private uint m_seed;

        private double[] m_cos2d = new double[MaxSources], m_sin2d = new double[MaxSources];
        private double[] m_rotmatrix = new double[MaxSources * 3 * 3];

        private EBasisTypes m_basistype;
        private EInterpTypes m_interptype;

        private double m_spacing = 0.0001d;

        public ModuleFractal SetSeed(int seed) { m_seed = (uint)seed; return this; }
        public ModuleFractal SetFractalType(EFractalTypes fractalType) { m_fractaltype = fractalType; return this; }
        public ModuleFractal SetBasisType(EBasisTypes basisType) { m_basistype = basisType; return this; }
        public ModuleFractal SetInterpTypes(EInterpTypes interpType) { m_interptype = interpType; return this; }
        public ModuleFractal SetOctaves(int octaves) { m_octaves = octaves < 1 ? 1 : (octaves > MaxSources ? MaxSources : octaves); return this; }
        public ModuleFractal SetFrequency(double frequency) { m_frequency = frequency; return this; }
        public ModuleFractal SetLacunarity(double lacunarity) { m_lacunarity = lacunarity; return this; }
        public ModuleFractal SetDerivSpacing(double spacing) { m_spacing = spacing; return this; }
        public ModuleFractal Build()
        {
            //set seed
            for (int i = 0; i < MaxSources; ++i)
            {
                var random = new Random((uint)(m_seed + i * SeedOffset));

                double ax, ay, az;
                double len;

                ax = random.NextDouble();
                ay = random.NextDouble();
                az = random.NextDouble();
                len = math.sqrt(ax * ax + ay * ay + az * az);
                ax /= len;
                ay /= len;
                az /= len;
                SetRotationAngle(ax, ay, az, random.NextDouble() * 3.141592 * 2.0, i);
                double angle = random.NextDouble() * 3.14159265 * 2.0;
                m_cos2d[i] = math.cos(angle);
                m_sin2d[i] = math.sin(angle);
            }
            //set fractaltype
            switch (m_fractaltype)
            {
                case EFractalTypes.FBM: m_h = 1.0; m_gain = 0.5; m_offset = 0; FBm_calcWeights(); break;
                case EFractalTypes.RIDGEDMULTI: m_h = 0.9; m_gain = 0.5; m_offset = 1; RidgedMulti_calcWeights(); break;
                case EFractalTypes.BILLOW: m_h = 1; m_gain = 0.5; m_offset = 0; Billow_calcWeights(); break;
                case EFractalTypes.MULTI: m_h = 1; m_offset = 0; m_gain = 0; Multi_calcWeights(); break;
                case EFractalTypes.HYBRIDMULTI: m_h = 0.25; m_gain = 1; m_offset = 0.7; HybridMulti_calcWeights(); break;
                case EFractalTypes.DECARPENTIERSWISS: m_h = 0.9; m_gain = 0.6; m_offset = 0.15; DeCarpentierSwiss_calcWeights(); break;
                default: m_h = 1.0; m_gain = 0; m_offset = 0; FBm_calcWeights(); break;
            }
            return this;
        }



        public override JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DFractal data = GetData();
            return Fractal2Job.JobHandle(inputs, outputs, data, dependsOn);
        }
        public override JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DFractal data = GetData();
            return Fractal3Job.JobHandle(inputs, outputs, data, dependsOn);
        }
        public override JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DFractal data = GetData();
            return Fractal4Job.JobHandle(inputs, outputs, data, dependsOn);
        }
        public override JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default)
        {
            DFractal data = GetData();
            return Fractal6Job.JobHandle(inputs, outputs, data, dependsOn);
        }


        private DFractal GetData()
        {
            return new DFractal(m_basistype, m_fractaltype, m_interptype,
                m_seed, m_octaves, m_frequency, m_lacunarity, m_gain, m_offset,
                m_spacing, m_exparray, m_correct, m_cos2d, m_sin2d, m_rotmatrix);
        }
        private void SetRotationAngle(double x, double y, double z, double angle, int index)
        {
            m_rotmatrix[0 + index * 9] = 1 + (1 - math.cos(angle)) * (x * x - 1);
            m_rotmatrix[1 + index * 9] = -z * math.sin(angle) + (1 - math.cos(angle)) * x * y;
            m_rotmatrix[2 + index * 9] = y * math.sin(angle) + (1 - math.cos(angle)) * x * z;

            m_rotmatrix[3 + index * 9] = z * math.sin(angle) + (1 - math.cos(angle)) * x * y;
            m_rotmatrix[4 + index * 9] = 1 + (1 - math.cos(angle)) * (y * y - 1);
            m_rotmatrix[5 + index * 9] = -x * math.sin(angle) + (1 - math.cos(angle)) * y * z;

            m_rotmatrix[6 + index * 9] = -y * math.sin(angle) + (1 - math.cos(angle)) * x * z;
            m_rotmatrix[7 + index * 9] = x * math.sin(angle) + (1 - math.cos(angle)) * y * z;
            m_rotmatrix[8 + index * 9] = 1 + (1 - math.cos(angle)) * (z * z - 1);
        }

        #region Weights
        private void FBm_calcWeights()
        {
            double minvalue = 0.0, maxvalue = 0.0;
            for (int i = 0; i < MaxSources; ++i)
            {
                double exparray = math.pow(m_lacunarity, -i * m_h);
                m_exparray[i] = exparray;

                minvalue += -1.0 * exparray;
                maxvalue += 1.0 * exparray;
                double A = -1.0, B = 1.0;
                double scale = (B - A) / (maxvalue - minvalue);
                double bias = A - minvalue * scale;
                m_correct[i * 2 + 0] = scale;
                m_correct[i * 2 + 1] = bias;
            }
        }
        private void RidgedMulti_calcWeights()
        {
            double minvalue = 0.0, maxvalue = 0.0;
            for (int i = 0; i < MaxSources; ++i)
            {
                double exparray = math.pow(m_lacunarity, -i * m_h);
                m_exparray[i] = exparray;
                minvalue += (m_offset - 1.0) * (m_offset - 1.0) * exparray;
                maxvalue += (m_offset) * (m_offset) * exparray;
                double A = -1.0, B = 1.0;
                double scale = (B - A) / (maxvalue - minvalue);
                double bias = A - minvalue * scale;
                m_correct[i * 2 + 0] = scale;
                m_correct[i * 2 + 1] = bias;
            }
        }
        private void Billow_calcWeights()
        {
            double minvalue = 0.0, maxvalue = 0.0;
            for (int i = 0; i < MaxSources; ++i)
            {
                double exparray = math.pow(m_lacunarity, -i * m_h);
                m_exparray[i] = exparray;

                minvalue += (m_offset - 1.0) * (m_offset - 1.0) * exparray;
                maxvalue += (m_offset) * (m_offset) * exparray;

                double A = -1.0, B = 1.0;
                double scale = (B - A) / (maxvalue - minvalue);
                double bias = A - minvalue * scale;
                m_correct[i * 2 + 0] = scale;
                m_correct[i * 2 + 1] = bias;
            }
        }
        private void Multi_calcWeights()
        {
            double minvalue = 1.0, maxvalue = 1.0;
            for (int i = 0; i < MaxSources; ++i)
            {
                double exparray = math.pow(m_lacunarity, -i * m_h);
                m_exparray[i] = exparray;

                minvalue *= -1.0 * exparray + 1.0;
                maxvalue *= 1.0 * exparray + 1.0;

                double A = -1.0, B = 1.0;
                double scale = (B - A) / (maxvalue - minvalue);
                double bias = A - minvalue * scale;
                m_correct[i * 2 + 0] = scale;
                m_correct[i * 2 + 1] = bias;
            }
        }
        private void DeCarpentierSwiss_calcWeights()
        {
            double minvalue = 0.0, maxvalue = 0.0;
            for (int i = 0; i < MaxSources; ++i)
            {
                double exparray = math.pow(m_lacunarity, -i * m_h);
                m_exparray[i] = exparray;

                minvalue += (m_offset - 1.0) * (m_offset - 1.0) * exparray;
                maxvalue += m_offset * m_offset * exparray;

                double A = -1.0, B = 1.0;
                double scale = (B - A) / (maxvalue - minvalue);
                double bias = A - minvalue * scale;
                m_correct[i * 2 + 0] = scale;
                m_correct[i * 2 + 1] = bias;
            }
        }
        private void HybridMulti_calcWeights()
        {
            double minvalue = 1.0, maxvalue = 1.0;
            double weightmin = 0.0, weightmax = 0.0;
            double A = -1.0, B = 1.0, scale, bias;
            for (int i = 0; i < MaxSources; ++i)
            {
                double exparray = math.pow(m_lacunarity, -i * m_h);
                m_exparray[i] = exparray;

                if (i == 0)
                {
                    minvalue = m_offset - 1.0;
                    maxvalue = m_offset + 1.0;
                    weightmin = m_gain * minvalue;
                    weightmax = m_gain * maxvalue;

                    scale = (B - A) / (maxvalue - minvalue);
                    bias = A - minvalue * scale;

                }
                else
                {
                    if (weightmin > 1.0) weightmin = 1.0;
                    if (weightmax > 1.0) weightmax = 1.0;

                    double signal = (m_offset - 1.0) * exparray;
                    minvalue += signal * weightmin;
                    weightmin *= m_gain * signal;

                    signal = (m_offset + 1.0) * exparray;
                    maxvalue += signal * weightmax;
                    weightmax *= m_gain * signal;


                    scale = (B - A) / (maxvalue - minvalue);
                    bias = A - minvalue * scale;
                }
                m_correct[i * 2 + 0] = scale;
                m_correct[i * 2 + 1] = bias;
            }

        }
        #endregion
    }

    public struct DFractal
    {
        public EBasisTypes BasisType;
        public EFractalTypes FractalType;
        public EInterpTypes InterpType;

        public uint Seed;
        public int Octaves;
        public double Frequency, Lacunarity;
        public double Gain, Offset;

        public double Spacing;

        /// <summary>
        /// 所有计算用的数组缓存在这里
        /// 按照 0:exparray,1-2:correct,3:cos2d,4:sin2d,
        ///      5-13:rotmatrix
        /// 的顺序排列,共MaxSources个排列
        /// 当MaxSources=20时占用2240byte
        /// </summary>
        public FixedList4096Bytes<double> MagicNumbers;

        public DFractal(EBasisTypes basisType, EFractalTypes fractalType, EInterpTypes interpType,
            uint seed, int octaves, double frequency, double lacunarity, double gain, double offset,
            double spacing, double[] exparray, double[] correct, double[] cos2d, double[] sin2d,
            double[] rotmatrix)
        {
            BasisType = basisType;
            FractalType = fractalType;
            InterpType = interpType;
            Seed = seed;
            Octaves = octaves;
            Frequency = frequency;
            Lacunarity = lacunarity;
            Gain = gain;
            Offset = offset;
            Spacing = spacing;
            MagicNumbers = new FixedList4096Bytes<double>();
            for (int i = 0; i < 20; i++)
            {
                MagicNumbers.Add(exparray[i]);
                MagicNumbers.Add(correct[i * 2]);
                MagicNumbers.Add(correct[i * 2 + 1]);
                MagicNumbers.Add(cos2d[i]);
                MagicNumbers.Add(sin2d[i]);
                MagicNumbers.Add(rotmatrix[i * 9]);
                MagicNumbers.Add(rotmatrix[i * 9 + 1]);
                MagicNumbers.Add(rotmatrix[i * 9 + 2]);
                MagicNumbers.Add(rotmatrix[i * 9 + 3]);
                MagicNumbers.Add(rotmatrix[i * 9 + 4]);
                MagicNumbers.Add(rotmatrix[i * 9 + 5]);
                MagicNumbers.Add(rotmatrix[i * 9 + 6]);
                MagicNumbers.Add(rotmatrix[i * 9 + 7]);
                MagicNumbers.Add(rotmatrix[i * 9 + 8]);
            }
        }
    }


}