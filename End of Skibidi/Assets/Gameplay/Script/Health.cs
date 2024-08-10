using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;

    private Renderer[] renderers;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // damage
            anim.SetTrigger("hurt");
            // iFrames
            StartCoroutine(Invulnerability());
        }
        else
        {
            // die
            if (!dead)
            {
                anim.SetTrigger("die");

                //player
                if(GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;

                //enemy
                if(GetComponent<EnemySideways>() != null)
                   GetComponentInParent<EnemySideways>().enabled = false;

                dead = true;
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            SetColor(new Color(1, 0, 0, 0.5f)); // Set color to semi-transparent red
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            SetColor(Color.white); // Set color back to white
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
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

