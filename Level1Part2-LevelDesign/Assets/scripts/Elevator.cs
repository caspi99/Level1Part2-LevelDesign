using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    private Vector3 startPosition;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float speed = 3f;

    private bool isMoving = false;
    private float timer = 0f;
    private bool playerOnPlatform = false;
    private bool atTop = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            timer = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlatform && !isMoving)
        {
            timer += Time.deltaTime;

            if(timer > waitTime)
            {
                if (!atTop)
                {
                    StartCoroutine(MoveElevator(targetPosition.position));
                }
                else
                {
                    StartCoroutine(MoveElevator(startPosition));
                }
                atTop = !atTop;
            }
        }
    }

    private IEnumerator MoveElevator(Vector3 target)
    {
        isMoving = true;
        float journey = 0f;
        Vector3 initialPosition = transform.position;

        while (journey < 1f)
        {
            journey += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(initialPosition, target, journey);
            yield return null;
        }
    }
}
