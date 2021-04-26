using System;
using System.Collections.Generic;
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
    public ParticleSystem deathEffect;

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
    private bool _ignoreFallDamage;


    public GameObject RewardTopHat;
    public GameObject RewardCrown;
    public GameObject RewardBoot1;
    public GameObject RewardBoot2;
    public GameObject RewardScarf;


    MeshRenderer assFlameCube;
    private float _dx;
    private float _fly;
    private float _legTimer;
    public SpriteRenderer TorsoSR;

    private bool floating;

    private bool _died;

    private bool _jetJustEnded;

    private GameController _game;
    private MainUI _ui;
    private Sounds _sounds;

    private Quaternion _originalRotation;
    private float _originalGroundColliderHeight;
    private RigidbodyConstraints _originalConstraints;

    public float RemainingFly => FlyTime > 0 ? _flyTimer / FlyTime : 1f;

    private Musician _musician;

    public bool Died => _died;
    private void Awake()
    {
        _flyTimer = FlyTime;
        _rigidbody = GetComponent<Rigidbody>();
        assFlameCube = assFlame.GetComponent<MeshRenderer>();

        _ui = FindObjectOfType<MainUI>();
        _game = FindObjectOfType<GameController>();
        _sounds = FindObjectOfType<Sounds>();

        _musician = FindObjectOfType<Musician>();

        _originalRotation = transform.rotation;
        _originalGroundColliderHeight = groundCollider.height;
        _originalConstraints = _rigidbody.constraints;
    }

    private void Start()
    {
        leg1.centerOfMass = new Vector3(0, -0.2f, 0);
        leg2.centerOfMass = new Vector3(0, -0.2f, 0);
        //UpdateRewards();
    }

    public void OnLevelRestart()
    {
        slideCollider.enabled = true;
        groundCollider.height = _originalGroundColliderHeight;

        _rigidbody.constraints = _originalConstraints;

        transform.rotation = _originalRotation;

        ResetVelocity();
        ResetFuel();

        _died = false;
        _ignoreFallDamage = false;
    }

    public void OnLevelEnter()
    {
        foreach (var cmp in GetComponents<HeroMod>()) Destroy(cmp);

        ResetVelocity();
        ResetFuel();

        _ignoreFallDamage = false;
    }

    public void OnLevelExit()
    {
        // ignore fall damage on exit since some levels have a collider right under exit trigger and hero dies :(
        _ignoreFallDamage = true;

        _sounds.PlayRandom("laugh");
    }

    public void ResetVelocity()
    {
        // including leg joints
        var rigs = GetComponentsInChildren<Rigidbody>();

        foreach (var rig in rigs)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    internal void ResetFuel()
    {
        _flyTimer = FlyTime;
    }

    private void Update()
    {
        if (_died || Time.timeScale == 0)
        {
            return;
        }

        _dx = Input.GetAxisRaw("Horizontal");
        if (_dx < 0) TorsoSR.flipX = true;
        if (_dx > 0) TorsoSR.flipX = false;

        _fly = Input.GetAxisRaw("Jump");
        if (_fly != 0) TorsoSR.sprite = flySprite;
        else TorsoSR.sprite = standSprite;

        MoveRewards();

        if (_fly != 0 && _flyTimer > 0)
        {
            _flyTimer -= Time.deltaTime;
            if (!assFlame.isPlaying)
            {
                _musician?.OnStartAssFlame();
                assFlame.Play();
                assFlameCube.enabled = true;

                _sounds.PlayLoop("jet2_loop_b");
                _jetJustEnded = true;
            }
        }
        else
        {
            _musician?.OnStopAssFlame();
            assFlame.Stop();
            assFlameCube.enabled = false;

            _sounds.StopLoop("jet2_loop_b");

            if (Input.GetButtonDown("Jump"))
            {
                _sounds.PlayRandom("fart");
            }

            if (_fly != 0 && _jetJustEnded)
            {
                _jetJustEnded = false;
                _sounds.PlayExact("jet2_end");
            }
        }

        if (!floating) _flyTimer = FlyTime;

        _legTimer += Time.deltaTime;
        if (_legTimer >= 1f)
        {
            _legTimer = 0f;
        }
    }

    private void MoveRewards()
    {
        if (_fly != 0)
        {
            if (TorsoSR.flipX == true)
            {
                RewardCrown.transform.localPosition = new Vector3(-0.057f, 0.93f, 0);
                RewardCrown.transform.localRotation = Quaternion.Euler(new Vector3(0,0,32));
                RewardTopHat.transform.localPosition = new Vector3(-0.05f, 0.91f, 0);
                RewardTopHat.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 32));
            }
            else
            {
                RewardCrown.transform.localPosition = new Vector3(0.064f, 0.93f, 0);
                RewardCrown.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -32));
                RewardTopHat.transform.localPosition = new Vector3(0.058f, 0.908f, 0);
                RewardTopHat.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -32));
            }
        }
        else
        {
            if (TorsoSR.flipX == true)
            {
                RewardCrown.transform.localPosition = new Vector3(0.055f, 0.94f, 0);
                RewardCrown.transform.localRotation = Quaternion.identity;
                RewardTopHat.transform.localPosition = new Vector3(0.059f, 0.94f, 0);
                RewardTopHat.transform.localRotation = Quaternion.identity;
            }
            else
            {
                RewardCrown.transform.localPosition = new Vector3(-0.038f, 0.94f, 0);
                RewardCrown.transform.localRotation = Quaternion.identity;
                RewardTopHat.transform.localPosition = new Vector3(-0.044f, 0.92f, 0);
                RewardTopHat.transform.localRotation = Quaternion.identity;
            }
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
        floating = !Physics.Raycast(transform.position + Vector3.up * 0.5f, -Vector3.up, out hit, 0.55f, ~LayerMask.GetMask("Hero", "HeroLegs", "IgnoreHero", "FluidParticle"));
        leg1.angularDrag = LegAngDragFly;
        leg2.angularDrag = LegAngDragFly;
        if (_dx == 0)
        {
            if (!floating && _fly == 0)
            {
                // this causes hero to stuck in triggers, movable rigidbodies and cancels fall impulse.
                // _rigidbody.velocity = Vector3.zero;
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
        _musician?.OnHeroDie();

        _sounds.PlayRandom("grunt");
        _sounds.PlayRandom("crunchy_thump2");

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezePositionZ;

        slideCollider.enabled = false;
        groundCollider.height = 0.5f;

        _ui.ShowDeadMessage();
        _game.SlowDown();

        deathEffect.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > fallDamageMagnitude && ragdollOnDeath && !_ignoreFallDamage)
        {
            DieHero();
        }

        if (collision.impulse.magnitude > 5f)
        {
            _sounds.PlayRandom("pillow_");
        }
        else if (collision.impulse.magnitude > 2f)
        {
            _sounds.PlayRandom("weak_thump");
        }
    }

    public void UpdateRewards()
    {
        RewardTopHat.SetActive(false);
        RewardScarf.SetActive(false);
        RewardBoot1.SetActive(false);
        RewardBoot2.SetActive(false);
        RewardCrown.SetActive(false);

        foreach (var r in HeroStats.ExistingRewards)
        {
            ApplyReward(r);
        }
    }

    void ApplyReward(Rewards reward)
    {
        switch (reward)
        {
            case Rewards.TopHat:
                RewardTopHat.SetActive(true);
                break;
            case Rewards.FireScarf:
                RewardScarf.SetActive(true);
                break;
            case Rewards.RedBoots:
                RewardBoot1.SetActive(true);
                RewardBoot2.SetActive(true);
                break;
            case Rewards.Crown:
                RewardCrown.SetActive(true);
                break;
        }
    }
}