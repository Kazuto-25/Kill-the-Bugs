using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarBgController : MonoBehaviour
{
    public float speed;
    public float posX;

    public string playerTag = "Player";
    public GameObject player;
    private Player_Controller playerController;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerController = player.GetComponent<Player_Controller>();
    }

    void Update()
    {
        if (playerController.isAlive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x < posX)
            {
                transform.position = new Vector2(27, transform.position.y);
            }
        }
    }
}
