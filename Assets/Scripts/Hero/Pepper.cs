using System;
using DG.Tweening;
using UnityEngine;

public class Pepper : MonoBehaviour
{
    public ParticleSystem nomEffect;
    public Transform pepper;

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
            nomEffect.Play();
            pepper.DOScale(0, 0.4f);

            Destroy(gameObject, 0.5f);
        }
    }
}