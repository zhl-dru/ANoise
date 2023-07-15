using ANoise;

public class ExampleFloor : Example
{
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
            ModuleMult mult = new ModuleMult()
            .SetSource1(autoCorrect)
            .SetSource2(8)
            .Build();
            ModuleFloor floor = new ModuleFloor()
            .SetSource(mult)
            .Build();
            ModuleAutoCorrect floorAutoCorrect = new ModuleAutoCorrect()
            .SetSource(floor)
            .SetSeed(NoiseSet.Seed)
            .SetRange(0, 1)
            .Build();
            Complete(floorAutoCorrect);
        });

        DrawImage();
        Dispose();
    }
}
