using UnityEngine;

public enum TrackBehaviour
{
    LocalPos,
    GlobalPos
}
public class TrackPlayer : MonoBehaviour
{
    public TrackBehaviour TrackBehaviour = TrackBehaviour.GlobalPos;
    public float Amplitude = 0.3f;
    private Vector3 startPos;
    private GameObject hero;

    void Start()
    {
        startPos = TrackBehaviour == TrackBehaviour.GlobalPos ? transform.position : transform.localPosition;
        hero = GameObject.FindGameObjectWithTag("Player");
        if (hero == null)
        {
            Debug.LogWarning("TrakPlayer couldnt find player");
            Destroy(this);
        }
    }

    void Update()
    {
        switch (TrackBehaviour)
        {
            case TrackBehaviour.LocalPos:
                var vc = hero.transform.position - (transform.parent.position + startPos);
                var scale = Mathf.Min(vc.magnitude / 10, 1);
                var newPos = startPos + vc.normalized * (Amplitude * scale);
                transform.localPosition = new Vector3(newPos.x, newPos.y, startPos.z);
                break;
            case TrackBehaviour.GlobalPos:
            default:
                vc = hero.transform.position - startPos;
                scale = Mathf.Min(vc.magnitude / 10, 1);
                newPos = startPos + vc.normalized * (Amplitude * scale);
                transform.position = new Vector3(newPos.x, newPos.y, startPos.z);
                break;
        }
    }
}
