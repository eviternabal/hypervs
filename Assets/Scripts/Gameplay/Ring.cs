using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ring : MonoBehaviour
{
    [SerializeField] private int earnValue;
    [SerializeField] private float earnSpeed;
    [SerializeField] private bool isFirstRing;

    static public Action<int> CollidesWithRing;
    static public Action<float> IncrementSpeed;
    static public Action<bool> StartCountTimeToDecrement;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollidesWithRing?.Invoke(earnValue);
            Debug.Log("Suma velocidad");
            IncrementSpeed?.Invoke(earnSpeed);
            if (isFirstRing)
            {
                Debug.Log("Comienza a contar");
                StartCountTimeToDecrement?.Invoke(true);
            }
        }
    }
}
