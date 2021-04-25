using System;
using UnityEngine;

public class Pepper : MonoBehaviour
{
    private bool _picked;

    private void OnTriggerEnter(Collider other)
    {
        if (_picked) return;
        if (other.CompareTag("Player"))
        {
            _picked = true;
            HeroStats.HoldingPeppers++;
            Destroy(gameObject);
        }
    }
}