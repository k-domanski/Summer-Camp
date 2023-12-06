using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Elympics;
using System;

public class PlayerPanelUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;

    public int PlayerScore { get; private set; } = -1;

    public void Initialize(PlayerData playerData, ElympicsInt score, Action onPlayerScoreChanged)
    {
        SetupView(playerData);

        score.ValueChanged += (int lastValue, int newValue) =>
        {
            UpdateScoreView(newValue);
            onPlayerScoreChanged();
        };
    }

    private void UpdateScoreView(int newValue)
    {
        PlayerScore = newValue;
        playerScore.text = newValue.ToString();
    }

    private void SetupView(PlayerData playerData)
    {
        playerName.text = $"Player_{playerData.PlayerID}";
    }
}
