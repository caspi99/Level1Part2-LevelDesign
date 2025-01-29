using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFollow : MonoBehaviour
{
    public Transform player;
    public float yOffset = 10f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, player.position.y - yOffset, transform.position.z);
    }
}
