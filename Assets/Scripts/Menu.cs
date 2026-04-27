using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string game = null;
    [SerializeField] string Instructions = null;
    [SerializeField] string Credits = null;

    public void InitGame()
    {
        print("Game Button");
        SceneManager.LoadScene(game);
    }

    public void InitInstructions()
    {
        print("Instructions Button");
        SceneManager.LoadScene(Instructions);
    }

    public void InitCredits()
    {
        print("Credits Button");
        SceneManager.LoadScene(Credits);
    }
}
