using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enermy"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.VirusDestroy);
            Destroy(collision.gameObject);
            GameManager.Instance.SpawnVirusBoom(collision.transform);
            GameManager.Instance.SetScore();

        }
    }
}
