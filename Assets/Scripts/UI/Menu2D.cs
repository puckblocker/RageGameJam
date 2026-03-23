using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2D : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Animator sceneAnim;
    public void QuitGameOver()
    {
        StartCoroutine(FadeCooldown());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void StartGame()
    {
        StartCoroutine(FadeCooldown());
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

    private IEnumerator FadeCooldown()
    {
        sceneAnim.Play("Crossfade_End");
        yield return new WaitForSeconds(2);
    }
}
