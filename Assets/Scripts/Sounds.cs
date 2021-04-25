using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sounds : MonoBehaviour
{
    public AudioSource aSource;

    public GameObject loopPlayerGO;

    public List<AudioClip> sounds;

    private Dictionary<string, AudioClip> _soundsByName;
    private Dictionary<string, List<AudioClip>> _soundsByPrefix;
    private Dictionary<string, AudioSource> _loopSounds;

    private void Awake()
    {
        _soundsByPrefix = new Dictionary<string, List<AudioClip>>();
        _soundsByName = new Dictionary<string, AudioClip>();
        _loopSounds = new Dictionary<string, AudioSource>();

        foreach (var sound in sounds)
        {
            _soundsByName.Add(sound.name, sound);
        }
    }

    public void PlayLoop(string soundName)
    {
        if (!_loopSounds.ContainsKey(soundName))
        {
            var loopAudioObj = Instantiate(loopPlayerGO, transform);

            var clip = _soundsByName[soundName];

            var loopAudio = loopAudioObj.GetComponent<AudioSource>();
            loopAudio.clip = clip;
            loopAudio.loop = true;

            _loopSounds.Add(soundName, loopAudio);
        }

        var loop = _loopSounds[soundName];

        if (!loop.isPlaying)
        {
            loop.Play();
        }

        loop.DOFade(1, 0.1f);

    }

    public void StopLoop(string soundName)
    {
        if (_loopSounds.ContainsKey(soundName))
        {
            _loopSounds[soundName].DOFade(0, 0.1f);
            
            // _loopSounds[soundName].Stop(); // stopping causes 'cracking clicks' on some effects
        }
    }

    public void StopAllLoops()
    {
        foreach (var loopSound in _loopSounds)
        {
            loopSound.Value.Stop();
        }
    }

    public void PlayExact(string soundName)
    {
        var clip = _soundsByName[soundName];

        aSource.PlayOneShot(clip);
    }

    public void PlayRandom(string soundPrefix)
    {
        if (!_soundsByPrefix.ContainsKey(soundPrefix))
        {
            var list = new List<AudioClip>();
            foreach (var sound in sounds)
            {
                if (sound.name.StartsWith(soundPrefix))
                {
                    list.Add(sound);
                }
            }

            _soundsByPrefix.Add(soundPrefix, list);
        }

        var clips = _soundsByPrefix[soundPrefix];
        var clip = clips[Random.Range(0, clips.Count)];

        aSource.PlayOneShot(clip);
    }

    void Update()
    {
        // todo: play cooldowns
    }
}