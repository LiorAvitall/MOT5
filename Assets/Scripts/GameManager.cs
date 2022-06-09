using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

enum AspectOfElement { Light = 0, Death = 1, Destruction = 3, Life = 4, Control = 5 }

public class GameManager : MonoBehaviour
{
    #region Photon
    [SerializeField] PhotonView _photonView;
    #endregion

    [SerializeField] private GameObject _playerUI;
    [SerializeField] private Transform _gameCanvas;

    private void Start()
    {
        Duel();
    }

    private void Duel()
    {
        if (_photonView.IsMine)
        {

            GameObject playerBoard = Instantiate(_playerUI, _gameCanvas);
        }
        else
        {
            GameObject playerBoard = Instantiate(_playerUI, _gameCanvas);
            playerBoard.transform.rotation = new Quaternion(_playerUI.transform.rotation.x, _playerUI.transform.rotation.y, _playerUI.transform.rotation.z + 180, _playerUI.transform.rotation.w);
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
