using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionBox : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    public void Set_Volume(Slider slider)
    {
        string type = slider.name;
        mixer.SetFloat(type, slider.value);

        Debug.Log($"{type} {slider.value}");

        if(slider.value == -20)
            mixer.SetFloat(type, -80f);
        else
            mixer.SetFloat(type, slider.value);
        float value;
        mixer.GetFloat(type, out value);
        Debug.Log(value);
    }
}
