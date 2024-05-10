using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public float spawnRate = 1.5f;
    private int score;
    public int lives = 5;
    public bool gameOver;
    public bool titleOn;
    public bool pausedOn;
    public bool freeplayOn = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI finalScoreText;
    public RawImage gameOverImage;
    public RawImage PauseMenu;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;
    public List<GameObject> titleSpawnedPrefabs;

    public Slider MusicVol;
    public Slider SFXVol;
    public Toggle freeplayToggle;
    public Button exitButton;
    public Button PauseButton;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        freeplayOn = true;
        freeplayToggle.isOn = false;
        titleOn = true;
        StartCoroutine(titleSpawner());

        MusicVol.value = 0.5f;
        SFXVol.value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(3);
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

    public void OnToggleChange()
    {
        freeplayOn = !freeplayOn;
    }
}
