using System.Collections.Generic;
using Elympics;
using System;

public class ServerHandler : ElympicsMonoBehaviour, IServerHandlerGuid
{
    public event Action AllPlayersConnected;
    public event Action RaceStarted;
    
    private readonly HashSet<ElympicsPlayer> playersConnected = new HashSet<ElympicsPlayer>();
    
    private int playersNeededToStart;
    private bool gameRunning;
    public void OnServerInit(InitialMatchPlayerDatasGuid initialMatchPlayerDatas)
    {
        playersNeededToStart = initialMatchPlayerDatas.Count;
    }

    public void OnPlayerDisconnected(ElympicsPlayer player)
    {
        Elympics.EndGame();
    }

    public void OnPlayerConnected(ElympicsPlayer player)
    {
        playersConnected.Add(player);
        if (playersConnected.Count != playersNeededToStart || gameRunning)
            return;

        gameRunning = true;
        AllPlayersConnected?.Invoke();
    }

    public void StartRace()
    {
        RaceStarted?.Invoke();
    }
}