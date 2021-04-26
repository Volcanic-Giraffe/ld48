
using System;
using DG.Tweening;
using UnityEngine;

public class Musician : MonoBehaviour
{
    private static bool AlreadyOnScene = false;

    public AudioSource Main;
    public AudioSource Tuned;

    private float _initialVolumeMain;
    private float _initialVolumeTuned;
    private void Awake()
    {
        if (AlreadyOnScene)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        AlreadyOnScene = true;

        _initialVolumeMain = Main.volume;
        _initialVolumeTuned = Tuned.volume;
    }

    private void Start()
    {
        Main.volume = _initialVolumeMain;
        Tuned.volume = 0;
        Main.Play();
        Tuned.Play();
    }
    
    private void Update()
    {
        Main.loop = true;
        Tuned.loop = true;
    }

    public void OnHeroDie()
    {
        OnStopAssFlame();
    }

    public void OnStartAssFlame()
    {
        DOTween.Kill(Main);
        DOTween.Kill(Tuned);
        Main.DOFade(0, 0.1f);
        Tuned.DOFade(_initialVolumeTuned, 0.05f);
    }

    public void OnStopAssFlame()
    {
        DOTween.Kill(Main);
        DOTween.Kill(Tuned);
        Main.DOFade(_initialVolumeMain, 0.1f);
        Tuned.DOFade(0, 0.2f);
    }
}
