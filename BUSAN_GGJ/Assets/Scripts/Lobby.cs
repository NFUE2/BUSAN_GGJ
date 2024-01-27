using UnityEngine;
using UnityEngine.UI;


public class Lobby : MonoBehaviour
{
    [SerializeField ]private static bool[] album = new bool[6];
    [SerializeField] private GameObject new_img;

    void Start()
    {
        if(album == GameManager.Instance.album)
        {
            Debug.Log("1");
        }
        else
        {
            Debug.Log("0");
        }
    }
}
