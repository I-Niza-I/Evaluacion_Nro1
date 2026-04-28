using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private string retryLevel;
    [SerializeField] private string backToMenu;
    [SerializeField] private string nextLevel;
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(retryLevel);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(backToMenu);
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevel);
    }
}