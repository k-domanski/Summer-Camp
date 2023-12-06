using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System;

public class ScoreManager : ElympicsMonoBehaviour, IInitializable
{
    [Header("Parameters")]
    [SerializeField] private int pointsToWin = 0;
    
    [Header("References")]
    [SerializeField] PlayersProvider playersProvider;

    public ElympicsInt WinnerID { get; private set; } = new ElympicsInt(-1);

    public bool IsReady { get; private set; } = false;
    public event Action IsReadyChanged;

    private ElympicsArray<ElympicsInt> playerScores = null;
    private ElympicsBool gameOver = new ElympicsBool();

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

    public ElympicsInt GetPlayerScore(int playerID)
    {
        return playerScores.Values[playerID];
    }

    private void SetupManager()
    {
        PreparePlayersScores();
        SubscribeToDeathControllers();

        IsReady = true;
        IsReadyChanged?.Invoke();
    }

    private void PreparePlayersScores()
    {
        int numberOfPlayers = playersProvider.AllPlayers.Length;

        ElympicsInt[] temporary = new ElympicsInt[numberOfPlayers];

        for (int i = 0; i < numberOfPlayers; i++)
        {
            temporary[i] = new ElympicsInt(0);
        }

        playerScores = new ElympicsArray<ElympicsInt>(temporary);
    }

    private void SubscribeToDeathControllers()
    {
        foreach(PlayerData playerData in playersProvider.AllPlayers)
        {
            if(playerData.TryGetComponent<DeathController>(out var deathController))
            {
                deathController.onPlayerDeath += ProcessPlayerDeath;
            }
        }
    }

    private void ProcessPlayerDeath(int playerID)
    {
        if (gameOver.Value)
            return;
        //Dirty way to score for 2 players
        int index = playerID == 1 ? 0 : 1;

        playerScores.Values[index].Value++;

        if(Elympics.IsServer && playerScores.Values[index].Value >= pointsToWin)
        {
            //Debug.Log($"Player ID: {index}, Points: {playerScores.Values[index].Value}, Points to win: {pointsToWin}");
            WinnerID.Value = index;
            gameOver.Value = true;
            //Change Game State?
            //Disable input, disable spawners, disable obstacles
            //Or something connected to WinnerID value changed event
        }
    }
}
