using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject Player;
    public Vector2 offset;
    public float smoothSpeed = 0.125f;

    // LateUpdate is called once per frame, after all Update calls
    void LateUpdate()
    {
        if (Player != null)
        {
            Vector3 desiredPosition = new Vector3(Player.transform.position.x + offset.x, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
