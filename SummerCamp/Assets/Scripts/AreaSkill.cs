using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkill : ASkill
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
        area.transform.position = aimPosition;
    }

    private GameObject CreateArea()
    {
        return ElympicsInstantiate(prefab.gameObject.name, this.PredictableFor);
    }

}
