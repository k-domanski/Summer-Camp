    using Elympics;
    using UnityEngine;

    public class PlayerSkillController : ElympicsMonoBehaviour
    {
        [SerializeField] private SkillManager skillManager;
        [SerializeField]
        private SkillController primarySkill;
        [SerializeField]
        private SkillController secondarySkill;

        public SkillController SecondarySkillController => secondarySkill;
        public SkillController PrimarySkillController => primarySkill;

        public bool TriggerSkillPickup(int skillID)
        {
            if (secondarySkill.IsSkillAssigned == false)
            {
                SetSkill(skillID);
                return true;
            }

            return false;
        }
        
        public void SetSkill(int skillID)
        {
            var skill = skillManager.GetSkill(skillID);
            secondarySkill.Set(skill);
        }

        public void UsePrimary(bool isFire, bool isActive, Vector3 worldPos)
        {
            //primarySkill.ProcessInput(isFire, isActive, worldPos);
        }

        public void UseSecondary(bool isFire, bool isActive, Vector3 worldPos)
        {
            secondarySkill.ProcessInput(isFire, isActive, worldPos);
        }
    }