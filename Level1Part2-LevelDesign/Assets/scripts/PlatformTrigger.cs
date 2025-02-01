using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public Animator platformAnimator;
    public Transform platform;
    public Transform player;
    private bool inPlatform = false;
    public CharacterController controller;
    private Vector3 lastPlatformPosition;


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

    private void FixedUpdate()
    {
        if (inPlatform)
        {
            //controller.enabled = false;
            Vector3 platformMovement = (platform.position - lastPlatformPosition);
            lastPlatformPosition = platform.position;
            controller.Move(platformMovement);
            //controller.enabled = true;
        }
    }
}
