using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI playerInfo;
    [SerializeField] private int mainMenuSceneIndex = 0;

    private void Awake()
    {
        canvasGroup.alpha = 0.0f;
    }

    public void ShowGameOverScreen(PlayerData playerData)
    {
        canvasGroup.alpha = 1.0f;
        playerInfo.text = $"Player_{playerData.PlayerID} won the game!";
    }

    public void OnBackToMenuButtonPressed()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
