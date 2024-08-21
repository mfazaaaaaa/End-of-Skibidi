using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    private int selectedLevel;

    public void SelectLevel(int level)
    {
        selectedLevel = level;
    }

    public void EnterSelectedLevel()
    {
        if (selectedLevel != 0)
        {
            SceneManager.LoadScene("Level" + selectedLevel);
        }
    }
}

