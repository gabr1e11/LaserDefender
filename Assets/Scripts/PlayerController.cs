using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float Health = 100.0f;
    public float Speed = 100.0f;
    public float FiringRate = 0.05f;

    public AudioClip m_deathAudioClip;
    public AudioClip m_hitAudioClip;
    public GameObject m_explosion;
    public GameObject Shot;
    
    private float xmin = -5.0f;
    private float xmax = 5.0f;

    private SpriteRenderer m_spriteRenderer;
    private Text m_lifeText;

    // Use this for initialization
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x +
               m_spriteRenderer.bounds.extents.x;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x -
               m_spriteRenderer.bounds.extents.x;

        m_lifeText = GameObject.Find("Life").GetComponent<Text>();
        m_lifeText.text = Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * (Speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * (Speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, FiringRate);
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        float clampedX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    void Fire()
    {
        GameObject shot = Instantiate(Shot, transform.position + (Vector3.up * m_spriteRenderer.bounds.extents.y), Quaternion.identity);
        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0.0f, Speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Projectile projectile = coll.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Hit();
            Health -= projectile.GetDamage();
            AudioSource.PlayClipAtPoint(m_hitAudioClip, Camera.main.transform.position);

            m_lifeText.text = Health.ToString();

            if (Health <= 0.0f)
                Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(m_deathAudioClip, Camera.main.transform.position);
        GameObject explosion = Instantiate(m_explosion, transform.position, Quaternion.identity);
        LevelManager levManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levManager.LoadLevel("Lose Screen", 2.0f);
        Destroy(gameObject);
    }
}
