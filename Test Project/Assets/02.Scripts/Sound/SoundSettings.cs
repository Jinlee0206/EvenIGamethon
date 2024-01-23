using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;

    void Start()
    {
        SoundManager.Inst.PlayBGM(Resources.Load("GAME_MAIN_BGM_01") as AudioClip);
        //bgmSlider.value = SoundManager.Inst.BgmVolume;

    }

    public void OnChangeBGMVolume(float v)
    {
        SoundManager.Inst.BgmVolume = v;
        // PlayerPrefs�� �����ϴ� ������ �ʿ��ϴٸ� ���⿡ �߰��� �� �ֽ��ϴ�.
    }
}
