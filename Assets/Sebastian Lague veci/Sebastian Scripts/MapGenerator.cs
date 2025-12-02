using Unity.Mathematics;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        MapDysplayer display = Object.FindFirstObjectByType<MapDysplayer> ();
        display.DrawNoiseMap(noiseMap);
    }
}
