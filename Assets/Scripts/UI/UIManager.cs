using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void ActiveGameOverPanel()
    {
        gameOverPanel.SetActive(!gameOverPanel.gameObject.activeSelf);
    }
}
