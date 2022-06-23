using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class OldHand : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Data Script")]
    [SerializeField] private OldDataHandler _dataHandler;

    [Header("AspectList")]
    public List<OldCardData> CardsInHand;

    [Header("CurrentAspects")]
    public OldCard CurrentCardInHand;
    public OldCardData CurrentCardDataInHand;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        OldCard currentCard = eventData.pointerDrag.GetComponent<OldCard>();

        CurrentCardInHand = currentCard;
        CurrentCardDataInHand = currentCard.GetComponent<OldCardDisplay>().CardData;
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
