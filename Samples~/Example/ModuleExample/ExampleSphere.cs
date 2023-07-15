using ANoise;

public class ExampleSphere : Example
{
    public double Cx = 0.5;
    public double Cy = 0.5;
    public double R = 0.5;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            ModuleSphere sphere = new ModuleSphere()
            .SetCenterX(Cx)
            .SetCenterY(Cy)
            .SetCenterZ(0)
            .SetCenterW(0)
            .SetCenterU(0)
            .SetCenterV(0)
            .SetRadius(R)
            .Build();

            Complete(sphere);
        });

        DrawImage();
        Dispose();
    }
}
