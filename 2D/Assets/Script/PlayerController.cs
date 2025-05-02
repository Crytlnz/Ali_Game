using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteCounter;

    private Vector2 moveInput;
    private bool isAttacking = false;
    private bool isFacingRight = true;
    private bool isMoving;
    private bool isRunning;
    private bool isDead = false;

    private TouchingDirection touchingDirection;
    private Rigidbody2D rb;
    private Animator animator;

    public bool CanMove => !isAttacking && !isDead && animator.GetBool(AnimationStrings.canMove);

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving && !touchingDirection.IsOnWall)
                {
                    return touchingDirection.IsGrounded ? (isRunning ? runSpeed : walkSpeed) : airWalkSpeed;
                }
            }
            return 0;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
    }

    private void Update()
    {
        if (isDead) return;

        if (touchingDirection.IsGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // Jump gravity modifier
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (isDead) return;

        if (!isAttacking)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isAttacking || isDead) return;  // Cek apakah karakter mati sebelum melanjutkan

        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        animator.SetBool(AnimationStrings.IsMoving, isMoving);
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (!CanMove) return;

        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (isAttacking || isDead) return;  // Cek apakah karakter mati sebelum melanjutkan

        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }

        animator.SetBool(AnimationStrings.IsRunning, isRunning);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isDead) return;  // Cek apakah karakter mati sebelum melanjutkan

        if (context.started && coyoteCounter > 0 && CanMove)
        {
            animator.SetTrigger(AnimationStrings.JumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            coyoteCounter = 0;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isDead) return;  // Cek apakah karakter mati sebelum melanjutkan

        if (context.started && !isAttacking && !isDead)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetBool(AnimationStrings.canMove, false);
        animator.SetTrigger(AnimationStrings.AttackTrigger);
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        animator.SetBool(AnimationStrings.canMove, true);
    }

    // Dipanggil dari script Health.cs saat player mati
    public void SetDead()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        moveInput = Vector2.zero;

        animator.SetBool(AnimationStrings.IsMoving, false);
        animator.SetBool(AnimationStrings.IsRunning, false);
        animator.SetBool(AnimationStrings.canMove, false);
    }
}
