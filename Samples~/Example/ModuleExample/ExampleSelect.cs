using ANoise;

public class ExampleSelect : Example
{
    public NoiseSet NoiseSet2;
    public double X1 = 0;
    public double X2 = 0;
    public double Y1 = 0;
    public double Y2 = 1;
    public double Falloff = 0.5;
    public double Threshold = 0.5;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            ModuleFractal fractal1 = new ModuleFractal()
            .SetSeed(NoiseSet.Seed)
            .SetOctaves(NoiseSet.Octaves)
            .SetFrequency(NoiseSet.Frequency)
            .SetLacunarity(NoiseSet.Lacunarity)
            .SetDerivSpacing(NoiseSet.DerivSpacing)
            .SetBasisType(NoiseSet.BasisType)
            .SetFractalType(NoiseSet.FractalType)
            .SetInterpTypes(NoiseSet.InterpType)
            .Build();
            ModuleFractal fractal2 = new ModuleFractal()
            .SetSeed(NoiseSet2.Seed)
            .SetOctaves(NoiseSet2.Octaves)
            .SetFrequency(NoiseSet2.Frequency)
            .SetLacunarity(NoiseSet2.Lacunarity)
            .SetDerivSpacing(NoiseSet2.DerivSpacing)
            .SetBasisType(NoiseSet2.BasisType)
            .SetFractalType(NoiseSet2.FractalType)
            .SetInterpTypes(NoiseSet2.InterpType)
            .Build();
            ModuleAutoCorrect autoCorrect1 = new ModuleAutoCorrect()
            .SetSeed(NoiseSet.Seed)
            .SetSource(fractal1)
            .SetRange(0, 1)
            .Build();
            ModuleAutoCorrect autoCorrect2 = new ModuleAutoCorrect()
            .SetSeed(NoiseSet2.Seed)
            .SetSource(fractal2)
            .SetRange(0, 1)
            .Build();
            ModuleGradient gradient = new ModuleGradient()
            .SetX(X1, X2)
            .SetY(Y1, Y2)
            .SetZ(0, 0)
            .SetW(0, 0)
            .SetU(0, 0)
            .SetV(0, 0)
            .Build();
            ModuleSelect select = new ModuleSelect()
            .SetLowSource(autoCorrect1)
            .SetHighSource(autoCorrect2)
            .SetControlSource(gradient)
            .SetFalloff(Falloff)
            .SetThreshold(Threshold)
            .Build();

            Complete(select);
        });

        DrawImage();
        Dispose();
    }
}
