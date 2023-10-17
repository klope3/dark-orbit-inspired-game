using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private GameInput input;
    [SerializeField] private ThrusterMovement playerThruster;
    [SerializeField] private Transform referenceTransform;

    private void Update()
    {
        Vector3 moveDirection = referenceTransform.right * input.MovementAxis.x + referenceTransform.forward * input.MovementAxis.y;

        playerThruster.moveDirectionHorz = new Vector2(moveDirection.x, moveDirection.z);
        playerThruster.verticalDirection = input.MovementVertical;
    }
}
