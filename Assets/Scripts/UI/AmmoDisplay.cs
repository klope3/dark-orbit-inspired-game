using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Image ammoBar;

    public void UpdateAmmo()
    {
        float newAmount = (float)currentWeapon.CurAmmo / (float)currentWeapon.MaxAmmo;
        ammoBar.fillAmount = newAmount;
    }
}
