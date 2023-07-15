using ANoise;

public class ExampleTiers : Example
{
    public int NumTiers = 5;
    public bool Smooth = true;

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
            ModuleTiers tiers = new ModuleTiers()
            .SetSource(autoCorrect)
            .SetNumTiers(NumTiers)
            .SetSmooth(Smooth)
            .Build();

            Complete(tiers);
        });

        DrawImage();
        Dispose();
    }
}
