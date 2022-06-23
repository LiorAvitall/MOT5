using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentsData : MonoBehaviour
{
    #region Game Components
    private Hand _hand;
    private Battlefield _battlefield;
    private Deck _deck;
    private Tomb _tomb;

    public Hand Hand => _hand;
    public Battlefield Battlefield => _battlefield;
    public Deck Deck => _deck;
    public Tomb Tomb => _tomb;
    #endregion

    [Header("Data Scripts")]
    public Deck DeckData; 
    public Hand HandData; 
    public Battlefield BattlefieldData; 
    public Battlefield OpponentBattlefieldData; 
    public Tomb TombData;

    public GameObject SacrificeOverlay;
    public GameObject LastPlacedCardOnBattelfield;

    public bool IsReviving = false;
    public bool IsSacrificing = false;
    public bool IsDestroying = false;
    //public bool IsDrawingWithLight = false;
}
