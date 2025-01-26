using UnityEngine;
using System.Collections;

public class IsometricCharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private Vector3 initialPos = Vector3.zero;

    private CharacterController characterController;
    private Vector3 gravity = Vector3.zero;
    private Vector3 direction;

    private float gravityScale = -10f;
    private bool isDashing = false;

    const string idle = "Idle";
    const string walk = "Walk";
    [SerializeField] private Animator animator;
    bool isWalking = false;
    bool isAttacking = false;

    [SerializeField] private GameObject attackArea;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        attackArea.SetActive(false);
        initialPos = transform.position;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!isDashing && !isAttacking)
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
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !isAttacking)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetMouseButtonDown(0) && !isDashing && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            characterController.Move(direction * moveSpeed * Time.fixedDeltaTime);
        }

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

        direction = Vector3.zero;
        yield return new WaitForEndOfFrame();
        isDashing = false;
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        direction = Vector3.zero;
        animator.SetTrigger("isAttack");

        yield return new WaitForSeconds(0.20f);
        attackArea.SetActive(true);
        yield return new WaitForSeconds(0.85f);

        attackArea.SetActive(false);
        isAttacking = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            characterController.enabled = false;
            transform.position = initialPos;
            characterController.enabled = true;
        }
    }
}
