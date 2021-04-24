using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var hs = other.GetComponent<HeroScript>();
        if (hs != null)
        {
            hs.DieHero();
        }
    }
}
