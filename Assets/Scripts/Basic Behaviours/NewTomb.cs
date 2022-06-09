using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class NewTomb : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Photon
    [SerializeField] private PhotonView _photonView;
    #endregion

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
        if (_photonView.IsMine)
        {
            //get current card
            CardData cardToTomb = eventData.pointerDrag.GetComponent<CardDisplay>().CardData;

            //add current card to tomb
            NewEventHandler.Instance.Tomb.CardsInTomb.Add(cardToTomb);

            //check if works
            print(cardToTomb.Name);

            //remove placed cards from hand
            NewEventHandler.Instance.Hand.CardsInHand.Remove(cardToTomb);

            Destroy(eventData.pointerDrag);

            NewEventHandler.Instance.IsSacrificing = false;
            //NewEventHandler.Instance.SacrificeOverlay.SetActive(false);
        }
    }

    public void CardToDestroy(PointerEventData eventData)
    {
        if (!_photonView.IsMine)
        {
            print("Tried drawing line");
            //get current card
            CardData cardToTomb = eventData.pointerDrag.GetComponent<CardDisplay>().CardData;

            //add current card to tomb
            NewEventHandler.Instance.Tomb.CardsInTomb.Add(cardToTomb);

            //check if works
            print(cardToTomb.Name);

            //remove placed cards from battlefield
            NewEventHandler.Instance.Battlefield.CardsInField.Remove(cardToTomb);

            Destroy(eventData.pointerDrag);

            NewEventHandler.Instance.IsDestroying = false;
        }
        //_myEventHandler.TargetLine.SetPosition(0, _myDataHandler.LastPlacedCardOnBattelfield.transform.position);
        //_myEventHandler.TargetLine.SetPosition(1, eventData.position);
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
