using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private GameController gc;

    void Start()
    {
        gc = GameController.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (CompareTag("Reward"))
        {
            gc.AddPoints(value);
            Debug.Log("Recolectado Reward!");
        }
        else if (CompareTag("Life"))
        {
            gc.AddLives(value);
            Debug.Log("Recolectado Life!");
        }

        Debug.Log("Tag del objeto: " + gameObject.tag);

        Destroy(gameObject);
    }
}