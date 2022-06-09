using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{

    [SerializeField]
    private CreateRoomMenu _createRoomMenu;
    [SerializeField]
    private RoomListingsMenu _roomListingsMenu;
   
    private RoomCanvases _canvases;

 

    public void FirstInitialize(RoomCanvases canvases)
    {
        _canvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
        _roomListingsMenu.FirstInitialize(canvases);
    }
}
