using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public float Amplitude = 0.3f;
    private Vector3 startPos;
    private GameObject hero;

    void Start()
    {
        startPos = transform.localPosition;
        hero = GameObject.FindGameObjectWithTag("Player");
        if (hero == null)
        {
            Debug.LogWarning("TrakPlayer couldnt find player");
            Destroy(this);
        }
    }

    void Update()
    {
        var vc = (transform.parent.position + startPos) - hero.transform.position;
        var scale = Mathf.Min(vc.magnitude / 10, 1);
        transform.localPosition = startPos+vc.normalized * (Amplitude / scale);
    }
}
