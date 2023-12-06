using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HUDController : MonoBehaviour
{
    public event Action<bool> onShowScoreboardValueChanged;

    public void ProcessHUDActions(bool showScoreBoard)
    {
        onShowScoreboardValueChanged?.Invoke(showScoreBoard);
    }
}
