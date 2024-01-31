using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum AudioType { BGM, SFX }

    [Header("#BGM")]
    public AudioClip[] bgmClips;                        // BGM Ŭ�� ������
    public float bgmVolume;
    AudioSource bgmPlayer;                              // BGM �÷��̾�� ����

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;                                // SFX ���� ä��
    AudioSource[] sfxPlayers;                           // SFX�� ���ÿ� �������� �����
    int channelIndex;

    public enum BGM {
        BGM_Opening,
        BGM_OpeningCartoon,
        BGM_Lobby,
        BGM_Shop,
        BGM_IllustratedGuideArchive,
        BGM_IllustratedGuideHamster,
        BGM_IllustratedGuideMonster,
        BGM_Chapter01,
        BGM_Chapter01Cartton
    }

    public enum SFX {
        SFX_OpeningEffect,
        SFX_UI,

        SFX_Wheel = 4,
        SFX_Corn = 5,

    }

    public float BGMVolume
    {
        get => GetVolume(AudioType.BGM);
        set => OnVolumeChanged(AudioType.BGM, value);
    }

    public float SFXVolume
    {
        get => GetVolume(AudioType.SFX);
        set => OnVolumeChanged(AudioType.SFX, value);
    }

    private void Awake()
    {
        this.Initialize_DontDestroyOnLoad();
        Init();
    }

    private void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;                          // ���� ���� �� ��� ����
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // �뷮 ����ȭ
        bgmPlayer.dopplerLevel = 0.0f;
        bgmPlayer.reverbZoneMix = 0.0f;
        //bgmPlayer.clip = bgmClips;

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int idx = 0; idx < sfxPlayers.Length; idx++)
        {
            sfxPlayers[idx] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[idx].playOnAwake = false;
            sfxPlayers[idx].volume = sfxVolume;
            sfxPlayers[idx].dopplerLevel = 0.0f;
            sfxPlayers[idx].reverbZoneMix = 0.0f;
        }

        bgmVolume = 1.0f - PlayerPrefs.GetFloat("BGM_Volume");           // default ���� 0�̱� ������ 1.0f - value�� ����
        sfxVolume = 1.0f - PlayerPrefs.GetFloat("Effect_Volume");
    }

    // BGM ����� ���� �Լ�
    public void PlayBgm(BGM bgm)
    {
         bgmPlayer.clip = bgmClips[(int)bgm];
         bgmPlayer.Play();
    }

    public void StopBgm()
    {
        if (bgmPlayer != null) bgmPlayer.Stop();
    }

    // ȿ���� ����� ���� �Լ�
    public void PlaySfx(SFX sfx)
    {
        // ���� �ִ� �ϳ��� sfxPlayer���� clip�� �Ҵ��ϰ� ����
        for (int idx = 0; idx < sfxPlayers.Length; idx++)
        {
            int loopIndex = (idx + channelIndex) % sfxPlayers.Length;    // ä�� ������ŭ ��ȸ�ϵ��� ä���ε��� ���� Ȱ��

            if (sfxPlayers[loopIndex].isPlaying) continue;               // ���� ���� sfxPlayer�� �� ����

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void OnChangedBGMVolume(float value)
    {
        BGMVolume = value;
        bgmPlayer.volume = BGMVolume;
    }

    public float GetVolume(AudioType type)
    {
        return type == AudioType.BGM ? bgmPlayer.volume : sfxPlayers[0].volume;
    }

    public void OnVolumeChanged(AudioType type, float value)
    {
        PlayerPrefs.SetFloat(type == AudioType.BGM ? "BGM_Volume" : "SFX_Volume", 1.0f - value);

        if (type == AudioType.BGM)
        {
            bgmPlayer.volume = value;
        }
        else
        {
            foreach (var player in sfxPlayers)
            {
                player.volume = value;
            }
        }
    }
}
