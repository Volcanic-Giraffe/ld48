
using System;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Flameable>().OnFlameStopped += OnOnFlameStopped;
        GetComponent<Flameable>().OnFlameStarted += OnOnFlameStarted;
    }

    private void OnOnFlameStarted()
    {
        
    }

    private void OnOnFlameStopped()
    {
        Destroy(gameObject);
    }
}
