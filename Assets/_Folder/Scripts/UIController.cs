using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;


    private BlobbyController _blobbyController;


    private void Start()
    {
        _blobbyController = BlobbyController.instance;
        
        _blobbyController.onDeath.AddListener(GameOver);
    }

    private void GameOver()
    {
        gameoverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
