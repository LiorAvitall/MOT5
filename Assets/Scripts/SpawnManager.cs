using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;/*, _playerBoard, _eventHandler;
    [SerializeField] private Transform _gameCanvasTransform;

    private GameObject _gameCanvas;
    //public float _minX, _minZ, _maxX, _maxZ;
    */
    void Start()
    {
        //_gameCanvas = GameObject.Find(_gameCanvasTransform.name);

        InitializeDuel();
        //Vector3 randomPos = new Vector3(Random.Range(_minX, _maxX), 0.5f, Random.Range(_minZ, _maxZ));
        //PhotonNetwork.Instantiate(_playerPrefab.name, randomPos, Quaternion.identity);
        //PhotonNetwork.InstantiateRoomObject(_patientPrefab.name, _patientSpawner.position, _patientPrefab.transform.rotation);
    }

    private void InitializeDuel()
    {
        GameObject myPlayer = PhotonNetwork.Instantiate(_player.name, Vector3.zero, Quaternion.identity);
        PlayerController playerController = myPlayer.GetComponent<PlayerController>();
        myPlayer.name = playerController.playerPhotonView.Owner.NickName;
        DontDestroyOnLoad(myPlayer);

        
        //_gameCanvas.transform.position = Vector2.zero;
        //
        //GameObject player1EventHandlerGO = PhotonNetwork.Instantiate(_eventHandler.name, Vector3.zero, Quaternion.identity);
        //player1EventHandlerGO.name = $"{PhotonNetwork.LocalPlayer.NickName} Event Handler";
        //
        //GameObject player1Board = PhotonNetwork.Instantiate(_playerBoard.name, Vector3.zero, Quaternion.identity);
        //player1Board.name = $"{PhotonNetwork.LocalPlayer.NickName} Board";
        //
        //player1Board.transform.parent = _gameCanvas.transform;
        //
        //DontDestroyOnLoad(_gameCanvas);
        //
        //GameObject player2Board = PhotonNetwork.Instantiate("Player Board", Vector3.zero, Quaternion.identity);
        //player2Board.name = "Player2 Board";
        //player2Board.transform.parent = _gameCanvas;
        //player2Board.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
    }
}
