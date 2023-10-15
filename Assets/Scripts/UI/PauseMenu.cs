using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Weapon playerWeapon;
    public bool IsMenuOpen { get; private set; }

    public void TogglePauseMenu()
    {
        IsMenuOpen = !IsMenuOpen;
        pauseMenu.SetActive(IsMenuOpen);
        Time.timeScale = IsMenuOpen ? 0 : 1;
        gameManager.SetCursorLock(!IsMenuOpen);
        playerCamera.enabled = !IsMenuOpen;
        playerWeapon.enabled = !IsMenuOpen;
        AudioListener.pause = !IsMenuOpen; //later, will want to still allow menu sounds to play
    }
}
