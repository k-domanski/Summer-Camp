using System.Collections.Generic;
using Elympics;
using UnityEngine;

public class ServerHandler : ElympicsMonoBehaviour, IServerHandlerGuid
{		
    private readonly HashSet<ElympicsPlayer> _playersConnected = new HashSet<ElympicsPlayer>();
    
    public void OnServerInit(InitialMatchPlayerDatasGuid initialMatchPlayerDatas)
    {
        
    }

    public void OnPlayerDisconnected(ElympicsPlayer player)
    {
    }

    public void OnPlayerConnected(ElympicsPlayer player)
    {
    }
}