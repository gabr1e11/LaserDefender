using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {

    public float Speed;

    private ParticleSystem m_particleSystem;

    // Use this for initialization
    void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = m_particleSystem.main;
        main.startSpeed = Speed;
        main.startLifetime = 2.0f * m_particleSystem.shape.box.z / Speed;

        // Restart prewarm state
        m_particleSystem.Clear();
        m_particleSystem.Simulate(m_particleSystem.main.duration);
        m_particleSystem.Play();
    }
}
