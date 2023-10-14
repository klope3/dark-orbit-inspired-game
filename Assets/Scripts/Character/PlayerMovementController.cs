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
        playerThruster.moveDirection = referenceTransform.right * input.MovementAxis.x + referenceTransform.forward * input.MovementAxis.y;
        playerThruster.verticalDirection = input.MovementVertical;
    }
}
