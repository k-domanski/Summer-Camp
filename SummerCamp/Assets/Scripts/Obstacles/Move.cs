using System;
using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;
using UnityEngine.Serialization;

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

    private Vector3 currentDirection;
    private bool toOrigin;

    private void Start()
    {
        MoveToDestination();
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
