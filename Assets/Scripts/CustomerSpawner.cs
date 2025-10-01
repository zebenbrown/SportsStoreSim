using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public Vector3 spawnPosition;

    private float spawnInterval = 8f;
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
            if (customerCount == 0)
            {
                spawnInterval = 1f;
            }

            spawnInterval = 5f;
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(customer, spawnPosition, Quaternion.identity);
            customerCount++;
        }
    }
}
