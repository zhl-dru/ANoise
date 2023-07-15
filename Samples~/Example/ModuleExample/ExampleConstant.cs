using ANoise;

public class ExampleConstant : Example
{
    public double Value;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            ModuleConstant constant = new ModuleConstant()
            .SetValue(Value)
            .Build();
            Complete(constant);
        });

        DrawImage();
        Dispose();
    }
}
