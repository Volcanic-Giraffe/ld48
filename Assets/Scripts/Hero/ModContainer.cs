using System;
using UnityEngine;

public class ModContainer : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}