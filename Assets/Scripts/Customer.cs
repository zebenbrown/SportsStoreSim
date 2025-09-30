using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private float cash = 100;
    public TextMeshProUGUI cashText;
    private NavMeshAgent agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameManager.instance.getBatShelfPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == GameManager.instance.getBatShelfPosition())
        {
            StartCoroutine(shopBehaviour());
        }
    }

    //later will take a product as a parameter instead
    public void purchaseItem(float amount)
    {
        cash -= amount;
        cashText.text = "Cash: " + cash.ToString("f2");
    }
    
    IEnumerator shopBehaviour()
    {
        //int productChoice = Random.Range(1, 3);
        // if (productChoice == 1)
        // {
        //     GameManager.instance.sellBat();
        // }
        //
        // if (productChoice == 2)
        // {
        //     GameManager.instance.sellCleats();
        // }

        GameManager.instance.sellBat();
        purchaseItem(50f);

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
