using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsAppearing : MonoBehaviour
{
    [SerializeField] private GameObject tiles;
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private float moveSpeed = 2f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MovePlatforms());
        }
    }

    private IEnumerator MovePlatforms()
    {
        float journey = 0f;
        Vector3 initialPosition = tiles.transform.position;
        while (journey < 2f)
        {
            journey += Time.deltaTime * moveSpeed;
            tiles.transform.position = Vector3.Lerp(initialPosition, targetPosition.transform.position, journey);
            yield return null;
        }
    }
}
