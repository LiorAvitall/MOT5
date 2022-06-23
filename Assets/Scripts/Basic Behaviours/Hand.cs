using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;

public class Hand : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView { get => _playerPhotonView; set => _playerPhotonView = value; }
    #endregion

    [Header("Data Script")]
    [SerializeField] private PlayerData _dataHandler;

    [Header("AspectList")]
    public List<AspectData> CardsInHand;

    [Header("CurrentAspects")]
    public Aspect CurrentCardInHand;
    public AspectData CurrentCardDataInHand;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Aspect currentCard = eventData.pointerDrag.GetComponent<Aspect>();

        CurrentCardInHand = currentCard;
        CurrentCardDataInHand = currentCard.GetComponent<AspectDisplayData>().CardData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_dataHandler.IsSacrificing)
            return;

        else if (_dataHandler.IsSacrificing)
            _dataHandler.Tomb.CardToSacrifice(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        print("card Placed");

        if (CurrentCardInHand.IsCardInHand)
            CurrentCardInHand.ParentToReturn = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
