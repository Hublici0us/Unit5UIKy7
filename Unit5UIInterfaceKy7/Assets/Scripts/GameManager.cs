using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public float spawnRate = 1;
    private int score;
    public bool gameOver;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI finalScoreText;
    public RawImage gameOverImage;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (!gameOver)
        {
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

    public void StartGame()
    {
        gameOver = false;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        scoreText.gameObject.SetActive(true);
        titleScreen.SetActive(false);
    }
}
