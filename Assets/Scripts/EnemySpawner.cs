using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int EnemiesPerRow;
    public int EnemyPadding;
    public GameObject EnemyPrefab;
    public float FormationXSpeed;
    public float FormationYSpeed;

    private SpriteRenderer m_enemyPrefabSpriteRenderer;
    private float m_enemyInitXCoord;

    private float m_formationHalfWidth;
    private float m_formationHalfHeight;
    private float m_playAreaMinX;
    private float m_playAreaMaxX;

    enum FormationState
    {
        MoveLeft, MoveRight
    };
    private FormationState m_formationState;

    // Use this for initialization
    void Start()
    {
        m_enemyPrefabSpriteRenderer = EnemyPrefab.GetComponent<SpriteRenderer>();

        float leftEnemyToCenter = (EnemiesPerRow - 1) * (m_enemyPrefabSpriteRenderer.bounds.size.x + EnemyPadding) / 2.0f;

        m_formationHalfWidth = leftEnemyToCenter + m_enemyPrefabSpriteRenderer.bounds.extents.x;
        m_formationHalfHeight = m_enemyPrefabSpriteRenderer.bounds.extents.y;

        m_playAreaMinX = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x;
        m_playAreaMaxX = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;

        m_enemyInitXCoord = -leftEnemyToCenter;
        CreateRowEnemy(200.0f);

        m_formationState = FormationState.MoveLeft;
    }

    void CreateRowEnemy(float yCoord)
    {
        float xCoord = m_enemyInitXCoord;

        for (int i=0; i<EnemiesPerRow; ++i)
        {
            GameObject enemy = Instantiate(EnemyPrefab, new Vector3(xCoord, yCoord, 0.0f), Quaternion.identity);
            enemy.transform.parent = transform;

            xCoord += (m_enemyPrefabSpriteRenderer.bounds.extents.x * 2.0f) + EnemyPadding;
        }
    }

    void Update()
    {
        Vector3 newPos = transform.position;

        switch (m_formationState)
        {
            case FormationState.MoveLeft:
                {
                    newPos += Vector3.left * FormationXSpeed * Time.deltaTime;

                    if ((newPos.x - m_formationHalfWidth) < m_playAreaMinX)
                    {
                        m_formationState = FormationState.MoveRight;
                    }
                }
                break;
            case FormationState.MoveRight:
                {
                    newPos += Vector3.right * FormationXSpeed * Time.deltaTime;

                    if ((newPos.x + m_formationHalfWidth) > m_playAreaMaxX)
                    {
                        m_formationState = FormationState.MoveLeft;
                    }
                }
                break;
        }
        newPos += Vector3.down * FormationYSpeed * Time.deltaTime;
        transform.position = newPos;
    }
}
