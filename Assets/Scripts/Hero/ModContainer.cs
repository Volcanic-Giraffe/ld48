﻿using System;
using UnityEngine;

public class ModContainer : MonoBehaviour
{
    public string Name;
    public string Description;
    
    private Sounds _sounds;

    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(var otherMod in other.GetComponents<HeroMod>()) Destroy(otherMod);

            var mods = GetComponents<HeroMod>();
            if (mods.Length == 0) Debug.LogError("Добавьте в контейнер поверапа HeroMod в качестве компонентов");
            foreach (var mod in mods)
            {
                var heroMod = other.gameObject.AddComponent(mod.GetType()) as HeroMod;
                heroMod.Coefficient = mod.Coefficient;
            }
            
            var ui = FindObjectOfType<MainUI>();
            ui.ShowPowerup(Name, Description);

            _sounds.PlayRandom("bonus");
            
            Destroy(gameObject);
        }
    }
}