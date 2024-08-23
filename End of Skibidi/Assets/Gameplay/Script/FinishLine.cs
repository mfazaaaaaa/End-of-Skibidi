using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    private GameplayManager gameplayManager;

    private void Start()
    {
        // Pastikan panel kemenangan tidak langsung aktif saat game dimulai
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tampilkan panel kemenangan
            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0; // Menghentikan waktu saat menang (opsional)
            }

            // Hitung jumlah bintang yang diperoleh
            gameplayManager.CalculateStars();
        }
    }

    public void CompleteLevel()
    {
        // Tampilkan panel kemenangan, hentikan waktu, dll.
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0; // Menghentikan waktu saat menang (opsional)
        }

        // Hitung jumlah bintang yang diperoleh
        gameplayManager.CalculateStars();
    }

}
