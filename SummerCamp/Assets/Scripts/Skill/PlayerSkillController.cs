﻿
    using System;
    using Elympics;
    using UnityEngine;

    public class PlayerSkillController : ElympicsMonoBehaviour
    {
        
        [SerializeField] private SkillManager skillManager;
        [SerializeField]
        private SkillController primarySkill;
        [SerializeField]
        private SkillController secondarySkill;
        
        public ElympicsInt secondarySkillId { get; private set; } = new ElympicsInt(-1);

        public void Start()
        {
            if (Elympics.IsClient)
            {
                secondarySkillId.ValueChanged += OnSecondarySkillChanged;
            }
        }

        private void OnDestroy()
        {
            if (Elympics.IsClient)
            {
                secondarySkillId.ValueChanged -= OnSecondarySkillChanged;
            }
        }

        public bool TriggerSkillPickup(int skillID)
        {
            if (secondarySkill.IsSkillAssigned == false)
            {
                Debug.Log($"TRIGGER SKILL PICKUP ID: {skillID}");

                secondarySkillId.Value = skillID;
                SetSkill(skillID);
                return true;
            }

            return false;
        }
        
        public void SetSkill(int skillID)
        {
            secondarySkill.Set(skillManager.GetSkill(skillID));
        }

        private void OnSecondarySkillChanged(int lastvalue, int newvalue)
        {
            SetSkill(newvalue);
        }

        public void UsePrimary(bool isFire, bool isActive, Vector3 worldPos)
        {
            primarySkill.ProcessInput(isFire, isActive, worldPos);
        }

        public void UseSecondary(bool isFire, bool isActive, Vector3 worldPos)
        {
            secondarySkill.ProcessInput(isFire, isActive, worldPos);
        }
    }