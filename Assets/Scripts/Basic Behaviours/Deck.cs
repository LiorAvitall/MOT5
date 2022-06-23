using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Deck : MonoBehaviour
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _playerPhotonView;
    public PhotonView PlayerPhotonView { get => _playerPhotonView; set => _playerPhotonView = value; }
    #endregion

    [Header("Data Script")]
    [SerializeField] private DataHandler _dataHandler;

    [Header("AspectList")]
    [SerializeField] private List<AspectData> _aspectsInDeck = new List<AspectData>(25);

    [Header("AspectPrefab")]
    [SerializeField] private GameObject _cardPrefab;

    [Header("AspectData")]
    [SerializeField] private AspectData _lightCard;

    [SerializeField]
    private AspectData _deathCard, _destructionCard, _lifeCard, _controlCard;

    private bool _isDrawingWithLight = false;
    private int _maxDeckSize = 25, _currentDeckSize;

    private void Start()
    {
        _aspectsInDeck.Clear();

        for (int i = 0; i < 5; i++)
        {
            _aspectsInDeck.Add(_lightCard);
            _aspectsInDeck.Add(_deathCard);
            _aspectsInDeck.Add(_destructionCard);
            _aspectsInDeck.Add(_lifeCard);
            _aspectsInDeck.Add(_controlCard);
        }
        //randomize _aspectsInDeck list
        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            AspectData temp = _aspectsInDeck[i];
            int randomIndex = UnityEngine.Random.Range(i, _aspectsInDeck.Count);
            _aspectsInDeck[i] = _aspectsInDeck[randomIndex];
            _aspectsInDeck[randomIndex] = temp;
        }
    }

    private void Update()
    {
        if (_currentDeckSize > _maxDeckSize)
        {
            _currentDeckSize = _maxDeckSize;
        }
    }

    public void InitializeGame()
    {
        //get top 4 cards in deck
        List<AspectData> cardsToHand = _aspectsInDeck.GetRange(0, 4);

        //add said cards to hand
        _dataHandler.HandData.CardsInHand.AddRange(cardsToHand);

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (AspectData card in cardsToHand)
        {
            _cardPrefab.GetComponent<AspectDisplayData>().CardData = card;
            Instantiate(_cardPrefab, _dataHandler.HandData.transform);

            //check if works (update: it does)
            print(card.Name);
        }

        //remove drawn cards from deck
        _aspectsInDeck.RemoveRange(0, 4);
        _currentDeckSize -= 4;
    }

    public void InitializeGameShowCase()
    {
        List<AspectData> cardsToHand = new List<AspectData>(4);

        //get top 4 cards in deck
        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].Name == "Light")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].Name == "Life")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].Name == "Death")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].Name == "Control")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        //add said cards to hand
        _dataHandler.HandData.CardsInHand = cardsToHand;

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (AspectData card in cardsToHand)
        {
            _cardPrefab.GetComponent<AspectDisplayData>().CardData = card;
            Instantiate(_cardPrefab, _dataHandler.HandData.transform);

            //check if works (update: it does)
            print(card.Name);
        }

        //remove drawn cards from deck
        _aspectsInDeck.RemoveRange(0, 4);
        _currentDeckSize -= 4;
    }

    public void DrawCard()
    {
        //get top card in deck & adds it to the hand
        _dataHandler.HandData.CardsInHand.Add(_aspectsInDeck[0]);

        //reads said card data and creates a prefab based on that data in the hand
        _cardPrefab.GetComponent<AspectDisplayData>().CardData = _aspectsInDeck[0];
        Instantiate(_cardPrefab, _dataHandler.HandData.transform);
        
        //check if works (update: it does)
        print(_aspectsInDeck[0].Name);

        _aspectsInDeck.RemoveAt(0);
        _currentDeckSize --;
    }

    public void DrawTwo()
    {
        //get top 2 cards in deck
        List<AspectData> cardsToHand = _aspectsInDeck.GetRange(0, 2);

        //add said cards to hand
        _dataHandler.HandData.CardsInHand.AddRange(cardsToHand);

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (AspectData card in cardsToHand)
        {
            _cardPrefab.GetComponent<AspectDisplayData>().CardData = card;
            Instantiate(_cardPrefab, _dataHandler.HandData.transform);

            //check if works (update: it does)
            print(card.Name);
        }

        //remove drawn cards from deck
        _aspectsInDeck.RemoveRange(0, 2);
        _currentDeckSize -= 2;
    }
}
