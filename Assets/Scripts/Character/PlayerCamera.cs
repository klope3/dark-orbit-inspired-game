using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameInput input;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private Transform cameraFollowAxis;
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Transform weaponBasePivot;
    [SerializeField] private Transform weaponTarget;
    [SerializeField] private float maxDownwardAngle;

    private void LateUpdate()
    {
        Look();
        AimWeapon();
    }

    private void Look()
    {
        cameraFollow.rotation *= Quaternion.AngleAxis(input.LookAxis.y * -1, Vector3.right);
        cameraFollowAxis.rotation *= Quaternion.AngleAxis(input.LookAxis.x, Vector3.up);

        Vector3 angles = cameraFollow.localEulerAngles;
        float adjustedX = angles.x > 180 ? angles.x - 360 : angles.x;

        if (adjustedX < -1 * 80)
        {
            angles.x = 360 - 80;
        }
        if (adjustedX < 180 && adjustedX > 60)
        {
            angles.x = 60;
        }
        angles.y = 0;
        angles.z = 0;
        cameraFollow.localEulerAngles = angles;
    }

    private void AimWeapon()
    {
        weaponPivot.LookAt(weaponTarget, weaponTarget.up);

        Vector3 weaponAngles = weaponPivot.localEulerAngles;
        Vector3 weaponBaseAngles = weaponPivot.localEulerAngles;
        if (weaponAngles.x < 270 && weaponAngles.x > maxDownwardAngle)
        {
            weaponAngles.x = maxDownwardAngle;
        }
        weaponPivot.localEulerAngles = weaponAngles;

        weaponBaseAngles.x = 0;
        weaponBaseAngles.z = 0;
        weaponBasePivot.localEulerAngles = weaponBaseAngles;
    }
}
