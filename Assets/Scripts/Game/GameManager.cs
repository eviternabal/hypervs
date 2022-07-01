using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using NAMESPACENAME.Gameplay.Ship;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToDecrementForward;
    [SerializeField] private float decrementForwardValue;
    [SerializeField] private ShipController shipController;

    static public Action<float> DecrementShipSpeed;

    public int GetScore { get { return score; } }

    private float timerDecrementForward;
    private bool canCountTime;
    private int score;
    private UIManager uiManager;

    void Start()
    {
        Time.timeScale = 1;
        uiManager = FindObjectOfType<UIManager>();
        score = 0;
        Ring.CollidesWithRing += IncreasePoints;
        Ring.StartCountTimeToDecrement += SetCanCountTime;
    }

    private void OnDisable()
    {
        Ring.CollidesWithRing -= IncreasePoints;
        Ring.StartCountTimeToDecrement -= SetCanCountTime;
    }

    void Update()
    {
        StartCount();
    }

    private void IncreasePoints(int earnValue)
    {
        score += earnValue;
    }

    private void SetCanCountTime(bool value)
    {
        canCountTime = value;
    }

    private void StartCount()
    {
        if (canCountTime)
        {
            if (timerDecrementForward < timeToDecrementForward)
                timerDecrementForward += Time.deltaTime;
            else
            {
                DecrementShipSpeed?.Invoke(decrementForwardValue);
                timerDecrementForward = 0.0f;
                CheckGameOver();
            }
        }
    }

    private void CheckGameOver()
    {
        if (shipController.GetForwardSpeed <= 0)
        {
            uiManager.ActiveGameOverPanel();
            PauseGame(true);
        }
    }

    public void PauseGame(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
    }
}
