using System;
using System.Collections;
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
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI Peppers;
    public Slider jetpackMeter;


    public TextMeshProUGUI ResultTimeText;
    public TextMeshProUGUI ResultDeathText;
    public TextMeshProUGUI ResultPeppersText;
    public TextMeshProUGUI ResultTimeVal;
    public TextMeshProUGUI ResultDeathVal;
    public TextMeshProUGUI ResultPeppersVal;

    private HeroScript _hero;
    private Sounds _sounds;

    private void Awake()
    {
        Reset();
        _sounds = FindObjectOfType<Sounds>();
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

        ResultTimeText.enabled = false;
        ResultDeathText.enabled = false;
        ResultPeppersText.enabled = false;
        ResultTimeVal.enabled = false;
        ResultDeathVal.enabled = false;
        ResultPeppersVal.enabled = false;
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
        SecondaryText.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f);

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
        TimeText.text = $"{HeroStats.ElapsedTime / 60:00}:{HeroStats.ElapsedTime % 60:00}";
        Peppers.text = (HeroStats.Peppers + HeroStats.HoldingPeppers).ToString();
    }

    private void UpdateJetPack()
    {
        if (_hero == null) return;

        jetpackMeter.value = _hero.RemainingFly;
    }

    public Coroutine ShowResults()
    {
        return StartCoroutine(ShowResultsCR());
    }

    private IEnumerator ShowResultsCR()
    {
        var wait = 0.4f;

        ResultTimeText.enabled = true;
        _sounds.PlayExact("slap2");
        yield return new WaitForSeconds(wait);

        ResultTimeVal.enabled = true;
        ResultTimeVal.text = $"{HeroStats.ElapsedTime / 60:00}:{HeroStats.ElapsedTime % 60:00}";
        _sounds.PlayExact("slap2");
        yield return new WaitForSeconds(wait);


        ResultDeathText.enabled = true;
        _sounds.PlayExact("slap2");
        yield return new WaitForSeconds(wait);

        ResultDeathVal.enabled = true;
        ResultDeathVal.text = HeroStats.Deaths.ToString();
        _sounds.PlayExact("slap2");
        yield return new WaitForSeconds(wait);


        ResultPeppersText.enabled = true;
        _sounds.PlayExact("slap2");
        yield return new WaitForSeconds(wait);

        ResultPeppersVal.enabled = true;
        ResultPeppersVal.text = HeroStats.Peppers.ToString();
        _sounds.PlayExact("slap2");
        yield return new WaitForSeconds(wait);
    }
}
