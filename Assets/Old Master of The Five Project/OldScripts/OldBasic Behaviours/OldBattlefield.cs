using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class OldBattlefield : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Data Script")]
    [SerializeField] private OldDataHandler _myDataHandler;
    [SerializeField] private OldDataHandler _opponentDataHandler;
    [SerializeField] private OldEventHandler _myEventHandler;

    [Header("AspectList")]
    public List<OldCardData> CardsInField;

    [Header("CurrentAspects")]
    public OldCard CurrentCardInBattlefield;
    public OldCardData CurrentCardDataInBattlefield;

    public PointerEventData ClickEventData;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        OldCard currentCard = eventData.pointerDrag.GetComponent<OldCard>();

        CurrentCardInBattlefield = currentCard;
        CurrentCardDataInBattlefield = currentCard.GetComponent<OldCardDisplay>().CardData;

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
