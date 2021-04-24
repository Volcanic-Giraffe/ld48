
using System;
using UnityEngine;

public class Flameable : MonoBehaviour
{
    public event Action OnFlameStopped;
    public float SecondsToFire = -1;
    public bool DecreaseFireWithTime;

    public ParticleSystem FirePrefab;

    private float _whenStartedToFire;
    private bool _isBurning;
    private ParticleSystem _fire;

    private float _initialRate;

    private void Awake()
    {
        
    }

    public void DoBurn()
    {
        _whenStartedToFire = Time.time; 
        
        if (_isBurning) return;
        _isBurning = true;
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
                _initialRate * (1 - (Time.time - _whenStartedToFire) / SecondsToFire);
        }

        if (_isBurning)
        {
            var shapeModule = _fire.shape;
            shapeModule.scale = transform.localScale;
        }
    }
}
