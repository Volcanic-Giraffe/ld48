
using System;
using UnityEngine;

public class Flameable : MonoBehaviour
{
    public event Action OnFlameStopped;
    public event Action OnFlameStarted;
    public float SecondsToFire = -1;
    public bool DecreaseFireWithTime;

    public ParticleSystem FirePrefab;

    public bool CanRenewFire = true;
    
    private float _whenStartedToFire;
    private bool _isBurning;
    private ParticleSystem _fire;

    private float _initialRate;

    public bool IsBurning => _isBurning;

    public float Progress
    {
        get
        {
            if (!_isBurning) return 0;
            if (DecreaseFireWithTime) return (1 - (Time.time - _whenStartedToFire) / SecondsToFire);
            return 1;
        }
    }

    private void Awake()
    {
        
    }

    public void DoBurn()
    {
        if (_isBurning && !CanRenewFire) return;
        _whenStartedToFire = Time.time; 
        
        if (_isBurning) return;
        _isBurning = true;
        OnFlameStarted?.Invoke();
        _fire = Instantiate(FirePrefab, transform);
        _initialRate = _fire.emission.rateOverTimeMultiplier;
    }

    private void Update()
    {
        if (_isBurning && SecondsToFire > 0 && SecondsToFire + _whenStartedToFire < Time.time)
        {
            _isBurning = false;
            Destroy(_fire.gameObject);
            OnFlameStopped?.Invoke();
        }
        if (_isBurning && DecreaseFireWithTime && SecondsToFire > 0 )
        {
            var emissionModule = _fire.emission;
            emissionModule.rateOverTimeMultiplier =
                _initialRate * (1 - (Time.time - _whenStartedToFire) / SecondsToFire) * transform.localScale.x;
        }

        if (_isBurning)
        {
            var shapeModule = _fire.shape;
            shapeModule.scale = transform.localScale;
        }
    }
}
