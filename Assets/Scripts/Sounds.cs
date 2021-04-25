using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sounds : MonoBehaviour
{
    public AudioSource aSource;
    
    public List<AudioClip> sounds;

    private Dictionary<string, AudioClip> soundsByName;

    private Dictionary<string, List<AudioClip>> soundsByPrefix;
    
    private void Awake()
    {
        soundsByPrefix = new Dictionary<string, List<AudioClip>>();
        soundsByName = new Dictionary<string, AudioClip>();

        foreach (var sound in sounds)
        {
            soundsByName.Add(sound.name, sound);
        }
    }

    public void PlayExact(string name)
    {
        var clip = soundsByName[name];
        
        aSource.PlayOneShot(clip);
    }

    public void PlayRandom(string prefix)
    {
        if (!soundsByPrefix.ContainsKey(prefix))
        {
            var list = new List<AudioClip>();
            foreach (var sound in sounds)
            {
                if (sound.name.StartsWith(prefix))
                {
                    list.Add(sound);
                }
            }
            soundsByPrefix.Add(prefix, list);
        }

        var clips = soundsByPrefix[prefix];
        var clip = clips[Random.Range(0, clips.Count)];
        
        aSource.PlayOneShot(clip);
    }
    
    void Update()
    {
        // todo: play cooldowns
    }
}
