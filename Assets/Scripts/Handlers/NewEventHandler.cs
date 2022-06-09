using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NewEventHandler : MonoBehaviour
{
    public static NewEventHandler Instance { get; private set; }

    [Header("Sections")]
    public NewDeck Deck;
    public NewHand Hand;
    public NewBattlefield Battlefield;
    public NewTomb Tomb;

    //public GameObject SacrificeOverlay;
    public GameObject LastPlacedCardOnBattelfield;

    public bool IsSacrificing = false;
    public bool IsDestroying = false;

    public PhotonView PhotonView;

    //public LineRenderer TargetLine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

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
        Deck.InitializeGame();
    }

    public void DrawCard()
    {
        Deck.DrawCard();
    }

    public void DrawTwo()
    {
        Deck.DrawTwo();
    }

    public void Sacrifice()
    {
        IsSacrificing = true;
        //SacrificeOverlay.SetActive(true);
    }

    // need fixing
    public void Destroy()
    {
        IsDestroying = true;

        //_myDataHandler.SacrificeOverlay.SetActive(true);
    }

    // when the player place the card on the battlefield
    public void BattlefieldPlaceCard(NewCard currentTarget)
    {
        //get current card
        CardData cardToField = currentTarget.gameObject.GetComponent<CardDisplay>().CardData;

        //add current card to battlefield
        Battlefield.CardsInField.Add(cardToField);

        //check if works
        print(cardToField.Name);

        //remove placed cards from hand
        Hand.CardsInHand.Remove(cardToField);

        Action(cardToField);

        //currentTarget.IsOnBattlefield = true;

        // get last placed card on field
        LastPlacedCardOnBattelfield = currentTarget.gameObject;

        // addintional code here ----- V
    }

    
    public void Action(CardData card)
    {
        if (card is LightCard)
            (card as LightCard2).Action();
        //else if (card is DeathCard)
        //    (card as DeathCard).Action(this);
        //else if (card is DestructionCard)
        //    (card as DestructionCard).Action(this);
        //else if (card is LifeCard)
        //    (card as LifeCard).Action(this);
        //else if (card is ControlCard)
        //    (card as ControlCard).Action(this);
    }
    /*
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
    */
}
