using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Elympics;

public class MatchmakingLogger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchmakingStatus;
    [SerializeField] private MatchmakingMessages matchmakingMessages;

    private MatchmakingManager matchmakingManager;

    private void Awake()
    {
        matchmakingManager = GetComponent<MatchmakingManager>();
    }

    private void Start()
    {
        ElympicsLobbyClient.Instance.Matchmaker.MatchmakingStarted += DisplayMatchmakingStarted;
        ElympicsLobbyClient.Instance.Matchmaker.MatchmakingMatchFound += _ => DisplayMatchFound();
        ElympicsLobbyClient.Instance.Matchmaker.MatchmakingFailed += _ => DisplayMatchmakingFailed();
    }

    private void OnDestroy()
    {
        ElympicsLobbyClient.Instance.Matchmaker.MatchmakingStarted -= DisplayMatchmakingStarted;
        ElympicsLobbyClient.Instance.Matchmaker.MatchmakingMatchFound -= _ => DisplayMatchFound();
        ElympicsLobbyClient.Instance.Matchmaker.MatchmakingFailed -= _ => DisplayMatchmakingFailed();
    }

    private void DisplayMatchmakingStarted()
    {
        matchmakingManager.ControlPlayButton(false);
        // Disable quit button during matchmaking just in case
        matchmakingManager.ControlQuitButton(false);
        matchmakingStatus.text = matchmakingMessages.startingMessage;
    }

    private void DisplayMatchFound()
    {
        matchmakingStatus.text = matchmakingMessages.finishedMessage;
    }

    private void DisplayMatchmakingFailed()
    {
        matchmakingManager.ControlPlayButton(true);
        matchmakingManager.ControlQuitButton(true);
        matchmakingStatus.text = matchmakingMessages.errorMessage;
    }
}

[System.Serializable]
public struct MatchmakingMessages
{
    public string startingMessage;
    public string finishedMessage;
    public string errorMessage;
}