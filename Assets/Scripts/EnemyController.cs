using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float Health = 30.0f;
    public float ShootFrequency = 1.0f;
    public float ShotSpeed = 20.0f;
    public int m_enemyPoints = 10;
    public AudioClip m_deathAudioClip;

    public GameObject Shot;

    private ScoreKeeper m_scoreKeeper;
    private BoxCollider2D m_boxCollider;

    void Start()
    {
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Projectile projectile = coll.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Hit();

            Health -= projectile.GetDamage();
            if (Health <= 0.0f)
            {
                EnemyDestroyed();
            }
        }
    }

    void EnemyDestroyed()
    {
        m_scoreKeeper.Score(m_enemyPoints);
        AudioSource.PlayClipAtPoint(m_deathAudioClip, Camera.main.transform.position);
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        float probability = Time.deltaTime * ShootFrequency;
        if (Random.value < probability)
            Fire();
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + (Vector3.down * m_boxCollider.bounds.extents.y);
        GameObject shot = Instantiate(Shot, startPosition, Quaternion.identity);
        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0.0f, -ShotSpeed);
    }
}
