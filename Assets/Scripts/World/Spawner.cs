using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool pool;
    [SerializeField, Min(0.001f)] private float spawnsPerSecond;
    [SerializeField, Min(0.001f)] private float minSpawnRadius;
    [SerializeField, Min(0.001f)] private float maxSpawnRadius;
    [SerializeField, Min(1)] private int maxSpawnedCount;
    private float spawnTimer;
    private int spawnedCount;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > 1 / spawnsPerSecond)
        {
            spawnTimer = 0;
            if (spawnedCount < maxSpawnedCount) Spawn();
        }
    }

    private void Spawn()
    {
        Debug.Log("Spawn");
        if (minSpawnRadius > maxSpawnRadius)
        {
            Debug.LogWarning("This spawner's min radius is greater than its max radius!");
        }

        GameObject go = pool.GetPooledObject();
        go.SetActive(true);
        Vector3 randPos = transform.position + Random.insideUnitSphere.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        go.transform.position = randPos;
        Spawnable spawnable = go.GetComponent<Spawnable>();
        if (spawnable == null)
        {
            Debug.LogWarning("Spawned an object with no Spawnable component!");
        }
        else
        {
            spawnable.originSpawner = this;
            spawnable.OnDie += Spawnable_OnDie;
            spawnedCount++;
        }
    }
    
    private void Spawnable_OnDie(Spawnable spawnable)
    {
        spawnable.OnDie -= Spawnable_OnDie;
        spawnedCount--;
    }
}
