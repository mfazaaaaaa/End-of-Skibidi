using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 20f;
    private float jumpingPower = 13f;
    private float jumpMultiplier = 2f;
    private float fallMultiplier = 5f;
    private bool isFacingRight = true;
    private Animator anim;
    private bool canMove = false;

    [Header("Bounce Settings")]
    public float bounceForce = 10f; // Kekuatan pantulan saat menginjak bos

    // Tambahkan variabel untuk cooldown dan status lompat
    private bool isJumping = false;
    private float jumpSFXCooldown = 0.2f;  // Waktu jeda agar SFX tidak diputar berulang
    private float lastJumpTime = 0f;

    [Header("Audio")]
    public AudioClip jumpSFX;
    public AudioClip walkSFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Memastikan SFX Jump diputar tanpa gangguan
        if (isJumping && Time.time - lastJumpTime >= jumpSFXCooldown)
        {
            AudioManager.Instance.PlaySFX(jumpSFX);
            lastJumpTime = Time.time;
            isJumping = false;
        }
    }


    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(5f);
        canMove = true;
    }

    private bool isWalking = false;

    private void Update()
    {
        if (!canMove) return;

        if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }

        anim.SetBool("run", Mathf.Abs(horizontal) > 0.1f);
        anim.SetBool("jump", !IsGrounded());

        if (IsGrounded() && Mathf.Abs(horizontal) > 0.1f)
        {
            if (!isWalking)
            {
                AudioManager.Instance.PlaySFX(walkSFX);
                isWalking = true;
            }
        }
        else
        {
            isWalking = false;
        }

        if (IsGrounded())
        {
            isJumping = false;
        }
    }


    public void Jump(InputAction.CallbackContext context)
{
    if (context.performed && IsGrounded() && canMove)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower * jumpMultiplier);

        // Memastikan SFX lompat diputar
        AudioManager.Instance.PlaySFX(jumpSFX);
    }
}



    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && canMove)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(1, rb, bounceForce);
        }
    }
}
