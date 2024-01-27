using UnityEngine;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int width = 1920;

    [SerializeField]
    private int height = 1080;

    public bool[] clear_check = new bool[2];
    public bool[] album = new bool[6];

    public float bgm_vol = 0.0f;
    public float eff_vol = 0.0f;


    private void Start()
    {
        Set_Screen();
        Application.targetFrameRate = 60;
    }

    private void Set_Screen()
    {
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        float aspect = (float)width / height;
        float deviceAspect = (float)deviceWidth / deviceHeight;

        Screen.SetResolution(width, (int)((float)deviceHeight / deviceWidth * width), true);

        if (aspect < deviceAspect)
        {
            float newWidth = aspect / deviceAspect;
            Camera.main.rect = new Rect((1.0f - newWidth) / 2.0f, 0.0f, newWidth, 1.0f);
        }
        else
        {
            float newHeight = (deviceAspect / aspect);
            Camera.main.rect = new Rect(0.0f, (1.0f - newHeight) / 2.0f, 1.0f, newHeight);
        }
    }
}
