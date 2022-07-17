using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;

    public Transform roomContent;

    public TMP_InputField playerName;
    public Canvas lobbyCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnOpenFindRoomPress()
    {
        if (PhotonNetwork.IsConnected == false) Debug.LogError("Not connected to photonnetwork");
        // Show roomlisting
        lobbyCanvas.enabled = true;

    }
    public void OnCloseFindRoomPress()
    {
        // Show roomlisting
        lobbyCanvas.enabled = false;
    }
    public void OnCreateRoomPress()
    {
        GenerateRandomRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        if (returnCode == 32766) // duplicate room found
        {
            GenerateRandomRoom();
        }
        else
        {
            Debug.LogError("Couldnt create room");
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        PhotonNetwork.LoadLevel("GameScene");
    }

    private void GenerateRandomRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        string randomizedCode = RandomString(6);
        var a = PhotonNetwork.CreateRoom(randomizedCode, options, TypedLobby.Default);
    }

    private Random random = new Random();
    public string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private List<RoomItem> roomItems = new List<RoomItem>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (i > roomItems.Count)
            {
                var newRoom = Instantiate(roomPrefab, roomContent);
                var roomitem = newRoom.GetComponent<RoomItem>();
                roomItems.Add(roomitem);
                roomitem.SetRoomInfo(roomList[i]);
            }
            else
            {
                roomItems[i].SetRoomInfo(roomList[i]);
            }
        }
    }
}
