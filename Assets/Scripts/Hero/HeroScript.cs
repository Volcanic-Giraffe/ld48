﻿using System;
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
    
    [Space]
    [Header("Legs:")]
    [Tooltip("Сила вращения ног (1-мало, 20-быстро)")]
    public float LegRotationForce;
    [Tooltip("Скованность ног когда летит")]
    public float LegAngDragFly;
    [Tooltip("Скованность ног когда стоит")]
    public float LegAngDragStay;
    [Tooltip("Максимальная скорость вращения ногов (>50 - физика трещит")]
    public float LegMaxAngVel;
    
    [Tooltip("Ноге 1")]
    public Rigidbody leg1;
    [Tooltip("Ноге 2")]
    public Rigidbody leg2;

    
    [Space]
    public CapsuleCollider slideCollider;
    public CapsuleCollider groundCollider;

    public Sprite flySprite;
    public Sprite standSprite;
    public Transform groundChecker;

    public float fallDamageMagnitude;
    public bool ragdollOnDeath;


    MeshRenderer assFlameCube;
    private float _dx;
    private float _fly;
    private float _legTimer;
    private SpriteRenderer _sr;
    private bool floating;

    private bool _died;

    private GameLoopController _game;
    private MainUI _ui;

    private Quaternion _originalRotation;
    private float _originalGroundColliderHeight;
    private RigidbodyConstraints _originalConstraints;

    public float RemainingFly => FlyTime > 0 ? _flyTimer / FlyTime : 1f;

    public bool Died => _died;

    private void Awake()
    {
        _flyTimer = FlyTime;
        _rigidbody = GetComponent<Rigidbody>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        assFlameCube = assFlame.GetComponent<MeshRenderer>();

        _ui = FindObjectOfType<MainUI>();
        _game = FindObjectOfType<GameLoopController>();

        _originalRotation = transform.rotation;
        _originalGroundColliderHeight = groundCollider.height;
        _originalConstraints = _rigidbody.constraints;
    }

    private void Start()
    {
        leg1.centerOfMass = new Vector3(0, -0.2f, 0);
        leg2.centerOfMass = new Vector3(0, -0.2f, 0);
    }

    public void OnLevelRestart()
    {
        slideCollider.enabled = true;
        groundCollider.height = _originalGroundColliderHeight;
        _rigidbody.constraints = _originalConstraints;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = _originalRotation;


        _died = false;
    }

    private void Update()
    {
        if (_died || Time.timeScale == 0)
        {
            return;
        }

        _dx = Input.GetAxisRaw("Horizontal");
        if (_dx < 0) _sr.flipX = true; 
        if (_dx > 0) _sr.flipX = false;

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

        if (_died)
        {
            return;
        }
        
        leg1.maxAngularVelocity = LegMaxAngVel;
        leg2.maxAngularVelocity = LegMaxAngVel;

        RaycastHit hit;
        floating = !Physics.Raycast(transform.position + Vector3.up * 0.5f, -Vector3.up, out hit, 0.55f, ~LayerMask.GetMask("Hero", "HeroLegs"));
        leg1.angularDrag = LegAngDragFly;
        leg2.angularDrag = LegAngDragFly;
        if (_dx == 0)
        {
            if (!floating && _fly == 0)
            {
                _rigidbody.velocity = Vector3.zero;
                leg1.angularDrag = LegAngDragStay;
                leg2.angularDrag = LegAngDragStay;
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

        if (_fly != 0 && _flyTimer > 0)
        {
            var vec = (Vector3.up + new Vector3(_dx * 0.2f, 0, 0)).normalized;
            _rigidbody.AddForce(vec * FlyPower * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    public void DieHero()
    {
        if (_died) return;
        _died = true;

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezePositionZ;

        slideCollider.enabled = false;
        groundCollider.height = 0.5f;

        _ui.ShowDeadMessage();
        _game.SlowDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > fallDamageMagnitude && ragdollOnDeath)
        {
            DieHero();
        }
    }
}