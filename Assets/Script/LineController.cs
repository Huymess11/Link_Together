using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject[] players;
    public GameObject colliderObject; 

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            UpdateLine();
            UpdateCollider();
        }
    }

    private void UpdateLine()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 2)
        {
            lineRenderer.SetPosition(0, players[0].transform.position);
            lineRenderer.SetPosition(1, players[1].transform.position);
        }
    }

    private void UpdateCollider()
    {
        if (colliderObject != null && players.Length == 2)
        {
            Vector3 start = players[0].transform.position;
            Vector3 end = players[1].transform.position;
            colliderObject.transform.position = (start + end) / 2;
            colliderObject.transform.up = end - start;
            float distance = Vector3.Distance(start, end);
            colliderObject.transform.localScale = new Vector3(0.2f, distance, 1);
        }
    }
}
