using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI centralHint;
    public TextMeshProUGUI roomName;
    public Slider jetpackMeter;

    private HeroScript _hero;

    private void Awake()
    {
        centralHint.SetText(string.Empty);
        roomName.SetText(string.Empty);
    }

    private void Start()
    {
        _hero = FindObjectOfType<HeroScript>();
    }

    public void SetCentralHint(string text, float time = 3.0f)
    {
        centralHint.SetText(text);
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
