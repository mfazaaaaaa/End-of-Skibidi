using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private bool canMove = false;  // Menentukan apakah player bisa bergerak atau tidak

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // Mulai coroutine untuk menunda eksekusi script
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        // Tunggu selama 4 detik
        yield return new WaitForSeconds(5f);

        // Setelah 4 detik, izinkan player untuk bergerak
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;  // Jika belum boleh bergerak, hentikan eksekusi FixedUpdate

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Tambahkan kecepatan jatuh saat di udara
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Update()
    {
        if (!canMove) return;  // Jika belum boleh bergerak, hentikan eksekusi Update

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
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && canMove)  // Tambahkan pengecekan canMove
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * jumpMultiplier);
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
        if (canMove)  // Hanya baca input jika canMove true
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && canMove)  // Hanya boleh menyerang jika canMove true
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
