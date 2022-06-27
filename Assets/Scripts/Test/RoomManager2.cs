using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager2 : MonoBehaviour
{
    [SerializeField] Text roomName;

    private void Awake()
    {
        //instantiate player
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            PhotonNetwork.Instantiate("Player", new Vector3(2, 0, 0), Quaternion.identity);
        else
            PhotonNetwork.Instantiate("Player", new Vector3(-2, 0, 0), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}