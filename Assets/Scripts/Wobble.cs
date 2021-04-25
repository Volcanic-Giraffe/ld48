using DG.Tweening;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public float Amount = 1f;
    public float Duration = 1f;
    private float initialPos;

    void Start()
    {
        initialPos = transform.localPosition.y;
        this.DoTweenUp();
    }

    void DoTweenUp()
    {
        DOTween.To(() => transform.localPosition.y, y => transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z), initialPos + Amount, Duration)
            .SetEase(Ease.InOutQuad).OnComplete(this.DoTweenDown);
    }

    void DoTweenDown()
    {
        DOTween.To(() => transform.localPosition.y, y => transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z), initialPos, Duration)
           .SetEase(Ease.InOutQuad).OnComplete(this.DoTweenUp);
    }

}
