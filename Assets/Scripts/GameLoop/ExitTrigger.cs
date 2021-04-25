
using System;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private bool _exitedOnce; 
    
    private Sounds _sounds;

    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_exitedOnce) return;
        
        if (other.CompareTag("Player"))
        {
            _exitedOnce = true;
            
            _sounds.PlayExact("bonus4");
            
            FindObjectOfType<GameLoopController>().OnExit();
        }
    }
}
