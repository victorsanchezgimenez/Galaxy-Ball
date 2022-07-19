using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] Button[] buttons;
    private int levelsUnlocked;

    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for(int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }


}
