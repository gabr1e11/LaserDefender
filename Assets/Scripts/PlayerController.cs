using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 100.0f;

    private float xmin = -5.0f;
    private float xmax = 5.0f;

    private SpriteRenderer m_spriteRenderer;
     
    // Use this for initialization
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x +
               m_spriteRenderer.bounds.extents.x;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x -
               m_spriteRenderer.bounds.extents.x;
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

        float clampedX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}
