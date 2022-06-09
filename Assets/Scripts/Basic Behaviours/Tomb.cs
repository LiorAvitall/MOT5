using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Tomb : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Data Script")]
    [SerializeField] private DataHandler _myDataHandler;
    [SerializeField] private DataHandler _opponentDataHandler;
    [SerializeField] private EventHandler _myEventHandler;

    [Header("AspectList")]
    public List<CardData> CardsInTomb;

    [Header("CurrentAspects")]
    public Card CurrentCardInHand;
    public CardData CurrentCardDataInTomb;

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void CardToSacrifice(PointerEventData eventData)
    {
        //get current card
        CardData cardToTomb = eventData.pointerDrag.GetComponent<CardDisplay>().CardData;

        //add current card to tomb
        _myDataHandler.TombData.CardsInTomb.Add(cardToTomb);

        //check if works
        print(cardToTomb.Name);

        //remove placed cards from hand
        _myDataHandler.HandData.CardsInHand.Remove(cardToTomb);

        Destroy(eventData.pointerDrag);

        _myDataHandler.IsSacrificing = false;
        _myDataHandler.SacrificeOverlay.SetActive(false);
    }

    public void CardToDestroy(PointerEventData eventData)
    {
        //_myEventHandler.TargetLine.SetPosition(0, _myDataHandler.LastPlacedCardOnBattelfield.transform.position);
        //_myEventHandler.TargetLine.SetPosition(1, eventData.position);
        print("Tried drawing line");
        //get current card
        CardData cardToTomb = eventData.pointerDrag.GetComponent<CardDisplay>().CardData;

        //add current card to tomb
        _opponentDataHandler.TombData.CardsInTomb.Add(cardToTomb);

        //check if works
        print(cardToTomb.Name);

        //remove placed cards from battlefield
        _opponentDataHandler.BattlefieldData.CardsInField.Remove(cardToTomb);

        Destroy(eventData.pointerDrag);

        _myDataHandler.IsDestroying = false;
    }

    private void Search()
    {
        /* Player should be able to vieww Tomb at all times
            this is correct for all players to all tombs
            which means that Tomb should be accesible
            by all player at any point of the game
        */
    }
}
