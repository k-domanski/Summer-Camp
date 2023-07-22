using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System;

public class PlayerScoresManager : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private PlayersProvider playersProvider;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FinishLine finishLine;

    public bool IsReady { get; private set; } = false;

    public event Action<int, int, float> onPlayerFinished;
    public event Action IsReadyChanged = null;

    private ElympicsArray<ElympicsFloat> playerScores;
    private ElympicsInt playerIndex = new ElympicsInt(0);
    private ElympicsInt playerID = new ElympicsInt(-1);

    public void Initialize()
    {
        if(playersProvider.IsReady)
        {
            SetupManager();
        }
        else
        {
            playersProvider.IsReadyChanged += SetupManager;
        }
    }

    private void OnDestroy()
    {
        finishLine.onPlayerFinished -= ProcessPlayerFinish;
    }

    public ElympicsFloat GetScoreForPlayer(int playerID)
    {
        return playerScores.Values[playerID];
    }

    private void SetupManager()
    {
        PreparePlayerScores();

        finishLine.onPlayerFinished += ProcessPlayerFinish;
        playerID.ValueChanged += OnPlayerFinish;

        IsReady = true;
        IsReadyChanged?.Invoke();
    }

    private void OnPlayerFinish(int lastValue, int newValue)
    {
        onPlayerFinished?.Invoke(playerIndex.Value, newValue, playerScores.Values[newValue].Value);
        playerIndex.Value++;
    }

    private void PreparePlayerScores()
    {
        int numberOfPlayers = playersProvider.AllPlayers.Length;

        ElympicsFloat[] localArray = new ElympicsFloat[numberOfPlayers];

        for(int i = 0; i < numberOfPlayers; i++)
        {
            localArray[i] = new ElympicsFloat(0.0f);
        }

        playerScores = new ElympicsArray<ElympicsFloat>(localArray);
    }

    private void ProcessPlayerFinish(int playerID)
    {
        playerScores.Values[playerID].Value = gameManager.CurrentTime.Value;
        this.playerID.Value = playerID;
    }
}
