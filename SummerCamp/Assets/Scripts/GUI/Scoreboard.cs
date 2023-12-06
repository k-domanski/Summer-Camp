using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scoreboard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerPanelUI playerPanelPrefab;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform playerPanelContainer;
    [SerializeField] private PlayersProvider playersProvider;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameOverUI gameOverUI;

    private HUDController clientHUDController;
    private List<PlayerPanelUI> createdPlayerPanels = new List<PlayerPanelUI>();

    private void Awake()
    {
        canvasGroup.alpha = 0.0f;

        if(playersProvider.IsReady)
        {
            SetupScoreboard();
        }
        else
        {
            playersProvider.IsReadyChanged += SetupScoreboard;
        }
    }

    private void SetupScoreboard()
    {
        if(playersProvider.ClientPlayer.TryGetComponent(out HUDController hudController))
        {
            clientHUDController = hudController;
            clientHUDController.onShowScoreboardValueChanged += SetScoreboardDisplayStatus;
        }

        if(scoreManager.IsReady)
        {
            SubscribeToScoreManager();
        }
        else
        {
            scoreManager.IsReadyChanged += SubscribeToScoreManager;
        }
    }

    private void SubscribeToScoreManager()
    {
        CreateScoreboardPlayers();

        scoreManager.WinnerID.ValueChanged += OnWinnerIDSet;
    }

    private void OnWinnerIDSet(int lastValue, int newValue)
    {
        if (newValue < 0)
            return;

        clientHUDController.onShowScoreboardValueChanged -= SetScoreboardDisplayStatus;

        SetScoreboardDisplayStatus(true);

        //show gameover screen

        gameOverUI.ShowGameOverScreen(playersProvider.AllPlayers[newValue]);
    }

    private void SetScoreboardDisplayStatus(bool show)
    {
        canvasGroup.alpha = show ? 1.0f : 0.0f;
    }

    private void CreateScoreboardPlayers()
    {
        foreach(PlayerData playerData in playersProvider.AllPlayers)
        {
            var scoreboardPlayer = Instantiate(playerPanelPrefab, playerPanelContainer);
            scoreboardPlayer.Initialize(playerData, scoreManager.GetPlayerScore(playerData.PlayerID), RefreshOrderOfPlayerScoreboard);

            createdPlayerPanels.Add(scoreboardPlayer);
        }
    }

    private void RefreshOrderOfPlayerScoreboard()
    {
        var sorted = createdPlayerPanels.OrderByDescending(x => x.PlayerScore);

        int index = 0;

        foreach(PlayerPanelUI scoreboardPlayer in sorted)
        {
            scoreboardPlayer.transform.SetSiblingIndex(index);
            index++;
        }
    }
}
