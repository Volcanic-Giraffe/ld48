using System;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Range(5, 30)]
    [Tooltip("Насколько быстро герой ускоряется при ходьбе")]
    public float Acceleration = 20;
    [Range(10, 20)]
    [Tooltip("Насколько эффективна жопная тяга")]
    public float FlyPower = 1;
    [Tooltip("Время, на которое хватает запаса топлива в жопе")]
    public float FlyTime = 1;
    private float _flyTimer;
    [Tooltip("Абсолютное ограничение скорости при ходьбе")]
    public float WalkSpeedLimit = 3;
    [Tooltip("Партиклер жопы")]
    public ParticleSystem assFlame;
    [Tooltip("Сила вращения ног (не работает)")]
    public float LegRotationForce;
    [Tooltip("Ноге 1")]
    public Rigidbody leg1;
    [Tooltip("Ноге 2")]
    public Rigidbody leg2;

    public Sprite flySprite;
    public Sprite standSprite;
    public Transform groundChecker;


    MeshRenderer assFlameCube;
    private float _dx;
    private float _fly;
    private float _legTimer;
    private SpriteRenderer _sr;
    private bool floating;

    private void Start()
    {
        _flyTimer = FlyTime;
        _rigidbody = GetComponent<Rigidbody>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        assFlameCube = assFlame.GetComponent<MeshRenderer>();
        leg1.centerOfMass = new Vector3(0, -0.2f, 0);
        leg2.centerOfMass = new Vector3(0, -0.2f, 0);
    }


    private void Update()
    {
        _dx = Input.GetAxisRaw("Horizontal");
        _sr.flipX = _dx < 0;

        _fly = Input.GetAxisRaw("Jump");
        if (_fly != 0) _sr.sprite = flySprite;
        else _sr.sprite = standSprite;


        if (_fly != 0 && _flyTimer > 0)
        {
            _flyTimer -= Time.deltaTime;
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

        if (!floating) _flyTimer = FlyTime;

        _legTimer += Time.deltaTime;
        if (_legTimer >= 1f)
        {
            _legTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
#if(UNITY_EDITOR)
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector3.up * -0.6f, Color.green);
#endif
        RaycastHit hit;
        floating = !Physics.Raycast(transform.position + Vector3.up * 0.5f, -Vector3.up, out hit, 0.55f, ~LayerMask.GetMask("Hero", "HeroLegs"));
        leg1.angularDrag = 1;
        leg2.angularDrag = 1;
        if (_dx == 0)
        {
            if (!floating && _fly == 0)
            {
                _rigidbody.velocity = Vector3.zero;
                leg1.angularDrag = 10;
                leg2.angularDrag = 10;
            }
        }
        else
        {
            if (_legTimer < 0.5f)
                leg1.AddTorque(new Vector3(0, 0, -_dx * LegRotationForce), ForceMode.VelocityChange);
            else
                leg2.AddTorque(new Vector3(0, 0, -_dx * LegRotationForce), ForceMode.Impulse);
            if (
                _rigidbody.velocity.x == 0 ||
                Mathf.Sign(_rigidbody.velocity.x) != Mathf.Sign(_dx) ||
                (Mathf.Sign(_rigidbody.velocity.x) == Mathf.Sign(_dx) && Mathf.Abs(_rigidbody.velocity.x) < WalkSpeedLimit))
                _rigidbody.AddForce(_dx * Acceleration * Time.fixedDeltaTime, 0, 0, ForceMode.Impulse);
        }

        if (_fly != 0 && _flyTimer > 0)
        {
            var vec = (Vector3.up + new Vector3(_dx * 0.2f, 0, 0)).normalized;
            _rigidbody.AddForce(vec * FlyPower * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}