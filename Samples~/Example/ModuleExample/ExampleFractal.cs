using ANoise;

/// <summary>
/// 该示例只演示模块的用法,
/// 纹理输出不正确,因为噪声范围是[-1,1],
/// 良好的纹理输出在ExampleAutoCorrect中
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
