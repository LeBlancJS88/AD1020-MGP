using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer Mixer; // Reference to your AudioMixer

    public void SetMasterVolume(float sliderValue)
    {
        if (sliderValue <= 0.0001f)
            Mixer.SetFloat("YHD_Master", -80f);
        else
            Mixer.SetFloat("YHD_Master", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (sliderValue <= 0.0001f)
            Mixer.SetFloat("YHD_Master", -80f);
        else
            Mixer.SetFloat("YHD_Music", Mathf.Log10(sliderValue) * 20);
    }

    public void SetAmbientVolume(float sliderValue)
    {
        if (sliderValue <= 0.0001f)
            Mixer.SetFloat("YHD_Master", -80f);
        else
            Mixer.SetFloat("YHD_Ambient", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (sliderValue <= 0.0001f)
            Mixer.SetFloat("YHD_Master", -80f);
        else
            Mixer.SetFloat("YHD_SFX", Mathf.Log10(sliderValue) * 20);
    }
}