using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldDeck : MonoBehaviour
{
    [Header("Data Script")]
    [SerializeField] private OldDataHandler _dataHandler;

    [Header("AspectList")]
    [SerializeField] private List<OldCardData> _aspectsInDeck = new List<OldCardData>(25);

    [Header("AspectPrefab")]
    [SerializeField] private GameObject _cardPrefab;

    [Header("AspectData")]
    [SerializeField] private OldCardData _lightCard;

    [SerializeField]
    private OldCardData _deathCard, _destructionCard, _lifeCard, _controlCard;

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
            OldCardData temp = _aspectsInDeck[i];
            int randomIndex = UnityEngine.Random.Range(i, _aspectsInDeck.Count);
            _aspectsInDeck[i] = _aspectsInDeck[randomIndex];
            _aspectsInDeck[randomIndex] = temp;
        }
    }

    public void InitializeGame()
    {
        //get top 4 cards in deck
        List<OldCardData> cardsToHand = _aspectsInDeck.GetRange(0, 4);

        //add said cards to hand
        _dataHandler.HandData.CardsInHand.AddRange(cardsToHand);

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (OldCardData card in cardsToHand)
        {
            _cardPrefab.GetComponent<OldCardDisplay>().CardData = card;
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
        List<OldCardData> cardsToHand = new List<OldCardData>(4);

        //get top 4 cards in deck
        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].name == "OldLightAspect")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].name == "OldLifeAspect")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].name == "OldDeathAspect")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        for (int i = 0; i < _aspectsInDeck.Count; i++)
        {
            if (_aspectsInDeck[i].name == "OldControlAspect")
            {
                cardsToHand.Add(_aspectsInDeck[i]);
                break;
            }
        }

        //add said cards to hand
        _dataHandler.HandData.CardsInHand = cardsToHand;

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (OldCardData card in cardsToHand)
        {
            _cardPrefab.GetComponent<OldCardDisplay>().CardData = card;
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
        _cardPrefab.GetComponent<OldCardDisplay>().CardData = _aspectsInDeck[0];
        Instantiate(_cardPrefab, _dataHandler.HandData.transform);
        
        //check if works (update: it does)
        print(_aspectsInDeck[0].Name);

        _aspectsInDeck.RemoveAt(0);
        _currentDeckSize --;
    }

    public void DrawTwo()
    {
        //get top 2 cards in deck
        List<OldCardData> cardsToHand = _aspectsInDeck.GetRange(0, 2);

        //add said cards to hand
        _dataHandler.HandData.CardsInHand.AddRange(cardsToHand);

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (OldCardData card in cardsToHand)
        {
            _cardPrefab.GetComponent<OldCardDisplay>().CardData = card;
            Instantiate(_cardPrefab, _dataHandler.HandData.transform);

            //check if works (update: it does)
            print(card.Name);
        }

        //remove drawn cards from deck
        _aspectsInDeck.RemoveRange(0, 2);
        _currentDeckSize -= 2;
    }
}
