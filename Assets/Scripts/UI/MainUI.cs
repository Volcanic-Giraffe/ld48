using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI centralHint;
    public TextMeshProUGUI deadText;
    public TextMeshProUGUI roomName;
    public Slider jetpackMeter;

    private HeroScript _hero;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        centralHint.DOKill();
        deadText.DOKill();
        
        centralHint.SetText(string.Empty);
        roomName.SetText(string.Empty);
        deadText.enabled = false;
        
        centralHint.color = Color.clear;
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
        centralHint.SetText(text);
        centralHint.color = Color.clear;
        centralHint.DOColor(Color.white, 0.2f).SetDelay(delay);
        centralHint.transform.DOPunchScale(Vector3.one * 0.1f,0.3f);
        
        if (hideAfter > 0)
        {
            centralHint.DOColor(Color.clear, 0.2f).SetDelay(hideAfter + delay);
        }
    }

    public void ShowDeadMessage()
    {
        deadText.enabled = true;
        deadText.color = Color.clear;
        deadText.DOColor(Color.white, 0.3f).SetDelay(1f);
    }

    public void SetRoomName(string text)
    {
        roomName.SetText(text);
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
