using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 100.0f;
    public GameObject Shot;

    private float xmin = -5.0f;
    private float xmax = 5.0f;

    private SpriteRenderer m_spriteRenderer;
    private GameObject m_currentShot;
     
    // Use this for initialization
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x +
               m_spriteRenderer.bounds.extents.x;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x -
               m_spriteRenderer.bounds.extents.x;

        m_currentShot = null;
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (m_currentShot == null)
                m_currentShot = Instantiate(Shot, transform.position + (Vector3.up * m_spriteRenderer.bounds.extents.y), Quaternion.identity);
        }

        float clampedX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}
