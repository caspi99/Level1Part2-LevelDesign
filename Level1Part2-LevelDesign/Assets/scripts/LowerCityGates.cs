using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LowerCityGates : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cityGates;
    private float y;

    void Start()
    {
        y = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cityGates.position = new Vector3(cityGates.position.x, cityGates.position.y - 20, cityGates.position.z);
            gameObject.SetActive(false);
        }
    }


}
