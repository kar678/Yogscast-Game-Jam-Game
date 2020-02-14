using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int PresentsToCollect;
    public int PresentsCollected;

    public PlayerData(Player player)
    {
        PresentsToCollect = player.PresentsToCollect;
        PresentsCollected = player.PresentsCollected;
    }
}
