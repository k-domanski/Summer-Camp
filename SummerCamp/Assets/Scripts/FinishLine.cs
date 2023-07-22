using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Elympics;

public class FinishLine : ElympicsMonoBehaviour
{
    public event Action<int> onPlayerFinished;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerData>(out var playerData))
        {
            if (Elympics.IsServer)
            {
                onPlayerFinished?.Invoke(playerData.PlayerID);
            }

            //robocze
            playerData.GetComponent<InputProvider>().DisableInput();
        }
    }
}
