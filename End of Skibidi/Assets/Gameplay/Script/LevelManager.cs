using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelStars
{
    public GameObject[] starImages; // Array bintang untuk setiap level
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons; // Array tombol level
    [SerializeField] private LevelStars[] levelStars; // Array kelas LevelStars untuk setiap level

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

            // Ambil jumlah bintang yang disimpan untuk level ini
            int starsEarned = PlayerPrefs.GetInt("Level" + (i + 1) + "Stars", 0);

            // Nonaktifkan semua bintang sebelum menampilkan bintang yang diperoleh
            foreach (GameObject star in levelStars[i].starImages)
            {
                star.SetActive(false);
            }

            // Tampilkan jumlah bintang yang diperoleh
            for (int j = 0; j < starsEarned; j++)
            {
                if (j < levelStars[i].starImages.Length)
                {
                    levelStars[i].starImages[j].SetActive(true); // Aktifkan gambar bintang
                }
            }
        }
    }

    public void SelectLevel(int levelIndex)
    {
        // Fungsi untuk memuat level yang dipilih
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
}
