using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AIState startingState;
    [SerializeField] private float pursuePlayerDistance;
    [SerializeField] private float wanderRadius;
    [SerializeField] private LayerMask sightBlockingLayers;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Weapon weapon;
    [SerializeField] private AIPath aiPath;
    //[SerializeField] private NavMeshAgent agent;
    public bool canAttack = true;
    private Transform playerTransform;
    private const float behaviorChangeMinTime = 0.5f;
    private const float behaviorChangeMaxTime = 5;
    private float navigationChangeTimer;
    private float navigationChangeTimerTarget;
    private bool canSeePlayer;
    private Vector3 pursuePlayerOffset;
    public Vector3 wanderAnchor;
    [HideInInspector] public AIState aiState;

    public enum AIState
    {
        None,
        Wander,
        AttackPlayer,
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        aiState = startingState;
        ResetNavigationTimer();
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        CheckSeePlayer();
        NavigationChange();
        AimAndAttack();
    }

    private void CheckSeePlayer()
    {
        Vector3 aimDirection = playerTransform.position - weaponPivot.position;
        Ray ray = new Ray(weaponPivot.position, aimDirection);
        RaycastHit rayHit;
        Physics.Raycast(ray, out rayHit, float.MaxValue, sightBlockingLayers);
        canSeePlayer = rayHit.collider.transform == playerTransform;
    }

    private void ResetNavigationTimer()
    {
        navigationChangeTimerTarget = Random.Range(behaviorChangeMinTime, behaviorChangeMaxTime);
        navigationChangeTimer = 0;
    }

    private void ChooseNavigationTarget()
    {
        if (!canSeePlayer) return;
        //if (!agent.isOnNavMesh)
        //{
        //    Debug.LogWarning("Did not set destination because the agent isn't on a navmesh");
        //    return;
        //}

        pursuePlayerOffset = Random.insideUnitCircle.normalized * pursuePlayerDistance;
        if (aiState == AIState.AttackPlayer)
        {
            //agent.destination = playerTransform.position + pursuePlayerOffset;
            aiPath.destination = playerTransform.position + pursuePlayerOffset;
        }
        else if (aiState == AIState.Wander)
        {
            Vector2 randPoint = Random.insideUnitCircle;
            //agent.destination = wanderAnchor + new Vector3(randPoint.x, 0, randPoint.y) * wanderRadius;
            aiPath.destination = wanderAnchor + new Vector3(randPoint.x, 0, randPoint.y) * wanderRadius;
        }
    }

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

    private void NavigationChange()
    {
        if (navigationChangeTimer >= navigationChangeTimerTarget)
        {
            ChooseNavigationTarget();
            ResetNavigationTimer();
        }
        else
        {
            navigationChangeTimer += Time.deltaTime;
        }
    }
}
