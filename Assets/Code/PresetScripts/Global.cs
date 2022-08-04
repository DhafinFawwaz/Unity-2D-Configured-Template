public static class Global
{
    public static float delayDuration = 1f;
    public static float EaseInQuartCurve(float x){return x*x*x*x;}
    public static float EaseOutQuartCurve(float x){return -((1-x)*(1-x)*(1-x)*(1-x)) + 1;}
    public static float EaseInOutQuartCurve(float x){return x < 0.5 ? 8 * x * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;}

    public static float EaseInCubicCurve(float x){return x*x*x;}
    public static float EaseOutCubicCurve(float x){return -((1-x)*(1-x)*(1-x)) + 1;}
    public static float EaseInOutCubicCurve(float x){return x < 0.5 ? 4 * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;}

    public static float EaseInQuadCurve(float x){return x*x;}
    public static float EaseOutQuadCurve(float x){return -((1-x)*(1-x)) + 1;}
    public static float EaseInOutQuadCurve(float x){return x < 0.5 ? 2 * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)) / 2;}
}
