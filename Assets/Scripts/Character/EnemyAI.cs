using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float pursuePlayerDistance;
    [SerializeField] private LayerMask sightBlockingLayers;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Weapon weapon;
    [SerializeField] private NavMeshAgent agent;
    //[SerializeField] private ThrusterMovement thruster;
    public bool canAttack = true;
    private Transform playerTransform;
    private const float behaviorChangeMinTime = 0.5f;
    private const float behaviorChangeMaxTime = 5;
    private float behaviorChangeTimer;
    private float behaviorChangeTimerTarget;
    private bool canSeePlayer;
    private Vector3 pursuePlayerOffset;
    //private bool hasReachedTarget;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        ResetBehaviorTimer();
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector3 aimDirection = playerTransform.position - weaponPivot.position;
        Ray ray = new Ray(weaponPivot.position, aimDirection);
        RaycastHit rayHit;
        Physics.Raycast(ray, out rayHit, float.MaxValue, sightBlockingLayers);
        canSeePlayer = rayHit.collider.transform == playerTransform;

        if (behaviorChangeTimer >= behaviorChangeTimerTarget)
        {
            ChooseNavigationTarget();
            ResetBehaviorTimer();
        }
        else
        {
            behaviorChangeTimer += Time.deltaTime;
        }

        AimAndAttack();
        //Move();
    }

    private void ResetBehaviorTimer()
    {
        behaviorChangeTimerTarget = Random.Range(behaviorChangeMinTime, behaviorChangeMaxTime);
        behaviorChangeTimer = 0;
    }

    private void ChooseNavigationTarget()
    {
        if (!canSeePlayer) return;

        //pursuePlayerOffset = Random.insideUnitSphere.normalized * pursuePlayerDistance;
        pursuePlayerOffset = Random.insideUnitCircle.normalized * pursuePlayerDistance;
        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("Did not set destination because the agent isn't on a navmesh");
        }
        else
        {
            agent.destination = playerTransform.position + pursuePlayerOffset;
        }
        //pursuePlayerOffset.y = Mathf.Abs(pursuePlayerOffset.y);
        //hasReachedTarget = false;
    }

    //private void Move()
    //{
    //    Vector3 vecToTarget = (playerTransform.position + pursuePlayerOffset) - transform.position;
    //    if (vecToTarget.magnitude < 1 || hasReachedTarget)
    //    {
    //        hasReachedTarget = true;
    //        thruster.moveDirectionHorz = Vector2.zero;
    //        thruster.verticalDirection = 0;
    //        return;
    //    }
    //
    //    thruster.moveDirectionHorz = new Vector2(vecToTarget.x, vecToTarget.z);
    //    thruster.verticalDirection = vecToTarget.y < 0 ? -1 : 1;
    //
    //    Debug.DrawLine(transform.position, playerTransform.position + pursuePlayerOffset);
    //}

    private void AimAndAttack()
    {
        weaponPivot.LookAt(playerTransform);

        if (canAttack && canSeePlayer)
        {
            weapon.SetTriggerOn();
        }
        else
        {
            weapon.SetTriggerOff();
        }
    }
}
