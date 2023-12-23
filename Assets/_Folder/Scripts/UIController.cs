using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        Application.targetFrameRate = 60;
        _Score = 0;
        _blobbyController = BlobbyController.instance;
        
        _blobbyController.onDeath.AddListener(GameOver);
        _blobbyController.onScore.AddListener(UpdateScore);
        
        AdsManager.instance.RequestBannerAd();
        AdsManager.instance.onInterstitialAdClosed.AddListener(ReloadScene);
        
        AdsManager.instance.onRewardSuccesfull.AddListener(Reward);
    }


    private void UpdateScore(int score)
    {
        _Score += score;
        scoreText.text = "SCORE: " + _Score.ToString();
        scoreText.transform.DOScale(new Vector3(1.04f,1.04f,1.04f),0.1f).OnComplete(() => scoreText.transform.DOScale(Vector3.one,0.1f));
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
        StartCoroutine(Panel());
    }

    private IEnumerator Panel()
    {
        gameStarted = false;
        yield return new WaitForSeconds(.3f);
        gameoverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
       
        gameOverScore.text = "SCORE: " + _Score;
        if (_Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore",_Score);
        }
        gameOverHighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
    }
    
    public void RestartGame()
    {
        AdsManager.instance.ShowInterstitialAd();
    }
    
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WatchRewarded()
    {
        AdsManager.instance.ShowRewardedAd();
    }

    public void Reward()
    {
        gameStarted = true;
        gameoverPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        _blobbyController.OpenBird();
    }

}
