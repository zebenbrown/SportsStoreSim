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
    bool itemPurchased = false;
    
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
    }
    
    IEnumerator shopBehaviour()
    {
        switch (shelfChoice)
        {
            case 0:
                GameManager.instance.sellBat();
                purchaseItem(50f);
                itemPurchased =  true;
                Destroy(gameObject);
                itemPurchased  = false;
                break;
            
            case 1:
                GameManager.instance.sellCleats();
                purchaseItem(100f);
                itemPurchased =  true;
                Destroy(gameObject);
                itemPurchased =  false;
                break;
        }
        
        yield return new WaitForSeconds(1f);
    }

    IEnumerator InitalizeCustomer()
    {
        yield return new WaitForSeconds(1f);


        //if both items have inventory then randomly pick a product
        if (GameManager.instance.getCleatInventory() > 0 && GameManager.instance.getBatInventory() > 0)
        {
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
        
        //if cleats have inventory set destination to that shelf
        else if (GameManager.instance.getCleatInventory() > 0)
        {
            agent.SetDestination(GameManager.instance.getCleatShelfPosition());
        }
        
        //if bats have inventory set destination to that shelf
        else if (GameManager.instance.getBatInventory() > 0)
        {
            agent.SetDestination(GameManager.instance.getBatShelfPosition());
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        //if other has no parent then return
        if (other.transform.parent == null)
        {
            return;
        }
        
        //if other is a shelf
        if (!other.transform.parent.CompareTag("Shelf")) return;
        if (!itemPurchased)
        {
            StartCoroutine(shopBehaviour());
        }
    }
}
