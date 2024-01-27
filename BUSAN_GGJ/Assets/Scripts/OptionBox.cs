using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionBox : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField] private Slider[] slider;

    private void Start()
    {
        float value;
        for (int i = 0; i < slider.Length; i++)
        {
            mixer.GetFloat(slider[i].name,out value);
            slider[i].value = value;
        }
    }

    public void Set_Volume(Slider slider)
    {
        string type = slider.name;
        mixer.SetFloat(type, slider.value);


        if(slider.value == -20)
            mixer.SetFloat(type, -80f);
        else
            mixer.SetFloat(type, slider.value);
        float value;
        mixer.GetFloat(type, out value);

        if (type == "BGM") GameManager.Instance.bgm_vol = value;
        else GameManager.Instance.eff_vol = value;
    }
}
