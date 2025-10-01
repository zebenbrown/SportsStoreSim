using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public Vector3 spawnPosition;

    private float spawnInterval = 1f;

    private int customerCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCustomer());
    }

    IEnumerator SpawnCustomer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (GameManager.instance.getBatInventory() > 0 || GameManager.instance.getCleatInventory() > 0)
            {
                spawnInterval = 5f;
                Instantiate(customer, spawnPosition, Quaternion.identity);
                customerCount++;
            }
        }
    }
}