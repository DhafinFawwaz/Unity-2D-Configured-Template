public static class Ease
{
    public static float InQuart(float x){return x*x*x*x;}
    public static float OutQuart(float x){return -((1-x)*(1-x)*(1-x)*(1-x)) + 1;}
    public static float InOutQuart(float x){return x < 0.5 ? 8 * x * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;}

    public static float InCubic(float x){return x*x*x;}
    public static float OutCubic(float x){return -((1-x)*(1-x)*(1-x)) + 1;}
    public static float InOutCubic(float x){return x < 0.5 ? 4 * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;}

    public static float InQuad(float x){return x*x;}
    public static float OutQuad(float x){return -((1-x)*(1-x)) + 1;}
    public static float InOutQuad(float x){return x < 0.5 ? 2 * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)) / 2;}

    public static float OutPowBack(float x, float p){return -(x-1)*(x-1)*(x-1)*(x-1) 
        + p*(x-1)*(x-1)*(x-1) + p*(x-1)*(x-1) + 1;}
    public static float OutBack(float x){return 1 + 2.70158f*(x-1)*(x-1)*(x-1) 
        + 1.70158f*(x-1)*(x-1);}
}
