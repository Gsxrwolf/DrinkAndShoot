using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPoolSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int enemyStartAmount;
    [SerializeField] int enemyRefillAmount;
    [SerializeField] int maxEnemyAmount;

    [SerializeField] Vector3 cachePosition;

    [SerializeField] float enemySpawnDistance;
    [SerializeField] int enemySpawnRange;

    [SerializeField] string spawnableFloorTag;

    private List<GameObject> activeEnemyList = new List<GameObject>();
    private List<GameObject> cacheEnemyList = new List<GameObject>();

    private float timer;
    [SerializeField] public float spawnRate;


    [SerializeField] public float activeCount;
    [SerializeField] public float cacheCount;
    void Start()
    {
        InstantiateNewEnemies(enemyStartAmount);
    }

    void Update()
    {
        activeCount = activeEnemyList.Count;
        cacheCount = cacheEnemyList.Count;

        if (activeEnemyList.Count + cacheEnemyList.Count < maxEnemyAmount)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                timer = 0;
                SpawnNewEnemy();
            }
        }
    }

    public void SpawnNewEnemy()
    {
        GameObject newEnemy;
        if (cacheEnemyList.Count <= 0)
        {
            InstantiateNewEnemies(enemyRefillAmount);
        }
        newEnemy = cacheEnemyList.First();
        newEnemy.transform.position = GetNewSpawnPosition();
        cacheEnemyList.Remove(newEnemy);
        newEnemy.SetActive(true);
        activeEnemyList.Add(newEnemy);
    }
    private void InstantiateNewEnemies(int amount)
    {
        for (int i = 0; i < enemyStartAmount; i++)
        {
            GameObject newEnemy;
            newEnemy = Instantiate(enemyPrefab, cachePosition, transform.rotation, transform);
            newEnemy.SetActive(false);
            newEnemy.GetComponent<AIBehavior>().player = player;
            newEnemy.GetComponent<AIBehavior>().spawner = this;
            cacheEnemyList.Add(newEnemy);
        }
    }
    public void DespawnEnemy(GameObject _enemy)
    {
        activeEnemyList.Remove(_enemy);

        _enemy.transform.position = cachePosition;
        _enemy.SetActive(false);
        cacheEnemyList.Add(_enemy);


    }
    private Vector3 GetNewSpawnPosition()
    {
        Vector3 spawnPosition;

        System.Random rnd = new System.Random();

        spawnPosition = (Quaternion.AngleAxis(rnd.Next(-enemySpawnRange, enemySpawnRange), Vector3.up) * player.transform.forward.normalized) * enemySpawnDistance;
        RaycastHit hit;
        Physics.Raycast(spawnPosition, Vector3.down, out hit);
        if (hit.collider.CompareTag(spawnableFloorTag))
        {
            spawnPosition = hit.point;
        }
        else
        {
            spawnPosition = GetNewSpawnPosition();
        }


        return spawnPosition;
    }
}
