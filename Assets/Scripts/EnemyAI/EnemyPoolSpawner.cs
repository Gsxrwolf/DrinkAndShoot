using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPoolSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int enemyStartAmount;
    [SerializeField] int enemyRefillAmount;

    [SerializeField] Vector3 cachePosition;

    [SerializeField] float enemySpawnDistance;
    [SerializeField] int enemySpawnRange;

    [SerializeField] string spawnableFloorTag;

    private List<GameObject> activeEnemyList;
    private List<GameObject> cacheEnemyList;
    void Start()
    {
        InstantiateNewEnemies(enemyStartAmount);
    }

    void Update()
    {

    }

    private GameObject SpawnNewEnemy()
    {
        GameObject newEnemy;
        while (cacheEnemyList.Count > 0)
        {
            InstantiateNewEnemies(enemyRefillAmount);
        }
        newEnemy = cacheEnemyList.First();
        cacheEnemyList.Remove(newEnemy);

        newEnemy.transform.position = GetNewSpawnPosition();
        newEnemy.SetActive(true);
        activeEnemyList.Add(newEnemy);


        return newEnemy;
    }
    private void InstantiateNewEnemies(int amount)
    {
        for (int i = 0; i < enemyStartAmount; i++)
        {
            cacheEnemyList.Add(Instantiate(enemyPrefab, cachePosition, transform.rotation, transform));
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

        spawnPosition = (Quaternion.AngleAxis(rnd.Next(-enemySpawnRange,enemySpawnRange), Vector3.up) * player.transform.forward.normalized) * enemySpawnDistance;
        RaycastHit hit;
        Physics.Raycast(spawnPosition,Vector3.down, out hit);
        if(hit.collider.CompareTag(spawnableFloorTag))
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
