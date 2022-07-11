using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private List<GameObject> shipsInStore = new List<GameObject>();

    private int indexForSwitchItems;
    private int lastIndexForSwitchItems;

    private void Start()
    {
        shipsInStore[0].SetActive(true);
        for(int i = 1; i < shipsInStore.Count; i++)
        {
            shipsInStore[i].SetActive(false);
        }
        indexForSwitchItems = 0;
    }

    public void SwitchPanels()
    {
        menuPanel.SetActive(!menuPanel.gameObject.activeSelf);
        storePanel.SetActive(!storePanel.gameObject.activeSelf);
    }

    public void SwitchItemsInStore()
    {
        shipsInStore[lastIndexForSwitchItems].SetActive(!shipsInStore[lastIndexForSwitchItems].activeSelf);
        shipsInStore[indexForSwitchItems].SetActive(!shipsInStore[indexForSwitchItems].activeSelf);
        lastIndexForSwitchItems = indexForSwitchItems;
        indexForSwitchItems++;
        if(indexForSwitchItems == shipsInStore.Count)
            indexForSwitchItems = 0;
    }

    public void OnClickBuy()
    {
        Debug.Log("lastIndexForSwitchItems: " + lastIndexForSwitchItems);
        StoreManager.instanceStoreManager.BuyShip(lastIndexForSwitchItems);
        Debug.Log("compro la nave: " + lastIndexForSwitchItems);
    }
}
