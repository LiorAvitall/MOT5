using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class NewBattlefield : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Photon
    [SerializeField] private PhotonView _photonView;
    #endregion

    [Header("AspectList")]
    public List<CardData> CardsInField;

    [Header("CurrentAspects")]
    public NewCard CurrentCardInBattlefield;
    public CardData CurrentCardDataInBattlefield;

    public static void InBattlefieldState()
    {

    }
    public PointerEventData ClickEventData;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            Debug.Log("called: Battlefield On Pointer Enter");

            if (eventData.pointerDrag == null)
                return;

            NewCard currentCard = eventData.pointerDrag.GetComponent<NewCard>();

            CurrentCardInBattlefield = currentCard;
            CurrentCardDataInBattlefield = currentCard.GetComponent<CardDisplay>().CardData;

            if (CurrentCardInBattlefield != null)
            {
                CurrentCardInBattlefield.OldParent = transform;
                //CurrentCardInBattlefield.IsCardInHand = false;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_photonView.IsMine && CurrentCardInBattlefield != null)
        {
            Debug.Log("called: Battlefield On Drop");

            CurrentCardInBattlefield.NextParent = transform;
            //CurrentCardInBattlefield.IsCardInHand = false;
            NewEventHandler.Instance.BattlefieldPlaceCard(CurrentCardInBattlefield);
            CurrentCardInBattlefield = null;
            CurrentCardDataInBattlefield = null;

            Debug.Log("card Placed");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_photonView.IsMine)
        {
            if (eventData.pointerDrag == null)
                return;

            if (CurrentCardInBattlefield != null && CurrentCardInBattlefield.OldParent == transform)
            {
                CurrentCardInBattlefield.OldParent = transform;
                //CurrentCardInBattlefield.IsCardInHand = false;
            }
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
}
