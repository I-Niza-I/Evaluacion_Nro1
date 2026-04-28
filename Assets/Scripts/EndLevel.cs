using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private GameController gc;

    void Start()
    {
        gc = GameController.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (!gc.CanFinishLevel())
        {
            Debug.Log("Faltan dinos por derrotar!");
            gc.FlashDinosCount();
            return;
        }

        if (CompareTag("EndLevel"))
        {
            gc.WinLevel();
        }
    }

   
}