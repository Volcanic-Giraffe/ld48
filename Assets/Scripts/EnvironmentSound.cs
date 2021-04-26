using UnityEngine;

public class EnvironmentSound : MonoBehaviour
{
    public float radius;

    public string loopSound;
    public string randomSound;
    public float Cooldown = 10f; 

    private HeroScript _hero;
    private Sounds _sounds;
    private float cooldown = 0;
    private void Awake()
    {
        _sounds = FindObjectOfType<Sounds>();
    }

    void Update()
    {
        if (_hero == null)
        {
            _hero = FindObjectOfType<HeroScript>();
        }
        else
        {
            if (Vector3.Distance(transform.position, _hero.transform.position) < radius)
            {
                if (!string.IsNullOrEmpty(loopSound))
                    _sounds.PlayLoop(loopSound);
                else if (!string.IsNullOrEmpty(randomSound))
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = Cooldown;
                        _sounds.PlayRandom(randomSound);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(loopSound))
                    _sounds.StopLoop(loopSound);
            }
        }
    }

#if UNITY_EDITOR

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}