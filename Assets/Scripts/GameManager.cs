using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Player stuff
    public GameObject player;


    // Level system
    public bool gameCanEnd = true;


    // UI
    public Canvas canvas;
    public EventSystem eventSystem;

    public MenuScript pauseMenuPrefab;
    public MenuScript nextLevelMenuPrefab;
    public GameOverMenu gameOverMenuPrefab;
    public MenuScript startMenuPrefab;

    private bool paused;

    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        OnStartMenu();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Pause();
        SceneManager.LoadScene("Level1",LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        Pause();
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }

    public void PlayerTakeDamage()
    {
        OnDeath();
    }

    public void OnStartMenu()
    {
        var menu = Instantiate(startMenuPrefab, canvas.transform);
        eventSystem.SetSelectedGameObject(menu.GetComponentInChildren<Button>().gameObject);
    }

    public void OnPauseMenu()
    {
        if (paused) { return; }
        
        paused = true;
        Pause();

        var menu = Instantiate(pauseMenuPrefab, canvas.transform);
        eventSystem.SetSelectedGameObject(menu.GetComponentInChildren<Button>().gameObject);
    }

    public void OnNextLevelMenu()
    {
        if (nextLevel == null || nextLevel == "")
        {
            // Last level, win!
            OnWin();
            return;
        }

        Pause();

        var menu = Instantiate(nextLevelMenuPrefab, canvas.transform);
        eventSystem.SetSelectedGameObject(menu.GetComponentInChildren<Button>().gameObject);
    }

    void OnDeath()
    {
        if (!gameCanEnd) return;

        Pause();

        GameOverMenu gameOver = Instantiate(gameOverMenuPrefab, canvas.transform);
        eventSystem.SetSelectedGameObject(gameOver.GetComponentInChildren<Button>().gameObject);

        gameOver.Lost();
    }

    public void OnWin()
    {
        Pause();

        if (!gameCanEnd)
        {
            Restart();
        }

        GameOverMenu gameOver = Instantiate(gameOverMenuPrefab, canvas.transform);
        eventSystem.SetSelectedGameObject(gameOver.GetComponentInChildren<Button>().gameObject);

        gameOver.Won();
    }
}
