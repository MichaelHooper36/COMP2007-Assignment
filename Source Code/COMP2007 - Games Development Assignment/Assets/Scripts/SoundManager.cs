using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider soundSlider;

    public GameObject volumeNumber;
    public GameObject soundNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 50);
            PlayerPrefs.SetFloat("soundVolume", 50);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        AudioListener.volume = soundSlider.value;

        volumeNumber.GetComponent<TextMeshProUGUI>().text = volumeSlider.value.ToString();
        soundNumber.GetComponent<TextMeshProUGUI>().text = soundSlider.value.ToString();

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
}
