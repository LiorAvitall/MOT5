using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string _gameVersion = "0.0.0";
    public string GameVersion => _gameVersion;

    [SerializeField] private string _nickName = "Player";
    public string NickName => _nickName + PhotonNetwork.LocalPlayer.ActorNumber.ToString();
}
