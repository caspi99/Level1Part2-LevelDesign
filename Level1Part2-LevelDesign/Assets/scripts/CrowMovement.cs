using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrowMovement : MonoBehaviour
{
    public Animator anim;
    public GameObject crow;
    public Transform puntoB;
    public float speed = 0.5f;
    public float waitSeconds = 0.5f;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) // Si es en 3D
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("flying", true); // Activa la animación
            StartCoroutine(MoveCrow());
        }
    }

    IEnumerator MoveCrow()
    {
        yield return new WaitForSeconds(waitSeconds);
        float journey = 0f;
        Vector3 initialPosition = crow.transform.position;



        Vector3 direction = (puntoB.position - initialPosition).normalized;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            crow.transform.rotation = targetRotation;
        }

        while (journey < 1f)
        {
            journey += Time.deltaTime * speed;
            crow.transform.position = Vector3.Lerp(initialPosition, puntoB.position, journey);
            yield return null;
        }

        anim.SetBool("flying", false);
    }
}
