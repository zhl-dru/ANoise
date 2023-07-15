using ANoise;

public class ExampleScaleOffset : Example
{
    public double Scale = 0.5;
    public double Offset = 0.5;

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
            ModuleScaleOffset scaleOffset = new ModuleScaleOffset()
            .SetSource(autoCorrect)
            .SetScale(Scale)
            .SetOffset(Offset)
            .Build();

            Complete(scaleOffset);
        });

        DrawImage();
        Dispose();
    }
}
