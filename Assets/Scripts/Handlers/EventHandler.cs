using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [Header("Data Script")]
    [SerializeField] private PlayerData _myDataHandler;
    [SerializeField] private PlayerData _opponentDataHandler;

    private int ifFiveIWin = 0;
    private bool didIWin = false;

    private void Update()
    {
        if (didIWin)
        {
            Debug.Log("I Won");
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
        _myDataHandler.Deck.PlayerPhotonView.RPC("InitializeGame", Photon.Pun.RpcTarget.AllBuffered);
    }

    public void StartGameShowCase()
    {
        // Draw first card from deck's aspect list from deck to hand
        _myDataHandler.Deck.PlayerPhotonView.RPC("InitializeGameShowCase", Photon.Pun.RpcTarget.AllBuffered);
    }

    public void DrawCard()
    {
        _myDataHandler.Deck.PlayerPhotonView.RPC("DrawCard", Photon.Pun.RpcTarget.AllBuffered);
    }

    public void DrawTwo()
    {
        _myDataHandler.Deck.PlayerPhotonView.RPC("DrawTwo", Photon.Pun.RpcTarget.AllBuffered);
    }

    public void Revive()
    {
        _myDataHandler.IsReviving = true;
        _myDataHandler.Tomb.SearchTomb();
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
    public void BattlefieldPlaceCard(Aspect currentTarget)
    {
        //get current card
        AspectData cardToField = currentTarget.gameObject.GetComponent<AspectDisplayData>().CardData;

        //add current card to battlefield
        _myDataHandler.Battlefield.CardsInField.Add(cardToField);

        //check if works
        print(cardToField.Name);

        //remove placed cards from hand
        _myDataHandler.Hand.CardsInHand.Remove(cardToField);

        Action(cardToField);

        currentTarget.IsOnBattlefield = true;

        // get last placed card on field
        _myDataHandler.LastPlacedCardOnBattelfield = currentTarget.gameObject;

        // addintional code here ----- V

        for (int i = 0; i < _myDataHandler.Battlefield.CardsInField.Count; i++)
        {
            if (_myDataHandler.Battlefield.CardsInField[i].PrimodialPower == PowerType.Light)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.Battlefield.CardsInField.Count; i++)
        {
            if (_myDataHandler.Battlefield.CardsInField[i].PrimodialPower == PowerType.Death)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.Battlefield.CardsInField.Count; i++)
        {
            if (_myDataHandler.Battlefield.CardsInField[i].PrimodialPower == PowerType.Control)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.Battlefield.CardsInField.Count; i++)
        {
            if (_myDataHandler.Battlefield.CardsInField[i].PrimodialPower == PowerType.Destruction)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.Battlefield.CardsInField.Count; i++)
        {
            if (_myDataHandler.Battlefield.CardsInField[i].PrimodialPower == PowerType.Life)
            {
                ifFiveIWin++;
                break;
            }
        }

        if (ifFiveIWin < 5)
        {
            Debug.Log("Counted " + ifFiveIWin);
            ifFiveIWin = 0;
        }
        else
        {
            didIWin = true;
        }
    }

    public void Action(AspectData card)
    {
        if (card is LightAspect)
            (card as LightAspect).Action(this);
        else if (card is DeathAspect)
            (card as DeathAspect).Action(this);
        else if (card is DestructionAspect)
            (card as DestructionAspect).Action(this);
        else if (card is LifeAspect)
            (card as LifeAspect).Action(this);
        else if (card is ControlAspect)
            (card as ControlAspect).Action(this);
    }

    public void SupremeAction(AspectData card)
    {
        if (card is LightAspect)
            (card as LightAspect).SupremeAction();
        else if (card is DeathAspect)
            (card as DeathAspect).SupremeAction();
        else if (card is DestructionAspect)
            (card as DestructionAspect).SupremeAction();
        else if (card is LifeAspect)
            (card as LifeAspect).SupremeAction();
        else if (card is ControlAspect)
            (card as ControlAspect).SupremeAction();
    }

    public void SecondaryAction(AspectData card)
    {
        if (card is LightAspect)
            (card as LightAspect).SecondaryAction();
        else if (card is DeathAspect)
            (card as DeathAspect).SecondaryAction();
        else if (card is DestructionAspect)
            (card as DestructionAspect).SecondaryAction();
        else if (card is LifeAspect)
            (card as LifeAspect).SecondaryAction();
        else if (card is ControlAspect)
            (card as ControlAspect).SecondaryAction();
    }
}
