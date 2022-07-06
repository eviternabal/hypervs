using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParticles : MonoBehaviour
{

    [SerializeField] private GameObject[] objectsToEnable;

    [Header("Camera FOV Change")]
    [SerializeField] private Camera cam;
    [SerializeField] private int targetFOV;
    [SerializeField] private float speed;

    private bool changingFov;


    private void Start()
    {
        cam = Camera.main;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changingFov = true;
            if (objectsToEnable != null)
            {

                foreach (GameObject obj in objectsToEnable)
                {
                    obj.SetActive(true);
                }
            }


        }
    }

    private void Update()
    {
        ChangeFov();
    }

    private void ChangeFov()
    {
        if (changingFov)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, speed);
        }

        if (cam.fieldOfView >= 79.99f)
        {
            cam.fieldOfView = 80f;
            changingFov = false;
        }
    }



}
