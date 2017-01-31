using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldController : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_cycleDuration = 1.0f;

    // Use this for initialization
    void Start()
    {
        m_System = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];

        CycleParticlesAlpha();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CycleParticlesAlpha();
    }

    void CycleParticlesAlpha()
    {
        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);
        //Debug.Log("Particles: " + numParticlesAlive);
        // Change only the particles that are alive
        Random.State oldState = Random.state;

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Color color = m_Particles[i].GetCurrentColor(m_System);

            Random.InitState((int)m_Particles[i].randomSeed);

            float newAlpha = (Mathf.Sin((m_cycleDuration * Random.value) + 2.0f * Mathf.PI * Time.time / m_cycleDuration) + 1.0f) / 2.0f;

            m_Particles[i].startColor = new Color(color.r, color.g, color.b, newAlpha);
        }
        // Apply the particle changes to the particle system
        m_System.SetParticles(m_Particles, numParticlesAlive);

        Random.state = oldState;
    }
}
