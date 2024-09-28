using TMPro;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private GameObject shieldPrefab;

    void Start()
    {
       playerName.text = photonView.Owner.NickName;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shield"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.PickUpItem);
            shieldPrefab.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("bomb"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Explore);
            StunnedEffect.Instance.Effect();
            DestroyAllVirus();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enermy")|| collision.CompareTag("Bullet"))
        {
            photonView.RPC("GameOverRPC", RpcTarget.All);
        }
    }
    private void DestroyAllVirus()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Enermy");
        foreach(GameObject virus in viruses)
        {
            GameManager.Instance.SetScore();
            Destroy(virus);
            GameManager.Instance.SpawnVirusBoom(virus.transform);
        }
    }
    [PunRPC]
    private void GameOverRPC()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.GameOver);
        GameManager.Instance.GameOver();
    }
    
}
