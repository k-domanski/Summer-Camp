using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Elympics;

public class FinishLine : ElympicsMonoBehaviour
{
    public ElympicsFloat p1Time = new ElympicsFloat();
    public ElympicsFloat p2Time = new ElympicsFloat();


    [SerializeField] private PlayersProvider playersProvider;
    [SerializeField] private RaceTimer timer;

    public event Action<int> onPlayerFinished;
    public event Action<List<ElympicsFloat>, List<ElympicsInt>> onScoreUpdated;

    public ElympicsBool scoreUpdate = new ElympicsBool();

    private static List<ElympicsFloat> playersTime = new List<ElympicsFloat>();
    private static List<ElympicsInt> playersID = new List<ElympicsInt>();

    private bool reachedFinish;
    private void Start() => scoreUpdate.ValueChanged += HandleScoreUpdate;

    private void HandleScoreUpdate(bool lastValue, bool newValue)
    {
        //onScoreUpdated?.Invoke(playersTime, playersID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Elympics.IsServer)
            return;

        if(other.TryGetComponent<PlayerData>(out var playerData))
        {
            if (!reachedFinish)
            {
                p1Time.Value = timer.CurrentTime.Value;

                reachedFinish = playersProvider.ClientPlayer.PlayerID == playerData.PlayerID;
                //playersTime.Add(new ElympicsFloat(timer.CurrentTime.Value));
                //onPlayerFinished?.Invoke(playerData.PlayerID, new ElympicsFloat(timer.CurrentTime.Value));
                scoreUpdate.Value = true;

            }
            onPlayerFinished?.Invoke(playerData.PlayerID);

            //robocze
            playerData.GetComponent<InputProvider>().DisableInput();
        }
    }
}
