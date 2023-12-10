using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtons : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    public GameObject lastActive;

    public void ButtonClick(GameObject activeScreen)
    {
        for(int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
        activeScreen.SetActive(true);
    }

    public void DisableCams()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].activeInHierarchy)
            {
                lastActive = screens[i];
            }
            screens[i].SetActive(false);
        }
    }

    public void EnableCams()
    {
        lastActive.SetActive(true);
    }
}
