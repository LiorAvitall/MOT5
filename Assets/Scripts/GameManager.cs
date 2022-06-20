using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

enum AspectOfElement { Light = 0, Death = 1, Destruction = 3, Life = 4, Control = 5 }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Photon
    [SerializeField] private PhotonView _photonView;
    #endregion

    [SerializeField] private GameObject _playerBoard, _eventHandler;
    [SerializeField] private Transform _gameCanvas;

    [SerializeField] public List<PlayerController> PlayerList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //_gameCanvas.position = Vector2.zero;
        //Duel();
    }

    private void Duel()
    {
        //InitializeDuel();
    }

    private void Brawl()
    {
        // Run [1 vs 1 vs 1] & [1 vs 1 vs 1 vs 1] envirnoments & rules
    }

    private void TeamFight()
    {
        // Run [2 vs 2] envirnoment & rules
    }
}
