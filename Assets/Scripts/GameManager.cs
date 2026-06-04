using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Player { get; private set; }
    [SerializeField] private GameObject playerReference;
    
    private void Awake()
    {
        if (!playerReference)
            throw new System.NullReferenceException("No player reference found");
        
        Player = playerReference;
    }
}