using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopController : MonoBehaviour
{
    private const float RestartCooldown = 1f;

    private GameObject Hero;
    public GameObject[] LevelPrefabs;


    private int _levelIdx = 0;

    private float _restartCooldown;

    private bool _paused;

    private void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Player");
        StartLevel();
    }

    private void Update()
    {
        if (_restartCooldown >= 0) _restartCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Level Restart") && _restartCooldown <= 0)
        {
            _restartCooldown = RestartCooldown;
            RestartLevel();
        }

        if (Input.GetButtonDown("Level Pause"))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            PrevLevel();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            NextLevel();
        }
    }

    public void TogglePause()
    {
        if (_paused)
        {
            UnpauseLevel();
        }
        else
        {
            PauseLevel();
        }
    }

    public void PauseLevel()
    {
        _paused = true;
        Time.timeScale = 0;
    }

    public void UnpauseLevel()
    {
        _paused = false;
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        StartLevel();

        var hero = FindObjectOfType<HeroScript>();
        hero.OnLevelRestart();

        var ui = FindObjectOfType<MainUI>();
        ui.OnLevelRestart();
    }

    public void StartLevel()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        var level = Instantiate(LevelPrefabs[_levelIdx], transform);
        var enter = GameObject.FindGameObjectWithTag("Enter");
        if (enter == null) throw new Exception($"В {level} не найден вход, добавьте объект с тэгом Enter");
        Hero.transform.position = enter.transform.position - Vector3.up;
        foreach (var cmp in Hero.GetComponents<HeroMod>()) Destroy(cmp);
    }

    public void OnExit()
    {
        NextLevel();
    }

    private void NextLevel()
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

    private void PrevLevel()
    {
        _levelIdx--;
        if (_levelIdx >= 0)
        {
            StartLevel();
        }
    }
}