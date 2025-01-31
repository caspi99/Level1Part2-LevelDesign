using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public Animator platformAnimator;
    public Transform platform;
    public Transform player;
    private bool inPlatform = false;
    public CharacterController controller;
    private Vector3 lastPlatformPosition;
    public float var = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPlatform = true;
            platformAnimator.SetTrigger("Activate");
            lastPlatformPosition = platform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player = null;
            inPlatform = false;
        }
    }

    private void Update()
    {
        if (inPlatform)
        {
            controller.enabled = false;
            Vector3 platformMovement = (platform.position - lastPlatformPosition)*var;
            player.position += platformMovement; // Move the player with the platform
            controller.enabled = true;
            lastPlatformPosition = platform.position;
        }
    }
}
