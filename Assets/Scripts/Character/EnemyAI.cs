using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask sightBlockingLayers;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Weapon weapon;
    public bool canAttack = true;
    private Transform playerTransform;
    private float tempAttackTimer;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        tempAttackTimer += Time.deltaTime;

        Vector3 aimDirection = playerTransform.position - weaponPivot.position;
        Ray ray = new Ray(weaponPivot.position, aimDirection);
        RaycastHit rayHit;
        Physics.Raycast(ray, out rayHit, float.MaxValue, sightBlockingLayers);
        bool canSeePlayer = rayHit.collider.transform == playerTransform;

        weaponPivot.LookAt(playerTransform);

        if (canAttack && tempAttackTimer > 2 && canSeePlayer)
        {
            weapon.SetTriggerOn();
            weapon.SetTriggerOff();
            tempAttackTimer = 0;
        }
    }
}
