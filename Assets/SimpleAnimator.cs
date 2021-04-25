using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(UnityEngine.SpriteRenderer))]
public class SimpleAnimator : MonoBehaviour
{
    public Sprite[] Sprites;
    public float FrameDelay;
    private UnityEngine.SpriteRenderer _sr;
    private float _delayTimer;
    private int _idx;

    void Start()
    {
        _idx = 0;
        _sr = GetComponent<UnityEngine.SpriteRenderer>();
        _delayTimer = FrameDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Sprites.Length == 0) return;
        _delayTimer -= Time.deltaTime;
        if(_delayTimer <=0)
        {
            _idx++;
            if (_idx >= Sprites.Length) _idx = 0;
            _delayTimer = FrameDelay;
            _sr.sprite = Sprites[_idx];
        }
    }
}
