using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Rotate : ElympicsMonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Vector3 rotationAxis;

    public void Update()
    {
        transform.Rotate(rotationAxis, Time.deltaTime * speed);
    }
}
