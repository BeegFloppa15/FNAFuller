using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadNextSceneCoroutine());
    }

    IEnumerator loadNextSceneCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("ThanksForPlayingScene");
    }

}
