using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objSpwanner : MonoBehaviour
{
    public GameObject platformPrefab; // Reference to the platform prefab
    public float xOffset = 10f; // Offset distance to spawn the new platform

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Calculate the new position for the next platform
            Vector3 spawnPosition = new Vector3(collision.transform.position.x + xOffset, 0, transform.position.z);

            // Instantiate the new platform at the calculated position
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

}