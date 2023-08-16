using System;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private ASkill[] allskills;

    public int[] SkillIDs { get; private set; }

    private void Awake()
    {
        SkillIDs = allskills.Select(entry => entry.SkillID).ToArray();
    }

    public ASkill GetSkill(int skillID)
    {
        return allskills.First(entry => entry.SkillID == skillID);
    }

    public int GetRandomSkillID()
    {
        int randomNumber = UnityEngine.Random.Range(0, allskills.Length);
        return allskills[randomNumber].SkillID;
    }

    [ContextMenu("Get skills in Children")]
    void GetSkills()
    {
        allskills = GetComponentsInChildren<ASkill>();
    }
}