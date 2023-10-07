using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifespan;
    [SerializeField] private LayerMask impactLayers;
    [HideInInspector] public Vector3 movementVector;
    private float lifeTimer;
    public UnityEvent OnImpact;

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifespan)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 prevPos = transform.position;
        Vector3 nextPos = transform.position + movementVector * Time.deltaTime * speed;
        Ray ray = new Ray(prevPos, nextPos);
        RaycastHit rayHit;
        bool hit = Physics.Raycast(ray, out rayHit, (nextPos - prevPos).magnitude, impactLayers);

        if (hit)
        {
            transform.position = rayHit.point;
            HealthHandler health = rayHit.collider.GetComponent<HealthHandler>();
            if (health != null)
            {
                health.AddHealth(-1 * damage);
            }
            OnImpact?.Invoke();
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = nextPos;
        }
    }

    private void OnEnable()
    {
        lifeTimer = 0;
    }
}
