using Elympics;
using UnityEngine;

public class PlayerData : ElympicsMonoBehaviour
{
    [SerializeField] private int playerID = 0;
    [SerializeField] private PlayerSkillController skillController;

    public int PlayerID => playerID;
    public PlayerSkillController SkillController => skillController;
    
    public void UsePrimary(bool isFire, bool isActive, Vector3 worldPos)
    {
        skillController.UsePrimary(isFire, isActive, worldPos);
    }

    public void UseSecondary(bool isFire, bool isActive, Vector3 worldPos)
    {
        skillController.UseSecondary(isFire, isActive, worldPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.IsPredictableForMe)
        {
            var skillSpawner = other.GetComponentInParent<SkillSpawner>();
            if (skillSpawner != null && skillSpawner.AvailableForPickUp)
            {
                if (skillController.TriggerSkillPickup(skillSpawner.SkillID))
                {
                    skillSpawner.Pick();
                }
            }
        }
    }
}
