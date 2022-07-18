using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;
    public GameObject roomPrefab;

    public Transform roomContent;

    public TMP_InputField playerNameInputField;
    public string playerName;

    public Canvas lobbyCanvas;
    TypedLobby catufoLobby = new TypedLobby("Catufo", LobbyType.Default);

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnOpenFindRoomPress()
    {
        if (PhotonNetwork.IsConnected == false) Debug.LogError("Not connected to photonnetwork");
        if (PhotonNetwork.InLobby == false)
        {
            PhotonNetwork.JoinLobby(catufoLobby);
        }

        lobbyCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        lobbyCanvas.GetComponent<CanvasGroup>().interactable = true;
        lobbyCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnCloseFindRoomPress()
    {
        lobbyCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        lobbyCanvas.GetComponent<CanvasGroup>().interactable = false;
        lobbyCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnCreateRoomPress()
    {
        GenerateRandomRoom();
    }

    public override void OnConnectedToMaster() // callback function for when first connection is made
    {
        PhotonNetwork.JoinLobby(catufoLobby);
        //PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Connected");
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
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.CurrentRoom);
        playerName = playerNameInputField.text;
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void GenerateRandomRoom()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.LogError("Not connected to photon yet.");
            return;
        }

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {"RoomName", "InputRoomNameHere"}
        };

        string randomizedCode = RandomString(6);
        var a = PhotonNetwork.CreateRoom(randomizedCode, options, catufoLobby);

    }

    private List<RoomItem> roomItems = new List<RoomItem>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Updating room list");
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (i >= roomItems.Count)
            {
                var newRoom = Instantiate(roomPrefab, roomContent);
                var roomitem = newRoom.GetComponent<RoomItem>();
                roomItems.Add(roomitem);
                roomitem.SetRoomInfo(roomList[i]);
            }
            else
            {
                roomItems[i].gameObject.SetActive(true);
                roomItems[i].SetRoomInfo(roomList[i]);
            }

            /*if (roomList[i].CustomProperties.TryGetValue("RoomType", out var roomType))
            {
                if(roomType.ToString() == "Normal Game")
                {
                    
                }
            }*/
        }
    }

    private Random random = new Random();
    public string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public void RefreshRoomList()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinLobby(catufoLobby);
    }

    [Button]
    private void DebugRoom()
    {
        //Debug.Log(PhotonNetwork.IsConnected);
        Debug.Log(PhotonNetwork.CurrentRoom);
    }
}
