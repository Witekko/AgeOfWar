using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer myMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetMasterVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        myMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }
}