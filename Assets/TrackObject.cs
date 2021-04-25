using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public TrackBehaviour TrackBehaviour = TrackBehaviour.GlobalPos;
    public float Amplitude = 0.3f;
    private Vector3 startPos;
    public GameObject Target;

    void Start()
    {
        startPos = TrackBehaviour == TrackBehaviour.GlobalPos ? transform.position : transform.localPosition;
    }

    void Update()
    {
        switch (TrackBehaviour)
        {
            case TrackBehaviour.LocalPos:
                var vc = Target.transform.position - transform.TransformPoint(startPos);
                var scale = Mathf.Min(vc.magnitude / 10, 1);
                var newPos = startPos + vc.normalized * (Amplitude * scale);
                transform.localPosition = new Vector3(newPos.x, newPos.y, startPos.z);
                break;
            case TrackBehaviour.GlobalPos:
            default:
                vc = Target.transform.position - startPos;
                scale = Mathf.Min(vc.magnitude / 10, 1);
                newPos = startPos + vc.normalized * (Amplitude * scale);
                transform.position = new Vector3(newPos.x, newPos.y, startPos.z);
                break;
        }
    }
}
