using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public float Amplitude = 0.3f;
    private Vector3 startPos;
    public GameObject Target;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        var vc = Target.transform.position - startPos;
        var scale = Mathf.Min(vc.magnitude / 10, 1);
        var newPos = startPos + vc.normalized * (Amplitude * scale);
        transform.position = new Vector3(newPos.x, newPos.y, startPos.z);
    }
}
