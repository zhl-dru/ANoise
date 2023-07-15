using ANoise;

/// <summary>
/// ModuleAutoCorrect.Build()使用随机的10000个2/3/4/6d坐标估计源模块的最大最小值
/// 这是一个相当耗时的过程，实际使用中应该只执行一次
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
