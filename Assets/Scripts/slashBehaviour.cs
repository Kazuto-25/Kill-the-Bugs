using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashBehaviour : MonoBehaviour
{
    public float speed;
    public Player_Controller playerController;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("isDead");
            if (playerController != null)
            {
                playerController.AddKill();
            }
            Destroy(collision.gameObject, 1.45f);           
        }
    }
}
