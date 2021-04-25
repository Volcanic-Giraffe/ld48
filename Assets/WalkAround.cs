using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAround : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rb;
    private SimpleAnimator _anim;
    private int direction;
    public GameObject WallCheckRight;
    public GameObject WallCheckLeft;
    public bool Charge = false;
    private bool _charging;
    private SpriteRenderer _sr;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<SimpleAnimator>();
        direction = Random.value > 0.5 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Charge && !_charging)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction * Vector3.right, out hit, 100f, LayerMask.GetMask("Hero", "HeroLegs")))
            {
                StartCharge();
            }
        }
    }

    private void StartCharge()
    {
        _charging = true;
        if (_anim != null) _anim.FrameDelay /= 3f;
        Speed *= 3;
    }

    private void EndCharge()
    {
        _charging = false;
        if (_anim != null) _anim.FrameDelay *= 3f;
        Speed /= 3f;
    }

    private void FixedUpdate()
    {
#if (UNITY_EDITOR)
        Debug.DrawRay(WallCheckRight.transform.position, Vector3.right, Color.green);
        Debug.DrawRay(WallCheckLeft.transform.position, -Vector3.right, Color.green);
#endif

        if (direction > 0 && Physics.Raycast(WallCheckRight.transform.position, Vector3.right, 0.6f, ~LayerMask.GetMask("Enemy", "Hero", "HeroLegs")))
        {
            if (_charging) EndCharge();
            direction = -1;
            _sr.flipX = false;
        }

        if (direction < 0 && Physics.Raycast(WallCheckLeft.transform.position, -Vector3.right, 0.6f, ~LayerMask.GetMask("Enemy", "Hero", "HeroLegs")))
        {
            if (_charging) EndCharge();
            direction = 1;
            _sr.flipX = true;
        }

        _rb.MovePosition(transform.position + Vector3.right * direction * Speed * Time.fixedDeltaTime);
    }
}
