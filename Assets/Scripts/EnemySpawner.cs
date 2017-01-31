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
    private Vector2 m_playAreaMin;
    private Vector2 m_playAreaMax;

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

        m_playAreaMin = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        m_playAreaMax = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));

        m_enemyInitXCoord = -leftEnemyToCenter;
        m_formationState = FormationState.MoveLeft;
    }

    void CreateEnemyRow(float yCoord)
    {
        float xCoord = m_enemyInitXCoord;

        for (int i=0; i<EnemiesPerRow; ++i)
        {
            GameObject position = new GameObject("Position");
            position.transform.parent = transform;
            position.transform.position = new Vector3(xCoord, yCoord, 0.0f);

            Instantiate(EnemyPrefab, position.transform, false);

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

                    if ((newPos.x - m_formationHalfWidth) < m_playAreaMin.x)
                    {
                        m_formationState = FormationState.MoveRight;
                    }
                }
                break;
            case FormationState.MoveRight:
                {
                    newPos += Vector3.right * FormationXSpeed * Time.deltaTime;

                    if ((newPos.x + m_formationHalfWidth) > m_playAreaMax.x)
                    {
                        m_formationState = FormationState.MoveLeft;
                    }
                }
                break;
        }
        newPos += Vector3.down * FormationYSpeed * Time.deltaTime;
        transform.position = newPos;

        if (AllMembersDead())
            CreateEnemyRow(m_playAreaMax.y - m_enemyPrefabSpriteRenderer.bounds.extents.y);
    }

    bool AllMembersDead()
    {
        return transform.childCount == 0;
    }
}
