using ANoise;

public class ExampleSimpleOperation : Example
{
    public double V1 = 0.1;
    public double V2 = 0.5;
    public double V3 = 0.7;
    public double V4 = 0.3;
    public double V5 = 2.0;
    public double V6 = 4.0;


    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            // r = ((v1 + v2 + v3 - v4) * v5) / v6;

            ModuleBase v1 = new ModuleConstant().SetValue(V1).Build();
            ModuleBase v2 = new ModuleConstant().SetValue(V2).Build();
            ModuleBase v3 = new ModuleConstant().SetValue(V3).Build();
            ModuleBase v4 = new ModuleConstant().SetValue(V4).Build();
            ModuleBase v5 = new ModuleConstant().SetValue(V5).Build();
            ModuleBase v6 = new ModuleConstant().SetValue(V6).Build();

            // 1
            ModuleAdd step1 = new ModuleAdd()
            .SetSource1(v1)
            .SetSource2(v2)
            .Build();
            ModuleAdd step2 = new ModuleAdd()
            .SetSource1(step1)
            .SetSource2(v3)
            .Build();
            ModuleSub step3 = new ModuleSub()
            .SetSource1(step2)
            .SetSource2(v4)
            .Build();
            ModuleMult step4 = new ModuleMult()
            .SetSource1(step3)
            .SetSource2(v5)
            .Build();
            ModuleDiv step5 = new ModuleDiv()
            .SetSource1(step4)
            .SetSource2(v6)
            .Build();

            // 2
            ModuleBase r = ((v1 + v2 + v3 - v4) * v5) / v6;

            Complete(r);
        });

        DrawImage();
        Dispose();
    }
}
