using ANoise;

public class ExampleBrightContrast : Example
{
    public double Bright = -0.5;
    public double Threshold = 0.25;
    public double Factor = 2.0;

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
            .SetRange(0, 1)
            .Build();
            ModuleBrightContrast brightContrast = new ModuleBrightContrast()
            .SetSource(autoCorrect)
            .SetBright(Bright)
            .SetThreshold(Threshold)
            .SetFactor(Factor)
            .Build();
            Complete(brightContrast);
        });

        DrawImage();
        Dispose();
    }
}
