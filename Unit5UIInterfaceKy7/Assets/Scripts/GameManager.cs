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
    public RawImage gameOverImage;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
