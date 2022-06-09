using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

enum AspectOfElement { Light = 0, Death = 1, Destruction = 3, Life = 4, Control = 5 }

public class GameManager : MonoBehaviour
{
    #region Photon
    [SerializeField] private PhotonView _photonView;
    #endregion

    [SerializeField] private GameObject _playerBoard;
    [SerializeField] private Transform _gameCanvas;

    private void Start()
    {
        Duel();
    }

    private void Duel()
    {
        InitializeDuel();
    }

    private void InitializeDuel()
    {
        if (_photonView.IsMine)
        {
            GameObject player1Board = Instantiate(_playerBoard, _gameCanvas);
            player1Board.name = "Player1 Board";

            GameObject player2Board = Instantiate(_playerBoard, _gameCanvas);
            player2Board.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);

            player2Board.name = "Player2 Board";
        }
        else
        {
            GameObject player1Board = Instantiate(_playerBoard, _gameCanvas);
            player1Board.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
            player1Board.name = "Player1 Board";

            GameObject player2Board = Instantiate(_playerBoard, _gameCanvas);
            player2Board.name = "Player2 Board";
        }
    }

    private void Brawl()
    {
        // Run [1 vs 1 vs 1] & [1 vs 1 vs 1 vs 1] envirnoments & rules
    }

    private void TeamFight()
    {
        // Run [2 vs 2] envirnoment & rules
    }
}
