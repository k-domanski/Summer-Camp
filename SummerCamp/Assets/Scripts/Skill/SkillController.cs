using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

//Temporary
public class SkillController : ElympicsMonoBehaviour
{
    [SerializeField] private ASkill currentSkill;

    public bool IsSkillAssigned => currentSkill != null;
    public int SkillID => currentSkill.SkillID;

    public void ProcessInput(bool isFire, bool isActive, Vector3 worldPos)
    {
        

        if (IsSkillAssigned == false)
        {
            return;
        }
        
        currentSkill.Indicator.ShowIndicator(isActive);

        if(isActive)
        {
            currentSkill.UpdateAimPosition(worldPos);
            
            if (isFire)
            {
                currentSkill.PerformPrimaryAction();
            }

            
        }

        if (!currentSkill.HasCharges)
        {
            currentSkill.Indicator.ShowIndicator(false);
            currentSkill = null;
        }

    }

    public void Set(ASkill skill)
    {
        currentSkill = skill;
        currentSkill.ResetCharges();
    }
}
