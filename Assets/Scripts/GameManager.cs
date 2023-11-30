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
    [SerializeField] private Vector3 bossTempWanderAnchor;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private Weapon playerWeapon;
    public UnityEvent OnBossKilled;

    public bool IsPauseMenuOpen { get; private set; }
    public bool IsPlayerMenuOpen { get; private set; }

    private void Awake()
    {
        SpawnBoss();
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

    private void SpawnBoss()
    {
        if (spawnBoss)
        {
            GameObject boss = Instantiate(enemyBoss);
            boss.transform.position = tempBossSpawnPoint.position;
            boss.GetComponent<HealthHandler>().OnDied += GameManager_OnBossDied;
            EnemyAI bossAI = boss.GetComponent<EnemyAI>();
            bossAI.aiState = EnemyAI.AIState.Wander;
            bossAI.wanderAnchor = bossTempWanderAnchor;
        }
    }

    public void SetCursorLock(bool b)
    {
        if (allowLockCursor)
        {
            Cursor.lockState = b ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !b;
        }
    }

    public void UpdateGameState()
    {
        SetTimeFrozen(IsPauseMenuOpen);
        SetCursorLock(!IsPauseMenuOpen && !IsPlayerMenuOpen);
        playerCamera.enabled = !IsPauseMenuOpen && !IsPlayerMenuOpen;
        playerWeapon.enabled = !IsPauseMenuOpen && !IsPlayerMenuOpen;
        AudioListener.pause = !IsPauseMenuOpen; //later, will want to still allow menu sounds to play
    }

    public void TogglePauseMenu()
    {
        IsPauseMenuOpen = !IsPauseMenuOpen;
        pauseMenu.SetActive(IsPauseMenuOpen);
        UpdateGameState();
    }

    public void TogglePlayerMenu()
    {
        if (IsPauseMenuOpen) return;

        IsPlayerMenuOpen = !IsPlayerMenuOpen;
        playerMenu.SetActive(IsPlayerMenuOpen);
        UpdateGameState();
    }
}
