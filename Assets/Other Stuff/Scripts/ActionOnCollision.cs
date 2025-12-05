using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnCollision : MonoBehaviour
{
    public UnityEvent OnCollision;

    public UnityEvent OnExit;

    public List<string> acceptedTags;

    [Header("Visual Feedback")]
    public Material TurnedOnMaterial;
    public Material TurnedOffMaterial;

    private MeshRenderer meshRenderer;
    private int collisionCount = 0;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateMaterial();
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (acceptedTags.Contains(tag))
        {
            collisionCount++;
            OnCollision?.Invoke();
            UpdateMaterial();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (acceptedTags.Contains(tag))
        {
            collisionCount--;
        }

        if (collisionCount <= 0)
        {
            OnExit?.Invoke();
        }
    }

    void UpdateMaterial()
    {
        if (meshRenderer == null) return;

        if (collisionCount > 0 && TurnedOnMaterial != null) {
            meshRenderer.material = TurnedOnMaterial;
        }
        else if (collisionCount <= 0 && TurnedOffMaterial != null) {
            meshRenderer.material = TurnedOffMaterial;
        }
    }
}