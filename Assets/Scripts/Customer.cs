using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private float cash = 100;
    //public TextMeshProUGUI cashText;
    private NavMeshAgent agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(InitalizeCustomer());
    }

    //later will take a product as a parameter instead
    public void purchaseItem(float amount)
    {
        cash -= amount;
        //cashText.text = "Cash: " + cash.ToString("f2");
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

    IEnumerator InitalizeCustomer()
    {
        yield return new WaitForSeconds(1f);

        if (agent.isOnNavMesh)
        {
            agent.SetDestination(GameManager.instance.getBatShelfPosition());
        }
        else
        {
            Debug.Log("Agent not on navmesh");
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Shelf"))
        {
            Debug.Log("OnCollisionEnter");
            GameManager.instance.sellBat();
            Destroy(gameObject);
        }
    }
}
