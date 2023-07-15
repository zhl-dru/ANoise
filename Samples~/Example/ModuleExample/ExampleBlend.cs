using ANoise;

public class ExampleBlend : Example
{
    public double Low = 0.2;
    public double High = 0.8;

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
            .SetSource(fractal)
            .SetSeed(NoiseSet.Seed)
            .SetRange(0, 1)
            .Build();
            ModuleBlend blend = new ModuleBlend()
            .SetControlSource(autoCorrect)
            .SetLowSource(Low)
            .SetHighSource(High)
            .Build();

            Complete(blend);
        });

        DrawImage();
        Dispose();
    }
}
