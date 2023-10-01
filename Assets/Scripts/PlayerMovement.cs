using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput input;
    [SerializeField] private float baseForce;
    [SerializeField] private float accelerationForce;
    [SerializeField] private bool useSpeedLimit;
    [SerializeField] private float speedLimit;
    [SerializeField] private bool useAutoBrake;
    [SerializeField] private float brakeForce;
    [SerializeField] private float stoppedTolerance;
    [SerializeField] private Transform referenceTransform;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 inputAxis = input.MovementAxis;

        if (useAutoBrake)
            AutoBrake(inputAxis);
        Move(inputAxis);
    }

    private void Move(Vector2 inputAxis)
    {
        Vector3 moveDirection = referenceTransform.right * input.MovementAxis.x + referenceTransform.forward * input.MovementAxis.y;
        moveDirection.Normalize();
        if (useSpeedLimit)
        {
            Vector3 oppositeDirection = -1 * rb.velocity.normalized;
            float speedToLimitRatio = rb.velocity.magnitude / speedLimit; //how close we currently are to the speed limit
            moveDirection += oppositeDirection * speedToLimitRatio;
        }

        rb.AddForce(moveDirection * accelerationForce * baseForce * Time.deltaTime);
    }

    private void AutoBrake(Vector2 input)
    {
        float brakeX = 0;
        float brakeZ = 0;

        if (input.x == 0)
            brakeX = -1 * rb.velocity.normalized.x;
        if (input.y == 0)
            brakeZ = -1 * rb.velocity.normalized.y;

        Vector2 brakeVec = Vector3.right * brakeX + Vector3.forward * brakeZ;
        rb.AddForce(brakeVec * brakeForce * baseForce * Time.deltaTime);

        if (rb.velocity.magnitude < stoppedTolerance)
            rb.velocity = Vector3.zero;
    }
}
