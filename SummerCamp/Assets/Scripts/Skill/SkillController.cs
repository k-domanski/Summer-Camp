using UnityEngine;
using Elympics;
using System;

public class SkillController : ElympicsMonoBehaviour
{
    [SerializeField] private ASkill currentSkill;

    public bool IsSkillAssigned => currentSkill != null;
    public int SkillID => currentSkill.SkillID;

    public event Action<ASkill> SkillChanged;
    public event Action<bool, ASkill> SkillActive;
    public event Action<ASkill> SkillUsed;

    public void ProcessInput(bool isFire, bool isActive, Vector3 worldPos)
    {
        if (IsSkillAssigned == false)
        {
            return;
        }
        
        currentSkill.Indicator.ShowIndicator(true);

        if(true)
        {
            currentSkill.UpdateAimPosition(worldPos);
            
            if (isFire)
            {
                if (currentSkill.TryPerformPrimaryAction())
                {
                    SkillUsed?.Invoke(currentSkill);
                }
            }
        }

        if (!currentSkill.HasCharges)
        {
            currentSkill.Indicator.ShowIndicator(false);
            currentSkill = null;
            SkillActive?.Invoke(IsSkillAssigned, null);
        }

    }

    public void Set(ASkill skill)
    {
        currentSkill = skill;
        currentSkill.ResetCharges();
        SkillActive?.Invoke(IsSkillAssigned, currentSkill);
        SkillChanged?.Invoke(skill);
    }
}
