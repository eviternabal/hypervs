using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using NAMESPACENAME.Gameplay.Ship;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToDecrementForward;
    [SerializeField] private float decrementForwardValue;
    [SerializeField] private float minAccelerationToLose;
    [SerializeField] private float pointsXAccelerationMod;
    [SerializeField] private ShipController shipController;

    static public Action<float> DecrementShipSpeed;

    public int GetScore { get { return score; } }
    public float GetAcceleration { get { return shipController.GetForwardSpeed; } }

    private float originalShipSpeed;
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
        originalShipSpeed = GetAcceleration;
    }

    private void OnDisable()
    {
        Ring.CollidesWithRing -= IncreasePoints;
        Ring.StartCountTimeToDecrement -= SetCanCountTime;
    }

    void Update()
    {
        StartCount();
        CheckCanLose();
    }

    private void IncreasePoints(int earnValue)
    {
        float speedPercentage = GetAcceleration / originalShipSpeed;
        score += earnValue * (int)(speedPercentage * pointsXAccelerationMod);
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
    [SerializeField] private float minToUnlockLose;
    public bool canLose = false;

    private void CheckCanLose()
    {
        if (shipController.GetForwardSpeed >= minToUnlockLose)
        {
            canLose = true;
        }
    }

    public void CheckGameOver()
    {


        if (shipController.GetForwardSpeed <= minAccelerationToLose && canLose)
        {
            uiManager.ActiveGameOverPanel();
            PauseGame(true);
            FindObjectOfType<AudioManager>().Play("GameOverSound");
        }

        if (shipController.GetForwardSpeed <= 0f)
        {
            uiManager.ActiveGameOverPanel();
            PauseGame(true);
            FindObjectOfType<AudioManager>().Play("GameOverSound");
        }
    }

    public void GameOverByHits()
    {
        uiManager.ActiveGameOverPanel();
        PauseGame(true);
        FindObjectOfType<AudioManager>().Play("GameOverSound");
    }

    public void PauseGame(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
