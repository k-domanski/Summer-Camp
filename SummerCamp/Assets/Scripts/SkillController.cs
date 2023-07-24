using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class SkillController : ElympicsMonoBehaviour
{
    //Temporary
    [SerializeField] private ASkill skill;

    public void ProcessInput(bool isFire)
    {
        if(isFire)
        {
            skill.PerformPrimaryAction();
        }
    }
}
