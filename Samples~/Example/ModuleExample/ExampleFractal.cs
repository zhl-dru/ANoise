using ANoise;

/// <summary>
/// ��ʾ��ֻ��ʾģ����÷�,
/// �����������ȷ,��Ϊ������Χ��[-1,1],
/// ���õ����������ExampleAutoCorrect��
/// </summary>
public class ExampleFractal : Example
{
    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            ModuleFractal fractal = new ModuleFractal()
            .SetSeed(NoiseSet.Seed)
            .SetOctaves(NoiseSet.Octaves)
            .SetFrequency(NoiseSet.Frequency)
            .SetLacunarity(NoiseSet.Lacunarity)
            .SetDerivSpacing(NoiseSet.DerivSpacing)
            .SetBasisType(NoiseSet.BasisType)
            .SetFractalType(NoiseSet.FractalType)
            .SetInterpTypes(NoiseSet.InterpType)
            .Build();
            Complete(fractal);
        });

        DrawImage();
        Dispose();
    }
}
