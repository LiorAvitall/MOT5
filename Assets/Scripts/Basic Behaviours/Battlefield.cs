using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Battlefield : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Data Script")]
    [SerializeField] private DataHandler _myDataHandler;
    [SerializeField] private DataHandler _opponentDataHandler;
    [SerializeField] private EventHandler _myEventHandler;

    [Header("AspectList")]
    public List<CardData> CardsInField;

    [Header("CurrentAspects")]
    public Card CurrentCardInBattlefield;
    public CardData CurrentCardDataInBattlefield;


    public PointerEventData ClickEventData;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Card currentCard = eventData.pointerDrag.GetComponent<Card>();

        CurrentCardInBattlefield = currentCard;
        CurrentCardDataInBattlefield = currentCard.GetComponent<CardDisplay>().CardData;

        if (CurrentCardInBattlefield != null)
        {
            CurrentCardInBattlefield.ParentToReturnPlaceholder = transform;
            CurrentCardInBattlefield.IsCardInHand = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CurrentCardInBattlefield != null)
        {
            CurrentCardInBattlefield.ParentToReturn = transform;
            CurrentCardInBattlefield.IsCardInHand = false;
            _myEventHandler.BattlefieldPlaceCard(CurrentCardInBattlefield);
            CurrentCardInBattlefield = null;
            CurrentCardDataInBattlefield = null;

            print("card Placed");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        if (CurrentCardInBattlefield != null && CurrentCardInBattlefield.ParentToReturnPlaceholder == transform)
        {
            CurrentCardInBattlefield.ParentToReturnPlaceholder = transform;
            CurrentCardInBattlefield.IsCardInHand = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ClickEventData = eventData;

        if (!_myDataHandler.IsDestroying)
            return;

        // need opponent eventData
        else if (_myDataHandler.IsDestroying)
            _myDataHandler.TombData.CardToDestroy(eventData);
    }
}
