using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [Header("Sin Movement")]
    [SerializeField] private bool movesHorizontally;
    [SerializeField] private bool movesVertically;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float offset;

    private Vector3 startPosition;

    [Header("Rotation")]
    [SerializeField] private bool randomRotate;
    [SerializeField] private float randomRotateMin;
    [SerializeField] private float randomRotateMax;
    private float rotationSpeed;
    [SerializeField] private bool rotateClockwise;
    [SerializeField] private float clockwiseRotationSpeed;
    

    private void Start()
    {
        rotationSpeed = Random.Range(randomRotateMin, randomRotateMax);
        startPosition = transform.position;
    }

    private void Update()
    {
        if (randomRotate)
        {
            transform.rotation = Quaternion.Euler(1 * Time.time * rotationSpeed, 1 * Time.time * rotationSpeed, 1 * Time.time * rotationSpeed);
        }

        if (rotateClockwise)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 1 * Time.time * -clockwiseRotationSpeed);
        }

        if (movesHorizontally)
        {
            movesVertically = false;
            transform.position = startPosition + new Vector3(offset + Mathf.Sin(Time.time * frequency) * amplitude, 0.0f, 0.0f);
        }

        if (movesVertically)
        {
            movesHorizontally = false;
            transform.position = startPosition + new Vector3(0.0f, offset + Mathf.Sin(Time.time * frequency) * amplitude, 0.0f);
        }
        
    }
}
