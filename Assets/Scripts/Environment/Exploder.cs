using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public float radius;
    public float force;

    private bool _explodedOnce;

    public void Explode()
    {
        if (_explodedOnce) return;
        
        _explodedOnce = true;
        
        Collider[] objects = Physics.OverlapSphere(transform.position, radius);
        
        int count = 0;
        
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponentInParent<Rigidbody>();
            if (r != null)
            {
                r.isKinematic = false;
                r.AddExplosionForce(force, transform.position, radius);
                r.AddTorque(new Vector3(0,0,force * 0.3f));

                count++;
            }
        }
    }
}
