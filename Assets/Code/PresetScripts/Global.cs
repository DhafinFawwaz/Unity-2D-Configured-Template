public static class Global
{
    public static float delayDuration = 1f;
    public static float EaseInQuartCurve(float x){return x*x*x*x;}
    public static float EaseOutQuartCurve(float x){return -((1-x)*(1-x)*(1-x)*(1-x)) + 1;}

    public static float EaseInCubicCurve(float x){return x*x*x;}
    public static float EaseOutCubicCurve(float x){return -((1-x)*(1-x)*(1-x)) + 1;}

    public static float EaseInQuadCurve(float x){return x*x;}
    public static float EaseOutQuadCurve(float x){return -((1-x)*(1-x)) + 1;}
}
