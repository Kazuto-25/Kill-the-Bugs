using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    private GameObject player;
    private Player_Controller playerController;

    public GameObject spawnLocation1;
    public GameObject spawnLocation2;

    public float spawnInterval = 2f;

    private string playerTag = "Player";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerController = player.GetComponent<Player_Controller>();


        InvokeRepeating("SpawnObstacle", spawnInterval, spawnInterval);
    }


    private void Update()
    {
        if (playerController.startDragging)
        {
            spawnInterval = 5;
        }

        else
        {
            spawnInterval = 2;
        }
    }

    void SpawnObstacle()
    {
        if (!playerController.startDragging && playerController.isAlive)
        {
                GameObject selectedSpawnLocation = Random.Range(0, 2) == 0 ? spawnLocation1 : spawnLocation2;
                Vector2 spawnPoint = selectedSpawnLocation.transform.position;

                GameObject obstaclePrefab = obstacles[Random.Range(0, obstacles.Length)];
                GameObject spawnedObstacle = Instantiate(obstaclePrefab, spawnPoint, Quaternion.identity);

                if (selectedSpawnLocation == spawnLocation1)
                {
                    Vector3 scale = spawnedObstacle.transform.localScale;
                    scale.y = -scale.y;
                    spawnedObstacle.transform.localScale = scale;
            }
        }
    }
}
