using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NewDeck : MonoBehaviour
{
    #region Photon
    [SerializeField] private PhotonView _photonView;
    #endregion

    [Header("AspectList")]
    [SerializeField] private List<CardData> _aspectsInDeck = new List<CardData>(25);

    [Header("AspectPrefab")]
    [SerializeField] private GameObject _cardPrefab;

    [Header("AspectData")]
    [SerializeField] private CardData _lightCard;

    [SerializeField]
    private CardData _deathCard, _destructionCard, _lifeCard, _controlCard;

    private int _maxDeckSize = 25, _currentDeckSize;

    public Button OnClickDeck;

    private void Start()
    {
        StartCoroutine(ShuffleDeck());
    }

    private IEnumerator ShuffleDeck()
    {
        yield return new WaitForSeconds(0.1f);

        if (_photonView.IsMine)
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
                CardData temp = _aspectsInDeck[i];
                int randomIndex = UnityEngine.Random.Range(i, _aspectsInDeck.Count);
                _aspectsInDeck[i] = _aspectsInDeck[randomIndex];
                _aspectsInDeck[randomIndex] = temp;
            }
        }
    }

    public void InitializeGame()
    {
        if (_photonView.IsMine)
        {
            //get top 4 cards in deck
            List<CardData> cardsToHand = _aspectsInDeck.GetRange(0, 4);

            //add said cards to hand
            //_dataHandler.HandData.CardsInHand.AddRange(cardsToHand);

            //loops through said cards's data, reads it and creates a prefab based on that data in the hand
            foreach (CardData cardata in cardsToHand)
            {
                _cardPrefab.GetComponent<CardDisplay>().CardData = cardata;
                Instantiate(_cardPrefab, NewEventHandler.Instance.MyHand.transform);

                //check if works (update: it does)
                print(cardata.Name);
            }

            //remove drawn cards from deck
            _aspectsInDeck.RemoveRange(0, 4);
            _currentDeckSize -= 4;
        }   
    }

    public void DrawCard()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("Drawing Card");
            //get top card in deck & adds it to the hand
            NewEventHandler.Instance.MyHand.CardsInHand.Add(_aspectsInDeck[0]);

            //reads said card data and creates a prefab based on that data in the hand
            _cardPrefab.GetComponent<CardDisplay>().CardData = _aspectsInDeck[0];

            //Instantiate(_cardPrefab, NewEventHandler.Instance.MyHand.transform);
            GameObject drawnCard = PhotonNetwork.Instantiate("Aspect Prefab", Vector2.zero, Quaternion.identity);
            drawnCard.transform.parent = NewEventHandler.Instance.MyHand.transform;
            //check if works (update: it does)
            print(_aspectsInDeck[0].Name);

            _aspectsInDeck.RemoveAt(0);
            _currentDeckSize--;
        }
        else
        {
            Debug.Log("This is not your deck");
        }
    }

    public void DrawTwo()
    {
        if (_photonView.IsMine)
        {
            //get top 2 cards in deck
            List<CardData> cardsToHand = _aspectsInDeck.GetRange(0, 2);

            //add said cards to hand
            NewEventHandler.Instance.MyHand.CardsInHand.AddRange(cardsToHand);

            //loops through said cards's data, reads it and creates a prefab based on that data in the hand
            foreach (CardData card in cardsToHand)
            {
                _cardPrefab.GetComponent<CardDisplay>().CardData = card;
                Instantiate(_cardPrefab, NewEventHandler.Instance.MyHand.transform);

                //check if works (update: it does)
                print(card.Name);
            }

            //remove drawn cards from deck
            _aspectsInDeck.RemoveRange(0, 2);
            _currentDeckSize -= 2;
        }
    }
}
