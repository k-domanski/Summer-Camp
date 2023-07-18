using System.Globalization;
using UnityEngine;
using Elympics;
using TMPro;

public class RaceTimer : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private ServerHandler serverHandler;
    
    public ElympicsFloat CurrentTime { get; } = new ElympicsFloat(0.0f);
    
    private bool isRunning;

    #region IUpdatable
    public void ElympicsUpdate()
    {
        if(Elympics.IsServer && isRunning)
        {
            CurrentTime.Value += Elympics.TickDuration;
        }
    }
    #endregion

    private void Awake()
    {
        serverHandler.AllPlayersConnected += StartTimer;
    }

    private void OnDestroy()
    {
        serverHandler.AllPlayersConnected -= StartTimer;
    }

    private void FixedUpdate()
    {
        timerText.text = CurrentTime.Value.ToString(CultureInfo.InvariantCulture);
    }

    public void StartTimer()
    {
        CurrentTime.Value = 0;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }    
}
