using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Elympics;
using System;

public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] private SkillController skillController;
    [SerializeField] private Image cooldownIndicator;
    [SerializeField] private PlayersProvider playersProvider;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private PlayerData playerData;

    private ASkill skill;

    private void Start()
    {
        canvasGroup.alpha = 0.0f;

        //if(playersProvider == null)
        //{
        //    playersProvider = FindObjectOfType<PlayersProvider>();
        //}

        //playerData = gameObject.transform.root.GetComponent<PlayerData>();

        if(playersProvider.ClientPlayer.PlayerID == playerData.PlayerID)
            skillController.onSkillActive += ShowIndicator;
        
    }

    private void Update()
    {
        if(skill != null)
            cooldownIndicator.fillAmount = skill.TimeRatio.Value;
    }

    private void ShowIndicator(bool show, ASkill skill)
    {
        canvasGroup.alpha = show ? 1.0f : 0.0f;

        if (skill != null)
            this.skill = skill;
    }

    private void OnTimeRatioChanged(float lastValue, float newValue)
    {
        Debug.Log($"ON time ratio changed value: {newValue}");
        cooldownIndicator.fillAmount = newValue;
    }
}
