using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [Header("Data Script")]
    [SerializeField] private DataHandler _myDataHandler;
    [SerializeField] private DataHandler _opponentDataHandler;
    
    public LineRenderer TargetLine;

    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }

    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }

    public void StartGame()
    {
        // Draw first card from deck's aspect list from deck to hand
        _myDataHandler.DeckData.InitializeGame();
    }

    public void DrawCard()
    {
        _myDataHandler.DeckData.DrawCard();
    }

    public void DrawTwo()
    {
        _myDataHandler.DeckData.DrawTwo();
    }

    public void Sacrifice()
    {
        _opponentDataHandler.IsSacrificing = true;
        _opponentDataHandler.SacrificeOverlay.SetActive(true);
    }

    // need fixing
    public void Destroy()
    {
        _myDataHandler.IsDestroying = true;
        
        //_myDataHandler.SacrificeOverlay.SetActive(true);
    }

    // when the player place the card on the battlefield
    public void BattlefieldPlaceCard(Card currentTarget)
    {
        //get current card
        CardData cardToField = currentTarget.gameObject.GetComponent<CardDisplay>().CardData;

        //add current card to battlefield
        _myDataHandler.BattlefieldData.CardsInField.Add(cardToField);

        //check if works
        print(cardToField.Name);

        //remove placed cards from hand
        _myDataHandler.HandData.CardsInHand.Remove(cardToField);

        Action(cardToField);

        currentTarget.IsOnBattlefield = true;

        // get last placed card on field
        _myDataHandler.LastPlacedCardOnBattelfield = currentTarget.gameObject;

        // addintional code here ----- V
    }

    public void Action(CardData card)
    {
        if (card is LightCard)
            (card as LightCard).Action(this);
        else if (card is DeathCard)
            (card as DeathCard).Action(this);
        else if (card is DestructionCard)
            (card as DestructionCard).Action(this);
        else if (card is LifeCard)
            (card as LifeCard).Action(this);
        else if (card is ControlCard)
            (card as ControlCard).Action(this);
    }

    public void SupremeAction(CardData card)
    {
        if (card is LightCard)
            (card as LightCard).SupremeAction();
        else if (card is DeathCard)
            (card as DeathCard).SupremeAction();
        else if (card is DestructionCard)
            (card as DestructionCard).SupremeAction();
        else if (card is LifeCard)
            (card as LifeCard).SupremeAction();
        else if (card is ControlCard)
            (card as ControlCard).SupremeAction();
    }

    public void SecondaryAction(CardData card)
    {
        if (card is LightCard)
            (card as LightCard).SecondaryAction();
        else if (card is DeathCard)
            (card as DeathCard).SecondaryAction();
        else if (card is DestructionCard)
            (card as DestructionCard).SecondaryAction();
        else if (card is LifeCard)
            (card as LifeCard).SecondaryAction();
        else if (card is ControlCard)
            (card as ControlCard).SecondaryAction();
    }
}
