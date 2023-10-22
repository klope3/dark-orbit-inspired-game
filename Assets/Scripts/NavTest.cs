using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    private Vector3 positionLastFrame;
    private int stuckFramesCount;

    private void Update()
    {
        agent.destination = target.position;
        //Debug.Log($"Is stopped: {agent.isStopped}; stale: {agent.isPathStale}; hasPath: {agent.hasPath}; pathStatus: {agent.pathStatus}, distance: {agent.remainingDistance}");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NavMeshPath path = new NavMeshPath();
            Debug.Log(agent.CalculatePath(target.position, path));
        }
        float distanceSinceLastFrame = Vector3.Distance(agent.transform.position, positionLastFrame);
        if (distanceSinceLastFrame < 0.2f)
        {
            stuckFramesCount++;
        } 
        else
        {
            if (stuckFramesCount > 500) Debug.Log("Unstuck");
            stuckFramesCount = 0;
        }
        if (stuckFramesCount == 500)
        {
            Debug.LogWarning("Stuck!!");
        }

        positionLastFrame = agent.transform.position;
    }
}
