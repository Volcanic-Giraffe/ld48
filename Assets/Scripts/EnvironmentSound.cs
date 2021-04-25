using UnityEngine;

public class EnvironmentSound : MonoBehaviour
{
    public float radius;

    public string loopSound;

    private HeroScript _hero;
    private Sounds _sounds;

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
                _sounds.PlayLoop(loopSound);
            }
            else
            {
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