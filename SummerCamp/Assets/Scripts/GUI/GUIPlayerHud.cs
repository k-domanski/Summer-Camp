using Elympics;
using UnityEngine;

public class GUIPlayerHud : ElympicsMonoBehaviour
{
    [SerializeField]
    private PlayersProvider playersProvider;

    [SerializeField]
    private GUIPlayerSkill primarySkill;
    [SerializeField]
    private GUIPlayerSkill secondarySkill;
    
    private void Start()
    {
        if (Elympics.IsServer)
        {
            gameObject.SetActive(false);
            return;
        }
        
        var clientPlayer = playersProvider.ClientPlayer;
        var secondarySkillController = clientPlayer.SkillController.SecondarySkillController;
        var primarySkillController = clientPlayer.SkillController.PrimarySkillController;

        secondarySkill.Set(secondarySkillController);
        primarySkill.Set(primarySkillController);
    }
}
