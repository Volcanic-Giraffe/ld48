using UnityEngine;

public class AnimateTransform : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    void Update()
    {
        if (Position != Vector3.zero)
            transform.localPosition += Position * Time.deltaTime;
        if (Scale != Vector3.zero)
            transform.localScale += Scale * Time.deltaTime;
        if (Rotation != Vector3.zero)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + Rotation * Time.deltaTime);
    }
}
