
using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TweenIdle : MonoBehaviour
{

    public float EverySeconds = 1;
    public float TweenAngleAtRandom = 0;

    public float TweenScaleAtRandom = 0;
    
    public float Duration = 0.5f;
    public Ease Ease;
    
    private void Start()
    {
        InvokeRepeating(nameof(DoTween), EverySeconds, EverySeconds);
    }

    void DoTween()
    {
        if (!enabled) return;
        var newAngle = Random.Range(-TweenAngleAtRandom, TweenAngleAtRandom);
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, newAngle), Duration).SetEase(Ease);
        var newScale = 1 + Random.Range(-TweenScaleAtRandom, TweenScaleAtRandom);
        transform.DOScale(newScale, Duration).SetEase(Ease);
    }
}
