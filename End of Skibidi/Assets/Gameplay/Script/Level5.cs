using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level5 : MonoBehaviour
{
    public Text levelText; // Drag and drop your UI Text component here

    void Start()
    {
        ShowLevelText("Level 5");
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
