using System;
using UnityEngine;

public class FlyStrengthMod : HeroMod
{
    private HeroScript _hero;
    private float oldVal;
    private void Start()
    {
        _hero = GetComponent<HeroScript>();
        if (_hero == null) return; // in container
        oldVal = _hero.FlyPower;
        _hero.FlyPower = oldVal * Coefficient;
        _hero.ResetFuel();
    }

    private void OnDestroy()
    {
        if (_hero == null) return;
        if (_hero.FlyPower * (1 / Coefficient) != oldVal) Debug.LogWarning("Someone else modified FlyPower! Be careful");
        _hero.FlyPower = oldVal;
    }
}