using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum Rewards
{
    Nothing, TopHat, Crown, RedBoots, FireScarf
}

public class FinishController : MonoBehaviour
{
    private MainUI _ui;
    private Sounds _sounds;
    private HeroScript _hero;
    public GameObject Exit;
    public GameObject SatanHand;
    public Sprite SatanHandWave;
    public Sprite SatanHandOk;
    public Sprite SatanHandThumbsUp;
    public GameObject SatanEyeLeft;
    public GameObject SatanEyeRight;

    public GameObject MessageBox;
    public GameObject MessageBoxSprite;
    public Sprite TopHat;
    public Sprite Crown;
    public Sprite Scarf;
    public Sprite Boots;

    void Start()
    {
        _ui = FindObjectOfType<MainUI>();
        _sounds = FindObjectOfType<Sounds>();
        _hero = FindObjectOfType<HeroScript>();
        StartCoroutine(EndingCR());
        
        // TEST
        //HeroStats.Peppers = 2;
        //HeroStats.ElapsedTime = 60*10;
        //HeroStats.Deaths = 3;
    }

    private IEnumerator EndingCR()
    {
        yield return new WaitForSeconds(2);
        yield return _ui.ShowResults();

        var rewards = GetRewards();
        SetSatanSprites(rewards);
        yield return new WaitForSeconds(0.5f);

        // если собрал всё за один раз
        if (rewards.Contains(Rewards.FireScarf) && rewards.Contains(Rewards.RedBoots) && rewards.Contains(Rewards.Crown))
        {
            SatanEyeLeft.transform.DOScale(1.1f, 0.3f);
            SatanEyeRight.transform.DOScale(1.1f, 0.3f);
        }
        SatanHand.transform.DORotate(new Vector3(0, 0, 7), 0.4f).SetEase(Ease.OutCubic);
        _sounds.PlayExact("laugh1");

        yield return new WaitForSeconds(0.6f);
        var displayedReward = rewards.Find(x => !HeroStats.ExistingRewards.Contains(x));
        ShowMsgBox(displayedReward);

        foreach (var reward in rewards)
        {
            if (!HeroStats.ExistingRewards.Contains(reward))
            {
                HeroStats.ExistingRewards.Add(reward);
            }
        }
        _hero.UpdateRewards();

        yield return new WaitForSeconds(1f);
        yield return Exit.transform.DOMoveY(-1.4f, 2f).SetEase(Ease.OutCubic).WaitForCompletion();
    }

    private void ShowMsgBox(Rewards displayedReward)
    {
        if (displayedReward == Rewards.Nothing) return;

        var msr = MessageBoxSprite.GetComponent<SpriteRenderer>();
        switch (displayedReward)
        {
            case Rewards.Crown:
                msr.sprite = Crown;
                break;
            case Rewards.FireScarf:
                msr.sprite = Scarf;
                break;
            case Rewards.RedBoots:
                msr.sprite = Boots;
                break;
            case Rewards.TopHat:
                msr.sprite = TopHat;
                break;
        }
        MessageBox.SetActive(true);
        MessageBoxSprite.SetActive(true);
    }

    private List<Rewards> GetRewards()
    {
        var result = new List<Rewards>();

        if (HeroStats.Deaths == 0)
        {
            result.Add(Rewards.FireScarf);
        }

        if (HeroStats.ElapsedTime < HeroStats.SuperTimePar)
        {
            result.Add(Rewards.RedBoots);
        }

        if (HeroStats.Peppers >= HeroStats.TotalPeppers)
        {
            result.Add(Rewards.Crown);
        }

        result.Add(Rewards.TopHat);

        return result;
    }

    private void SetSatanSprites(List<Rewards> rewards)
    {
        var ssr = SatanHand.GetComponentInChildren<SpriteRenderer>();
        if (rewards.Contains(Rewards.FireScarf) && rewards.Contains(Rewards.RedBoots) && rewards.Contains(Rewards.Crown))
        {
            ssr.sprite = SatanHandThumbsUp;
        }
        else if (rewards.Contains(Rewards.FireScarf) || rewards.Contains(Rewards.RedBoots) || rewards.Contains(Rewards.Crown))
        {
            ssr.sprite = SatanHandOk;
        }
        else
        {
            ssr.sprite = SatanHandWave;
        }
    }
}
