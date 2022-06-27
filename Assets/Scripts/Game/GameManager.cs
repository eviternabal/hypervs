using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToDecrementForward;
    [SerializeField] private float decrementForwardValue;

    private float timerDecrementForward;

    static public Action<float> DecrementShipSpeed;

    public int score;
    void Start()
    {
        score = 0;
        Ring.CollidesWithRing += IncreasePoints;
    }

    private void OnDisable()
    {
        Ring.CollidesWithRing -= IncreasePoints;
    }

    void Update()
    {
        if (timerDecrementForward < timeToDecrementForward)
            timerDecrementForward += Time.deltaTime;
        else
        {
            DecrementShipSpeed?.Invoke(decrementForwardValue);
            timerDecrementForward = 0.0f;
        }
    }

    private void IncreasePoints(int earnValue)
    {
        score += earnValue;
    }
}
