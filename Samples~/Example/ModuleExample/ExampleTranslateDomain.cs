using ANoise;

public class ExampleTranslateDomain : Example
{
    public double X = 1.0;
    public double Y = 1.0;

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
            ModuleTranslateDomain translateDomain = new ModuleTranslateDomain()
            .SetSource(autoCorrect)
            .SetAxisX(X)
            .SetAxisY(Y)
            .SetAxisZ(0)
            .SetAxisW(0)
            .SetAxisU(0)
            .SetAxisV(0)
            .Build();
            Complete(translateDomain);
        });

        DrawImage();
        Dispose();
    }
}
