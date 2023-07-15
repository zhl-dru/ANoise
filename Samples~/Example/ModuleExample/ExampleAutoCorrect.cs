using ANoise;

/// <summary>
/// ModuleAutoCorrect.Build()ʹ�������10000��2/3/4/6d�������Դģ��������Сֵ
/// ����һ���൱��ʱ�Ĺ��̣�ʵ��ʹ����Ӧ��ִֻ��һ��
/// </summary>
public class ExampleAutoCorrect : Example
{
    public double Low = 0.0;
    public double High = 1.0;

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
            ModuleAutoCorrect autoCorrect = new ModuleAutoCorrect()
            .SetSeed(NoiseSet.Seed)
            .SetSource(fractal)
            .SetRange(Low, High)
            .Build();
            Complete(autoCorrect);
        });

        DrawImage();
        Dispose();
    }
}
