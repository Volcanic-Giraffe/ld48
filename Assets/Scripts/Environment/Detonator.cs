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

    private void Awake()
    {
        _handleOrigin = handle.position;
    }

    private void Update()
    {
        if (Vector3.Distance(handle.position, _handleOrigin) >= handleTriggerDistance)
        {
            target.Explode();
        }
    }
}
