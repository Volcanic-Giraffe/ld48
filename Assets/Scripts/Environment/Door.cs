
using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 OpenDirection = Vector3.up;

    private Vector3 _initialPos;
    private void Awake()
    {
        _initialPos = transform.position;
    }

    public void Trigger(bool opened)
    {
        if (opened)
        {
            //todo: animate
            transform.position = _initialPos + OpenDirection;
        }
        else
        {
            transform.position = _initialPos;
        }
    }
}
