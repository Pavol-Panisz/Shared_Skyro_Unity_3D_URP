using UnityEngine;

public class Manager : MonoBehaviour
{
    public static GameObject Instance;

    void Awake()
    {
        if (!Instance)
        {
            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
