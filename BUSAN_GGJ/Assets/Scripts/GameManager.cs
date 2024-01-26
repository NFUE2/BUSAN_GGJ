using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    private void Awake()
    {
        Set_Screen();
    }

    private void Set_Screen()
    {
        Screen.SetResolution(width,height,true);
    }
}
