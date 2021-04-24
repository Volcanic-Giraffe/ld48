using System;
using UnityEngine;

public class ModContainer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var mod = GetComponent<HeroMod>();
            if (mod == null) Debug.LogError("Добавьте в контейнер поверапа любой HeroMod в качестве компонента");
            var heroMod = other.gameObject.AddComponent(mod.GetType()) as HeroMod;
            heroMod.Coefficient = mod.Coefficient;
            Destroy(gameObject);
        }
    }
}