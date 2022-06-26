using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerData : MonoBehaviour
{
    private GameMode _currentGameMode;

    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    private Player[] _playersInRoom;
    public Player[] PlayersInRoom => _playersInRoom;
    #endregion

    #region Game Components
    private Hand _hand;
    private Battlefield _battlefield;
    private Deck _deck;
    private Tomb _tomb;

    public Hand Hand => _hand;
    public Battlefield Battlefield => _battlefield;
    public Deck Deck => _deck;
    public Tomb Tomb => _tomb;
    #endregion

    #region GameObjects References
    private GameObject _gameCanvas, _playerUI;
    private GameObject _handGO, _battlefieldGO, _deckGO, _tombGO;

    public GameObject GameCanvas => _gameCanvas;
    public GameObject PlayerUI => _playerUI;
    public GameObject HandGO => _handGO;
    public GameObject BattlefieldGO => _battlefieldGO;
    public GameObject DeckGO => _deckGO;
    public GameObject TombGO => _tombGO;
    #endregion

    [Header("Data Scripts")]
    public Battlefield OpponentBattlefieldData; 

    public GameObject SacrificeOverlay;
    public GameObject LastPlacedCardOnBattelfield;

    public bool IsReviving = false;
    public bool IsSacrificing = false;
    public bool IsDestroying = false;
    //public bool IsDrawingWithLight = false;

    #region Monobehavior Callbacks
    private void Start()
    {
        InitializePlayerComponents();
    }

    private void Update()
    {
        // Update PlayerList as long as not all of the expected players are logged in
        if (PhotonNetwork.PlayerList.Length < 4)
        {
            if (_currentGameMode != GameMode.Duel)
            {
                _playersInRoom = PhotonNetwork.PlayerList;
            }
            else if (PhotonNetwork.PlayerList.Length < 2)
            {
                _playersInRoom = PhotonNetwork.PlayerList;
            }
        }
    }
    #endregion

    #region Methods
    private void InitializePlayerComponents()
    {
        _gameCanvas = GameObject.Find("Game Canvas");

        for (int i = 0; i < _playersInRoom.Length; i++)
        {
            if (_photonView.IsMine)
            {
                // Set PlayerUI by ActorNumber
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    _playerUI = _gameCanvas.transform.GetChild(0).gameObject;
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    _playerUI = _gameCanvas.transform.GetChild(1).gameObject;
                }

                // Set GameObjects
                _handGO = _playerUI.transform.GetChild(0).gameObject;
                _battlefieldGO = _playerUI.transform.GetChild(1).gameObject;
                _deckGO = _playerUI.transform.GetChild(2).gameObject;
                _tombGO = _playerUI.transform.GetChild(3).gameObject;

                // Set Scripts
                _hand = _handGO.GetComponent<Hand>();
                _battlefield = _battlefieldGO.GetComponent<Battlefield>();
                _deck = _deckGO.GetComponent<Deck>();
                _tomb = _tombGO.GetComponent<Tomb>();
                
                // Set PhotonView
                _hand.PlayerPhotonView = _photonView;
                _battlefield.PlayerPhotonView = _photonView;
                _deck.PlayerPhotonView = _photonView;
                _tomb.PlayerPhotonView = _photonView;
            }

            return;
        }
    }
    #endregion
}