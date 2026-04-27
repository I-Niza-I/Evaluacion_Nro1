using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public GameObject objectToMove = null;
    public float speed = 1.0f;
    public Transform startPoint;
    public Transform endPoint;

    private Vector3 moveTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTo = endPoint.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, moveTo, speed * Time.deltaTime);
        if (objectToMove.transform.position == endPoint.position)
        {
            moveTo = startPoint.position;
        }
        else if (objectToMove.transform.position == startPoint.position)
        {
            moveTo = endPoint.position;
        }
    }
}
