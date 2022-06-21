using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class NewBattlefield : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView { get => _playerPhotonView; set => _playerPhotonView = value; }
    #endregion

    [Header("AspectList")]
    private List<CardData> _cardsInField;
    public List<CardData> CardsInField { get => _cardsInField; set => _cardsInField = value; }

    [Header("CurrentAspects")]
    public Card CurrentCardInBattlefield;
    public CardData CurrentCardDataInBattlefield;

    [Header("Required Components")]
    [SerializeField] private NewHand _hand;

    public static void InBattlefieldState()
    {

    }
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
            BattlefieldPlaceCard(CurrentCardInBattlefield);
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
        if (_photonView.IsMine)
            return;

        if (!NewEventHandler.Instance.PhotonView.IsMine)
        {

        }
        //ClickEventData = eventData;

        //if (!_myDataHandler.IsDestroying)
        //    return;

        // need opponent eventData

        //else if (_myDataHandler.IsDestroying)
        //    _myDataHandler.TombData.CardToDestroy(eventData);
    }

    public void BattlefieldPlaceCard(Card currentTarget)
    {
        //get current card
        CardData cardToField = currentTarget.gameObject.GetComponent<CardDisplay>().CardData;

        //add current card to battlefield
        _cardsInField.Add(cardToField);

        //check if works
        print(cardToField.Name);

        //remove placed cards from hand
        _hand.CardsInHand.Remove(cardToField);

        //apply card effect
        //Action(cardToField);

        currentTarget.IsOnBattlefield = true;

        // get last placed card on field
       // _myDataHandler.LastPlacedCardOnBattelfield = currentTarget.gameObject;

        // addintional code here ----- V
    }
}
