using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenager : MonoBehaviour
{
    // Start is called before the first frame update

    public static SceneMenager Instance;
    [SerializeField] private List<Button> mainSceneButtons;
    [SerializeField] private Text goldText;
    [SerializeField] private Text expText;
    [SerializeField] private Text heathPlayer;
    [SerializeField] private Text heathEnemy;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject GameWonScreen;
    [SerializeField] private GameObject GamePausedScreen;
    [SerializeField] private GameObject GamePausedOptionsScreen;
    [SerializeField] private GameObject Hud;
    private bool isGamePaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateUIGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void UpdateUIExp(int exp)
    {
        expText.text = exp.ToString();
    }

    public void UpdateUIPlayerHP(float health)
    {
        heathPlayer.text = health.ToString() + "/500";
    }

    public void UpdateUIEnemyHP(float health)
    {
        heathEnemy.text = health.ToString() + "/500";
    }

    public void SetSceneActive(Winner winner)
    {
        if (winner == Winner.Player)
        {
            SetGameWonScene();
        }
        else if (winner == Winner.Enemy)
        {
            SetGameOverScene();
        }
    }

    private void SetGameOverScene()
    {
        GameOverScreen.SetActive(true);
        SetButtonInteractivity(false);
    }

    private void SetGameWonScene()
    {
        GameWonScreen.SetActive(true);
        SetButtonInteractivity(false);
    }

    private void SetButtonInteractivity(bool active)
    {
        foreach (Button button in mainSceneButtons)
        {
            button.interactable = active;
        }
    }

    public void SetGameScreen()
    {
        SceneManager.LoadScene(1);
        SetButtonInteractivity(true);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void LoadMainMenu()
    {
        // Za³aduj now¹ scenê (Scene 0)
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync((SceneManager.GetActiveScene().buildIndex));
    }

    public void Resume()
    {
        GamePausedScreen.SetActive(false);
        GamePausedOptionsScreen.SetActive(false);
        Hud.SetActive(true);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void Pause()
    {
        Hud.SetActive(false);
        GamePausedScreen.SetActive(true);

        Time.timeScale = 0f;
        isGamePaused = true;
    }
}