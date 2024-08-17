using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private LevelManager levelManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Damage
            anim.SetTrigger("hurt");
        }
        else
        {
            // Die
            if (!dead)
            {
                anim.SetTrigger("die");

                // Disable enemy
                if (GetComponent<EnemySideways>() != null)
                    GetComponentInParent<EnemySideways>().enabled = false;

                // Inform LevelManager bahwa musuh ini telah dikalahkan
                levelManager.EnemyDefeated();

                dead = true;
                Destroy(gameObject, 2f); // Hancurkan musuh setelah animasi mati
            }
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
