using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpactEffect : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private ParticleSystem effect;

    private void Awake()
    {
        projectile.OnProjectileImpact += Projectile_OnProjectileImpact;
    }

    private void Projectile_OnProjectileImpact(Projectile projectile, RaycastHit rayHit)
    {
        effect.transform.forward = rayHit.normal;
        effect.Play();
    }
}
