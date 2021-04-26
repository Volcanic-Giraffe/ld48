
using System;
using UnityEngine;

[RequireComponent(typeof(Flameable))]
[RequireComponent(typeof(MeshProducer))]
public class SpawnMoreIfFlamed : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Flameable>().OnFlameStarted += () =>
        {
            GetComponent<MeshProducer>().Intensity = 1;
        };
        GetComponent<Flameable>().OnFlameStopped += () =>
        {
            GetComponent<MeshProducer>().Intensity = 0;
        };
    }
}
