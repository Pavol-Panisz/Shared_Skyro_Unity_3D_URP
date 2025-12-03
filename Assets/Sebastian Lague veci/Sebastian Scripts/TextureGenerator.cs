using UnityEngine;

public static class TextureGenerator 
{
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texure = new Texture2D(width, height);
        texure.SetPixels(colorMap);
        texure.Apply();
        return texure;
    }

    public static Texture2D TextureFromHeightMap(float [,] heightMap)
    {
        int width = heightMap.GetLength (0);
        int heght = heightMap.GetLength (1);

        Color[] colorMap = new Color[width * heght];
        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x,y]);
            }
        }
        return TextureFromColorMap (colorMap, width, heght);
    }
}
