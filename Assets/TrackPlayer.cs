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
    public bool LookAt;
    public float AtMaxDistance = 0;

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
        Vector3 vc;
        if (AtMaxDistance != 0 && Vector3.Distance(hero.transform.position, transform.position) > AtMaxDistance) return;
        switch (TrackBehaviour)
        {
            case TrackBehaviour.LocalPos:
                vc = hero.transform.position - (transform.parent.position + startPos);
                var scale = Mathf.Min(vc.magnitude / 10, 1);
                var newPos = startPos + vc.normalized * (Amplitude * scale);
                transform.localPosition = new Vector3(newPos.x, newPos.y, startPos.z);
                if (LookAt) transform.up = vc;
                break;
            case TrackBehaviour.GlobalPos:
            default:
                vc = hero.transform.position - startPos;
                scale = Mathf.Min(vc.magnitude / 10, 1);
                newPos = startPos + vc.normalized * (Amplitude * scale);
                transform.position = new Vector3(newPos.x, newPos.y, startPos.z);
                if (LookAt) transform.up = vc;
                break;
        }

    }
}
