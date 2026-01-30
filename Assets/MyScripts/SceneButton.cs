using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;
    [SerializeField, HideInInspector] private string scenePath;
    [SerializeField] private bool autoAddToBuildSettings = true;
#endif

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("SceneChanger: No scene assigned or name empty. Assign a Scene in the Inspector and add it to Build Settings.");
                return;
            }
            SceneManager.LoadScene(sceneName);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
            scenePath = AssetDatabase.GetAssetPath(sceneAsset);
        }

        if (!string.IsNullOrEmpty(scenePath))
        {
            bool found = false;
            foreach (var s in EditorBuildSettings.scenes)
            {
                if (s.path == scenePath)
                {
                    found = true;
                    if (!s.enabled)
                    {
                        Debug.LogWarning($"Scene '{sceneName}' is in Build Settings but disabled. Enable it to load at runtime.");
                        if (autoAddToBuildSettings)
                        {
                            EnableSceneInBuildSettings(scenePath);
                        }
                    }
                    break;
                }
            }
            if (!found)
            {
                Debug.LogWarning($"Scene '{sceneName}' is not added to Build Settings/Active Build Profile. Add it to load at runtime.");
                if (autoAddToBuildSettings && sceneAsset != null)
                {
                    AddSceneToBuildSettings(scenePath);
                }
            }
        }
    }

    [ContextMenu("Add Assigned Scene To Build Settings")]
    private void AddAssignedSceneToBuildSettings()
    {
        if (!string.IsNullOrEmpty(scenePath))
        {
            AddSceneToBuildSettings(scenePath);
        }
    }

    private static void AddSceneToBuildSettings(string path)
    {
        var scenes = new System.Collections.Generic.List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        foreach (var s in scenes)
        {
            if (s.path == path)
            {
                s.enabled = true;
                EditorBuildSettings.scenes = scenes.ToArray();
                Debug.Log($"Scene added/enabled in Build Settings: {path}");
                return;
            }
        }
        scenes.Add(new EditorBuildSettingsScene(path, true));
        EditorBuildSettings.scenes = scenes.ToArray();
        Debug.Log($"Scene added to Build Settings: {path}");
    }

    private static void EnableSceneInBuildSettings(string path)
    {
        var scenes = new System.Collections.Generic.List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i].path == path)
            {
                scenes[i].enabled = true;
                EditorBuildSettings.scenes = scenes.ToArray();
                Debug.Log($"Scene enabled in Build Settings: {path}");
                return;
            }
        }
    }
#endif
}
