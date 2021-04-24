using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float velocityLimit = -1f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.attachedRigidbody.velocity.y < velocityLimit)
        {
          // KILL    
        }
    }
}
