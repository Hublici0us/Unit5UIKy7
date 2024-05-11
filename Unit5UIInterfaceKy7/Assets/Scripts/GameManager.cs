using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public float spawnRate = 1.5f;
    private int score;
    public int lives = 5;
    public float gameTimer;
    public bool gameOver;
    public bool titleOn;
    public bool pausedOn;
    public bool gameStarted;
    public bool boxing;
    public bool freeplayOn = false;
    public bool timerOn;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameTimeText;
    public RawImage gameOverImage;
    public RawImage PauseMenu;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;

    public List<GameObject> titleSpawnedPrefabs;

    public Slider MusicVol;
    public Slider SFXVol;
    public Toggle freeplayToggle;
    public Toggle timerToggle;
    public Button exitButton;
    public Button PauseButton;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        boxing = false;
        gameStarted = false;
        gameTimer = 60;
        freeplayOn = true;
        timerOn = true;
        timerToggle.isOn = true;
        freeplayToggle.isOn = false;
        titleOn = true;
        StartCoroutine(titleSpawner());

        MusicVol.value = 0.5f;
        SFXVol.value = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateGameTimer();
    }

    private void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = ("THREE");
        yield return new WaitForSeconds(1);
        countdownText.text = ("TWO");
        yield return new WaitForSeconds(1);
        countdownText.text = ("ONE");
        yield return new WaitForSeconds(1);
        countdownText.text = ("GO");
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);
        gameStarted = true;
        while (!gameOver)
        {
            Time.timeScale = 1;
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreAdd)
    {
        score += scoreAdd;
        scoreText.text = ("Score: " + score);
    }

    public void GameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        gameOver = true;
        finalScoreText.text = ("Score: " + score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        gameOver = false;
        titleOn = false;

        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        scoreText.gameObject.SetActive(true);
        titleScreen.SetActive(false);
        exitButton.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(true);
        if (!freeplayOn)
        {
            livesText.gameObject.SetActive(true);
            UpdateLives();
        }
        if (timerOn)
        {
            gameTimeText.gameObject.SetActive(true);
            gameTimeText.text = ("Time: " + gameTimer);
        }
        
    }

    IEnumerator titleSpawner()
    {
        while (titleOn)
        {
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(1);
            int startVariety = Random.Range(0, titleSpawnedPrefabs.Count);
            Instantiate(titleSpawnedPrefabs[startVariety], new Vector3(Random.Range(-6, 6), 15, -2), Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360)));
        }
    }

    public void UpdateLives()
    {
        livesText.text = ("Lives: " + lives);
    }

    public void PauseGame()
    {
        if (!pausedOn)
        {
            pausedOn = true;
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausedOn = false;
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnFreeplayToggleChange()
    {
        freeplayOn = !freeplayOn;
    }

    public void OnTimerToggleChange ()
    {
        timerOn = !timerOn;
        if (!timerOn)
        {
            timerText.gameObject.SetActive(false);
        }
        if (timerOn)
        {
            timerText.gameObject.SetActive(true) ;
        }
    }

    public void TimerChangeUp(float value)
    {
        if (gameTimer >= 180)
        {
            return;
        }
        else
        {
            gameTimer += value;
            timerText.text = ($"{Mathf.FloorToInt(gameTimer)}");
        }
    }

    public void TimerChangeDown(float value)
    {
        if (gameTimer <= 15)
        {
            return;
        }
        else
        {
            gameTimer += value;
            timerText.text = ($"{Mathf.FloorToInt(gameTimer)}");
        }
    }

    public void UpdateGameTimer()
    {
        if (!titleOn && !gameOver && gameStarted && timerOn)
        {
            if (gameTimer < 0.5f)
            {
                GameOver();
                return;
            }
            gameTimer -= Time.deltaTime;
            gameTimeText.text = ("Time: " + Mathf.FloorToInt(gameTimer));
        }
    }
}
