using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private List<PlayerScorePanelUI> playerScorePanels;
    [SerializeField] private FinishLine finishLine;
    [SerializeField] private PlayersProvider playersProvider;

    private static int playerCount = 0;

    private void Start()
    {
        if (playersProvider.IsReady)
        {
            GetPlayerCount();
        }
        else
        {
            playersProvider.IsReadyChanged += GetPlayerCount;
        }

        gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.0f;
        foreach (PlayerScorePanelUI panel in playerScorePanels)
        {
            panel.ShowPanel(false);
        }
    }

    private void OnEnable()
    {
        finishLine.onPlayerFinished += ShowScoreboard;
    }

    private void OnDisable()
    {
        finishLine.onPlayerFinished += ShowScoreboard;
        playersProvider.IsReadyChanged -= GetPlayerCount;
    }

    private void GetPlayerCount()
    {
        playerCount = playersProvider.AllPlayers.Length;
    }

    private void ShowScoreboard(int playerID)
    {
        if(playersProvider.ClientPlayer.PlayerID == playerID)
        {
            gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1.0f;
            for (int i = 0; i < playerCount; i++)
            {
                playerScorePanels[i].ShowPanel(true);
            }
        }
    }
}
