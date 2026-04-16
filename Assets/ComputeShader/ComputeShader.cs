using UnityEngine;

public class ComputeShaderTest : MonoBehaviour
{
    public ComputeShader computeShader;
    public Renderer targetRenderer;

    void Start()
    {
        RenderTexture rt = new RenderTexture(256, 256, 0);
        rt.enableRandomWrite = true;
        rt.Create();

        int kernel = computeShader.FindKernel("CSMain");
        computeShader.SetTexture(kernel, "Result", rt);

        computeShader.Dispatch(kernel, 32, 32, 1); // 256 / 8 = 32

        targetRenderer.material.mainTexture = rt;
    }
}
