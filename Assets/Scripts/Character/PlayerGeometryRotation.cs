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
        if (playerMovement.moveDirectionHorz.magnitude == 0) return;

        Vector3 playerDirectionAdjusted = new Vector3(playerMovement.moveDirectionHorz.x, 0, playerMovement.moveDirectionHorz.y);
        playerGeometry.transform.forward = Vector3.Lerp(playerGeometry.transform.forward, playerDirectionAdjusted, Time.deltaTime * lerpStrength);
    }
}
