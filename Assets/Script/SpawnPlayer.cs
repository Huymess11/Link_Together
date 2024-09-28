using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] private float minX, minY, maxX, maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));    
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition,Quaternion.identity);
    }
}
