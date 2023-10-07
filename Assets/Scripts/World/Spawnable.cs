using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [SerializeField] private bool invokeOnDestroy;
    [SerializeField] private bool invokeOnDisable;
    public Spawner originSpawner;
    public delegate void SpawnableEvent(Spawnable spawnable);
    public event SpawnableEvent OnDie;

    private void Awake()
    {
        HealthHandler health = GetComponent<HealthHandler>();
        if (health != null) health.OnDied += InvokeOnDie;
    }

    private void OnDestroy()
    {
        if (invokeOnDestroy) originSpawner.DecrementSpawnCount();
    }

    private void OnDisable()
    {
        if (invokeOnDisable) originSpawner.DecrementSpawnCount();
    }

    private void InvokeOnDie()
    {
        OnDie?.Invoke(this);
    }
}
