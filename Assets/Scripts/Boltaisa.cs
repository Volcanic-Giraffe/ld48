using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boltaisa : MonoBehaviour
{
    public bool onX = true;
    public bool onY = true;
    // Start is called before the first frame update
    public bool randoms = true;
    public float cX;
    public float t;
    public float cY;
    Vector3 initialPos = Vector3.zero;
    void Start()
    {
        if (randoms)
        {
            cX = Random.Range(0.1f, 0.5f);
            cY = Random.Range(0.3f, 0.6f);
            t = Random.Range(1, 12);
        }
        else
        {
            initialPos = transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var x = onX ? initialPos.x + Mathf.Sin(Mathf.DeltaAngle(Time.timeSinceLevelLoad * t, 0)) * cX : initialPos.x;
        var y = onY ? initialPos.y + Mathf.Cos(Mathf.DeltaAngle(Time.timeSinceLevelLoad * t, 0)) * cY : initialPos.y;
        transform.localPosition = new Vector3(x, y, 0);
    }
}
