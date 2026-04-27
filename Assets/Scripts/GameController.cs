using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private static GameController instance = null;                   // singleton del controlador del juego
    public static GameController GetInstance() { return instance; }

    [SerializeField] private GameObject gameUI = null;             
    [SerializeField] private GameObject player = null;              
    [SerializeField] private int maxVidas = 0;                         
    [SerializeField] private AudioClip audioGameOver = null;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private int totalDinos = 0;

    private bool isFlashingDinos = false;


    private AudioSource audioSource;

    private Text lives;
    private Text points;
    private Text dinos;
    private GameObject gameOver;
    private int livesNum = 0;
    private int pointsNum = 0;
    private int dinosNum = 0;

    void Awake()
    {
        // Patron Singleton: si ya existe una instancia, se destruye el nuevo objeto, sino se asigna la instancia al nuevo objeto
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        audioSource = GetComponent<AudioSource>();
        livesNum = maxVidas;

        lives = gameUI.transform.Find("Lives").gameObject.GetComponent<Text>();
        points = gameUI.transform.Find("Rewards").gameObject.GetComponent<Text>();
        dinos = gameUI.transform.Find("Dinos").gameObject.GetComponent<Text>();
        gameOver = gameUI.transform.Find("GameOver").gameObject;

        lives.text = livesNum.ToString();
        points.text = pointsNum.ToString();
        dinos.text = dinosNum.ToString();
        gameOver.SetActive(false);
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    
    public int RestLives()
    {
        try
        {
            if (livesNum > 0)
            {
                livesNum--;
                if (livesNum <= 0)
                {
                    GameOver();
                }
                lives.text = livesNum.ToString();
            }
        }
        catch (Exception e)
        {
            print(e);
        }

        return livesNum;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");

        gameOver.SetActive(true);

        // Detiene el tiempo
        Time.timeScale = 0f;

        player.GetComponent<Player_Movement>().enabled = false;

        if (audioGameOver != null)
        {
            audioSource.clip = audioGameOver;
            audioSource.Play();
        }
    }

    public float GetRightBound()
    {
        return rightBound;
    }
    public float GetLeftBound()
    {
        return leftBound;
    }

    public void AddLives(int n)
    {
        try
        {
            livesNum += n;
            lives.text = livesNum.ToString();
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    public void AddPoints(int n)
    {
        try
        {
            pointsNum += n;
            points.text = pointsNum.ToString();
        }
        catch (Exception e)
        {
            print(e);
        }
    }
    public void AddDinos(int n)
    {
        try
        {
            dinosNum += n;
            dinos.text = dinosNum.ToString();
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    public void FlashDinosRed()
    {
        if (!isFlashingDinos)
        {
            StartCoroutine(FlashDinosCoroutine());
        }
            
    }
    private IEnumerator FlashDinosCoroutine()
{
    isFlashingDinos = true;

    Color originalColor = dinos.color;

    dinos.color = Color.red;
    yield return new WaitForSeconds(1f);

    dinos.color =  Color.white;

    isFlashingDinos = false;
}
    public bool CanFinishLevel()
    {
        return dinosNum >= totalDinos;
    }
}
