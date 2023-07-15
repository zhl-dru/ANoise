using ANoise;

public class ExampleGradient : Example
{
    public double X1 = 0.5;
    public double X2 = 1.0;
    public double Y1 = 0.0;
    public double Y2 = 1.0;
    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            ModuleGradient gradient = new ModuleGradient()
            .SetX(X1, X2)
            .SetY(Y1, Y2)
            .SetZ(0, 0)
            .SetW(0, 0)
            .SetU(0, 0)
            .SetV(0, 0)
            .Build();
            Complete(gradient);
        });

        DrawImage();
        Dispose();
    }
}
