using UnityEngine;

public static class HeroStats
{
    public static int HoldingPeppers = 0;
    public static int Peppers = 0;
    public static float ElapsedTime = 0;
    public static int Deaths = 0;

    public static void Reset() {
        Peppers = 0;
        ElapsedTime = 0;
        Deaths = 0;
        HoldingPeppers = 0;
    }

}