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
    [SerializeField]
    private GameObject[] obstaclesRoot;
    
    private Vector3 startPos;
    private Vector3 currentDirection;
    private GameObject currentObstacle;
    
    private void Start()
    {
        if (Elympics.IsClient)
        {
            return;
        }
        startPos = origin.position;
        ServerRegisterToPlayersDeath();
        
        currentDirection = (destination.position - origin.position).normalized;
        
        foreach (var obstacleRoot in obstaclesRoot)
        {
            obstacleRoot.SetActive(false);
        }
        
        gameManager.RaceStarted += ResetObstacles;
    }

    private void ServerRegisterToPlayersDeath()
    {
        if (Elympics.IsServer == false)
        {
            return;
        }

        playersProvider.PlayerDied += ResetObstacles;
    }

    private void ResetObstacles()
    {
        if (currentObstacle != null)
        {
            currentObstacle.SetActive(false);
        }

        currentObstacle = GetRandomObstacle();
        currentObstacle.SetActive(true);
        
        this.transform.position = startPos;
    }

    private GameObject GetRandomObstacle()
    {
        int randomIndex = UnityEngine.Random.Range(0, obstaclesRoot.Length);
        return obstaclesRoot[randomIndex];
    }

    private void OnDestroy()
    {
        gameManager.RaceStarted -= ResetObstacles;
    }

    public void ElympicsUpdate()
    {
        if (gameManager.IsRunning.Value)
        {
            this.transform.position += currentDirection * (Elympics.TickDuration * moveSpeed);
        }

        if (Elympics.IsServer && CheckReached())
        {
            ResetObstacles();
        }
    }

    private bool CheckReached()
    {
        return CheckpointReached(destination.position);
    }

    private bool CheckpointReached(Vector3 checkpointPosition)
    {
        return Vector3.Distance(this.transform.position, checkpointPosition) <= reachMargin;
    }
}
