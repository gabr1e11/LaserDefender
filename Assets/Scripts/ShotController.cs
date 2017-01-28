using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    public float Speed;

    private float m_maxY;
    private SpriteRenderer m_spriteRenderer;

    // Use this for initialization
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_maxY = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f)).y + m_spriteRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * (Speed * Time.deltaTime);

        if (transform.position.y > m_maxY)
            Destroy(gameObject);
    }
}
