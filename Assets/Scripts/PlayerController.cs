using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region Photon
    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView => _playerPhotonView;
    #endregion

    #region Prefab references
    private Transform _tableTransform, _gameCanvas;
    private GameObject _playerTable, _playerBoard;
    #endregion
    private GameObject _currentTarget;
    private bool _isMyTurn, _isPhaseDone;
    
    #region State Machine
    private delegate void State();
    private State _currentState;
    #endregion

    private void Awake()
    {
        gameObject.name = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";

        if (_playerPhotonView.IsMine)
        {
            return;
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    private void Start()
    {
        if (_playerPhotonView.IsMine)
        {
            _currentState = DrawPhase;
        }
    }

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


    #region States
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

    #region PunRPC
    [PunRPC]
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

            DontDestroyOnLoad(_playerTable);
            DontDestroyOnLoad(_playerBoard);

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
            _playerBoard.transform.SetParent(_gameCanvas);
            DontDestroyOnLoad(_tableTransform);
        }
    }
    #endregion
}
