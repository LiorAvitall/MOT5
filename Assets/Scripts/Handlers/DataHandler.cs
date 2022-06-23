using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
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
