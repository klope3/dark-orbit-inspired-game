using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform muzzleLocation;
    [SerializeField] private GameObjectPool projectilePool;
    [SerializeField] private bool showAimLine;

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
        Fire();
    }

    public void SetTriggerOff()
    {
    }

    public void Fire()
    {
        GameObject pooledProj = projectilePool.GetPooledObject();
        pooledProj.SetActive(true);
        pooledProj.transform.position = muzzleLocation.position;
        Projectile proj = pooledProj.GetComponent<Projectile>();
        proj.movementVector = muzzleLocation.forward;
    }
}
