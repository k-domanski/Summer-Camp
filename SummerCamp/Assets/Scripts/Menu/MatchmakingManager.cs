using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.UI;
using System;

public class MatchmakingManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(PlayOnline);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(PlayOnline);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    public void PlayOnline()
    {
        ElympicsLobbyClient.Instance.PlayOnlineInRegion(null, null, null, "Default");
        ControlPlayButton(false);
        ControlQuitButton(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlPlayButton(bool canPlay)
    {
        startButton.interactable = canPlay;
    }

    public void ControlQuitButton(bool canQuit)
    {
        quitButton.interactable = canQuit;
    }
}
