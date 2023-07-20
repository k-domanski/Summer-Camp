using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private List<PlayerScorePanelUI> playerScorePanels;
    [SerializeField] private FinishLine finishLine;
    [SerializeField] private PlayersProvider playersProvider;


    private void OnEnable()
    {
        finishLine.onPlayerFinished += ShowScoreboard;
    }

    private void OnDisable()
    {
        finishLine.onPlayerFinished -= ShowScoreboard;
    }

    private void ShowScoreboard(int obj)
    {
        if(playersProvider.ClientPlayer.PlayerID == obj)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
