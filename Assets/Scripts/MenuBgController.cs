using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBgController : MonoBehaviour
{
    public float speed;
    public float posX;
    public float newPos;

    public bool goingLeft;
    public bool goingRight;

    // Update is called once per frame
    void Update()
    {

        if (goingRight )
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x > posX)
            {
                transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            }
        }

        else if (goingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x < newPos)
            {
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            }
        }        

    }
}
