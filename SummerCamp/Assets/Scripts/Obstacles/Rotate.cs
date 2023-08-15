using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Vector3 rotationAxis;

    private void FixedUpdate()
    {
        transform.Rotate(rotationAxis, Time.deltaTime * speed);

    }
}
