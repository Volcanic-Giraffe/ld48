using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopController : MonoBehaviour
{
    private const float RestartCooldown = 1f;

    private MainUI _ui;
    public GameObject[] LevelPrefabs;


    private int _levelIdx = 0;

    private float _restartCooldown;

    private bool _paused;
    private float oldVolume;
    private bool _speedUpRequested;

    private Sounds _sounds;

    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }

    private void Start()
    {
        HeroStats.Reset();
        _ui = FindObjectOfType<MainUI>();
        StartLevel();
    }

    private void Update()
    {
        if (_levelIdx < LevelPrefabs.Length - 1)
            HeroStats.ElapsedTime += Time.deltaTime;
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
        _ui.TogglePause(_paused);
    }

    public void PauseLevel()
    {
        _speedUpRequested = false;
        _paused = true;
        //oldVolume = _sounds.aSource.volume;
        //_sounds.aSource.volume = 0.05f;
        _sounds.StopAllLoops();
        Time.timeScale = 0;
    }

    public void UnpauseLevel()
    {
        _speedUpRequested = false;
        _paused = false;
        //_sounds.aSource.volume = oldVolume;
        Time.timeScale = 1f;
    }

    public void SlowDown(float delay = 3f)
    {
        Time.timeScale = 0.5f;
        _speedUpRequested = true;
        StartCoroutine(SpeedUp(delay));
    }

    private IEnumerator SpeedUp(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (_speedUpRequested)
        {
            _speedUpRequested = false;
            Time.timeScale = 1f;
        }
    }

    public void RestartLevel()
    {
        _sounds.PlayRandom("double_click");
        if (_levelIdx < LevelPrefabs.Length - 1) HeroStats.Deaths += 1;
        HeroStats.HoldingPeppers = 0;

        StartLevel();

        _speedUpRequested = false;
        Time.timeScale = 1f;

        var hero = FindObjectOfType<HeroScript>();
        hero.OnLevelRestart();

        var ui = FindObjectOfType<MainUI>();
        ui.OnLevelRestart();
    }

    public void StartLevel()
    {
        StartCoroutine(StartLevelCR());
    }

    private IEnumerator StartLevelCR()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        // alternative is DestroyImmediate
        yield return new WaitForEndOfFrame();

        var level = Instantiate(LevelPrefabs[_levelIdx], transform);
        var enter = GameObject.FindGameObjectWithTag("Enter");
        if (enter == null) throw new Exception($"В {level} не найден вход, добавьте объект с тэгом Enter");

        var hero = FindObjectOfType<HeroScript>();
        hero.OnLevelEnter();
        hero.transform.position = enter.transform.position - Vector3.up;
    }

    public void OnExit()
    {
        NextLevel();
    }

    private void NextLevel()
    {
        _sounds.StopAllLoops();

        HeroStats.Peppers += HeroStats.HoldingPeppers;
        HeroStats.HoldingPeppers = 0;

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
        _sounds.StopAllLoops();

        _levelIdx--;
        if (_levelIdx >= 0)
        {
            StartLevel();
        }
    }
}