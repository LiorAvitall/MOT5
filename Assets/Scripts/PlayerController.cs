using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class PlayerController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region State Machine
    private delegate void State();
    private State _currentState;
    #endregion

    #region Photon
    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView playerPhotonView => _playerPhotonView;
    #endregion

    private GameObject _currentTarget;
    private bool _isMyTurn, _isPhaseDone;

    private void Start()
    {
        _currentState = DrawPhase;
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
        throw new System.NotImplementedException();
    }
}
