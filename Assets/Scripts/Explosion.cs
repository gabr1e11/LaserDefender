using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    private ParticleSystem m_particleSystem;

    // Use this for initialization
    void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_particleSystem.IsAlive())
            Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        ParticleSystem.MainModule main = m_particleSystem.main;
        main.startColor = color;
    }
}
