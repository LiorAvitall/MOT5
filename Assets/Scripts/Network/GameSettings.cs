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

    private float _randomNum  = 0;

    [SerializeField] private string _nickName = "Player";
    public string NickName => _nickName + _randomNum;

    private void Awake()
    {
        _randomNum = Random.Range(0, 4);
    }
}
