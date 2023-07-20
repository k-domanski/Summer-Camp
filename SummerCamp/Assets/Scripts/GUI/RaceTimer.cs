using UnityEngine;
using Elympics;
using TMPro;

public class RaceTimer : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField]
    private TextMeshProUGUI raceTimer;
    [SerializeField]
    private TextMeshProUGUI countdownTimer;
    [SerializeField]
    private ServerHandler serverHandler;
    
    public ElympicsFloat CurrentTime { get; } = new ElympicsFloat(0.0f);
    public ElympicsBool IsRunning { get; } = new ElympicsBool();
    public ElympicsBool IsStarting { get; } = new ElympicsBool();
    
    #region IUpdatable
    public void ElympicsUpdate()
    {
        if(Elympics.IsServer)
        {
            if (IsStarting)
            {
                CurrentTime.Value -= Elympics.TickDuration;
                if (CurrentTime.Value <= 0)
                {
                    StartTimer();
                    IsStarting.Value = false;
                }
            }
            else if (IsRunning)
            {
                CurrentTime.Value += Elympics.TickDuration;
            }
        }

        if (Elympics.IsClient)
        {
            SetTimers();
        }
    }
    #endregion

    private void Awake()
    {
        serverHandler.AllPlayersConnected += StartCountdown;
        raceTimer.gameObject.SetActive(false);
        
        if (Elympics.IsClient)
        {
            IsStarting.ValueChanged += OnIsStartingValueChanged;
            IsRunning.ValueChanged += OnIsRunningValueChanged;
        }
    }

    private void OnDestroy()
    {
        serverHandler.AllPlayersConnected -= StartTimer;
    }

    private void OnIsRunningValueChanged(bool lastvalue, bool newvalue)
    {
        raceTimer.gameObject.SetActive(newvalue);
    }

    private void OnIsStartingValueChanged(bool lastvalue, bool newvalue)
    {
        countdownTimer.gameObject.SetActive(newvalue);
    }
    
    public void StartCountdown()
    {
        IsStarting.Value = true;
        CurrentTime.Value = 5;
    }
    
    public void StartTimer()
    {
        serverHandler.StartRace();
        CurrentTime.Value = 0;
        IsRunning.Value = true;
    }

    public void StopTimer()
    {
        IsRunning.Value = false;
    }

    private void SetTimers()
    {
        if (raceTimer.gameObject.activeSelf)
        {
            raceTimer.text = CurrentTime.Value.ToString("F");
        }

        if (countdownTimer.gameObject.activeSelf)
        {
            countdownTimer.text = CurrentTime.Value.ToString("0");
        }
    }
}
