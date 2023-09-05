using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : ASkill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius = 15.0f;
    [SerializeField] private LayerMask layer;


    private Vector3 aimPosition;

    public override void UpdateAimPosition(Vector3 worldPosition)
    {
        Vector3 direction = (worldPosition - transform.position);
        Vector3 magnitude = Vector3.ClampMagnitude(direction, radius);
        aimPosition = transform.position + magnitude;

        indicator.ApplyPosition(aimPosition);

        Ray ray = new Ray(aimPosition + Vector3.up, Vector3.down);
        canUseSkill = Physics.Raycast(ray, 1.5f, layer);    // dirty way to check if floor is there

        indicator.ChangeColor(canUseSkill);
    }

    protected override void ProcessSkillAction()
    {
        var area = CreateArea();
        area.transform.position = new Vector3(aimPosition.x, area.transform.position.y, aimPosition.z);
    }

    private GameObject CreateArea()
    {
        var area = ElympicsInstantiate(prefab.gameObject.name, this.PredictableFor);
        return area;
    }
}
