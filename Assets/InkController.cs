using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class InkController : MonoBehaviour
{
    VisualEffect visualEffect;
    float spawnTime;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float lifetime;
    float speed;
    void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();
    }
    private void OnEnable()
    {
        visualEffect.SetFloat("Radius", 0);
        var gradient = new Gradient();
        gradient.colorKeys = new GradientColorKey[2] {
            new GradientColorKey(UIButton.MainColor, 0),
            new GradientColorKey(UIButton.MainColor, 1) };
        visualEffect.SetGradient("Color", gradient);
        spawnTime = Time.time;
        speed = Random.Range(minSpeed, maxSpeed);
    }
    void Update()
    {
        var curLifeTime = Time.time - spawnTime;
        visualEffect.SetFloat("Radius", curLifeTime * speed);
        if (curLifeTime >= lifetime)
            PoolManager.Erase(gameObject);
    }
}
