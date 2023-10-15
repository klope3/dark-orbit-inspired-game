using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyBoss;
    [SerializeField] private Transform tempBossSpawnPoint;
    [SerializeField] private bool allowLockCursor = true;
    [SerializeField] private bool spawnBoss;
    public UnityEvent OnBossKilled;

    private void Awake()
    {
        if (spawnBoss)
        {
            GameObject boss = Instantiate(enemyBoss);
            boss.transform.position = tempBossSpawnPoint.position;
            boss.GetComponent<HealthHandler>().OnDied += GameManager_OnBossDied;
        }
        SetCursorLock(true);
    }

    private void GameManager_OnBossDied()
    {
        OnBossKilled?.Invoke();
    }

    public void SetTimeFrozen(bool b)
    {
        Time.timeScale = b ? 0 : 1;
    }

    public void SetCursorLock(bool b)
    {
        if (allowLockCursor)
        {
            Cursor.lockState = b ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !b;
        }
    }
}
