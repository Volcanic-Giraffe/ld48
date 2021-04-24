
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopController : MonoBehaviour
{
    private GameObject Hero;
    public GameObject[] LevelPrefabs;


    private int _levelIdx = 0;

    private void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Player");
        StartLevel();
    }

    public void StartLevel()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        var _levelPrefab = Instantiate(LevelPrefabs[_levelIdx], transform);
        var enter = GameObject.FindGameObjectWithTag("Enter");
        if (enter == null) throw new Exception($"В {_levelPrefab} не найден вход, добавьте объект с тэгом Enter");
        Hero.transform.position = enter.transform.position - Vector3.up;
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
