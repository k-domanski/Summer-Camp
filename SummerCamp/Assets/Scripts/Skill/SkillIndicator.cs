using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.UI;

public class SkillIndicator : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color color;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ApplyPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ApplyRotation(Vector3 position)
    {
        transform.LookAt(position);
    }

    public void ApplyRange(float scale)
    {
        Vector3 imageScale = GetComponentInChildren<Image>().rectTransform.localScale;
        imageScale.y = scale;
        GetComponentInChildren<Image>().rectTransform.localScale = imageScale;
    }

    public void ShowIndicator(bool show)
    {
        gameObject.SetActive(show);
    }

    public void ChangeColor(bool change)
    {
        image.color = change ? color : Color.red;
    }
}
