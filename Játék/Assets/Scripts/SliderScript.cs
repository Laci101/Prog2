using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{
    public string playerPrefsKey = "SliderValue";

    private UnityEngine.UI.Slider m_slider;

    void Start()
    {
        m_slider = GetComponent<UnityEngine.UI.Slider>();
        if (m_slider != null)
        {
            if (PlayerPrefs.HasKey(playerPrefsKey))
            {
                m_slider.value = PlayerPrefs.GetFloat(playerPrefsKey);
            }
            m_slider.onValueChanged.AddListener(delegate { SaveSliderValue(); });
        }
    }
    void SaveSliderValue()
    {
        PlayerPrefs.SetFloat(playerPrefsKey, m_slider.value);
    }

}
