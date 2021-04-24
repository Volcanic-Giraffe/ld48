using System;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Range(5, 30)]
    public float Acceleration = 20;
    [Range(10, 20)]
    public float FlyPower = 1;
    public float WalkSpeedLimit = 3;
    public ParticleSystem assFlame;
    public float LegRotationForce;
    public Rigidbody leg1;
    public Rigidbody leg2;

    MeshRenderer assFlameCube;
    private float _dx;
    private float _fly;
    private float _legTimer;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        assFlameCube = assFlame.GetComponent<MeshRenderer>();
        leg1.centerOfMass = new Vector3(0, 0.4f, 0);
        leg2.centerOfMass = new Vector3(0, 0.4f, 0);
    }


    private void Update()
    {
        _dx = Input.GetAxisRaw("Horizontal");
        _fly = Input.GetAxisRaw("Jump");

        if (_fly != 0)
        {
            if (!assFlame.isPlaying)
            {
                assFlame.Play();
                assFlameCube.enabled = true;
            }
        }
        else
        {
            assFlame.Stop();
            assFlameCube.enabled = false;
        }

        _legTimer += Time.deltaTime;
        if (_legTimer >= 1f)
        {
            _legTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        var floating = _rigidbody.velocity.y != 0;

        if (_dx == 0)
        {
            if (!floating)
            {
                _rigidbody.velocity = Vector3.zero;
                leg1.angularVelocity = Vector3.zero;
                leg2.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            if (_legTimer < 0.5f)
                leg1.AddTorque(new Vector3(0, 0, -_dx * LegRotationForce), ForceMode.VelocityChange);
            else
                leg2.AddTorque(new Vector3(0, 0, -_dx * LegRotationForce), ForceMode.VelocityChange);
            if (
                _rigidbody.velocity.x == 0 ||
                Mathf.Sign(_rigidbody.velocity.x) != Mathf.Sign(_dx) ||
                (Mathf.Sign(_rigidbody.velocity.x) == Mathf.Sign(_dx) && Mathf.Abs(_rigidbody.velocity.x) < WalkSpeedLimit))
                _rigidbody.AddForce(_dx * Acceleration * Time.fixedDeltaTime, 0, 0, ForceMode.Impulse);
        }

        if (_fly != 0)
        {
            var vec = (Vector3.up + new Vector3(_dx * 0.2f, 0, 0)).normalized;
            _rigidbody.AddForce(vec * FlyPower * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}