using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    private float cash = 100;
    //public TextMeshProUGUI cashText;
    private NavMeshAgent agent;
    private int shelfChoice;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(InitalizeCustomer());
        shelfChoice = Random.Range(0, 2);

    }

    private void Update()
    {
        if (agent.destination == GameManager.instance.getBatShelfPosition() &&
            GameManager.instance.getBatInventory() == 0)
        {
            Destroy(gameObject);
        }
        
        else if (agent.destination == GameManager.instance.getCleatShelfPosition() &&
                 GameManager.instance.getCleatInventory() > 0)
        {
            Destroy(gameObject);
        }
    }

    //later will take a product as a parameter instead
    public void purchaseItem(float amount)
    {
        cash -= amount;
        //cashText.text = "Cash: " + cash.ToString("f2");
    }
    
    IEnumerator shopBehaviour()
    {
        switch (shelfChoice)
        {
            case 0:
                GameManager.instance.sellBat();
                purchaseItem(50f);
                Destroy(gameObject);
                break;
            
            case 1:
                GameManager.instance.sellCleats();
                purchaseItem(100f);
                Destroy(gameObject);
                break;
        }
        
        yield return new WaitForSeconds(1f);
    }

    IEnumerator InitalizeCustomer()
    {
        yield return new WaitForSeconds(1f);
        

        switch (shelfChoice)
        {
            case 0:
                if (agent.isOnNavMesh)
                {
                    agent.SetDestination(GameManager.instance.getBatShelfPosition());
                }
                else
                {
                    Debug.Log("Agent not on navmesh");
                }
                break;
            
            case 1:
                if (agent.isOnNavMesh)
                {
                    agent.SetDestination(GameManager.instance.getCleatShelfPosition());
                }
                else
                {
                    Debug.Log("Agent not on navmesh");
                }
                break;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Shelf"))
        {
            Debug.Log("OnCollisionEnter");
            StartCoroutine(shopBehaviour());
        }
    }
}
