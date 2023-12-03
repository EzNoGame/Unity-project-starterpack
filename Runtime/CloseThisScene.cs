using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Close : MonoBehaviour
{
    public void CloseScene()
    {
        SceneManager.UnloadSceneAsync(this.gameObject.scene);
    }

    public void CloseObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
