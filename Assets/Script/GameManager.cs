using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private GameObject virusBoom;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreFinal;
    [SerializeField] private TextMeshProUGUI bestScore;
    int score;
    PhotonView photonView;


    private void Awake()
    {
       photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        
    }

    public void SetScoreText()
    {
        scoreTxt.text = score.ToString();
    }
    public void CheckToSetBestScore()
    {
        if (score > PlayerPrefs.GetInt("Best_Score"))
        {
            PlayerPrefs.SetInt("Best_Score", score);
        }
    }
    public void SetScore()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("UpdateScore", RpcTarget.AllBuffered);
        }
    }
    public void SpawnVirusBoom(Transform value)
    {
        Instantiate(virusBoom, value.position, Quaternion.identity);
    }
    [PunRPC]
    private void UpdateScore()
    {
        score++;
        SetScoreText();
        CheckToSetBestScore();
    }
    
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreFinal.text = score.ToString();
        bestScore.text = PlayerPrefs.GetInt("Best_Score").ToString();
        Time.timeScale = 0f;
    }
    public void Home()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        photonView.RPC("LeaveRoom", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LoadingScene");
    }
}
