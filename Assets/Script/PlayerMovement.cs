using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float durationSpeed = 5f;
    TrailRenderer trailRenderer;
    private Camera mainCamera;
    float originSpeed;
    Coroutine speedCoroutine;
    

    PhotonView pview;

    void Start()
    {
        mainCamera = Camera.main;
        pview = GetComponent<PhotonView>();
        trailRenderer = GetComponent<TrailRenderer>();
        originSpeed = speed;
    }

    void Update()
    {
        if (pview.IsMine)
        {
            Move(); 
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 newPosition = new Vector2(transform.position.x + moveX * speed * Time.deltaTime, transform.position.y + moveY * speed * Time.deltaTime);
        newPosition = ClampToCameraBounds(newPosition);
        transform.position = newPosition;
    }

    private Vector2 ClampToCameraBounds(Vector2 position)
    {
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        float clampedX = Mathf.Clamp(position.x, bottomLeft.x, topRight.x);
        float clampedY = Mathf.Clamp(position.y, bottomLeft.y, topRight.y);

        return new Vector2(clampedX, clampedY);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("flash"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.PickUpItem);
            Destroy(collision.gameObject);
            if(speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            speedCoroutine = StartCoroutine(SetSpeedLocalTime());
        }
    }
    IEnumerator SetSpeedLocalTime()
    {
        speed = originSpeed + 2f;
        trailRenderer.enabled = true;
        yield return new WaitForSeconds(durationSpeed);
        trailRenderer.enabled = false;
        speed = originSpeed;
        speedCoroutine = null;

    }
}
