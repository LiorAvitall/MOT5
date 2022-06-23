using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldDataHandler : MonoBehaviour
{
    [Header("Data Scripts")]
    public OldDeck DeckData; 
    public OldHand HandData; 
    public OldBattlefield BattlefieldData; 
    public OldBattlefield OpponentBattlefieldData; 
    public OldTomb TombData;

    public GameObject SacrificeOverlay;
    public GameObject LastPlacedCardOnBattelfield;

    public bool IsReviving = false;
    public bool IsSacrificing = false;
    public bool IsDestroying = false;
    //public bool IsDrawingWithLight = false;
}
