using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // si tu veux changer de scène

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused = false;

    public Button ResumeButton;
    public Button QuitButton;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        ResumeButton.onClick.AddListener(Resume);
        QuitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ou KeyCode.P
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

        if (Input.GetMouseButtonDown(0))
        {
            //
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reprend le temps
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Fige le temps
        isPaused = true;
    }

    public void QuitGame()
    {
        // Si tu veux revenir au menu principal
        // SceneManager.LoadScene("MainMenu");

        // Si tu veux juste quitter
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
