using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

//Temporary
public class SkillController : ElympicsMonoBehaviour
{
    [SerializeField] private ASkill currentSkill;
    //[SerializeField] private List<ASkill> availableSkills = new List<ASkill>();

    public void ProcessInput(bool isFire, bool isActive, Vector3 worldPos)
    {
        currentSkill.Indicator.ShowIndicator(isActive);

        if(isActive)
        {
            currentSkill.UpdateAimPosition(worldPos);
            
            if (isFire)
            {
                currentSkill.PerformPrimaryAction();
            }
        }
    }
}
