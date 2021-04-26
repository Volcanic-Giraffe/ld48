using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public string soundPrefix;

    [Tooltip("Use this value to prevent initial sounds from happening when scene loads and rigidbodies drop a bit")]
    public float silenceTime;

    public float magnitudeToTrigger;

    private Sounds _sounds;

    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }

    private void Update()
    {
        if (silenceTime > 0) silenceTime -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (silenceTime > 0) return;

        if (collision.impulse.magnitude > magnitudeToTrigger)
        {
            _sounds.PlayRandom(soundPrefix);
        }
    }
}
