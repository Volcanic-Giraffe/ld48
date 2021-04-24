
using System;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private bool _exitedOnce; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (_exitedOnce) return;
        
        if (other.CompareTag("Player"))
        {
            _exitedOnce = true;
            
            FindObjectOfType<GameLoopController>().OnExit();
        }
    }
}
