using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private string playerTag = "Player";
    private Player_Controller playerController;
    private float lastCheckedScore = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerController = player.GetComponent<Player_Controller>();
    }

    void Update()
    {
        if (playerController == null || !playerController.isAlive) return;

        if (playerController.score >= lastCheckedScore + 50)
        {
            if (lastCheckedScore < 200)
            {
                if(speed < 12)
                {
                    speed += 1;
                }

                lastCheckedScore += 50;
            }
        }

        if (!playerController.startDragging)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed * 2);
        }

        if (transform.position.y < -9)
        {
            transform.position = new Vector3(0, 9, 0);
        }
        else if (transform.position.y > 9)
        {
            transform.position = new Vector3(0, -9, 0);
        }
    }
}
