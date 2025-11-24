using UnityEditor.Animations;
using UnityEngine;

public class Fractals : MonoBehaviour
{
    [SerializeField, Range(1, 20)] int depth = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name = "Fractal " + depth;

        if (depth <= 1)
        {
            return;
        }

        Fractals childA = CreateChild(Vector3.right, Quaternion.Euler(0f, 0f, -90f));
        Fractals childB = CreateChild(Vector3.up, Quaternion.identity);
        Fractals childC = CreateChild(Vector3.left, Quaternion.Euler(0f, 0f, 90f));
        Fractals childD = CreateChild(Vector3.forward, Quaternion.Euler(90f, 0f, 0f));
        Fractals childE = CreateChild(Vector3.back, Quaternion.Euler(-90f, 0f, 0f));

        childA.transform.SetParent(transform, false);
        childB.transform.SetParent(transform, false);
        childC.transform.SetParent(transform, false);
        childD.transform.SetParent(transform, false);
        childE.transform.SetParent(transform, false);
    }

    void Update()
    {
        transform.Rotate(0f, 22.5f * Time.deltaTime, 0f);
    }

    Fractals CreateChild(Vector3 direction, Quaternion rotation)
    {
        Fractals child = Instantiate(this);
        child.depth = depth -1;
        child.transform.localPosition = 0.75f * direction;
        child.transform.localRotation = rotation;
        child.transform.localScale = 0.5f * Vector3.one;
        return child;
    }
}
