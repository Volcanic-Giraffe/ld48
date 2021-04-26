using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public Transform handle;
    public float handleTriggerDistance;

    public Exploder target;

    private Vector3 _handleOrigin;

    private float _triggerTimer;
    
    private Sounds _sounds;

    private bool _triggeredOnce;
    
    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
        
        _handleOrigin = handle.position;
    }

    private void Update()
    {
        if (_triggerTimer > 0)
        {
            _triggerTimer -= Time.deltaTime;

            if (_triggerTimer <= 0)
            {
                target.Explode();
            }
        }

        if (!_triggeredOnce && Vector3.Distance(handle.position, _handleOrigin) >= handleTriggerDistance)
        {
            _triggeredOnce = true;
            _sounds.PlayExact("button1");
            
            _triggerTimer = 0.5f;
        }
    }
}
