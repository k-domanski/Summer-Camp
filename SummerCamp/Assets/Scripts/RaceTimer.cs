using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class RaceTimer : ElympicsMonoBehaviour, IUpdatable
{
    public ElympicsFloat CurrentTime { get; } = new ElympicsFloat(0.0f);

    private ElympicsBool isRunning = new ElympicsBool(false);

    #region IUpdatable
    public void ElympicsUpdate()
    {
        if(isRunning.Value)
        {
            CurrentTime.Value += Elympics.TickDuration;
        }
    }
    #endregion

    public void StartTimer()
    {
        CurrentTime.Value = 0;
        isRunning.Value = true;
    }

    public void StopTimer()
    {
        isRunning.Value = false;
    }    
}
