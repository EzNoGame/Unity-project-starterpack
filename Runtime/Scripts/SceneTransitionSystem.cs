using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionSystem : Singleton<SceneTransitionSystem>
{
    private string _sceneName;

    public void LoadSceneTransist(string sceneName)
    {
        _sceneName = sceneName;
        GetComponent<Animator>().Play("FadeIn");
    }

    public void LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                break;
            }
        }
        GetComponent<Animator>().Play("FadeOut");
    }
}