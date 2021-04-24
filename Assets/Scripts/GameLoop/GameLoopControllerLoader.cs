
using System;
using UnityEngine;

public class GameLoopControllerLoader : MonoBehaviour
{ 
    public GameLoopController GameLoopControllerPrefab;

    private void Awake()
    {
        var gameLoopController = FindObjectOfType<GameLoopController>();
        if (!gameLoopController)
        {
            Instantiate(GameLoopControllerPrefab);
        }
        else
        {
            gameLoopController.Start();
        }
    }
}
