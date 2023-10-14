using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeometryRotation : MonoBehaviour
{
    [SerializeField] private Transform playerGeometry;
    [SerializeField] private ThrusterMovement playerMovement;
    [SerializeField] private float lerpStrength;

    private void Update()
    {
        if (playerMovement.moveDirection.magnitude == 0) return;

        playerGeometry.transform.forward = Vector3.Lerp(playerGeometry.transform.forward, playerMovement.moveDirection, Time.deltaTime * lerpStrength);

        //Debug.DrawLine(playerGeometry.position, playerGeometry.position + playerMovement.moveDirection * 10);
    }
}
