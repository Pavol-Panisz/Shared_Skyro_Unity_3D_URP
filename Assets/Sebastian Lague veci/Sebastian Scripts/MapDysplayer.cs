using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class MapDysplayer : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength (0);
        int heght = noiseMap.GetLength (1);

        Texture2D texture = new Texture2D(width, heght);

        Color[] colorMap = new Color[width * heght];
        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x,y]);
            }
        }
        texture.SetPixels (colorMap);
        texture.Apply ();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, heght);
    }
}
