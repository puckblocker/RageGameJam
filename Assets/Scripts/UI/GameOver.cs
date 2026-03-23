using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject gameOverMenu;
    public void gameEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverMenu.SetActive(true);
        // Update Score In UI
        scoreUI.text = "Score: " + PlayerBase.scoreVal.ToString();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
