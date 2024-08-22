using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingPanel, confirmpanel;

    [Header("Win")]
    [SerializeField] private GameObject winPanel;

    [Header("Lose")]
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        pauseScreen.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        settingPanel.SetActive(false);
        confirmpanel.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Setting(bool status)
    {
        settingPanel.SetActive(status);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Backmainmenu(bool status)
    {
        confirmpanel.SetActive(status);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
}
