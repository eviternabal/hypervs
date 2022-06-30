using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadSceneAsyncronic(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
