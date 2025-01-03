
using UnityEngine;

public class IsometricCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private CharacterController characterController;

    private Vector3 gravity = Vector3.zero;

    private float gravityScale = -10f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate direction relative to the camera
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        direction = Camera.main.transform.TransformDirection(direction.normalized);

        direction.y = 0; 


        characterController.Move(direction * moveSpeed * Time.deltaTime);

        // Apply gravity
        if (!characterController.isGrounded)
        {
            gravity.y += gravityScale * Time.deltaTime;
        }
        else
        {
            gravity.y = 0;
        }
        characterController.Move(gravity * Time.deltaTime);

        // Rotate character
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
