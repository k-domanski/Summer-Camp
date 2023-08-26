using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private List<PlayerScorePanelUI> playerScorePanels;
    [SerializeField] private PlayerScoresManager scoresManager;
    [SerializeField] private PlayersProvider playersProvider;

    private int mainMenuSceneIndex = 0;

    private void Awake()
    {
        if(scoresManager.IsReady)
        {
            Setup();
        }
        else
        {
            scoresManager.IsReadyChanged += Setup;
        }
        HideUI();
    }

    private void OnDestroy()
    {
        scoresManager.onPlayerFinished -= UpdateScore;
    }

    public void OnBackToMenuClicked()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    private void Setup()
    {
        scoresManager.onPlayerFinished += UpdateScore;
    }

    private void HideUI()
    {
        gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.0f;
        foreach (PlayerScorePanelUI panel in playerScorePanels)
        {
            panel.ShowPanel(false);
        }
    }

    private void ShowScoreboard(int playerID)
    {
        if(playersProvider.ClientPlayer.PlayerID == playerID)
        {
            gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1.0f;
            for (int i = 0; i < playersProvider.AllPlayers.Length; i++)
            {
                playerScorePanels[i].ShowPanel(true);
            }
        }
    }

    private void UpdateScore(int index, int playerID, float time)
    {
        ShowScoreboard(playerID);
        playerScorePanels[index].SetPlayerScoreText(playerID, time);
    }

    //private void ProcessPlayerFinish(ElympicsArray<ElympicsFloat> scores, int index)
    //{
    //    var temp = scores.Values.OrderBy(x => x.Value).Where(x => x.Value != 0.0f).ToArray();
    //    for(int i = 0; i < temp.Length; i++)
    //    {
    //        UpdateScore(i, 0, temp[i].Value);
    //    }
    //}
}
