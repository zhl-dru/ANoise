using ANoise;

public class ExampleMagnitude : Example
{
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
            ModuleMagnitude magnitude = new ModuleMagnitude()
            .SetX(autoCorrect)
            .SetY(Y)
            .SetZ(0)
            .SetW(0)
            .SetU(0)
            .SetV(0)
            .Build();
            Complete(magnitude);
        });

        DrawImage();
        Dispose();
    }
}
