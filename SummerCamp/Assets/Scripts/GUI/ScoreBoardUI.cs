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
    [SerializeField] private RaceTimer timer;

    private static int playerCount = 0;
    private ElympicsInt panelIndex = new ElympicsInt(0);

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
        finishLine.scoreUpdate.ValueChanged += TestTime;
        //finishLine.onPlayerFinished += HandleScore;
        //finishLine.p1Time.ValueChanged += HandleP1Score;
        //finishLine.p2Time.ValueChanged += HandleP2Score;
        //finishLine.onScoreUpdated += UpdateScore;
    }

    private void TestTime(bool lastValue, bool newValue)
    {
        playerScorePanels[0].SetPlayerScoreText(12, timer.CurrentTime.Value);
    }

    private void HandleP2Score(float lastValue, float newValue)
    {
        ShowScoreboard(1);
        playerScorePanels[1].SetPlayerScoreText(1, newValue);
    }

    private void HandleP1Score(float lastValue, float newValue)
    {
        ShowScoreboard(0);
        playerScorePanels[0].SetPlayerScoreText(0, newValue);
    }

    private void UpdateScore(List<ElympicsFloat> arg1, List<ElympicsInt> arg2)
    {
        for(int i = 0; i < arg1.Count; i++)
        {
            Debug.Log($"TIME: {arg1[i].Value} | ");
            playerScorePanels[i].SetPlayerScoreText(arg2[i].Value, arg1[i].Value);
            ShowScoreboard(arg2[i].Value);

        }
    }

    private void OnDisable()
    {
        //finishLine.onPlayerFinished -= HandleScore;
        playersProvider.IsReadyChanged -= GetPlayerCount;
    }

    private void GetPlayerCount()
    {
        playerCount = playersProvider.AllPlayers.Length;
    }

    private void HandleScore(int playerID, ElympicsFloat time)
    {
            ShowScoreboard(playerID);
            SetPlayerScore(playerID, time);
            panelIndex.Value++;
    }

    private void SetPlayerScore(int playerID, ElympicsFloat time)
    {
        playerScorePanels[panelIndex.Value].SetPlayerScoreText(playerID, time.Value);
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
