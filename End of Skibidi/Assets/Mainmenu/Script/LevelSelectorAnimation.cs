using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuAnimation : MonoBehaviour
{
    public RectTransform Mainmenu;
    public RectTransform Settingmenu;
    public RectTransform htp;
    public RectTransform Quit;

    void Start()
    {
 
    }


    void Update()
    {
        // Tidak ada logika di Update untuk saat ini
    }

    public void OptionButtontekan()
    {
        Mainmenu.DOAnchorPos(new Vector2(-1981, 0), 0.5f, false);  // Pindahkan Mainmenu ke atas layar
        Settingmenu.DOAnchorPos(new Vector2(0, 0), 0.5f, false);  // Pindahkan Settingmenu ke tengah layar
    }

    public void OptionbackButtonkan()
    {
        Mainmenu.DOAnchorPos(new Vector2(0, 0), 0.5f, false);  // Pindahkan Mainmenu ke atas layar
        Settingmenu.DOAnchorPos(new Vector2(0, - 1981), 0.5f, false);  // Pindahkan Settingmenu ke tengah layar
    }

    public void htpButtontekan()
    {
        Mainmenu.DOAnchorPos(new Vector2(-1981, 0), 0.5f, false);  // Pindahkan Mainmenu ke atas layar
        htp.DOAnchorPos(new Vector2(0, 0), 0.5f, false);  // Pindahkan Settingmenu ke tengah layar
    }

    public void htpbackButtonkan()
    {
        Mainmenu.DOAnchorPos(new Vector2(0, 0), 0.5f, false);  // Pindahkan Mainmenu ke atas layar
        htp.DOAnchorPos(new Vector2(0, -1981), 0.5f, false);  // Pindahkan Settingmenu ke tengah layar
    }
    public void exitButtontekan()
    {
        Mainmenu.DOAnchorPos(new Vector2(-1981, 0), 0.5f, false);  // Pindahkan Mainmenu ke atas layar
        Quit.DOAnchorPos(new Vector2(0, 0), 0.5f, false);  // Pindahkan Settingmenu ke tengah layar
    }

    public void exitbackButtonkan()
    {
        Mainmenu.DOAnchorPos(new Vector2(0, 0), 0.5f, false);  // Pindahkan Mainmenu ke atas layar
        Quit.DOAnchorPos(new Vector2(0, -1981), 0.5f, false);  // Pindahkan Settingmenu ke tengah layar
    }
}