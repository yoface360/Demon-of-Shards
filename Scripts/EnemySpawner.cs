using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<Transform> minibossSpawnLocs = new List<Transform>();
    [SerializeField]
    List<Transform> enemySpawnLocs = new List<Transform>();
    [SerializeField]
    GameObject miniboss;
    [SerializeField]
    Transform enemyParent;
    [SerializeField]
    GameObject goblin;
    int enemyCount = 5;
    bool[] enemiesNearby;
    float maxDist = 15f;

    // Start is called before the first frame update
    void Start()
    {
        enemiesNearby = new bool[enemySpawnLocs.Count];
        DecideMiniBossSpawn();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckToSpawnEnemies();
    }
    void DecideMiniBossSpawn()
    {
        int rand = Random.Range(0, 101);
        for (int i = 0; i < minibossSpawnLocs.Count; i++)
        {
            if (rand < (100 / minibossSpawnLocs.Count * i))
            {
                Instantiate(miniboss, minibossSpawnLocs[i].position, minibossSpawnLocs[i].rotation, enemyParent);
                break;
            }
        }
    }
    void CheckToSpawnEnemies()
    {
        if (enemyCount < 3)
        {
            int rand = Random.Range(0, 201);
            foreach (Transform t in enemySpawnLocs)
            {
                if (!Physics.CheckSphere(t.position, maxDist))
                {
                    Instantiate(goblin, t.position, t.rotation, enemyParent);
                    enemyCount++;
                }
            }
        }
    }
    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }
}
