using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static bool GameIsOver = false;

    public GameObject gameOverUI;

    public void GameOver()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Restart()
    {
        GameIsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        GameIsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}