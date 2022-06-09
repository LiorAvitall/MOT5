using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    private RoomCanvases _canvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        _canvases = canvases;
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
     
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully");
        _canvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed");
    }
}
