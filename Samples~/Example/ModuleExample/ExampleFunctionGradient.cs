using ANoise;

public class ExampleFunctionGradient : Example
{
    public double Spacing = 0.01;
    public EFunctionGradientAxis Axis;

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
            ModuleAutoCorrect autoCorrect1 = new ModuleAutoCorrect()
            .SetSource(fractal)
            .SetSeed(NoiseSet.Seed)
            .SetRange(0, 1)
            .Build();
            ModuleFunctionGradient functionGradient = new ModuleFunctionGradient()
            .SetSource(fractal)
            .SetSpacing(Spacing)
            .SetAxis(Axis)
            .Build();
            ModuleAutoCorrect autoCorrect2 = new ModuleAutoCorrect()
            .SetSource(functionGradient)
            .SetSeed(NoiseSet.Seed)
            .SetRange(0, 1)
            .Build();
            Complete(autoCorrect2);
        });

        DrawImage();
        Dispose();
    }
}
