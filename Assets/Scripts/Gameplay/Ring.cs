using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ring : MonoBehaviour
{
    [SerializeField] private int earnValue;
    [SerializeField] private float earnSpeed;

    static public Action<int> CollidesWithRing;
    static public Action<float> IncrementSpeed;
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
        }
    }
}
