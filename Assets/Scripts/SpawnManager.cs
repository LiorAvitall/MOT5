using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    #region Prefab references
    [SerializeField] private GameObject _player;

    private Transform _tableTransform;
    private GameObject _gameCanvas, _playerTable, _playerBoard;
    #endregion

    private void Awake()
    {
        
    }

    void Start()
    {
        //_gameCanvas = GameObject.Find("Game Canvas");
        //_gameCanvas.transform.position = Vector2.zero;
        //Player[] playersInRoom = PhotonNetwork.PlayerList;
        //for (int i = 0; i < playersInRoom.Length; i++)
        //{
        //    if (PhotonNetwork.LocalPlayer.ActorNumber != playersInRoom[i].ActorNumber)
        //        continue;
        //
        //    return;
        //}

        GameObject player = PhotonNetwork.Instantiate(_player.name, Vector3.zero, Quaternion.identity);
        player.name = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
    }

    private void InitializePlayer()
    {
        Player[] playersInRoom = PhotonNetwork.PlayerList;

        for (int i = 0; i < playersInRoom.Length; i++)
        {
            if (PhotonNetwork.LocalPlayer != playersInRoom[i])
                continue;

            _playerTable = PhotonNetwork.Instantiate("Player Table", Vector3.zero, Quaternion.identity);
            _playerTable.name = $"Player {PhotonNetwork.LocalPlayer.ActorNumber} Table";

            _playerBoard = PhotonNetwork.Instantiate("Player Board", Vector3.zero, Quaternion.identity);
            _playerBoard.name = $"Player {PhotonNetwork.LocalPlayer.ActorNumber} Board";
            _playerBoard.transform.SetParent(_gameCanvas.transform);

            switch (playersInRoom[i].ActorNumber)
            {
                case 1:
                    _tableTransform = GameObject.Find("Player 1 Table Position").transform;
                    break;

                case 2:
                    _tableTransform = GameObject.Find("Player 2 Table Position").transform;
                    _playerTable.transform.rotation = new Quaternion(0f, 0f + 180f, _playerTable.transform.rotation.z, 0f);

                    break;

                default:

                    break;
            }

            _playerTable.transform.position = _tableTransform.position;
            _playerTable.transform.SetParent(_tableTransform);
        }
    }
}
