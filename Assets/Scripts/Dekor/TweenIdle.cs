
using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TweenIdle : MonoBehaviour
{

    public float EverySeconds = 1;
    public float TweenAngleAtRandom = 0;

    public float TweenScaleAtRandom = 0;
    public float TweenPositionInRandomRadius = 0;
    
    public float Duration = 0.5f;
    public Ease Ease;

    private Vector3 _initial;
    
    private void Start()
    {
        _initial = transform.localPosition;
        InvokeRepeating(nameof(DoTween), Random.Range(EverySeconds/2, EverySeconds), Random.Range(0.9f, 1.1f) * EverySeconds);
    }

    void DoTween()
    {
        if (!enabled) return;
        if (TweenAngleAtRandom != 0)
        {
            var newAngle = Random.Range(-TweenAngleAtRandom, TweenAngleAtRandom);
            transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, newAngle), Duration).SetEase(Ease);
        }

        if (TweenScaleAtRandom != 0)
        {

            var newScale = 1 + Random.Range(-TweenScaleAtRandom, TweenScaleAtRandom);
            transform.DOScale(newScale, Duration).SetEase(Ease);
        }

        if (TweenPositionInRandomRadius != 0)
        {
            var newPosition = _initial + Random.insideUnitSphere * TweenPositionInRandomRadius;
            transform.DOLocalMove(newPosition, Duration).SetEase(Ease);
        }
    }
}
