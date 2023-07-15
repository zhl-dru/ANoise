using ANoise;

public class ExampleClamp : Example
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
            .SetSeed(NoiseSet.Seed)
            .SetSource(fractal)
            .SetRange(0, 1)
            .Build();
            ModuleClamp clamp = new ModuleClamp()
            .SetSource(autoCorrect)
            .SetRange(Low, High)
            .Build();
            Complete(clamp);
        });

        DrawImage();
        Dispose();
    }
}
