using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroWarningsHandler : MonoBehaviour
{

    public CanvasGroup warningGroup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BruhCoroutine());
    }

    
    IEnumerator BruhCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        while (warningGroup.alpha < 1)
        {
            warningGroup.alpha += Time.deltaTime;
            yield return null;
        }

        yield return null;

        yield return new WaitForSeconds(5f);

        while (warningGroup.alpha > 0)
        {
            warningGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MainMenu");
    }
}
