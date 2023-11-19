using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("Gameover Panel")]
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private TextMeshProUGUI gameOverHighScore;


    [Header("Start Panel")] 
    [SerializeField] private GameObject startPanel;
    
    private BlobbyController _blobbyController;

    private int _Score;

    public static bool gameStarted = false;

    private void Start()
    {
        _Score = 0;
        _blobbyController = BlobbyController.instance;
        
        _blobbyController.onDeath.AddListener(GameOver);
        _blobbyController.onScore.AddListener(UpdateScore);
    }


    private void UpdateScore(int score)
    {
        _Score += score;
        scoreText.text = "SCORE: " + _Score.ToString();
        
    }

    public void StartGame()
    {
        gameStarted = true;
        scoreText.gameObject.SetActive(true);
        startPanel.SetActive(false);
        _blobbyController.rb.isKinematic = false;
    }
    private void GameOver()
    {
        gameoverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
       
        gameOverScore.text = "SCORE: " + _Score;
        if (_Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore",_Score);
        }
        gameOverHighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
        
        gameStarted = false;
        
        Time.timeScale = 0;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
