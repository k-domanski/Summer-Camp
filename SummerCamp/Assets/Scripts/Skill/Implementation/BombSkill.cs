using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : ASkill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius = 15.0f;

    private Vector3 aimPosition;

    public override void UpdateAimPosition(Vector3 worldPosition)
    {
        Vector3 direction = (worldPosition - transform.position);
        Vector3 magnitude = Vector3.ClampMagnitude(direction, radius);
        aimPosition = transform.position + magnitude;

        indicator.ApplyPosition(aimPosition);
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
