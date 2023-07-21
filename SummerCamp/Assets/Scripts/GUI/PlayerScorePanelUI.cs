using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScorePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerTimeText;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetPlayerScoreText(int playerID, float time)
    {
        playerNameText.text = $"Player_{playerID}";
        playerTimeText.text = time.ToString();
    }

    public void ShowPanel(bool show)
    {
        canvasGroup.alpha = show ? 1.0f : 0.0f;
    }
}
