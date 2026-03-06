using UnityEngine;

public class fpsCounter : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text fpsText;

    int frames;
    float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        frames++;
        if(timeElapsed >= 1)
        {
            fpsText.text = "FPS:" + frames;
            timeElapsed = 0;
            frames = 0;
        }
    }
}
