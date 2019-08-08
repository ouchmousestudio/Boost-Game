using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] float timeToWait = 1.3f
    ;
    public void LoadNextScene()
    {
        StartCoroutine(WaitForTime());
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextScene)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void LoadStartScene()
    {
        StartCoroutine(WaitForTime());
        SceneManager.LoadScene(0);

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
    }

}
