using System;
using UnityEngine;

public class Pepper : MonoBehaviour
{
    private bool _picked;

    private Sounds _sounds;

    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_picked) return;
        if (other.CompareTag("Player"))
        {
            _sounds.PlayRandom("oh_yeah");
            _picked = true;
            HeroStats.HoldingPeppers++;
            Destroy(gameObject);
        }
    }
}