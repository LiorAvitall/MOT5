using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;
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

    public Hand Hand => _hand;
    public Battlefield Battlefield => _battlefield;
    public Deck Deck => _deck;
    public Tomb Tomb => _tomb;
    #endregion

    private Button _endPhaseBtn;

    #region Indicators
    private GameObject _currentTarget;
    private bool _isMyTurn, _isPhaseDone, _isNegating, _isOnStandby, _isOnDraw, _isOnAction, _isOnNegate, _isOnReaction, _isOnEnd, _tryAction;
    public bool IsMyTurn { get => _isMyTurn; set => _isMyTurn = value; }
    public bool IsNegating => _isNegating;
    #endregion

    #region State Machine
    private delegate void State();
    private State _currentState;
    #endregion

    #region Monobehavior Callbacks
    private void Start()
    {
        _gameCanvas = GameObject.Find("Game Canvas");
        InitializePlayer();

        _currentState = StandbyPhase;
        GameManager.Instance.PlayerList.Add(this);
    }

    private void Update()
    {
        Debug.Log($"Turn Start: {name}");
        _currentState.Invoke();

        // if (_opponePlayerController.TryAction)
        // {
        //      _isNegating = true;
        //      _currentState = NegationPhase;
        // }
    }
    #endregion

    #region States
    private void StandbyPhase()
    {
        Debug.Log($"{name} inisiated: Standby Phase");

        // not active
        _isOnEnd = false;
        _isOnStandby = true;

        if (_isMyTurn)
        {
            _currentState = DrawPhase;
        }
    }

    private void DrawPhase()
    {
        Debug.Log($"{name} inisiated: Draw Phase");

        _isOnStandby = false;
        _isOnDraw = true;

        _deck.PlayerPhotonView.RPC("DrawCard", RpcTarget.All);
        _currentState = ActionPhase;
    }

    private void ActionPhase()
    {
        Debug.Log($"{name} inisiated: Action Phase");

        _isOnDraw = false;
        _isOnAction = true;
        // use card from hand to the battlefield

        // if (PlacedCardOnBattlefield())
        // {
        //     _tryAction = true;
        // }

        if (_isPhaseDone)
        {
            _isPhaseDone = false;
            _currentState = NegatePhase;
        }
    }

    private void NegatePhase()
    {
        Debug.Log($"{name} inisiated: Negation Phase");

        if (_isNegating)
        {
            // if (requirments Met)
            // {
            //      negation phase action
            //      _currentState = NegatePhase;
            // }
            // else
            // {
            //      _isNegating = false;
            //      _currentState = NegatePhase;
            // }
        }
        else
        {
            if (_isMyTurn)
            {
                _currentState = ReactionPhase;
            }
            else
            {
                _currentState = StandbyPhase;
            }

        }
    }

    private void ReactionPhase()
    {
        Debug.Log($"{name} inisiated: Reaction Phase");

        _isOnAction = false;
        _isOnReaction = true;
        // if not negated play action effect

        _currentState = EndPhase;
    }

    private void EndPhase()
    {
        Debug.Log($"{name} inisiated: End Phase");

        _isOnReaction = false;
        _isOnEnd = true;

        _isMyTurn = false;
        _isPhaseDone = false;
        _currentState = StandbyPhase;
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
            if (_photonView.IsMine)
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
                _endPhaseBtn = _playerUI.transform.GetChild(4).GetComponent<Button>();

                _hand.PlayerPhotonView = _photonView;
                _battlefield.PlayerPhotonView = _photonView;
                _deck.PlayerPhotonView = _photonView;
                _tomb.PlayerPhotonView = _photonView;
                _endPhaseBtn.onClick.AddListener(ChangePhase);

                _currentState = StandbyPhase;
            }

            return;
        }
    }

    public void ChangePhase()
    {
        if (!_isPhaseDone)
        {
            _isPhaseDone = true;
        }

        Debug.Log("Changed Phase");
    }
    #endregion
}
