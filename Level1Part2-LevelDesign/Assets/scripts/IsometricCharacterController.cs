using UnityEngine;
using System.Collections;

public class IsometricCharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.2f;

    private CharacterController characterController;
    private Vector3 gravity = Vector3.zero;
    private Vector3 direction;

    private float gravityScale = -10f;
    private bool isDashing = false;

    const string idle = "Idle";
    const string walk = "Walk";
    [SerializeField] private Animator animator;
    bool isWalking = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!isDashing)
        {
            direction = new Vector3(horizontal, 0, vertical);
            direction = Camera.main.transform.TransformDirection(direction.normalized);
            direction.y = 0;

            if (horizontal == 0 && vertical == 0)
            {
                direction = Vector3.zero;
            }
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        bool shouldWalk = direction != Vector3.zero;
        if (shouldWalk != isWalking)
        {
            isWalking = shouldWalk;
            animator.SetBool("isWalking", isWalking);
            Debug.Log("Cambiando animación a: " + (isWalking ? "Walk" : "Idle"));
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            characterController.Move(direction * moveSpeed * Time.fixedDeltaTime);
        }

        // Aplicar gravedad
        if (!characterController.isGrounded)
        {
            gravity.y += gravityScale * Time.fixedDeltaTime;
        }
        else
        {
            gravity.y = 0f;
        }
        characterController.Move(gravity * Time.fixedDeltaTime);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(direction * dashSpeed * Time.deltaTime);
            yield return null;
        }

        direction = Vector3.zero; // Detener el movimiento inmediatamente
        yield return new WaitForEndOfFrame(); // Esperar un frame antes de permitir movimiento
        isDashing = false;
    }
}
