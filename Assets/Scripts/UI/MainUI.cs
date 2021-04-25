using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI SecondaryText;
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI PausedText;
    public Slider jetpackMeter;

    private HeroScript _hero;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        SecondaryText.DOKill();
        MainText.DOKill();
        
        SecondaryText.SetText(string.Empty);
        roomName.SetText(string.Empty);
        MainText.enabled = true;
        SecondaryText.enabled = true;

        MainText.color = Color.clear;
        SecondaryText.color = Color.clear;
    }

    private void Start()
    {
        _hero = FindObjectOfType<HeroScript>();
    }

    public void OnLevelRestart()
    {
        Reset();
    }
    
    public void ShowHint(string text, float hideAfter = 3.0f, float delay = 0f)
    {
        SecondaryText.SetText(text);
        SecondaryText.color = Color.clear;
        SecondaryText.DOColor(Color.white, 0.2f).SetDelay(delay);
        SecondaryText.transform.DOPunchScale(Vector3.one * 0.1f,0.3f);
        
        if (hideAfter > 0)
        {
            SecondaryText.DOColor(Color.clear, 0.2f).SetDelay(hideAfter + delay);
        }
    }

    public void ShowDeadMessage()
    {
        MainText.color = Color.clear;
        MainText.DOColor(Color.white, 0.3f).SetDelay(1f);
        MainText.text = "DEAD";

        ShowHint("press [R] to restart", -1, 3.5f);
    }

    public void ShowPowerup(string name, string description)
    {
        MainText.color = Color.clear;
        MainText.text = name;
        MainText.DOColor(Color.white, 0.3f);
        MainText.DOColor(Color.clear, 0.2f).SetDelay(3);

        ShowHint(description, 3, 0);
    }

    public void ShowRoomName(string text)
    {
        text ??= string.Empty;

        roomName.SetText(text);
    }

    public void TogglePause(bool val)
    {
        PausedText.enabled = val;
    }

    private void Update()
    {
        UpdateJetPack();
    }

    private void UpdateJetPack()
    {
        if (_hero == null) return;

        jetpackMeter.value = _hero.RemainingFly;
    }
}
