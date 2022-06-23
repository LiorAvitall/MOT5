using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class OldEventHandler : MonoBehaviour
{
    [Header("Data Script")]
    [SerializeField] private OldDataHandler _myDataHandler;
    [SerializeField] private OldDataHandler _opponentDataHandler;

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
        _myDataHandler.DeckData.InitializeGame();
    }

    public void StartGameShowCase()
    {
        // Draw first card from deck's aspect list from deck to hand
        _myDataHandler.DeckData.InitializeGameShowCase();
    }

    public void DrawCard()
    {
        _myDataHandler.DeckData.DrawCard();
    }

    public void DrawTwo()
    {
        _myDataHandler.DeckData.DrawTwo();
    }

    public void Revive()
    {
        _myDataHandler.IsReviving = true;
        _myDataHandler.TombData.SearchTomb();
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
    public void BattlefieldPlaceCard(OldCard currentTarget)
    {
        //get current card
        OldCardData cardToField = currentTarget.gameObject.GetComponent<OldCardDisplay>().CardData;

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

        for (int i = 0; i < _myDataHandler.BattlefieldData.CardsInField.Count; i++)
        {
            if (_myDataHandler.BattlefieldData.CardsInField[i].PrimodialPower == PowerType.Light)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.BattlefieldData.CardsInField.Count; i++)
        {
            if (_myDataHandler.BattlefieldData.CardsInField[i].PrimodialPower == PowerType.Death)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.BattlefieldData.CardsInField.Count; i++)
        {
            if (_myDataHandler.BattlefieldData.CardsInField[i].PrimodialPower == PowerType.Control)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.BattlefieldData.CardsInField.Count; i++)
        {
            if (_myDataHandler.BattlefieldData.CardsInField[i].PrimodialPower == PowerType.Destruction)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _myDataHandler.BattlefieldData.CardsInField.Count; i++)
        {
            if (_myDataHandler.BattlefieldData.CardsInField[i].PrimodialPower == PowerType.Life)
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

    public void Action(OldCardData card)
    {
        if (card is OldLightCard)
            (card as OldLightCard).Action(this);
        else if (card is OldDeathCard)
            (card as OldDeathCard).Action(this);
        else if (card is OldDestructionCard)
            (card as OldDestructionCard).Action(this);
        else if (card is OldLifeCard)
            (card as OldLifeCard).Action(this);
        else if (card is OldControlCard)
            (card as OldControlCard).Action(this);
    }

    public void SupremeAction(OldCardData card)
    {
        if (card is OldLightCard)
            (card as OldLightCard).SupremeAction();
        else if (card is OldDeathCard)
            (card as OldDeathCard).SupremeAction();
        else if (card is OldDestructionCard)
            (card as OldDestructionCard).SupremeAction();
        else if (card is OldLifeCard)
            (card as OldLifeCard).SupremeAction();
        else if (card is OldControlCard)
            (card as OldControlCard).SupremeAction();
    }

    public void SecondaryAction(OldCardData card)
    {
        if (card is OldLightCard)
            (card as OldLightCard).SecondaryAction();
        else if (card is OldDeathCard)
            (card as OldDeathCard).SecondaryAction();
        else if (card is OldDestructionCard)
            (card as OldDestructionCard).SecondaryAction();
        else if (card is OldLifeCard)
            (card as OldLifeCard).SecondaryAction();
        else if (card is OldControlCard)
            (card as OldControlCard).SecondaryAction();
    }
}
