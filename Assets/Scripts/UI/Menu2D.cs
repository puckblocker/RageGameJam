using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2D : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public void QuitGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
