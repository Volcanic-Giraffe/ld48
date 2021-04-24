using DG.Tweening;
using UnityEngine;

public class LudumDare : MonoBehaviour
{
    void Start()
    {
        transform.DOPunchScale(Vector3.one * 0.1f, 0.9f).SetLoops(-1);
    }
}