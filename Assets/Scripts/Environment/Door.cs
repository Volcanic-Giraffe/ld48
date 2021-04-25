
using System;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 OpenDirection = Vector3.up;

    private Vector3 _initialPos;
    private void Awake()
    {
        _initialPos = transform.position;
    }

    public void TriggerMove(float mv)
    {
        if (!DOTween.IsTweening(transform))
        {
            transform.position = Vector3.Lerp(_initialPos, _initialPos + OpenDirection, mv);
        }
    }

    public void Trigger(bool opened)
    {
        if (opened)
        {
            transform.DOMove(_initialPos + OpenDirection, 1f);
        }
        else
        {
            transform.DOMove(_initialPos, 1f);
        }
    }
}
