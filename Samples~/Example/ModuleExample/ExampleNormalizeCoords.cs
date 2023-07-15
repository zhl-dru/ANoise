using ANoise;

public class ExampleNormalizeCoords : Example
{
    public double length = 1.0;

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
            ModuleNormalizeCoords normalizeCoords = new ModuleNormalizeCoords()
            .SetSource(fractal)
            .SetLength(length)
            .Build();
            ModuleAutoCorrect autoCorrect = new ModuleAutoCorrect()
            .SetSource(normalizeCoords)
            .SetSeed(NoiseSet.Seed)
            .SetRange(0, 1)
            .Build();
            Complete(autoCorrect);
        });

        DrawImage();
        Dispose();
    }
}
