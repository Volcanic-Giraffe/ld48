using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float magnitudeToKill = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.attachedRigidbody.velocity.magnitude > magnitudeToKill)
        {
            var hero = other.GetComponent<HeroScript>();

            hero.DieHero();
        }
    }
}