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
    public string[] levelList;
    public bool gameCanEnd = true;
    private int currentLevel = 0;
    public int levelCount = 0;

    // UI
    public Canvas canvas;
    public EventSystem eventSystem;

    public MenuScript pauseMenuPrefab;
    public MenuScript nextLevelMenuPrefab;
    public GameOverMenu gameOverMenuPrefab;
    public MenuScript startMenuPrefab;

    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        if (levelList.Length == 0)
        {
            //Utils.Error("GameManager: Level List must have at least one level");
            return;
        }
        //Reset();
        //OnPauseMenu();
        OnStartMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Reset()
    {
        Resume();
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
        //StartCoroutine(LoadLevelAsync(0));
    }

    public void NextLevel()
    {
        Pause();
        StartCoroutine(LoadLevelAsync((currentLevel + 1) % levelList.Length));
    }

    private IEnumerator LoadLevelAsync(int nextLevel)
    {
        var asUnload = SceneManager.UnloadSceneAsync(levelList[currentLevel]);
        currentLevel = nextLevel;
        var asLoad = SceneManager.LoadSceneAsync(levelList[currentLevel], LoadSceneMode.Additive);

        while (!asUnload.isDone || !asLoad.isDone)
        {
            yield return null;
        }
        Reset();
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
        if (currentLevel == levelList.Length - 1)
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
