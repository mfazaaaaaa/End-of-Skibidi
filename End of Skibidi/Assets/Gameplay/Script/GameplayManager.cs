using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [Header("Enemy Management")]
    public int totalEnemies; // Jumlah total musuh di level ini
    private int defeatedEnemies; // Jumlah musuh yang sudah dikalahkan

    [Header("Star UI")]
    public GameObject[] stars; // Array untuk menampung UI Bintang (misalnya, 3 Image)

    private void Start()
    {
        defeatedEnemies = 0;
        // Pastikan bintang tidak tampil di awal
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }
    }

    // Fungsi untuk memanggil setiap kali musuh dikalahkan
    public void EnemyDefeated()
    {
        defeatedEnemies++;
    }

    // Fungsi untuk menentukan jumlah bintang saat level selesai
    public void CalculateStars()
    {
        float halfEnemies = totalEnemies / 2f;
        int starCount = 1; // Default 1 bintang

        if (defeatedEnemies == totalEnemies)
        {
            // 3 Bintang jika semua musuh dikalahkan
            starCount = 3;
            ActivateStars(3);
        }
        else if (defeatedEnemies >= halfEnemies)
        {
            // 2 Bintang jika setengah atau lebih musuh dikalahkan
            starCount = 2;
            ActivateStars(2);
        }
        else
        {
            // 1 Bintang jika hanya menyelesaikan level
            starCount = 1;
            ActivateStars(1);
        }

        // Simpan jumlah bintang terbaik di PlayerPrefs
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int storedStars = PlayerPrefs.GetInt("Level" + currentLevel + "Stars", 0);

        // Simpan hanya jika starCount lebih tinggi dari storedStars
        if (starCount > storedStars)
        {
            PlayerPrefs.SetInt("Level" + currentLevel + "Stars", starCount);
        }
    }


    // Fungsi untuk mengaktifkan bintang sesuai jumlah
    private void ActivateStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            if (i < stars.Length)
            {
                stars[i].SetActive(true);
            }
        }
    }
}
