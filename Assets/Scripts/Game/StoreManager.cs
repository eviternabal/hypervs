using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    static public StoreManager instanceStoreManager;
    static public StoreManager Instance { get { return instanceStoreManager; } }
    [SerializeField] private List<StoreItem> shipsAvailable = new List<StoreItem>();
    [SerializeField] private List<StoreItem> shipsBuyed = new List<StoreItem>();

    private int money = 0;

    public int GetMoney { get { return money; } }

    private void Awake()
    {
        if (instanceStoreManager != this && instanceStoreManager != null)
            Destroy(instanceStoreManager);
        else
            instanceStoreManager = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void BuyShip(int index)
    {
        if(money >= shipsAvailable[index].price)
            shipsBuyed.Add(shipsAvailable[index]);
    }
}
