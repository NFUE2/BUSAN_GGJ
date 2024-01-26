using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float width;

    [SerializeField]
    private float height;

    private void Awake()
    {
        
    }

    private void Set_Screen()
    {

        //Screen.SetResolution();
    }
}
