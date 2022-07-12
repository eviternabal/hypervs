using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectShip : MonoBehaviour
{
    [SerializeField] private List<GameObject> ships = new List<GameObject>();
    private StoreManager storeManager = null;
    private ScenesManager scenesManager = null;
    void Start()
    {
        storeManager = StoreManager.instanceStoreManager;
        scenesManager = FindObjectOfType<ScenesManager>();
        UIMenu.ShowShipOnSelectShip += ShowShipsAvailable;
    }

    private void OnDisable()
    {
        UIMenu.ShowShipOnSelectShip -= ShowShipsAvailable;
    }

    private void ShowShipsAvailable(int index)
    {
        ships[index].SetActive(!ships[index].activeSelf);
    }

    public void OnClickShip()
    {
        scenesManager.LoadScene("Gameplay");
        //Agregar evento que le pase la nave que compro el usuario al Gameplay
    }
}
