using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const float RestartCooldown = 1f;

    private int _currentScene = 0;

    private float _restartCooldown;
    
    private bool _speedUpRequested;
    private bool _paused;

    private Sounds _sounds;
    private MainUI _ui;
    
    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>(); 
        _ui = FindObjectOfType<MainUI>();

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
    }

    private void Update()
    {
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

    public void RestartLevel()
    {       
        _sounds.PlayRandom("double_click");

        HeroStats.Deaths += 1;
        HeroStats.HoldingPeppers = 0;
        
        Time.timeScale = 1f;
        
        _speedUpRequested = false;
        
        SceneManager.LoadScene(_currentScene);
        OnLevelLoaded();
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
        _sounds.StopAllLoops();
        Time.timeScale = 0;
    }

    public void UnpauseLevel()
    {
        _speedUpRequested = false;
        _paused = false;
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
    public void PrevLevel()
    {
        _sounds.StopAllLoops();
        
        _currentScene -= 1;
        if (_currentScene < 0) _currentScene = 0;

        SceneManager.LoadScene(_currentScene);
        OnLevelLoaded();
    }

    public void NextLevel()
    {
        _sounds.StopAllLoops();
        
        HeroStats.Peppers += HeroStats.HoldingPeppers;
        HeroStats.HoldingPeppers = 0;
        
        _currentScene += 1;
        SceneManager.LoadScene(_currentScene);
        OnLevelLoaded();
    }

    public void OnExit()
    {
        _currentScene += 1;

        SceneManager.LoadScene(_currentScene);
        OnLevelLoaded();
    }

    public void OnLevelLoaded()
    {
        var enter = GameObject.FindGameObjectWithTag("Enter");
        var hero = FindObjectOfType<HeroScript>();
        hero.OnLevelEnter();
        hero.transform.position = enter.transform.position - Vector3.up;
        
        _ui.OnLevelChange();
    }
}