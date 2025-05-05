using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Diagnostics;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volume_slider;
    [SerializeField] Slider music_slider;
    [SerializeField] Slider sound_slider;

    public GameObject volume_number;
    public GameObject music_number;
    public GameObject sound_number;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("music_volume") && !PlayerPrefs.HasKey("sound_volume") && !PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("music_volume", 0.5f);
            PlayerPrefs.SetFloat("sound_volume", 1.0f);
            PlayerPrefs.SetFloat("volume", 1.0f);
            Load();
        }
        else
        {
            Load();
        }

        volume_number.GetComponent<TextMeshProUGUI>().text = Mathf.Round(volume_slider.value * 100).ToString();
        music_number.GetComponent<TextMeshProUGUI>().text = Mathf.Round(music_slider.value * 100).ToString();
        sound_number.GetComponent<TextMeshProUGUI>().text = Mathf.Round(sound_slider.value * 100).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volume_slider.value;

        if (FPSController.background_music != null)
        {
            FPSController.background_music.volume = music_slider.value;
        }
        else
        {
            UnityEngine.Debug.LogWarning("FPSController.background_music is null");
        }

        volume_number.GetComponent<TextMeshProUGUI>().text = Mathf.Round(volume_slider.value * 100).ToString();
        music_number.GetComponent<TextMeshProUGUI>().text = Mathf.Round(music_slider.value * 100).ToString();
        sound_number.GetComponent<TextMeshProUGUI>().text = Mathf.Round(sound_slider.value * 100).ToString();

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume",  volume_slider.value);
        PlayerPrefs.SetFloat("music_volume", music_slider.value);
        PlayerPrefs.SetFloat("sound_volume", sound_slider.value);
    }

    private void Load()
    {
        if (volume_slider != null)
            volume_slider.value = PlayerPrefs.GetFloat("volume");

        if (music_slider != null)
            music_slider.value = PlayerPrefs.GetFloat("music_volume");

        if (sound_slider != null)
            sound_slider.value = PlayerPrefs.GetFloat("sound_volume");


    }
}
