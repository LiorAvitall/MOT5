using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class NewHand : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView { get => _playerPhotonView; set => _playerPhotonView = value; }
    #endregion

    [Header("AspectList")]
    public List<CardData> CardsInHand;

    [Header("CurrentAspects")]
    public Card CurrentCardInHand;
    public CardData CurrentCardDataInHand;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            Debug.Log("called: Hand On Pointer Enter");

            if (eventData.pointerDrag == null)
                return;

            Card currentCard = eventData.pointerDrag.GetComponent<Card>();

            CurrentCardInHand = currentCard;
            CurrentCardDataInHand = currentCard.GetComponent<CardDisplay>().CardData;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!NewEventHandler.Instance.IsSacrificing)
            return;

        else if (NewEventHandler.Instance.IsSacrificing)
            NewEventHandler.Instance.MyTomb.CardToSacrifice(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        if (_photonView.IsMine)
        {
            Debug.Log("called: Hand On Drop");

            //if (CurrentCardInHand.IsCardInHand)
            //    CurrentCardInHand.NextParent = transform;
        }

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
