using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControllerTest : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region Photon
    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView => _playerPhotonView;
    #endregion

    #region GameObjects References
    private GameObject _gameCanvas, _playerUI;
    private GameObject _handGO, _battlefieldGO, _deckGO, _tombGO;
    #endregion

    #region Game Components
    private Hand _hand;
    private Battlefield _battlefield;
    private Deck _deck;
    private Tomb _tomb;
    #endregion

    private GameObject _currentTarget;
    private bool _isMyTurn, _isPhaseDone;
    
    #region State Machine
    private delegate void State();
    private State _currentState;
    #endregion

    private void Start()
    {
        _gameCanvas = GameObject.Find("Game Canvas");
        InitializePlayer();

        _currentState = Standby;
    }

    #region Monobehavior Callbacks
    private void Update()
    {
        Debug.Log($"{_currentState}");

        if (_isMyTurn)
        {
            _currentState.Invoke();
        }
        else
        {
            // change user turn
        }
    }
    #endregion

    #region States
    private void Standby()
    {
        Debug.Log($"{name} inisiated: Standby Phase");

        // not active

        if (_isMyTurn)
        {
            _currentState = DrawPhase;
        }
    }

    private void DrawPhase()
    {
        Debug.Log($"{name} inisiated: Draw Phase");

        // draw card from deck

        if (_isPhaseDone)
        {
            _currentState = ActionPhase;
            _isPhaseDone = false;
        }
    }

    private void ActionPhase()
    {
        Debug.Log($"{name} inisiated: Action Phase");

        // use card from hand to the battlefield

        if (_isPhaseDone)
        {
            _currentState = NegatePhase;
            _isPhaseDone = false;
        }
    }

    private void NegatePhase()
    {
        Debug.Log($"{name} inisiated: Negation Phase");

        // while not my turn, if opponent made an action if I have the correct requirments, cancel that action's effect

        if (_isPhaseDone)
        {
            _currentState = ReactionPhase;
            _isPhaseDone = false;
        }
    }

    private void ReactionPhase()
    {
        Debug.Log($"{name} inisiated: Reaction Phase");

        // if not negated play action effect

        if (_isPhaseDone)
        {
            _currentState = EndPhase;
            _isPhaseDone = false;
        }
    }

    private void EndPhase()
    {
        Debug.Log($"{name} inisiated: End Phase");

        if (_isMyTurn)
        {
            _isMyTurn = false;
            _isPhaseDone = false;
            _currentState = DrawPhase;
        }
    }
    #endregion

    #region UI Events
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerEnter = _currentTarget;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _currentTarget = null;
    }
    #endregion

    #region Methods
    private void InitializePlayer()
    {
        Player[] playersInRoom = PhotonNetwork.PlayerList;
        
        for (int i = 0; i < playersInRoom.Length; i++)
        {
            if (_playerPhotonView.IsMine)
            {
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    _playerUI = _gameCanvas.transform.GetChild(0).gameObject;
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    _playerUI = _gameCanvas.transform.GetChild(1).gameObject;
                }

                _handGO = _playerUI.transform.GetChild(0).gameObject;
                _battlefieldGO = _playerUI.transform.GetChild(1).gameObject;
                _deckGO = _playerUI.transform.GetChild(2).gameObject;
                _tombGO = _playerUI.transform.GetChild(3).gameObject;

                _hand = _handGO.GetComponent<Hand>();
                _battlefield = _battlefieldGO.GetComponent<Battlefield>();
                _deck = _deckGO.GetComponent<Deck>();
                _tomb = _tombGO.GetComponent<Tomb>();

                _currentState = Standby;
            }
            else
            {
                return;
            }

        }

        

        

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    _playerUI = _gameCanvas.transform.GetChild(0).gameObject;
        //}
        //else
        //{
        //    _playerUI = _gameCanvas.transform.GetChild(1).gameObject;
        //}

        //for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //{
        //    if (_playerPhotonView.ViewID == )
        //    {
        //
        //    }
        //}

        //Player[] playersInRoom = PhotonNetwork.PlayerList;
        //
        //for (int i = 0; i < playersInRoom.Length; i++)
        //{
        //    if (PhotonNetwork.LocalPlayer != playersInRoom[i])
        //        continue;
        //}
    }
    #endregion
}
