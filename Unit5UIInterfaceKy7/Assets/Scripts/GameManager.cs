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

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI finalScoreText;
    public RawImage gameOverImage;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;
    public List<GameObject> titleSpawnedPrefabs;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        titleOn = true;
        StartCoroutine(titleSpawner());
        
        
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PauseGame();
            }
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
        livesText.gameObject.SetActive(true);
        titleScreen.SetActive(false);
        UpdateLives();
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
        Time.timeScale = 0;
    }
}
