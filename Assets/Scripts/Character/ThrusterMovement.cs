using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterMovement : MonoBehaviour
{
    //[SerializeField] private GameInput input;
    [SerializeField] private float baseForce;
    [SerializeField] private float accelerationForce;
    [SerializeField] private bool useSpeedLimit;
    [SerializeField] private float speedLimit;
    [SerializeField] private bool useAutoBrake;
    [SerializeField] private float brakeForce;
    [SerializeField] private float stoppedTolerance;
    //[SerializeField] private Transform referenceTransform;
    private Rigidbody rb;

    [HideInInspector] public Vector2 moveDirectionHorz;
    [HideInInspector] public int verticalDirection;
    [SerializeField, Tooltip("The upward force required to maintain " +
        "a vertical velocity of 0. Depends on the project's gravity " +
        "force setting.")] private float neutralVertForce;
    [SerializeField, Tooltip("How much to increase the vertical force " +
        "in order to ascend.")] private float ascendForce;
    [SerializeField, Tooltip("How much do decrease the vertical force " +
        "in order to descend.")] private float descendForce;
    //public Vector3 MoveDirection { get; private set; }
    //public bool IsThrusting { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Vector2 inputAxis = input.MovementAxis;

        if (useAutoBrake)
            AutoBrake();
        MoveHorizontal();
        MoveVertical();
    }

    private void MoveHorizontal()
    {
        Vector3 direction = new Vector3(moveDirectionHorz.x, 0, moveDirectionHorz.y).normalized;
        //Vector3 moveDirection = referenceTransform.right * input.MovementAxis.x + referenceTransform.forward * input.MovementAxis.y;
        //moveDirection.Normalize();
        //MoveDirection = moveDirection;
        if (useSpeedLimit)
        {
            Vector3 oppositeDirection = -1 * rb.velocity.normalized;
            float speedToLimitRatio = rb.velocity.magnitude / speedLimit; //how close we currently are to the speed limit
            direction += oppositeDirection * speedToLimitRatio;
        }

        rb.AddForce(direction * accelerationForce * baseForce * Time.deltaTime);

        //IsThrusting = moveDirection.magnitude > 0;
    }

    private void MoveVertical()
    {
        float vertForceAdjust = 0;
        if (verticalDirection < 0) vertForceAdjust = -1 * descendForce;
        if (verticalDirection > 0) vertForceAdjust = ascendForce;

        rb.AddForce(Vector2.up * (neutralVertForce + vertForceAdjust) * Time.deltaTime);
    }

    private void AutoBrake()
    {
        float brakeX = 0;
        float brakeZ = 0;

        if (moveDirectionHorz.x == 0)
            brakeX = -1 * rb.velocity.normalized.x;
        if (moveDirectionHorz.y == 0)
            brakeZ = -1 * rb.velocity.normalized.y;

        Vector2 brakeVec = Vector3.right * brakeX + Vector3.forward * brakeZ;
        rb.AddForce(brakeVec * brakeForce * baseForce * Time.deltaTime);

        if (rb.velocity.magnitude < stoppedTolerance)
            rb.velocity = Vector3.zero;
    }
}
