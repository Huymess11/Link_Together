 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VirusGun : MonoBehaviour
{
    [SerializeField] private List<Transform> firePoses = new List<Transform>();
    [SerializeField] private GameObject bulletPrefab;
    PhotonView photonView;
    float coolDownTime = 3f;
    float coolDownLoad = 0;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        coolDownLoad += Time.deltaTime;
        if (photonView.IsMine) 
        {
            if (coolDownLoad > coolDownTime)
            {
                photonView.RPC("FireBullet", RpcTarget.OthersBuffered); 
                coolDownLoad = 0;
            }
        }
    }
    [PunRPC]
    private void FireBullet()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.FireBullet);
        for (int i = 0; i< firePoses.Count; i++)
        {
           PhotonNetwork.Instantiate(bulletPrefab.name, firePoses[i].position, firePoses[i].localRotation);
        }
    }
}
