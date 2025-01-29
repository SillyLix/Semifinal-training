using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public bool isTopDown = true;
    public bool useLinearMovement = true;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public bool waiting;
    public bool canJump = true;
    public bool isRunning;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    public bool isGrounded;
    private bool facingRight = true;
    private float stillTime;

    void Update()
    {
        HandleInput();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        if (rb.linearVelocity.magnitude < 0.05f && isGrounded)
        {
            if (!waiting)
            {
                stillTime = Time.time;
                waiting = true;
            }
        }
        else
        {
            waiting = false;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = isTopDown ? Input.GetAxisRaw("Vertical") : 0;
        movement = new Vector2(moveX, moveY).normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (canJump && !isTopDown && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (moveX > 0 && !facingRight)
            Flip();
        else if (moveX < 0 && facingRight)
            Flip();

        if (Input.GetKeyDown(KeyCode.S) && rb.linearVelocity.magnitude < 0.1f)
        {
            animator.SetInteger("Random", Convert.ToInt32(UnityEngine.Random.Range(0, 4)));
            animator.SetBool("IsSitting", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetInteger("Random", 0);
            animator.SetBool("IsSitting", false);
        }
    }

    void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (useLinearMovement)
        {
            rb.linearVelocity = new Vector2(movement.x * currentSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, movement.x * currentSpeed, 0.1f), rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
        isGrounded = false;
    }

    void UpdateAnimator()
    {
        float boredness = waiting ? Time.time - stillTime : 0;
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("Bored", boredness);
        animator.SetBool("Falling", rb.linearVelocity.y < -0.1f && !isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTopDown)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}