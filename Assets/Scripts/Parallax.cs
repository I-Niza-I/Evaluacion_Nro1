using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float leftBound;
    private float rightBound;
    private float bgHalfWidth;
    private float camHalfWidth;

    void Start()
    {
        GameController gc = GameController.GetInstance();
        leftBound = gc.GetLeftBound();
        rightBound = gc.GetRightBound();

        bgHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        // Mitad del ancho visible de la cámara en unidades del mundo
        camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        float camX = Camera.main.transform.position.x;

        float camLeftLimit = leftBound + camHalfWidth;
        float camRightLimit = rightBound - camHalfWidth;

        float t = Mathf.InverseLerp(camLeftLimit, camRightLimit, camX);

        float newX = Mathf.Lerp(leftBound + bgHalfWidth, rightBound - bgHalfWidth, t);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
