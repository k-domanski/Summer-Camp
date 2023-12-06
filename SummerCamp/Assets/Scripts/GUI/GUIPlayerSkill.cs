using System.Collections;
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

    private ASkill currentSkill;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        ResetSkill(null, 0);
    }

    private void ResetSkill(Sprite img, float fill)
    {
        skillImage.sprite = img;
        SetCooldownFill(fill);
        canvasGroup.alpha = fill;
    }

    private void SetCooldownFill(float fill)
    {
        skillCooldown.fillAmount = fill;
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
        usageText.text = $"Charges: " + objCharges;
        usageText.gameObject.SetActive(objCharges > 0);
    }

    private void OnSkillChanged(ASkill skill)
    {
        currentSkill = skill;
        
        ResetSkill(skill.SkillImage, 1);
        SetCharges(skill.Charges);
        StartCoroutine(SetFill());
    }

    public IEnumerator SetFill()
    {
        while (currentSkill != null)
        {
            SetCooldownFill(currentSkill.TimeRatio);
            yield return null;
        }
    }

    private void SkillActive(bool skillActive, ASkill arg2)
    {
        if (skillActive == false)
        {
            ResetSkill(null, 0);
            currentSkill = null;
        }
    }
}