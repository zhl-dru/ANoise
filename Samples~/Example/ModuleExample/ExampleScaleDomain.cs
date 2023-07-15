using ANoise;

public class ExampleScaleDomain : Example
{
    public double X = 0.5;
    public double Y = 0.5;

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
            ModuleScaleDomain scaleDomain = new ModuleScaleDomain()
            .SetSource(autoCorrect)
            .SetScaleX(X)
            .SetScaleY(Y)
            .SetScaleZ(0)
            .SetScaleW(0)
            .SetScaleU(0)
            .SetScaleV(0)
            .Build();

            Complete(scaleDomain);
        });

        DrawImage();
        Dispose();
    }
}
