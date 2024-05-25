using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThxScreenManager : MonoBehaviour
{
    public CanvasGroup myGroup;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeAndLoadCoroutine());
    }

    IEnumerator fadeAndLoadCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
        while (myGroup.alpha < 1)
        {
            myGroup.alpha += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }


    public void returnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
