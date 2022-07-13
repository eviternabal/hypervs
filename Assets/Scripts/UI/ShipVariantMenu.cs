using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVariantMenu : MonoBehaviour
{
    [SerializeField] private float rotationAngle;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, rotationAngle * Time.deltaTime, 0.0f));
    }
}
