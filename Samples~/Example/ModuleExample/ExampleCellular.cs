using ANoise;

public class ExampleCellular : Example
{
    public int Seed;
    public double Frequency;
    public ECellularDistanceFunction CellularDistanceFunction;
    public double F1;
    public double F2;
    public double F3;
    public double F4;

    protected override void Generate()
    {
        base.Generate();
        ModuleRun(() =>
        {
            ModuleCellular cellular = new ModuleCellular()
            .SetSeed(Seed)
            .SetFrequency(Frequency)
            .SetDistanceType(CellularDistanceFunction)
            .SetCoefficients(F1, F2, F3, F4)
            .Build();
            ModuleAutoCorrect autoCorrect = new ModuleAutoCorrect()
            .SetSource(cellular)
            .SetSeed(Seed)
            .SetRange(0, 1)
            .Build();
            Complete(cellular);
        });

        DrawImage();
        Dispose();
    }
}
