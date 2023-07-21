using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Elympics;

public class FinishLine : ElympicsMonoBehaviour
{
    [SerializeField] PlayersProvider playersProvider;
    public event Action<int> onPlayerFinished;
    
    private bool reachedFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (Elympics.IsServer)
            return;

        if(other.TryGetComponent<PlayerData>(out var playerData))
        {
            if (!reachedFinish)
            {
                reachedFinish = playersProvider.ClientPlayer.PlayerID == playerData.PlayerID;
                onPlayerFinished?.Invoke(playerData.PlayerID);
            }

            //robocze
            playerData.GetComponent<InputProvider>().DisableInput();
        }
    }
}
