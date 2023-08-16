using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScorePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerTimeText;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        SetEmptyValue();
    }

    public void SetPlayerScoreText(int playerID, float time)
    {
        playerNameText.text = $"Player_{playerID}";
        playerTimeText.text = time.ToString("F") + "s";
    }

    public void ShowPanel(bool show)
    {
        canvasGroup.alpha = show ? 1.0f : 0.0f;
    }

    private void SetEmptyValue()
    {
        playerNameText.text = string.Empty;
        playerTimeText.text = string.Empty;
    }
}
