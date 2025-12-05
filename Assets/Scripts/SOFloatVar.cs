using UnityEngine;

[CreateAssetMenu] 
public class SOFloatVar : ScriptableObject
{
    [SerializeField] float defaultValue;
    public float value;

    
    void OnEnable()
    {
        value = defaultValue;        
    }

    


}
