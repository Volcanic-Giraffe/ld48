
using System;
using UnityEngine;

public class MeshConsumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(this.tag))
        {
            Destroy(other.gameObject);
        }
    }
}
