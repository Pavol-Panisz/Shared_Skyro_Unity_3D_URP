using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Toto viem volaù z Unity Eventov
    public void LoadScene(string sceneName)
    {
        // nasledovne 2 riadky su nove - naspat zapnu kurzor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(sceneName);
    }
}
