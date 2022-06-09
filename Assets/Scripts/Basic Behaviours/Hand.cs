using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Hand : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Data Script")]
    [SerializeField] private DataHandler _dataHandler;

    [Header("AspectList")]
    public List<CardData> CardsInHand;

    [Header("CurrentAspects")]
    public Card CurrentCardInHand;
    public CardData CurrentCardDataInHand;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Card currentCard = eventData.pointerDrag.GetComponent<Card>();

        CurrentCardInHand = currentCard;
        CurrentCardDataInHand = currentCard.GetComponent<CardDisplay>().CardData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_dataHandler.IsSacrificing)
            return;

        else if (_dataHandler.IsSacrificing)
            _dataHandler.TombData.CardToSacrifice(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        print("card Placed");

        if (CurrentCardInHand.IsCardInHand)
            CurrentCardInHand.ParentToReturn = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
