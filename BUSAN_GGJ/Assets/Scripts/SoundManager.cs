using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out audio);
    }

    public AudioSource get_audio { get { return audio; } }
}
