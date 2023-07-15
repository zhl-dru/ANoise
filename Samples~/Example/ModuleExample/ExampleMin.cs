using ANoise;

public class ExampleMin : Example
{
    public double V1 = 0.1;
    public double V2 = 0.5;


    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            ModuleConstant v1 = new ModuleConstant().SetValue(V1).Build();
            ModuleConstant v2 = new ModuleConstant().SetValue(V2).Build();

            ModuleMin min = new ModuleMin()
            .SetSource1(v1)
            .SetSource2(v2)
            .Build();

            Complete(min);
        });

        DrawImage();
        Dispose();
    }
}
