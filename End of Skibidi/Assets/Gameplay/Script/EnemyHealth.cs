using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Is Boss")]
    [SerializeField] private bool isBoss; // Tandai jika musuh ini adalah bos

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityDuration = 2f; // Durasi invulnerability
    [SerializeField] private int numberOfFlashes = 5; // Berapa kali berkedip saat invulnerable
    private bool isInvulnerable = false;
    private Renderer[] renderers; // Untuk mengubah warna musuh

    private GameplayManager gameplayManager;
    private FinishLine finish;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        gameplayManager = FindObjectOfType<GameplayManager>();
        finish = FindObjectOfType<FinishLine>();

        // Ambil semua renderer untuk mengubah warna
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void TakeDamage(float _damage, Rigidbody2D playerRb, float bounceForce)
    {
        if (isInvulnerable) return; // Jangan menerima damage jika sedang invulnerable

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Trigger animasi "hurt"
            anim.SetTrigger("hurt");

            // Pantulkan pemain setelah injakan
            BouncePlayer(playerRb, bounceForce);

            // Mulai invulnerability (iframe)
            StartCoroutine(Invulnerability());
        }
        else
        {
            // Kematian bos
            if (!dead)
            {
                anim.SetTrigger("die");

                // Disable enemy movement/behavior
                if (GetComponent<EnemySideways>() != null)
                    GetComponentInParent<EnemySideways>().enabled = false;

                // Inform GameplayManager bahwa musuh ini telah dikalahkan
                gameplayManager.EnemyDefeated();

                // Jika ini adalah bos, selesaikan level
                if (isBoss)
                {
                    finish.CompleteLevel();
                }

                dead = true;
                Destroy(gameObject, 2f); // Hancurkan musuh setelah animasi mati
            }
        }
    }

    private void BouncePlayer(Rigidbody2D playerRb, float bounceForce)
    {
        // Set kecepatan vertikal ke atas
        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);

        // Pantulan ke kiri atau kanan secara acak
        float randomDirection = Random.Range(-1f, 1f); 
        playerRb.AddForce(new Vector2(randomDirection * bounceForce, bounceForce), ForceMode2D.Impulse);
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        // Ubah lapisan collider untuk menghindari tabrakan dengan pemain selama invulnerable
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            SetColor(new Color(1, 0, 0, 0.5f)); // Set color to semi-transparent red
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
            SetColor(Color.white); // Kembali ke warna putih (atau warna semula)
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
        }

        // Kembali bisa menerima damage
        Physics2D.IgnoreLayerCollision(10, 11, false);
        isInvulnerable = false;
    }

    private void SetColor(Color color)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
