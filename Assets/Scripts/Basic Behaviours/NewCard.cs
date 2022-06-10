using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class NewCard : MonoBehaviour, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerExitHandler
{
    #region Photon
    [SerializeField] private PhotonView _photonView;
    #endregion

    #region Data
    private CardData _cardData;
    #endregion

    #region Parents
    [Header("Parents")]
    [SerializeField] private Transform _currentParent;
    public Transform CurrentParent { get => transform.parent; set => value = _currentParent; }

    public Transform OldParent, NextParent;

    [SerializeField]
    private Transform _deckTransform, _handTransform, _battlefieldTransform, _tombTransform;
    #endregion

    #region UI
    [Header("UI")]
    [SerializeField]  private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _onHoverOffsetHand = 160f, _onHoverOffsetBattleField = 80f;
    #endregion

    #region State Machine
    private delegate void State();
    private State _state;
    #endregion

    private void Start()
    {
        _cardData = GetComponent<CardDisplay>().CardData;
        _state = InHand;
    }

    private void Update()
    {
        _state.Invoke();
    }

    private void InDeck()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("called: In Deck");  
        }
    }

    private void InHand()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("called: In Hand");
            transform.parent = NewEventHandler.Instance.MyHand.transform;
        }
    }

    private void InBattlefield()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("called: In Battlefield");
        }
    }

    private void InTomb()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("called: In Tomb");
        }
    }

    private void Hover()
    {
       
    }

    private void StartDragging()
    {
        LayoutElement le = GetComponent<LayoutElement>();

        transform.SetParent(NextParent);

        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        transform.SetSiblingIndex(transform.GetSiblingIndex());

        NextParent = CurrentParent;
        OldParent = NextParent;

        transform.SetParent(transform.parent.parent);

        _canvasGroup.blocksRaycasts = false;
    }

    private void Drag()
    {
        if (transform.parent != OldParent)
            transform.SetParent(OldParent);

        int newSiblingIndex = OldParent.childCount;

        for (int i = 0; i < OldParent.childCount; i++)
        {
            if (transform.position.x < OldParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }

        transform.SetSiblingIndex(newSiblingIndex);
    }

    private void EndDrag()
    {
        transform.SetParent(NextParent);
        transform.SetSiblingIndex(transform.GetSiblingIndex());

        if (_currentParent != OldParent)
        {
            if (_currentParent = _deckTransform)
            {
                _state = InDeck;
            }
            else if (_currentParent = _handTransform)
            {
                _state = InHand;
            }
            else if (_currentParent = _battlefieldTransform)
            {
                _state = InBattlefield;
            }
            else if (_currentParent = _tombTransform)
            {
                _state = InTomb;
            }

        _canvasGroup.blocksRaycasts = true;
        }
    }

    private void Play()
    {
        if (_cardData == null)
            return;

        if (_cardData is LightCard2)
            (_cardData as LightCard2).Action();
        //else if (_cardData is DeathCard)
        //    (_cardData as DeathCard).Action(this);
        //else if (_cardData is DestructionCard)
        //    (_cardData as DestructionCard).Action(this);
        //else if (_cardData is LifeCard)
        //    (_cardData as LifeCard).Action(this);
        //else if (_cardData is ControlCard)
        //    (_cardData as ControlCard).Action(this);
    }

    #region Pointer Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("called: On Pointer Enter");

        if (_photonView.IsMine)
        {
            OldParent = transform;

            if (_state != InHand && _state != InBattlefield)
                return;

            // affects cards in battlefield
            else if (_state != InHand)
                transform.position = new Vector2(transform.position.x, transform.position.y + _onHoverOffsetBattleField);

            // affects cars in hand
            else
                transform.position = new Vector2(transform.position.x, transform.position.y + _onHoverOffsetHand);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("called: On Begin Drag");

        if (_photonView.IsMine)
        {
            _state = StartDragging;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("called: On Drag");

        if (_photonView.IsMine)
        {
            transform.position = eventData.position;

            _state = Drag;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("called: On End Drag");

        if (_photonView.IsMine)
        {
            _state = EndDrag;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("called: On Drop");

        if (_photonView.IsMine)
        {
            if (_state == InBattlefield)
            {
                transform.parent = NewEventHandler.Instance.MyBattlefield.transform;
                _state = Play;
            }
            else
            {
                transform.SetParent(OldParent);
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("called: On Pointer Exit");

        if (_photonView.IsMine)
        {
            if (_state != InHand && _state != InBattlefield)
                return;

            // affects cards in battlefield
            else if (_state != InHand)
                transform.position = new Vector2(transform.position.x, transform.position.y - _onHoverOffsetBattleField);

            // affects cars in hand
            else
                transform.position = new Vector2(transform.position.x, transform.position.y - _onHoverOffsetHand);
        }
    }
    #endregion
}
