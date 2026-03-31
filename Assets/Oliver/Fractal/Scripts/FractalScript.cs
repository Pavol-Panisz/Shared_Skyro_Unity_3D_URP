using Unity.Mathematics;
using UnityEngine;

namespace Fractal
{
    public class FractalScript : MonoBehaviour
    {
        [SerializeField]private int xRes;
        [SerializeField]private int yRes;

        [SerializeField]private float zoom;
        [SerializeField]private int iterations = 30;

        public int xOff;
        public int yOff;

        Texture2D texture;
        Color32[] colorBuffer;

        public Material mat;

        void Start()
        {
            texture = new Texture2D(xRes, yRes);
            texture.filterMode = FilterMode.Point;
            
            colorBuffer = new Color32[xRes * yRes];

            mat.SetTexture("_BaseMap", texture);

            texture.Apply();            
        }

        void Update()
        {
            float xPos;
            float yPos;

            for (int x = 0; x < xRes; x++)
            {
                for (int y = 0; y < yRes; y++)
                {
                    xPos = (((float)x / xRes) * 2 - 1) / zoom + xOff;
                    yPos = (((float)y / yRes) * 2 - 1) / zoom + yOff;
                    
                    float2 c = new float2(xPos, yPos);
                    float2 z = float2.zero;

                    bool isInside = true;
                    for (int n = 0; n < iterations; n++)
                    {
                        Vector2 z_next = SquareComplexNumber(z) + c;
                        
                        if (z_next.magnitude > 2f)
                        {
                            isInside = false;
                            Color finalCol = Color.Lerp(Color.white, Color.black, (float)n / iterations);
                            texture.SetPixel(x, y, finalCol);
                            break;
                        }

                        z = z_next;
                    }

                    if (isInside)
                    {
                        texture.SetPixel(x, y, Color.black);
                    }
                }
            }

            texture.Apply();
        }

        private float2 SquareComplexNumber(float2 inp)
        {
            float real =  inp.x * inp.x + inp.y * inp.y *(-1);
            float imag =  2 * inp.x * inp.y;

            return new float2(real, imag);
        }
    }
}