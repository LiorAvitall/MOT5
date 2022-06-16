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

    private void InitializeDuel()
    {
        if (_photonView.IsMine)
        {
            GameObject player1EventHandlerGO = PhotonNetwork.Instantiate("Event Handler", Vector3.zero, Quaternion.identity);
            //player1EventHandlerGO.name = "Player1";

            NewEventHandler player1EventHandler = player1EventHandlerGO.GetComponentInParent<NewEventHandler>();

            //player1EventHandler.MyDeck = new NewDeck();
            //player1EventHandler.MyHand = new NewHand();
            //player1EventHandler.MyBattlefield = new NewBattlefield();
            //player1EventHandler.MyTomb = new NewTomb();

            GameObject player1Board = PhotonNetwork.Instantiate("Player Board", Vector3.zero, Quaternion.identity);
            player1Board.name = "Player1 Board";
            player1Board.transform.parent = _gameCanvas;

            GameObject player2Board = PhotonNetwork.Instantiate("Player Board", Vector3.zero, Quaternion.identity);
            player2Board.name = "Player2 Board";
            player2Board.transform.parent = _gameCanvas;
            player2Board.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);

        }
        else
        {
            //GameObject player2EventHandler = PhotonNetwork.Instantiate("Event Handler", new Vector3(0f, 0f, 0f), Quaternion.identity);
            //player2EventHandler.name = "Player2";

            GameObject player2Board = Instantiate(_playerBoard, _gameCanvas);
            player2Board.name = "Player2 Board";

            GameObject player1Board = Instantiate(_playerBoard, _gameCanvas);
            player1Board.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
            player1Board.name = "Player1 Board";
        }
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
