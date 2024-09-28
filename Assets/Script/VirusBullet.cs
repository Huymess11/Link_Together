using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBullet : MonoBehaviour
{
    public float speed;
    void Start()
    {

    }
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        Invoke("DestroyBullet", 3);
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
