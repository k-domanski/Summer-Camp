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
                secondarySkillId.Value = skillID;
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

        private void OnSecondarySkillChanged(int lastvalue, int newvalue)
        {
            SetSkill(newvalue);
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