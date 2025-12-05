using JetBrains.Annotations;
using UnityEngine;

public static class MeshGenerator
{
    public static void GenerateTerrainMesh(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y], topLeftZ - y);

                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangels(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangels(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex ++;
            }
        }
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangels;
    public Vector2[] uvs;

    int triangelIndex;

    public MeshData (int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        triangels = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangels(int a, int b, int c)
    {
        triangels [triangelIndex] = a;
        triangels [triangelIndex + 1] = a;
        triangels [triangelIndex + 2] = c;
        triangelIndex += 3;
    }
}
