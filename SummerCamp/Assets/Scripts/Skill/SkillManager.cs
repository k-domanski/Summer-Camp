using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private ASkill[] allskills;

    public ASkill GetSkill(int skillID)
    {
        return allskills.First(entry => entry.SkillID == skillID);
    }

    [ContextMenu("Get skills in Children")]
    void GetSkills()
    {
        allskills = GetComponentsInChildren<ASkill>();
    }
}