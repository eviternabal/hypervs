using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        
    }

    private void IncreasePoints(int earnValue)
    {
        score += earnValue;
    }
}
