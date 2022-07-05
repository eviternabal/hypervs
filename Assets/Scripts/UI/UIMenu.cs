using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject storePanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SwitchPanels()
    {
        menuPanel.SetActive(!menuPanel.gameObject.activeSelf);
        storePanel.SetActive(!storePanel.gameObject.activeSelf);
    }
}
