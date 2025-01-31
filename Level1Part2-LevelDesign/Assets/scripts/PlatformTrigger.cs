using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public Animator platformAnimator;
    public Transform player;

    private Vector3 lastPlatformPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformAnimator.SetTrigger("Activate");
            lastPlatformPosition = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition;
            player.position += platformMovement; // Move the player with the platform
            Debug.Log(platformMovement.x);
            lastPlatformPosition = transform.position;
        }
    }
}
