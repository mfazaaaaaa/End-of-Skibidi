using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons; // Array tombol level

    private void Start()
    {
        // Cek status level yang sudah selesai
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1); // Default Level 1 terbuka

        // Loop melalui semua tombol level dan aktifkan sesuai status penyelesaian
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false; // Nonaktifkan level yang belum dibuka
            }
        }
    }

    public void SelectLevel(int levelIndex)
    {
        // Fungsi untuk memuat level yang dipilih
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
}
