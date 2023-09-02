using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Image skillCooldown;
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private TextMeshProUGUI usageText;
    
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
        skillController.SkillUsed += OnSkillUsed;
    }

    private void OnSkillUsed(ASkill obj)
    {
        SetCharges(obj.Charges);
    }

    private void SetCharges(int objCharges)
    {
        gameObject.SetActive(objCharges > 0);
        usageText.text = $"Charges: " + objCharges;
    }

    private void OnSkillChanged(ASkill skill)
    {
        ResetSkill(skill.SkillImage, 1);
        SetCharges(skill.Charges);
    }

    private void SkillActive(bool skillActive, ASkill arg2)
    {
        if (skillActive == false)
        {
            ResetSkill(null, 0);
        }
    }
}