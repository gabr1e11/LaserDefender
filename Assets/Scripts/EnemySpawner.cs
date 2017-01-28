using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int EnemiesPerRow;
    public int EnemyPadding;
    public GameObject EnemyPrefab;

    private SpriteRenderer m_enemyPrefabSpriteRenderer;
    private float m_enemyInitXCoord;

    // Use this for initialization
    void Start()
    {
        m_enemyPrefabSpriteRenderer = EnemyPrefab.GetComponent<SpriteRenderer>();

        m_enemyInitXCoord = -( (EnemiesPerRow - 1) * (m_enemyPrefabSpriteRenderer.bounds.extents.x * 2.0f + EnemyPadding)) / 2.0f;
        CreateRowEnemy(30.0f);
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
    // Update is called once per frame
    void Update()
    {

    }
}
