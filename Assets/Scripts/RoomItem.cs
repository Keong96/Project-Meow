using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    public TMP_Text lobbyName;
    public TMP_Text playerCount;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        lobbyName.text = roomInfo.CustomProperties["RoomName"].ToString();
        playerCount.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }
}
