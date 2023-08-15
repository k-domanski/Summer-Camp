using UnityEngine;

[CreateAssetMenu(fileName = "Skill Effect", menuName = "ScriptableObjects/SkillEffects" )]
public class SkillEffect : ScriptableObject
{
    public float duration;
    public float effectAmount;
}
