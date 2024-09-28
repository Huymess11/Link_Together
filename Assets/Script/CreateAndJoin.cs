using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField createInput;
    [SerializeField] private InputField joinInput;
    [SerializeField] private InputField nameInput;

    private void Start()
    {
        if (PlayerPrefs.HasKey("nameKey"))
        {
            nameInput.text = PlayerPrefs.GetString("nameKey");
        }
        if (PhotonNetwork.InLobby)
        {
            Debug.Log("Reconnected to Lobby");
        }
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            return;
        }
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            return;
        }

        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;
            PlayerPrefs.SetString("nameKey", nameInput.text);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);
        }
        else
        {
            Debug.Log("Please enter a name.");
        }
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            return;
        }
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            return;
        }


        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;
            PlayerPrefs.SetString("nameKey", nameInput.text);
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.Log("Please enter a name.");
        }
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Game");
        }
        else
        {
            Debug.Log("Waiting for player...");
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
