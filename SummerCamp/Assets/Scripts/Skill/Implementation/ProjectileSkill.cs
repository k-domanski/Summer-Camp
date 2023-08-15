using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class ProjectileSkill : ASkill
{
    [SerializeField] private float range;
    [SerializeField] private float movementSpeedDebuff;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Projectile projectilePrefab;

    private Vector3 direction;

    private void Start()
    {
        indicator.ApplyRange(range);
    }

    public override void UpdateAimPosition(Vector3 worldPosition)
    {
        indicator.ApplyRotation(worldPosition);
        worldPosition.y = spawnPoint.position.y;
        direction = (worldPosition - spawnPoint.position).normalized;
        
        #if UNITY_EDITOR
        Debug.DrawRay(spawnPoint.position, direction * 100.0f, Color.red);
        #endif
    }

    protected override void ProcessSkillAction()
    {
        var projectile = CreateProjectile();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = spawnPoint.transform.rotation;

        var proj = projectile.GetComponent<Projectile>();
        proj.SetSlowAmount(movementSpeedDebuff);
        proj.Launch(direction);
    }

    private GameObject CreateProjectile()
    {
        var projectile = ElympicsInstantiate(projectilePrefab.gameObject.name, this.PredictableFor);
        projectile.GetComponent<Projectile>().SetOwner(transform.root.gameObject.GetComponent<ElympicsBehaviour>());

        return projectile;
    }
}
