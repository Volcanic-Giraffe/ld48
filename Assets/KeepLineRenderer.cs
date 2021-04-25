using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLineRenderer : MonoBehaviour
{
    private LineRenderer _lr;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lr.SetPosition(_lr.positionCount-1, target.transform.localPosition);
    }
}
