using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject htp;
    [SerializeField] private GameObject quit;

    private void Awake()
    {
        levelSelector.SetActive(false);
        settings.SetActive(false);
        htp.SetActive(false);
        quit.SetActive(false);
    }

}
