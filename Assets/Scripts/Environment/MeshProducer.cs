﻿
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshProducer : MonoBehaviour
{
    public GameObject[] Prefabs;

    public float ItemsPerMinute;
    public float Randomize = 0.5f;

    public float Intensity = 1f;

    private Transform _parent;

    private void Awake()
    {
        _parent = transform.parent;
    }

    public void SetIntensity(float intensity)
    {
        Intensity = intensity;
    }
    
    private void Start()
    {
        var secondsBetweenSpawns = 60 / ItemsPerMinute;
        InvokeRepeating(nameof(Spawn), secondsBetweenSpawns * Random.Range(0.9f, 1.1f), secondsBetweenSpawns * Random.Range(0.9f, 1.1f));
    }

    void Spawn()
    {
        if (Intensity < Random.value) return;
        
        var prefab = Prefabs[Random.Range(0, Prefabs.Length - 1)];
        var go = Instantiate(
            prefab, 
            transform.position + Random.insideUnitSphere*Randomize, 
            transform.rotation, _parent
        );
        go.transform.localScale = go.transform.localScale * (1 + Randomize * Random.Range(-0.5f, 0.1f));

        go.tag = tag;
        foreach (Transform child in go.transform)
        {
            child.tag = tag;
        }
    }
}
