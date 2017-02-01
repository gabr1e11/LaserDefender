using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float Health = 30.0f;
    public float ShotsPerSecond = 1.0f;
    public float ShotSpeed = 20.0f;
    public int m_enemyPoints = 10;
    public AudioClip m_deathAudioClip;
    public AudioClip m_hitAudioClip;
    public GameObject m_explosion;

    public GameObject Shot;

    private ScoreKeeper m_scoreKeeper;
    private BoxCollider2D m_boxCollider;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    private Color m_explosionColor;

    void Start()
    {
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        Color[] enemyTexture = m_spriteRenderer.sprite.texture.GetPixels();

        float avgR = 0.0f, avgG = 0.0f, avgB = 0.0f;
        foreach (Color color in enemyTexture)
        {
            avgR += color.r;
            avgG += color.g;
            avgB += color.b;
        }

        avgR /= enemyTexture.Length;
        avgG /= enemyTexture.Length;
        avgB /= enemyTexture.Length;

        m_explosionColor = new Color(avgR, avgG, avgB, 1.0f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Projectile projectile = coll.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Hit();
            EnemyHit(projectile.GetDamage());
        }
    }

    void EnemyHit(float damage)
    {
        AudioSource.PlayClipAtPoint(m_hitAudioClip, Camera.main.transform.position);

        Health -= damage;
        if (Health <= 0.0f)
        {
            EnemyDestroyed();
        }
        else
        {
            m_animator.SetTrigger("Hit");
        }
    }

    void EnemyDestroyed()
    {
        m_scoreKeeper.Score(m_enemyPoints);
        AudioSource.PlayClipAtPoint(m_deathAudioClip, Camera.main.transform.position);
        GameObject explosion = Instantiate(m_explosion, transform.position, Quaternion.identity);

        Explosion explosionScript = explosion.GetComponent<Explosion>();
        explosionScript.SetColor(m_explosionColor);

        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        float probability = Time.deltaTime * ShotsPerSecond;
        if (Random.value < probability)
            Fire();
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + (Vector3.down * m_boxCollider.bounds.extents.y);
        GameObject shot = Instantiate(Shot, startPosition, Quaternion.identity);
        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0.0f, -Random.Range(ShotSpeed * 0.8f, ShotSpeed * 1.2f));
    }
}
