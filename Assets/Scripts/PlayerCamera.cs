using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameInput input;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private Transform cameraFollowAxis;
    [SerializeField] private float sensitivity;

    private void LateUpdate()
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
}
