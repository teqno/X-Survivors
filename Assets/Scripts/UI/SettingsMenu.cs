using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    private void OnGUI()
    {
        if (MainManager.Instance.volume != volumeSlider.value)
        {
            MainManager.Instance.volume = volumeSlider.value;
        }
    }
}
