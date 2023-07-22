using System;
using Elympics;
using UnityEngine;

public class GameManager : ElympicsMonoBehaviour, IUpdatable
{
    public event Action RaceStarted;

    [SerializeField]
    private ServerHandler serverHandler;
    [SerializeField]
    private GameObject[] disableAtGameStart;
    
    public ElympicsFloat CurrentTime { get; } = new ElympicsFloat(0.0f);
    public ElympicsBool IsRunning { get; } = new ElympicsBool();
    public ElympicsBool IsStarting { get; } = new ElympicsBool();
    
    private void Awake()
    {
        serverHandler.AllPlayersConnected += StartCountdown;
        IsRunning.ValueChanged += OnIsRunningChanged;
    }

    private void OnDestroy()
    {
        serverHandler.AllPlayersConnected -= StartCountdown;
        IsRunning.ValueChanged -= OnIsRunningChanged;
    }

    private void OnIsRunningChanged(bool lastvalue, bool newvalue)
    { 
        if(newvalue)
        {
            foreach (var gO in disableAtGameStart)
            {
                gO.SetActive(false);
            }
        }
    }

    public void StartCountdown()
    {
        IsStarting.Value = true;
        CurrentTime.Value = 5;
    }

    public void ElympicsUpdate()
    {
        if(Elympics.IsServer)
        {
            if (IsStarting.Value)
            {
                CurrentTime.Value -= Elympics.TickDuration;
                if (CurrentTime.Value <= 0)
                {
                    StartRace();
                }
            }
            else if (IsRunning.Value)
            {
                CurrentTime.Value += Elympics.TickDuration;
            }
        }
    }
        
    public void StartRace()
    {
        IsRunning.Value = true;
        IsStarting.Value = false;
        CurrentTime.Value = 0;

        RaceStarted?.Invoke();
    }
}
