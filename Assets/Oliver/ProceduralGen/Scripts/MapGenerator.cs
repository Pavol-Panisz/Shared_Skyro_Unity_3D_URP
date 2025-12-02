using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode{NoiseMap, ColorMap}
    [SerializeField]private DrawMode drawMode;

    [SerializeField]private int mapHeight;
    [SerializeField]private int mapWidth;
    [SerializeField]private float scale;

    [SerializeField]private int octaves;
    [SerializeField, Range(0, 1)]private float persistance;
    [SerializeField]private float lacunarity;

    [SerializeField]private int seed;
    [SerializeField]private Vector2 offset;

    public bool autoUpdate;

    [SerializeField]private TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
    
        MapDisplay mapDisplay = FindAnyObjectByType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
    }

    private void OnValidate() {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }

        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
