using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
