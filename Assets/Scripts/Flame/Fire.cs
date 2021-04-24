
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Fire : MonoBehaviour
{
    private ParticleSystem _part;
    private List<ParticleCollisionEvent> _collisionEvents;

    public float DelayBeforeFlameObstacles = -1;

    private float _startTime;
    void Start()
    {
        _part = GetComponent<ParticleSystem>();
        _collisionEvents = new List<ParticleCollisionEvent>();
        _startTime = Time.time;
    }
    
    void OnParticleCollision(GameObject other)
    {
        if (_startTime + DelayBeforeFlameObstacles > Time.time) return;
        //int numCollisionEvents = _part.GetCollisionEvents(other, _collisionEvents);

        Flameable flmb = other.GetComponent<Flameable>();
        if (flmb && flmb.transform != transform.parent) flmb.DoBurn();
        
    }

}
