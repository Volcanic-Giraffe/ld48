using System;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private ParticleSystem _particleSystem;

    public float Speed = 1;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private float _dx;
    private float _fly;

    private void Update()
    {
        _dx = Input.GetAxisRaw("Horizontal");
        _fly = Input.GetAxisRaw("Jump");

        if (_fly != 0)
        {
            if (!_particleSystem.isPlaying)
                _particleSystem.Play();
        }
        else
        {
            _particleSystem.Stop();
        }
    }

    private void FixedUpdate()
    {
        var floating = _rigidbody.velocity.y != 0;

        if (_dx == 0)
        {
            if (!floating)
                _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            _rigidbody.AddForce(_dx * Speed * Time.fixedDeltaTime, 0, 0, ForceMode.Impulse);
        }

        if (_fly != 0)
        {
            _rigidbody.AddForce(0, _fly * Speed * 0.3f, 0, ForceMode.Impulse);
        }
    }
}