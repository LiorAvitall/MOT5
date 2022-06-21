using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControllerTest : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView => _playerPhotonView;
    #endregion

    

    #region GameObjects References
    private GameObject _gameCanvas, _playerUI;
    private GameObject _handGO, _battlefieldGO, _deckGO, _tombGO;
    #endregion

    #region Game Components
    private NewHand _hand;
    private NewBattlefield _battlefield;
    private NewDeck _deck;
    private NewTomb _tomb;

    public NewHand Hand => _hand;
    public NewBattlefield Battlefield => _battlefield;
    public NewDeck Deck => _deck;
    public NewTomb Tomb => _tomb;
    #endregion

    private Button _endPhaseBtn;

    #region Indicators
    private GameObject _currentTarget;
    private bool _isMyTurn, _isPhaseDone;
    public bool IsMyTurn { get => _isMyTurn; set => _isMyTurn = value; }
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

        _currentState = Standby;
        GameManager.Instance.PlayerList.Add(this);
    }

    private void Update()
    {
        if (_isMyTurn)
        {
            Debug.Log($"Turn Start: {name}");
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

        _deck.PhotonView.RPC("DrawCard", RpcTarget.All);
        _currentState = ActionPhase;
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

                _hand = _handGO.GetComponent<NewHand>();
                _battlefield = _battlefieldGO.GetComponent<NewBattlefield>();
                _deck = _deckGO.GetComponent<NewDeck>();
                _tomb = _tombGO.GetComponent<NewTomb>();
                _endPhaseBtn = _playerUI.transform.GetChild(4).GetComponent<Button>();

                _hand.PlayerPhotonView = _playerPhotonView;
                _battlefield.PlayerPhotonView = _playerPhotonView;
                _deck.PlayerPhotonView = _playerPhotonView;
                _tomb.PlayerPhotonView = _playerPhotonView;
                _endPhaseBtn.onClick.AddListener(ChangePhase);

                _currentState = Standby;
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

    #region PunRPC Methods
    #endregion
}
