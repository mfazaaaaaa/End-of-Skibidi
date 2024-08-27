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

    private AudioManager audioManager; // Tambahkan referensi ke AudioManager

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        anim = GetComponent<Animator>();

        // Inisialisasi AudioManager
        audioManager = FindObjectOfType<AudioManager>();
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
            audioManager.PlaySFX(jumpSFX);
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

    private bool isWalking = false;  // Tambahkan variabel untuk cek status berjalan

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

        // Cek jika pemain sedang berjalan dan berada di tanah
        if (IsGrounded() && Mathf.Abs(horizontal) > 0.1f)
        {
            if (!isWalking)
            {
                isWalking = true;
                audioManager.PlaySFX(walkSFX);  // Mainkan SFX jalan hanya sekali
            }
        }
        else
        {
            isWalking = false;  // Reset status berjalan ketika berhenti atau melompat
        }

        // Reset status lompat jika sudah di tanah
        if (IsGrounded())
        {
            isJumping = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && canMove && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * jumpMultiplier);

            // Cek cooldown agar SFX lompat tidak diputar berulang
            if (Time.time - lastJumpTime >= jumpSFXCooldown)
            {
                if (!audioManager.sfxSource.isPlaying)  // Pastikan SFX lompat tidak terganggu
                {
                    audioManager.PlaySFX(jumpSFX);
                    lastJumpTime = Time.time;
                }
            }

            isJumping = true;
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
