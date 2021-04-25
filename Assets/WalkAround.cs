using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAround : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rb;
    private int direction;
    public GameObject WallCheckRight;
    public GameObject WallCheckLeft;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        direction = Random.value > 0.5 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
#if (UNITY_EDITOR)
        Debug.DrawRay(WallCheckRight.transform.position, Vector3.right, Color.green);
        Debug.DrawRay(WallCheckLeft.transform.position, -Vector3.right, Color.green);
#endif

        if (direction > 0 && Physics.Raycast(WallCheckRight.transform.position, Vector3.right, 1f, ~LayerMask.GetMask("IgnoreRaycast", "Hero", "HeroLegs"))) {
            direction = -1;
        }

        if (direction < 0 && Physics.Raycast(WallCheckLeft.transform.position, -Vector3.right, 1f, ~LayerMask.GetMask("IgnoreRaycast", "Hero", "HeroLegs"))) {
            direction = 1;
        }

        _rb.MovePosition(transform.position + Vector3.right * direction * Speed * Time.fixedDeltaTime);
    }
}
