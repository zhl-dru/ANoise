using ANoise;

public class ExampleBias : Example
{
    public double Bias = 0.5;

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
            ModuleBias bias = new ModuleBias()
            .SetSource(autoCorrect)
            .SetBias(Bias)
            .Build();

            Complete(bias);
        });

        DrawImage();
        Dispose();
    }
}
