
using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Afraid : MonoBehaviour
{
    private TweenIdle _idle;

    public float Angle = 0;
    public float Scale = 1;
    public Vector3 PositionShift = Vector3.zero;

    public float Duration = 0.3f;
    private Vector3 _initialPosition;

    private void Start()
    {
        _idle = GetComponent<TweenIdle>();
        _initialPosition = transform.position;
    }

    public void SetAfraid()
    {
        if (_idle)
        {
            _idle.enabled = false;
            DOTween.Kill(_idle.transform);
        }
        
        if (Angle != 0)
        {
            transform.DOLocalRotateQuaternion(quaternion.Euler(0, 0, Angle), Duration);
        }

        if (Scale != 1)
        {
            transform.DOScale(Scale, Duration);
        }

        if (PositionShift != Vector3.zero)
        {
            transform.DOMove(_initialPosition + PositionShift, Duration);
        }
    }

    public void SetUnafraid()
    {
        if (Angle != 0)
        {
            transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), Duration).onComplete += () =>
            {
                if (_idle) _idle.enabled = true;
            };
        }

        if (Scale != 1)
        {
            transform.DOScale(1, Duration).onComplete += () =>
            {
                if (_idle) _idle.enabled = true;
            };
        }

        if (PositionShift != Vector3.zero)
        {
            transform.DOMove(_initialPosition, Duration).onComplete += () =>
            {
                if (_idle) _idle.enabled = true;
            };
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var afraid in GetComponentsInChildren<Afraid>())
            {
                afraid.SetAfraid();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var afraid in GetComponentsInChildren<Afraid>())
            {
                afraid.SetUnafraid();
            }
            
        }
    }
}
