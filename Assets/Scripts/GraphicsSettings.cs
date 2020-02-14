using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public AudioMixer masterMixer;

    private void Start()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        float masterVolume = 0;
        masterMixer.GetFloat("Master", out masterVolume);
        volumeSlider.value = masterVolume;
    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("Master", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
