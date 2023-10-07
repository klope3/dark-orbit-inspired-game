using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyBoss;
    [SerializeField] private Transform tempBossSpawnPoint;
    public UnityEvent OnBossKilled;

    private void Awake()
    {
        GameObject boss = Instantiate(enemyBoss);
        boss.transform.position = tempBossSpawnPoint.position;
        boss.GetComponent<HealthHandler>().OnDied += GameManager_OnDied;
    }

    private void GameManager_OnDied()
    {
        OnBossKilled?.Invoke();
    }

    public void SetTimeFrozen(bool b)
    {
        Time.timeScale = b ? 0 : 1;
    }
}
