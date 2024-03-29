using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectShip : MonoBehaviour
{
    [SerializeField] private List<GameObject> ships = new List<GameObject>();
    [SerializeField] private List<Transform> shipsTransform = new List<Transform>();
    [SerializeField] private GameObject selectShipPanel;
    private StoreManager storeManager = null;
    private ScenesManager scenesManager = null;

    void Start()
    {
        storeManager = StoreManager.instanceStoreManager;
        scenesManager = FindObjectOfType<ScenesManager>();
        UIMenu.ShowShipOnSelectShip += ShowShipsAvailable;
        //ships[0].SetActive(!ships[0].activeSelf);
        //shipsTransform[0].gameObject.SetActive(!shipsTransform[0].gameObject.activeSelf);
        //Instantiate(storeManager.GetShipsBuyed[0].gameObject, shipsTransform[0].position, Quaternion.identity, shipsTransform[0]);
    }

    private void OnDisable()
    {
        UIMenu.ShowShipOnSelectShip -= ShowShipsAvailable;
    }

    private void ShowShipsAvailable(int index)
    {
        ships[index].SetActive(!ships[index].activeSelf);
        shipsTransform[index].gameObject.SetActive(!shipsTransform[index].gameObject.activeSelf);
        for(int i = 0; i < storeManager.GetShipsBuyed.Count; i++)
        {
            StoreItem item = Instantiate(storeManager.GetShipsBuyed[i], shipsTransform[index].position, Quaternion.identity, shipsTransform[index]);
            Vector3.Normalize(item.gameObject.transform.localScale);
        }
    }

    public void OnClickShip(string scene)
    {
        scenesManager.LoadScene(scene);
        //Agregar evento que le pase la nave que compro el usuario al Gameplay
    }
}
