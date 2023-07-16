using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int PlayerID => playerID;
    [SerializeField] private int playerID = 0;
}
