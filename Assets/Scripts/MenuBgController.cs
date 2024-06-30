using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBgController : MonoBehaviour
{
    public float speed;
    public float posX;
    public float newPos;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(transform.position.x > posX )
        {
            transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
        }
    }
}
