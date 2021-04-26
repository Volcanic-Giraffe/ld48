
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
            var player = other.gameObject.GetComponent<HeroScript>();

            if (!player.Died)
            {
                player.OnLevelExit();

                _exitedOnce = true;
                // FindObjectOfType<GameLoopController>().OnExit();
                
                FindObjectOfType<GameController>().OnExit();
            }
        }
    }
}
