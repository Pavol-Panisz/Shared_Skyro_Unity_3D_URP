using System;
using Unity.Collections;
using UnityEngine;

public static class Noise 
{
    public enum NormalizedMode {Local, Global}

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacumarity, int seed, Vector2 offset, NormalizedMode normalizedMode)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float maxPossibleHeight = 0;
        float aplitude = 1;
        float frequency = 1;

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += aplitude;
            aplitude *= persistance;
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxLocalNoiseMap = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                aplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i <octaves; i++)
                {
                    float sampleX = (x - halfHeight + octaveOffsets[i].x) / scale * frequency ;
                    float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * aplitude;

                    aplitude *= persistance;
                    frequency *= lacumarity;
                }  
                if (noiseHeight > maxLocalNoiseMap)
                {
                    maxLocalNoiseMap = noiseHeight;
                }
                else if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }
                noiseMap [x, y] = noiseHeight;
            }
        }

        for (int y = 0; y <mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                if (normalizedMode == NormalizedMode.Local)
                {
                   noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseMap, noiseMap[x, y]); 
                }
                else
                {
                    float normalizedHeight = (noiseMap[x, y] + 1) / (2 * maxPossibleHeight / 1.1f);
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight,0, int.MaxValue);
                }
            }
        }

        return noiseMap;
    }
}
