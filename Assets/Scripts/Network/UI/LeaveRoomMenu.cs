using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class LeaveRoomMenu : MonoBehaviour
{

    private RoomCanvases _roomCanvases;
public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _roomCanvases.CurrentRoomCanvas.Hide();
    }

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        
    }

}
