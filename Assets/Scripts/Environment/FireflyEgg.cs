
using System;
using UnityEngine;

public class FireflyEgg : MonoBehaviour
{
    public GameObject FireflyPrefab;

    public float StartAfter = 5f;

    private void Awake()
    {
        Invoke(nameof(Spawn), StartAfter);
    }

    void Spawn()
    {
        var ff = Instantiate(FireflyPrefab, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 1);
    }
}
