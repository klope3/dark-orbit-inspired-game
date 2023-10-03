using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Transform weaponTarget;
    [SerializeField] private Transform muzzleLocation;
    [SerializeField] private float maxDownwardAngle;
    [SerializeField] private bool showAimLine;

    private void OnDrawGizmos()
    {
        if (showAimLine && muzzleLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(muzzleLocation.position, muzzleLocation.position + muzzleLocation.forward * 1000);
        }
    }

    private void LateUpdate()
    {
        weaponPivot.LookAt(weaponTarget, weaponTarget.up);

        Vector3 angles = weaponPivot.localEulerAngles;
        if (angles.x < 270 && angles.x > maxDownwardAngle)
        {
            angles.x = maxDownwardAngle;
        }
        weaponPivot.localEulerAngles = angles;
    }

    public void SetTriggerOn()
    {
    }

    public void SetTriggerOff()
    {
    }
}
