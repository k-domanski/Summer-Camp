using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class AreaSkill : ASkill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius = 15.0f;
    [SerializeField] private float slowAmount = 5.0f;
    
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
        area.GetComponent<SlowEffectArea>().SetEffect(effect);
        area.transform.position = new Vector3(aimPosition.x, area.transform.position.y, aimPosition.z);
    }

    private GameObject CreateArea()
    {
        var area = ElympicsInstantiate(prefab.gameObject.name, this.PredictableFor);
        area.GetComponent<SlowEffectArea>().SetOwner(transform.root.gameObject.GetComponent<ElympicsBehaviour>());
        return area;
    }

}
