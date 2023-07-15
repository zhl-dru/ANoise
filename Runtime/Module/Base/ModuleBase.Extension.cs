namespace ANoise
{
    public abstract partial class ModuleBase
    {
        public static ModuleAdd operator +(ModuleBase m1, ModuleBase m2)
        {
            return new ModuleAdd().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleSub operator -(ModuleBase m1, ModuleBase m2)
        {
            return new ModuleSub().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleMult operator *(ModuleBase m1, ModuleBase m2)
        {
            return new ModuleMult().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleDiv operator /(ModuleBase m1, ModuleBase m2)
        {
            return new ModuleDiv().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleAdd operator +(double m1, ModuleBase m2)
        {
            return new ModuleAdd().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleSub operator -(double m1, ModuleBase m2)
        {
            return new ModuleSub().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleMult operator *(double m1, ModuleBase m2)
        {
            return new ModuleMult().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleDiv operator /(double m1, ModuleBase m2)
        {
            return new ModuleDiv().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleAdd operator +(ModuleBase m1, double m2)
        {
            return new ModuleAdd().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleSub operator -(ModuleBase m1, double m2)
        {
            return new ModuleSub().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleMult operator *(ModuleBase m1, double m2)
        {
            return new ModuleMult().SetSource1(m1).SetSource2(m2).Build();
        }
        public static ModuleDiv operator /(ModuleBase m1, double m2)
        {
            return new ModuleDiv().SetSource1(m1).SetSource2(m2).Build();
        }
    }

    public static partial class ModuleBaseExtension
    {
        public static ModuleCos cos(this ModuleBase source)
        {
            return new ModuleCos().SetSource(source).Build();
        }
        public static ModuleClamp clamp(this ModuleBase source, double low, double high)
        {
            return new ModuleClamp().SetSource(source).SetRange(low, high).Build();
        }
        public static ModuleFloor floor(this ModuleBase source)
        {
            return new ModuleFloor().SetSource(source).Build();
        }
        public static ModuleSin sin(this ModuleBase source)
        {
            return new ModuleSin().SetSource(source).Build();
        }
        public static ModuleTiers tiers(this ModuleBase source, int numtiers, bool smooth)
        {
            return new ModuleTiers().SetSource(source).SetNumTiers(numtiers).SetSmooth(smooth).Build();
        }
        public static ModuleFunctionGradient fg(this ModuleBase source, double spacing, EFunctionGradientAxis axis)
        {
            return new ModuleFunctionGradient().SetSource(source).SetSpacing(spacing).SetAxis(axis).Build();
        }
        public static ModuleMax max(this ModuleBase source, ModuleBase other)
        {
            return new ModuleMax().SetSource1(source).SetSource2(other).Build();
        }
        public static ModuleMax max(this ModuleBase source, double other)
        {
            return new ModuleMax().SetSource1(source).SetSource2(other).Build();
        }
        public static ModuleMin min(this ModuleBase source, ModuleBase other)
        {
            return new ModuleMin().SetSource1(source).SetSource2(other).Build();
        }
        public static ModuleMin min(this ModuleBase source, double other)
        {
            return new ModuleMin().SetSource1(source).SetSource2(other).Build();
        }
        public static ModuleAutoCorrect ac(this ModuleBase source, int seed, double low, double high)
        {
            return new ModuleAutoCorrect().SetSource(source).SetSeed(seed).SetRange(low, high).Build();
        }
        public static ModuleBias bias(this ModuleBase source, ModuleBase bias)
        {
            return new ModuleBias().SetSource(source).SetBias(bias).Build();
        }
        public static ModuleBias bias(this ModuleBase source, double bias)
        {
            return new ModuleBias().SetSource(source).SetBias(bias).Build();
        }
        public static ModuleGain gain(this ModuleBase source, ModuleBase gain)
        {
            return new ModuleGain().SetSource(source).SetGain(gain).Build();
        }
        public static ModuleGain gain(this ModuleBase source, double gain)
        {
            return new ModuleGain().SetSource(source).SetGain(gain).Build();
        }
        public static ModuleNormalizeCoords nc(this ModuleBase source, ModuleBase length)
        {
            return new ModuleNormalizeCoords().SetSource(source).SetLength(length).Build();
        }
        public static ModuleNormalizeCoords nc(this ModuleBase source, double length)
        {
            return new ModuleNormalizeCoords().SetSource(source).SetLength(length).Build();
        }
        public static ModulePow pow(this ModuleBase source, ModuleBase power)
        {
            return new ModulePow().SetSource(source).SetPower(power).Build();
        }
        public static ModulePow pow(this ModuleBase source, double power)
        {
            return new ModulePow().SetSource(source).SetPower(power).Build();
        }
        public static ModuleSawtooth sawtooth(this ModuleBase source, ModuleBase period)
        {
            return new ModuleSawtooth().SetSource(source).SetPeriod(period).Build();
        }
        public static ModuleSawtooth sawtooth(this ModuleBase source, double period)
        {
            return new ModuleSawtooth().SetSource(source).SetPeriod(period).Build();
        }
    }
}