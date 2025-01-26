using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Enemy") || other.CompareTag("DestructibleItem"))
        {
            Debug.Log("Golpeaste al enemigo: " + other.name);
            Destroy(other.gameObject);
        }
    }
}
