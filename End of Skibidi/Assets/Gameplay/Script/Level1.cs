using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1 : MonoBehaviour
{
    public Text levelText; // Drag and drop your UI Text component here

    private void Start()
    {
        // Inisialisasi
        if (levelText != null)
        {
            levelText.gameObject.SetActive(false); // Pastikan teks tidak aktif di awal
        }
    }



    public void ShowLevelText(string levelName)
    {
        StartCoroutine(DisplayLevelText(levelName));
    }

    private IEnumerator DisplayLevelText(string levelName)
    {
        if (levelText != null)
        {
            levelText.text = levelName;
            levelText.gameObject.SetActive(true); // Tampilkan teks

            yield return new WaitForSeconds(1f); 

            levelText.gameObject.SetActive(false); // Sembunyikan teks
        }
    }
}
