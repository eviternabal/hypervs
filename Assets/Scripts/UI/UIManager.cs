using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject scoreGO;
    [SerializeField] private GameObject accelerationGO;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text accelerationText;
    [SerializeField] private TMP_Text gameOverScoreText;

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

        gameOverScoreText.text = "Score: " + gameManager.GetScore;

        pauseButton.SetActive(!pauseButton.gameObject.activeSelf);
        scoreGO.SetActive(!scoreGO.gameObject.activeSelf);
        accelerationGO.SetActive(!accelerationGO.gameObject.activeSelf);
    }

    public void SendPause(bool value)
    {
        ActivePausePanel();
        gameManager.PauseGame(value);
    }

    private void ActivePausePanel()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            pausePanel.SetActive(!pausePanel.gameObject.activeSelf);
            pauseButton.SetActive(!pauseButton.gameObject.activeSelf);
            scoreGO.SetActive(!scoreGO.gameObject.activeSelf);
            accelerationGO.SetActive(!accelerationGO.gameObject.activeSelf);
        }
    }
}
