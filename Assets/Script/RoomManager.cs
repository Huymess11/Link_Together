using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;  
            PhotonNetwork.CurrentRoom.IsVisible = false;   
        }
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LoadingScene");
    }
}
