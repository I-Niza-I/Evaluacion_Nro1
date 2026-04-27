using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private float leftBound;
    private float rightBound;

    private Vector3 offset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController gc = GameController.GetInstance();

        player = gc.GetPlayer();
        offset = transform.position - player.transform.position;
        leftBound = gc.GetLeftBound();
        rightBound = gc.GetRightBound();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float verticalHeightSeen = Camera.main.orthographicSize * 2.0f;
        float verticalWidthSeen = verticalHeightSeen * Camera.main.aspect;
        float dx = verticalWidthSeen / 2.0f;

        Vector3 newPos = player.transform.position + offset;
        newPos.y = transform.position.y;
        // Limitar movimiento horizontal de la camara
        newPos.x = Mathf.Clamp(newPos.x, leftBound + dx, rightBound - dx);

        transform.position = newPos;
    }
}
