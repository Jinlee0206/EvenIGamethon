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
        SFX_OpeningEffect = 0,
        SFX_UI = 1,

        SFX_Wheel = 4,
        SFX_Corn = 5,
        SFX_Sub_Hamster_Voice_01 = 11,
        SFX_Sub_Hamster_Voice_02 = 12,
        SFX_Sub_Hamster_Voice_03 = 13,
        SFX_Sub_Hamster_Voice_04 = 14,
        SFX_Sub_Hamster_Voice_05 = 15,

        SFX_Grass_Effect = 29,
        SFX_Castle_Brake_01 = 30,
        SFX_Castle_Brake_02 = 31,
        SFX_Main_Hamster_Attack1 = 32,
        SFX_Main_Hamster_Attack2 = 33,
        SFX_Main_Hamster_Attack3 = 34,
        SFX_Main_Hamster_Attack_Woosh = 35,
        SFX_Main_Hamster_Fire_Attack = 36,
        SFX_Main_Hamster_Ice_Attack = 37,
        SFX_Main_Hamster_Electric_Attack = 38,
        SFX_Main_Hamster_Lightning_Attack = 39,
        SFX_Main_Hamster_Dark_Attack = 40,
        SFX_Main_Hamster_Missile_Attack = 41,
        SFX_Sub_Hamster_Arrow_Attack = 42,
        SFX_Sub_Hamster_Cannon_Attack = 43,
        SFX_Sub_Hamster_Black_Magic_Spell = 44,
        SFX_Sub_Hamster_Sheild_Spell = 45,
        SFX_Sub_Hamster_Heal_Spell = 46,
        SFX_Monster_Move_01 = 47,
        SFX_Monster_Move_02 = 48,
        SFX_Boss_Warning = 49,
        SFX_Monster_Smash_Castle_01 = 50,
        SFX_Monster_Smash_Castle_02 = 51,
        SFX_Monster_Smash_Castle_03 = 52,
        SFX_Monster_Die_01 = 53,
        SFX_Monster_Die_02 = 54,
        SFX_Monster_Die_03 = 55,
        SFX_Stage_Fail = 56,
        SFX_In_Game_Level_Up = 57,
        SFX_Stage_Clear = 58,
        SFX_Select_Skill = 59,
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
