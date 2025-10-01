using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float cash = 1000;
    private int baseballBatInventoryCount = 5;
    private int cleatInventoryCount = 5;
    private float batPrice = 29.99f;
    private float cleatPrice = 49.99f;
    //UI Start
    public Button buyBatButton;
    public Button buyCleatsButton;
    //UI End
    [SerializeField] private GameObject batShelf;
    [SerializeField] private GameObject cleatShelf;
    private CustomerSpawner customerSpawner;
    

    public static GameManager instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buyBatButton.onClick.AddListener(buyBat);
        buyCleatsButton.onClick.AddListener(buyCleats);
    }

    public float getCash()
    {
        return cash;
    }
    
    public void SpendCash(float amount)
    {
        cash -= amount;
    }

    public void buyBat()
    {
        baseballBatInventoryCount++;
        SpendCash(batPrice);
        UIManager.instance.updateUI();
    }

    public void sellBat()
    {
        if (baseballBatInventoryCount > 0)
        {
            baseballBatInventoryCount--;
            cash += 50;
            UIManager.instance.updateUI();
        }
    }

    public void buyCleats()
    {
        cleatInventoryCount++;
        SpendCash(cleatPrice);
        UIManager.instance.updateUI();
    }

    public void sellCleats()
    {
        if (cleatInventoryCount > 0)
        {
            cleatInventoryCount--;
            cash += 100;
            UIManager.instance.updateUI();
        }
    }

    public Vector3 getBatShelfPosition()
    {
        return batShelf.transform.position;
    }

    public Vector3 getCleatShelfPosition()
    {
        return cleatShelf.transform.position;
    }

    public int getBatInventory()
    {
        return baseballBatInventoryCount;
    }

    public int getCleatInventory()
    {
        return cleatInventoryCount;
    }
}
