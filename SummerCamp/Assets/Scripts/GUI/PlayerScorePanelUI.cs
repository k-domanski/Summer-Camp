using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScorePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerTimeText;

    public void SetPlayerScoreText(int playerID, float time)
    {
        playerNameText.text = $"Player_{playerID}";
        playerTimeText.text = time.ToString();
    }
}
