using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;


public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private delegate void State();
    private State _state;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _onHoverOffsetHand = 160f, _onHoverOffsetBattleField = 80f;

    private GameObject _card = null;
    private PhotonView _photonView;

    public Transform ParentToReturn = null;
    public Transform TempParentToReturn = null;
    
    public bool IsCardInHand = true, IsHoldingCard = false, IsOnBattlefield = false;

    private void Start()
    {
        _state = InBattlefield;
    }

    private void Update()
    {
        _state.Invoke();
    }

    private void InBattlefield()
    {
        Battlefield.InBattlefieldState();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            print("Drag Began");

            IsHoldingCard = true;
            _card = new GameObject($"{name}_Placeholder");
            _card.transform.SetParent(transform.parent);
            LayoutElement le = _card.AddComponent<LayoutElement>();
            le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            _card.transform.SetSiblingIndex(transform.GetSiblingIndex());

            ParentToReturn = transform.parent;
            TempParentToReturn = ParentToReturn;
            transform.SetParent(transform.parent.parent);
            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            print("Dragging");

            transform.position = eventData.position;

            if (_card.transform.parent != TempParentToReturn)
                _card.transform.SetParent(TempParentToReturn);

            int newSiblingIndex = TempParentToReturn.childCount;

            for (int i = 0; i < TempParentToReturn.childCount; i++)
            {
                if (transform.position.x < TempParentToReturn.GetChild(i).position.x)
                {
                    newSiblingIndex = i;

                    if (_card.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;

                    break;
                }
            }

            _card.transform.SetSiblingIndex(newSiblingIndex);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            print("Stopped Dragging");

            transform.SetParent(ParentToReturn);
            transform.SetSiblingIndex(_card.transform.GetSiblingIndex());
            _canvasGroup.blocksRaycasts = true;

            Destroy(_card);
            IsHoldingCard = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            print("Hovering");

            if (IsHoldingCard)
                return;

            // affects cards in battlefield
            else if (!IsCardInHand)
                transform.position = new Vector2(transform.position.x, transform.position.y + _onHoverOffsetBattleField);

            // affects cars in hand
            else
                transform.position = new Vector2(transform.position.x, transform.position.y + _onHoverOffsetHand);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            print("Stopped Hovering");

            if (IsHoldingCard)
                return;

            // affects cards in battlefield
            else if (!IsCardInHand)
                transform.position = new Vector2(transform.position.x, transform.position.y - _onHoverOffsetBattleField);

            // affects cars in hand
            else
                transform.position = new Vector2(transform.position.x, transform.position.y - _onHoverOffsetHand);
        }
    }
}
