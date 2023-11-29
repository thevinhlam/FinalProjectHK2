using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public int enemyNumbers = 2;
    public float spawnDelay = 2f;
    public List<Enemy1> enemyPrefabs = new List<Enemy1>();

    private Dictionary<int,ObjectPool> enemyObjectPools = new Dictionary<int,ObjectPool>();
    public SpawnMethod enemySpawnMethod = SpawnMethod.RoundRobin;
    
    private void Awake()
    {
        for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            enemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], enemyNumbers));
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        int spawnedEnemies = 0;
        while (spawnedEnemies < enemyNumbers)
        {
            if(enemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            else if(enemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            spawnedEnemies++;
            yield return wait;
        }
    }

    private void SpawnRoundRobinEnemy(int spawnedEnemies)
    {
        int spawnIndex = spawnedEnemies % enemyPrefabs.Count;
        DoSpawnEnemy(spawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
    }
    private void DoSpawnEnemy(int spawnIndex)
    {
        PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();
        if(poolableObject != null)
        {
            Enemy1 enemy = poolableObject.GetComponent<Enemy1>();
            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
            int vertexIndex = Random.Range(0 , triangulation.vertices.Length);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, -1))
            {
                enemy.navMeshAgent.Warp(hit.position);
                enemy.navMeshAgent.enabled = true;
            }
        }
        else
        {

        }
    }
    public enum SpawnMethod
    {
        RoundRobin,Random
    }

}
