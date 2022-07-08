using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private List<GameObject> shipsInStore = new List<GameObject>();

    private void Start()
    {
        shipsInStore[0].SetActive(true);
        for(int i = 1; i < shipsInStore.Count; i++)
        {
            shipsInStore[i].SetActive(false);
        }
    }

    public void SwitchPanels()
    {
        menuPanel.SetActive(!menuPanel.gameObject.activeSelf);
        storePanel.SetActive(!storePanel.gameObject.activeSelf);
    }
}
