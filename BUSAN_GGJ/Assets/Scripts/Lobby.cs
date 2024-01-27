using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Lobby : MonoBehaviour
{
    [SerializeField] public static bool[] album = new bool[6];
    [SerializeField] private Button[] ending_btn;
    [SerializeField] private GameObject notion;
    void Start()
    {
        bool chk = album.SequenceEqual(GameManager.Instance.album);
        if (!chk)
        {
            notion.SetActive(true);
            album = GameManager.Instance.album;
        }

        for (int i = 0; i < album.Length; i++)
            ending_btn[i].interactable = album[i];
    }


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
