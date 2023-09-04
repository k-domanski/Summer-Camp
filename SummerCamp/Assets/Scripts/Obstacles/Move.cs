using System;
using Elympics;
using UnityEngine;

public class Move : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private Transform destination;
    [SerializeField]
    private float reachMargin = 0.05f;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private PlayersProvider playersProvider;

    private Vector3 startPos;
    private Vector3 currentDirection;
    private bool toOrigin;
    
    private void Start()
    {
        startPos = origin.position;
        ServerRegisterToPlayersDeath();
        gameManager.RaceStarted += MoveToDestination;
    }

    private void ServerRegisterToPlayersDeath()
    {
        if (Elympics.IsServer == false)
        {
            return;
        }
        foreach (PlayerData playerData in playersProvider.AllPlayers)
        {
            if (playerData.TryGetComponent<DeathController>(out DeathController fetchedComponent))
            {
                fetchedComponent.onPlayerDeath += ResetObstacles;
            }
        }
    }

    private void ResetObstacles(int obj)
    {
        this.transform.position = startPos;
    }

    private void OnDestroy()
    {
        gameManager.RaceStarted -= MoveToDestination;
    }

    public void ElympicsUpdate()
    {
        this.transform.position += currentDirection * (Elympics.TickDuration * moveSpeed);
        CheckReached();
    }

    private void CheckReached()
    {
        if (toOrigin)
        {
            if (CheckpointReached(origin.position))
            {
                MoveToDestination();
            }
        }
        else
        {
            if (CheckpointReached(destination.position))
            {
                MoveToOrigin();
            }
        }
    }

    private bool CheckpointReached(Vector3 checkpointPosition)
    {
        return Vector3.Distance(this.transform.position, checkpointPosition) <= reachMargin;
    }

    private void MoveToOrigin()
    {
        toOrigin = true;
        currentDirection = (origin.position - destination.position).normalized;
    }

    private void MoveToDestination()
    {
        toOrigin = false;
        currentDirection = (destination.position - origin.position).normalized;
    }
}
