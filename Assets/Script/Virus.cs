using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviourPun
{
    private Vector3 targetPosition;
    [SerializeField] float speed = 1f;
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)  
        {
            SetRandomTarget();
        }
    }

    void Update()
    {
        MoveTowardsTarget();
    }
    private void MoveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (PhotonNetwork.IsMasterClient) 
            {
                SetRandomTarget();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
    public void SetRandomTarget()
    {
        Vector3 randomScreenPoint = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        Vector3 newTargetPosition = Camera.main.ScreenToWorldPoint(randomScreenPoint);
        newTargetPosition.z = 0;
        photonView.RPC("SetTargetPosition", RpcTarget.All, newTargetPosition);
    }
    [PunRPC]
    public void SetTargetPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bubble"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.VirusDestroy);
            collision.gameObject.SetActive(false);
            GameManager.Instance.SetScore();
            GameManager.Instance.SpawnVirusBoom(gameObject.transform);
            Destroy(gameObject);
        }
    }
}
