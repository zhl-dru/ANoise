namespace ANoise
{
    public enum EFractalTypes
    {
        FBM,
        RIDGEDMULTI,
        BILLOW,
        MULTI,
        HYBRIDMULTI,
        DECARPENTIERSWISS
    }

    public enum EBasisTypes
    {
        VALUE,
        GRADIENT,
        GRADVAL,
        SIMPLEX,
        WHITE
    }

    public enum EInterpTypes
    {
        NONE,
        LINEAR,
        CUBIC,
        QUINTIC
    }
    public enum EFunctionGradientAxis
    {
        X_AXIS,
        Y_AXIS,
        Z_AXIS,
        W_AXIS,
        U_AXIS,
        V_AXIS
    }
    public enum ECellularDistanceFunction
    {
        Euclidean,
        EuclideanSq,
        Manhattan,
        Hybrid,
    }
}