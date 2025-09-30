using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private float cash = 100;
    public TextMeshProUGUI cashText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //later will take a product as a parameter instead
    public void purchaseItem(float amount)
    {
        cash -= amount;
        cashText.text = "Cash: " + cash.ToString("f2");
    }
}
