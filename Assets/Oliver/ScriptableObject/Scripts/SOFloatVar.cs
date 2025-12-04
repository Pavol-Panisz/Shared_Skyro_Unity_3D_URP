using UnityEngine;

[CreateAssetMenu(fileName = "SOFloatVar", menuName = "Scriptable Objects/SOFloatVar")]
public class SOFloatVar : ScriptableObject
{
    [Range(0, 100)]public float value;
}
