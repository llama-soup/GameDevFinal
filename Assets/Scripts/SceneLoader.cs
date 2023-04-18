using UnityEngine;
using UnityEngine.SceneManagement;

// credit: https://blog.insane.engineer/post/unity_button_load_scene/

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
