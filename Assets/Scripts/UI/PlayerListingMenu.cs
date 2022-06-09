using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _playerListing;

    private RoomCanvases _roomCanvases;

    private List<PlayerListing> _listing = new List<PlayerListing>();

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        
    }

    public override void OnLeftRoom()
    {
        _content.DestroyChildren();
    }
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach (KeyValuePair<int,Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerinfo.Value);
        }
        
    }

    private void AddPlayerListing(Player player)
    {

        PlayerListing listing = Instantiate(_playerListing, _content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listing.Add(listing);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listing.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listing[index].gameObject);
            _listing.RemoveAt(index);
        }
    }


    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }
}
