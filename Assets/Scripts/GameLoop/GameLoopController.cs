
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopController : MonoBehaviour
{

    public GameObject[] LevelPrefabs;


    private int _levelIdx = 0;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        var _levelPrefab = Instantiate(LevelPrefabs[_levelIdx], transform);
    }

    public void OnExit()
    {
        _levelIdx++;
        if (_levelIdx < LevelPrefabs.Length)
        {
            StartLevel();
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
