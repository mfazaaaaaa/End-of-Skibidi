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
            CompleteLevel();
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

        // Simpan progres level (misalnya, membuka level berikutnya)
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        // Jika level selanjutnya ada dalam Build Settings, buka level tersebut
        if (nextLevel < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            // Simpan level tertinggi yang dicapai oleh pemain
            int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

            // Jika level yang dicapai lebih tinggi dari sebelumnya, update PlayerPrefs
            if (nextLevel > levelReached)
            {
                PlayerPrefs.SetInt("LevelReached", nextLevel);
            }
        }
    }
}
