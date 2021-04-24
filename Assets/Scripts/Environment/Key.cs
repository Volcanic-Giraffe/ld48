
using System;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    private Flameable _flameable;

    public UnityEvent<bool> OnKeyFire;
    
    private void Start()
    {
        _flameable = GetComponentInChildren<Flameable>();
        _flameable.OnFlameStopped += FlameableOnOnFlameStopped;
        _flameable.OnFlameStarted += FlameableOnOnFlameStarted;
    }

    private void FlameableOnOnFlameStarted()
    {
        OnKeyFire.Invoke(true);
    }

    private void FlameableOnOnFlameStopped()
    {
        OnKeyFire.Invoke(false);
    }
}
