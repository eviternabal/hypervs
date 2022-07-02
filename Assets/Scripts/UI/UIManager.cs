using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text accelerationText;

    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        scoreText.text = "" + gameManager.GetScore;
        accelerationText.text = "" + gameManager.GetAcceleration;
    }

    public void ActiveGameOverPanel()
    {
        gameOverPanel.SetActive(!gameOverPanel.gameObject.activeSelf);
    }

    public void SendPause(bool value)
    {
        ActivePausePanel();
        gameManager.PauseGame(value);
    }

    private void ActivePausePanel()
    {
        pausePanel.SetActive(!pausePanel.gameObject.activeSelf);
    }
}
