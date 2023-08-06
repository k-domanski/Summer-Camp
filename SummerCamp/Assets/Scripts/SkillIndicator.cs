using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class SkillIndicator : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ApplyPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ShowIndicator(bool show)
    {
        gameObject.SetActive(show);
    }
}
