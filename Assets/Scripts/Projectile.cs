using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float Damage = 0;

    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        //m_audioSource.pitch = 0.5f + Random.value/2.0f;
    }

    public float GetDamage()
    {
        return Damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
