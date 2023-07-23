using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.UI;

public class MatchmakingManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(PlayOnline);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(PlayOnline);
    }

    public void PlayOnline()
    {
        ElympicsLobbyClient.Instance.PlayOnlineInRegion(null, null, null, "Default");
        startButton.interactable = false;
    }
}
