﻿using System.Collections.Generic;
using UnityEngine;

public static class HeroStats
{
    // Сколько всего перцев (я не придумал, как получить программно)
    public const int TotalPeppers = 16;

    // Что считается спидраном для среднего LD-чувака
    // Без перцев можно пройти за 5:30 - 6:00, задроту можно и быстрее.
    public const int SuperTimePar = 9 * 60;

    public static int HoldingPeppers = 0;
    public static int Peppers = 0;
    public static int Loops = 0;
    public static float ElapsedTime = 0;
    public static int Deaths = 0;

    public static List<Rewards> ExistingRewards = new List<Rewards>();

    public static void Reset() {
        Peppers = 0;
        ElapsedTime = 0;
        Deaths = 0;
        HoldingPeppers = 0;
        // rewards are not reset 
    }

}