using Elympics;
using UnityEngine;

public class PlayerData : ElympicsMonoBehaviour
{
    public int PlayerID => playerID;

    [SerializeField] private int playerID = 0;
    [SerializeField] private PlayerSkillController skillController;
    
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
        if (Elympics.IsServer)
        {
            var skillSpawner = other.GetComponentInParent<SkillSpawner>();
            if (skillSpawner != null)
            {
                skillController.TriggerSkillPickup(skillSpawner.SkillID);
            }
        }
    }
}
