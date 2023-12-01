using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Weapon : MonoBehaviour
{
    [SerializeField, Min(0.001f)] private float shotsPerSecond;
    [SerializeField] private FireType fireType;
    [SerializeField] private AmmoType ammoType;
    [SerializeField, ShowIf("ammoType", AmmoType.Recharge), Min(1)] private float rechargePerSecond;
    [SerializeField] private Transform muzzleLocation;
    [SerializeField] private GameObjectPool projectilePool;
    [SerializeField, Tooltip("Whether to try and find a pool on Awake. " +
        "Useful for weapons instantiated at runtime, since these won't have " +
        "a reference to a pool yet.")] private bool findPool;
    [SerializeField, Tooltip("The tag to use for finding a pool on Awake. " +
        "Does nothing if findPool is false.")] private string findPoolTag;
    [SerializeField] private bool showAimLine;
    [SerializeField, Min(1)] private float maxAmmo;
    [SerializeField, Min(1)] private float startingAmmo;
    [SerializeField, Min(0)] private float ammoPerShot;
    [ShowInInspector, ReadOnly] private float currentAmmo;
    private float triggerTimer;
    private bool wasTriggerPulled; //last frame
    private bool isTriggerPulled; //this frame
    public UnityEvent OnFire;
    public UnityEvent OnAmmoChange;

    public float CurAmmo
    {
        get
        {
            return currentAmmo;
        }
    }

    public float MaxAmmo
    {
        get
        {
            return maxAmmo;
        }
    }

    public AmmoType WeaponAmmoType
    {
        get
        {
            return ammoType;
        }
    }

    public enum FireType
    {
        SemiAutomatic,
        Automatic
    }

    public enum AmmoType
    {
        Normal,
        Recharge
    }

    private void Awake()
    {
        AddAmmo(startingAmmo);

        if (findPool)
        {
            string findError = $"The weapon attached to {gameObject.name} couldn't find a GameObjectPool with the tag {findPoolTag}!";
            GameObject objWithTag = GameObject.FindGameObjectWithTag(findPoolTag);
            if (objWithTag == null)
            {
                Debug.LogWarning(findError);
            }
            projectilePool = objWithTag.GetComponent<GameObjectPool>();
            if (projectilePool == null)
            {
                Debug.LogWarning(findError);
            }
        }
    }

    private void Update()
    {
        if (ammoType == AmmoType.Recharge)
        {
            float amountToRecharge = rechargePerSecond * Time.deltaTime;
            AddAmmo(amountToRecharge);
        }
        TryFire();

        wasTriggerPulled = isTriggerPulled;
    }

    private void OnDrawGizmos()
    {
        if (showAimLine && muzzleLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(muzzleLocation.position, muzzleLocation.position + muzzleLocation.forward * 1000);
        }
    }

    public void SetTriggerOn()
    {
        isTriggerPulled = true;
    }

    public void SetTriggerOff()
    {
        isTriggerPulled = false;
    }

    private void TryFire()
    {
        bool triggerTimerReady = triggerTimer >= 1 / shotsPerSecond;
        if (!triggerTimerReady)
        {
            triggerTimer += Time.deltaTime;
        }

        bool canAutoFire = isTriggerPulled && triggerTimerReady;
        bool canSemiAutoFire = !wasTriggerPulled && isTriggerPulled && triggerTimerReady;
        bool isFiringAllowedByTrigger = (canAutoFire && fireType == FireType.Automatic) || (canSemiAutoFire && fireType == FireType.SemiAutomatic);
        bool enoughAmmo = currentAmmo >= ammoPerShot;

        if (isFiringAllowedByTrigger && enoughAmmo)
        {
            Fire();
        }
    }

    public void Fire()
    {
        GameObject pooledProj = projectilePool.GetPooledObject();
        pooledProj.SetActive(true);
        pooledProj.transform.position = muzzleLocation.position;
        Projectile proj = pooledProj.GetComponent<Projectile>();
        proj.Launch(muzzleLocation.forward);
        proj.transform.forward = muzzleLocation.forward;
        triggerTimer = 0;
        AddAmmo(-1 * ammoPerShot);

        OnFire?.Invoke();
    }

    public void AddAmmo(float amount)
    {
        float prevAmmo = currentAmmo;
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        
        if (prevAmmo != currentAmmo) OnAmmoChange?.Invoke();
    }
}
