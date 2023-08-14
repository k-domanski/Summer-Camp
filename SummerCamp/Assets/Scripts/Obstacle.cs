using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Obstacle : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField] private float speed = 5.0f;

    public void ElympicsUpdate()
    {
        transform.Rotate(Vector3.up, Elympics.TickDuration * speed);
    }
}
