using System;
using UnityEngine;

public class WeekSauceMod : HeroMod
{
    private HeroScript _hero;

    private float oldTime;
    private void Start()
    {
        _hero = GetComponent<HeroScript>();
        if (_hero == null) return; // in container
        oldTime = _hero.FlyTime;
        _hero.FlyTime = oldTime * Coefficient;
    }

    private void OnDestroy()
    {
        if (_hero == null) return;
        if (_hero.FlyTime * (1 / Coefficient) != oldTime) Debug.LogWarning("Someone else modified flytime! Be careful");
        _hero.FlyTime = oldTime;
    }
}