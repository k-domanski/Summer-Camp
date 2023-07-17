using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishLine : MonoBehaviour
{
    public event Action<int> onPlayerFinished;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerData>(out var playerData))
        {
            onPlayerFinished?.Invoke(playerData.PlayerID);
        }
    }
}
