using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuAnimation : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation settinganim;
    [SerializeField] private DOTweenAnimation mainmenuanim;

    public void BackButton()
    {
        mainmenuanim.DORestart();
        settinganim.DOPlayBackwards();
    }


}
