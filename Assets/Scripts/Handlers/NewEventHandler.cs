using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NewEventHandler : MonoBehaviour
{
    public static NewEventHandler Instance { get; private set; }

    #region Photon
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView { get => _photonView; set =>_photonView = value; }
    public int PhotonID { get => _photonView.ViewID; }
    #endregion

    [SerializeField] private Transform _gameCanvas;

    [Header("Player Components")]
    private NewDeck _myDeck;
    private NewHand _myHand;
    private NewBattlefield _myBattlefield;
    private NewTomb _myTomb;

    public NewDeck MyDeck { get => _myDeck; set => _myDeck = value; }
    public NewHand MyHand { get => _myHand; set => _myHand = value; }
    public NewBattlefield MyBattlefield { get => _myBattlefield; set => _myBattlefield = value; }
    public NewTomb MyTomb { get => _myTomb; set => _myTomb = value; }

    //public GameObject SacrificeOverlay;
    public GameObject LastPlacedCardOnBattelfield;

    public bool IsSacrificing = false;
    public bool IsDestroying = false;


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

    private void Start()
    {

        // get player components
        StartCoroutine(FindPlayerComponents());
    }

    private IEnumerator FindPlayerComponents()
    {
        yield return new WaitForSeconds(0.1f);

        Transform playerBoard;
        _gameCanvas = GameObject.Find("Game Canvas").transform;

        if (_photonView.IsMine)
        {
            playerBoard = _gameCanvas.Find("Player1 Board");
        }
        else
        {
            playerBoard = _gameCanvas.Find("Player2 Board");
        }

        _myDeck = playerBoard.Find("Deck").GetComponent<NewDeck>();
        _myHand = playerBoard.Find("Hand").GetComponent<NewHand>();
        _myBattlefield = playerBoard.Find("Battlefield").GetComponent<NewBattlefield>();
        _myTomb = playerBoard.Find("Tomb").GetComponent<NewTomb>();
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
        //_myDeck.InitializeGame();
    }

    public void DrawCard()
    {
        //_myDeck.DrawCard();
    }

    public void DrawTwo()
    {
        //MyDeck.DrawTwo();
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
        MyBattlefield.CardsInField.Add(cardToField);

        //check if works
        print(cardToField.Name);

        //remove placed cards from hand
        MyHand.CardsInHand.Remove(cardToField);

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
