using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class EnemyPoolManager : MonoBehaviour, IDeathHandler, INoLiveEnemyHandler
{
    public List<GameObject> enemyPrefs;

    public Transform spawnZone;
    float maxXSpawnPoint, minXSpawnPoint;
    float maxZSpawnPoint, minZSpawnPoint;

    public int waveCount;
    public int waveSize;

    public List<GameObject> enemiesPool;

    public List<GameObject> liveEnemies;
    private List<GameObject> deadEnemies;

    private void Start()
    {
        EventBus.Subscribe(this);

        deadEnemies = new List<GameObject>();
        liveEnemies = new List<GameObject>();
        enemiesPool = new List<GameObject>();

        maxXSpawnPoint = spawnZone.position.x + spawnZone.localScale.x / 2;
        maxZSpawnPoint = spawnZone.position.z + spawnZone.localScale.z / 2;

        minXSpawnPoint = spawnZone.position.x - spawnZone.localScale.x / 2;
        minZSpawnPoint = spawnZone.position.z - spawnZone.localScale.z / 2;

        for (int i=0; i < waveSize*waveCount; i++)
        {
            InstantiateEnemy();
        }
    }

    public void PushDeadEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        liveEnemies.Remove(enemy);
        deadEnemies.Add(enemy);
        EventBus.RaiseEvent<IEnemiesListHandler>(h => h.RemoveEnemyFromList(enemy));
    }

    public void DeathHandle(GameObject obj)
    {
        PushDeadEnemy(obj);
    }

    public void NoLiveEnemyLeft()
    {
        if (enemiesPool.Count > 0)
        {
            for (int i=0; i<waveSize; i++) 
            {
                SetLive(enemiesPool[enemiesPool.Count - 1]);
            }
        }

        else 
        if(enemiesPool.Count == 0 && liveEnemies.Count == 0)
            EventBus.RaiseEvent<IAllEnemiesKilled>(h => h.AllEnemiesKilled());
    }

    public void InstantiateEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(minXSpawnPoint, maxXSpawnPoint), spawnZone.position.y, Random.Range(minZSpawnPoint, maxZSpawnPoint));
        GameObject spawnedEnemy = Instantiate(enemyPrefs[0], spawnPos, Quaternion.identity);
        enemiesPool.Add(spawnedEnemy);
        spawnedEnemy.SetActive(false);
    }

    public void SetLive(GameObject enemy)
    {
        liveEnemies.Add(enemy);
        enemiesPool.Remove(enemy);

        enemy.SetActive(true);

        EventBus.RaiseEvent<IEnemiesListHandler>(h => h.AddEnemyToList(enemy));
    }
}
