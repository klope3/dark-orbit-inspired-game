using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeometryRotation : MonoBehaviour
{
    [SerializeField] private Transform playerGeometry;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float lerpStrength;

    private void Update()
    {
        if (!playerMovement.IsThrusting) return;

        playerGeometry.transform.forward = Vector3.Lerp(playerGeometry.transform.forward, playerMovement.MoveDirection, Time.deltaTime * lerpStrength);

        Debug.DrawLine(playerGeometry.position, playerGeometry.position + playerMovement.MoveDirection * 10);
    }
}
