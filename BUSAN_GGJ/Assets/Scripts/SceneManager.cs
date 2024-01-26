using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void Reload()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
