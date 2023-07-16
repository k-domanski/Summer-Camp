using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class CameraInitializer : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private Camera serverCamera = null;

    public void Initialize()
    {
        PlayerData playerData = GetComponent<PlayerData>();

        InitilizeCameras(playerData.PlayerID);
    }

    private void InitilizeCameras(int playerID)
    {
        Camera[] camerasInChildren = GetComponentsInChildren<Camera>();

        bool enableCamera = false;

        if(Elympics.IsClient)
        {
            enableCamera = (int)Elympics.Player == playerID;
        }
        else if(Elympics.IsServer)
        {
            enableCamera = playerID == 0;
        }

        foreach (Camera camera in camerasInChildren)
        {
            camera.enabled = enableCamera;

            // ??? from tutorial
            if (camera.TryGetComponent<AudioListener>(out var audioListener))
            {
                Destroy(audioListener);
            }
        }

    }
}
