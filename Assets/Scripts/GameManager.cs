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
    //private int customerCount = 0;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI inventoryBatText;
    public TextMeshProUGUI inventoryCleatsText;
    public Button buyBatButton;
    public Button sellBatButton;
    public Button buyCleatsButton;
    public Button sellCleatsButton;
    private Customer[]  customers;
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
        cashText.text = "Cash: " + cash.ToString("f2");
        buyBatButton.onClick.AddListener(buyBat);
        sellBatButton.onClick.AddListener(sellBat);
        buyCleatsButton.onClick.AddListener(buyCleats);
        sellCleatsButton.onClick.AddListener(sellCleats);
        updateUI();
        
    }
    
    public void SpendCash(float amount)
    {
        cash -= amount;
        cashText.text = "Cash: " + cash.ToString("f2");
    }

    public void buyBat()
    {
        baseballBatInventoryCount++;
        SpendCash(batPrice);
        updateUI();
    }

    public void sellBat()
    {
        if (baseballBatInventoryCount > 0)
        {
            baseballBatInventoryCount--;
            cash += 50;
            updateUI();
        }
    }

    public void buyCleats()
    {
        cleatInventoryCount++;
        SpendCash(cleatPrice);
        updateUI();
    }

    public void sellCleats()
    {
        if (cleatInventoryCount > 0)
        {
            cleatInventoryCount--;
            cash += 100;
            updateUI();
        }
    }

    void updateUI()
    {
        cashText.text = "Cash: " + cash.ToString("f2");
        inventoryBatText.text = "Baseball Bat(s): " + baseballBatInventoryCount;
        inventoryCleatsText.text = "Cleats: " + cleatInventoryCount;
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
