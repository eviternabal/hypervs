using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ring : MonoBehaviour
{
    [SerializeField] private int earnValue;

    static public Action<int> CollidesWithRing;
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
        }
    }
}
