
using System;
using UnityEngine;

[RequireComponent(typeof(Flameable))]
public class DestroyAfterFlame : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Flameable>().OnFlameStopped += () => Destroy(gameObject);
        GetComponent<Flameable>().CanRenewFire = false;
    }
}
