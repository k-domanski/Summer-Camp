using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

//Temporary
public class SkillController : ElympicsMonoBehaviour
{
    [SerializeField] private ASkill skill;

    public void ProcessInput(bool isFire)
    {
        if(isFire)
        {
            skill.PerformPrimaryAction();
        }
    }
}
