
using System;
using UnityEngine;

public class MeshConsumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(this.tag))
        {
            // so it can fall through the ground and die there
            other.enabled = false;
            Destroy(other.gameObject,1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(this.tag))
        {
            // so it can fall through the ground and die there
            other.collider.enabled = false;
            Destroy(other.gameObject,1f);
        }        
    }
}
