using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System;

public class DeathController : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField] private float deathTime = 0.0f;

    public ElympicsBool IsDead { get; private set; } = new ElympicsBool();
    public ElympicsFloat CurrentDeathTime { get; private set; } = new ElympicsFloat(0.0f);

    public event Action<int> onPlayerDeath = null;

    public void ElympicsUpdate()
    {
        if (!IsDead.Value || !Elympics.IsServer)
            return;

        CurrentDeathTime.Value -= Elympics.TickDuration;

        if(CurrentDeathTime.Value < 0)
        {
            RespawnPlayer();
        }
    }

    public void ProcessPlayerDeath()
    {
        CurrentDeathTime.Value = deathTime;
        IsDead.Value = true;
        onPlayerDeath?.Invoke((int)PredictableFor);
    }

    private void RespawnPlayer()
    {
        //TODO
    }
}
