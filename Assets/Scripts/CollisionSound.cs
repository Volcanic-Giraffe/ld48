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

    private float _cooldown;

    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }

    private void Update()
    {
        if (silenceTime > 0) silenceTime -= Time.deltaTime;
        if (_cooldown > 0) _cooldown -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (silenceTime > 0) return;

        if (collision.impulse.magnitude > magnitudeToTrigger && _cooldown <= 0)
        {
            _cooldown = 0.4f;
            _sounds.PlayRandom(soundPrefix);
        }
    }
}