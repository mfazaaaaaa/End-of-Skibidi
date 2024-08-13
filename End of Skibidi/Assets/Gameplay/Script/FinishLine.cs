using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void Start()
    {
        // Pastikan panel kemenangan tidak langsung aktif saat game dimulai
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    // Fungsi ini dipanggil saat sesuatu masuk ke dalam BoxCollider2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Periksa apakah objek yang masuk adalah player (misalnya dengan tag "Player")
        if (other.CompareTag("Player"))
        {
            // Tampilkan panel kemenangan
            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0; // Menghentikan waktu saat menang (opsional)
            }
        }
    }
}
