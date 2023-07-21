using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System;
using System.Linq;

public class PlayersProvider : ElympicsMonoBehaviour, IInitializable
{
    public PlayerData ClientPlayer { get; private set; }
    public PlayerData[] AllPlayers { get; private set; }
    public bool IsReady { get; private set; }

    public event Action IsReadyChanged;

    public void Initialize()
    {
        FindAllPlayers();
        FindClientPlayer();
        IsReady = true;
        IsReadyChanged?.Invoke();
    }

    public PlayerData FindPlayerByID(int id)
    {
        return AllPlayers.FirstOrDefault(x => x.PlayerID == id);
    }


    private void FindAllPlayers()
    {
        AllPlayers = FindObjectsOfType<PlayerData>().OrderBy(x => x.PlayerID).ToArray();
    }

    private void FindClientPlayer()
    {
        foreach (PlayerData player in AllPlayers)
        {
            if((int)Elympics.Player == player.PlayerID)
            {
                ClientPlayer = player;
                return;
            }
        }

        //TODO: Disable HUD for server
        ClientPlayer = AllPlayers[0];
    }

    
}
