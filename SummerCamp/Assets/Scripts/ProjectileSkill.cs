using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : ASkill
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Projectile projectilePrefab;

    protected override void ProcessSkillAction()
    {
        var projectile = CreateProjectile();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = spawnPoint.transform.rotation;

        projectile.GetComponent<Projectile>().Launch(spawnPoint.transform.forward);
    }

    private GameObject CreateProjectile()
    {
        return ElympicsInstantiate(projectilePrefab.gameObject.name, this.PredictableFor);
    }
}
