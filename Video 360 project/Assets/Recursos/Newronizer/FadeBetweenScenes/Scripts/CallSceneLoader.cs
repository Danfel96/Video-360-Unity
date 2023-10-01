using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSceneLoader : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneLoader.Instance.LoadScene(scene);
    }

    public void Quit()
    {
        SceneLoader.Instance.QuitGame();
    }
}
