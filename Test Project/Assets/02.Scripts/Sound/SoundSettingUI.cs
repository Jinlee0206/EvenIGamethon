using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingUI : MonoBehaviour
{
    public Slider bgmSlider;

    void Start()
    {
        SoundSettings soundSettings = FindObjectOfType<SoundSettings>();
        if (soundSettings != null)
        {
            // UnityAction�� �̺�Ʈ �ڵ鷯 ���
            bgmSlider.onValueChanged.AddListener(soundSettings.OnChangeBGMVolume);
        }
        else
        {
            Debug.LogError("SoundSettings not found in the scene.");
        }
    }
}
