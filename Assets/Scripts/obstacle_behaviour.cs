using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private Player_Controller playerController;
    private string playerTag = "Player";
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
            transform.Translate(Vector2.left * Time.deltaTime * speed);

            if (transform.position.x < -100)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * 2);
        }
    }
}
