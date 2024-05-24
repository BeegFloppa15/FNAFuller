using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void quitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void startNewGame()
    {
        StartCoroutine(startNewGameCoroutine());
    }

    IEnumerator startNewGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("TestingUIAttempt2");
    }
}
