using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    public TMP_Text lobbyName;
    public TMP_Text playerCount;
    private RoomInfo roominfo;
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        if (roomInfo.CustomProperties.TryGetValue("RoomName", out var roomname))
        {
            lobbyName.text = (string)roomname;
        }

        playerCount.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
        this.roominfo = roomInfo;
    }

    public void OnButtonPress()
    {
        PhotonNetwork.JoinRoom(roominfo.Name);
    }
}
