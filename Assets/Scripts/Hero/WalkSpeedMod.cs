using System;
using UnityEngine;

public class WalkSpeedMod : HeroMod
{
    private HeroScript _hero;
    private float accOldVal;
    private float limitOldVal;
    private void Start()
    {
        _hero = GetComponent<HeroScript>();
        if (_hero == null) return; // in container
        limitOldVal = _hero.WalkSpeedLimit;
        accOldVal = _hero.Acceleration;
        _hero.Acceleration = accOldVal * Coefficient;
        _hero.WalkSpeedLimit = limitOldVal * Coefficient;
    }

    private void OnDestroy()
    {
        if (_hero == null) return;
        _hero.Acceleration = accOldVal;
        _hero.WalkSpeedLimit = limitOldVal;
    }
}