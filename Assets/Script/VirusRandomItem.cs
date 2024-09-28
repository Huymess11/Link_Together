using Photon.Pun;
using UnityEngine;

public class VirusRandomItem : MonoBehaviour
{
    [SerializeField] private ItemData data;
    private int totalWeight = 155;
    PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnDestroy()
    {
        photonView.RPC("DropItem", RpcTarget.OthersBuffered);
    }
    [PunRPC]
    private void DropItem()
    {
        float randomValue = Random.Range(0, totalWeight);

        if (randomValue < 25)
        {
            PhotonNetwork.Instantiate(data.data[0].itemPrefab.name, transform.position, Quaternion.identity);
        }
        else if (randomValue < 25 + 50)
        {
            PhotonNetwork.Instantiate(data.data[1].itemPrefab.name, transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(data.data[2].itemPrefab.name, transform.position, Quaternion.identity);
        }
    }
}
