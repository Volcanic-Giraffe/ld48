using System;
using UnityEngine;

public class FlyLengthMod : HeroMod
{
    private HeroScript _hero;
    private float oldVal;
    private void Start()
    {
        _hero = GetComponent<HeroScript>();
        if (_hero == null) return; // in container
        oldVal = _hero.FlyTime;
        _hero.FlyTime = oldVal * Coefficient;
    }

    private void OnDestroy()
    {
        if (_hero == null) return;
        if (_hero.FlyTime * (1 / Coefficient) != oldVal) Debug.LogWarning("Someone else modified flytime! Be careful");
        _hero.FlyTime = oldVal;
    }
}