using UnityEngine;
using UnityEngine.UI;

public class GUIPlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Image skillCooldown;
    [SerializeField]
    private Image skillImage;
    
    private void Awake()
    {
        ResetSkill(null, 0);
    }

    private void ResetSkill(Sprite img, float fill)
    {
        skillImage.sprite = img;
        SetCooldownFill(fill);
    }

    private void SetCooldownFill(float fill)
    {
        skillCooldown.fillAmount = 0;
    }

    public void Set(SkillController skillController)
    {
        skillController.SkillActive += SkillActive;
        skillController.SkillChanged += OnSkillChanged;
    }

    
    private void OnSkillChanged(ASkill skill)
    {
        ResetSkill(skill.SkillImage, 1);
    }

    private void SkillActive(bool skillActive, ASkill arg2)
    {
        if (skillActive == false)
        {
            ResetSkill(null, 0);
        }
    }
}