using BackEnd;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button logoutButton;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Inst;
    }

    void Start()
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = audioManager.GetVolume(AudioManager.AudioType.BGM);
            bgmSlider.onValueChanged.AddListener(value => audioManager.OnVolumeChanged(AudioManager.AudioType.BGM, value));
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = audioManager.GetVolume(AudioManager.AudioType.SFX);
            sfxSlider.onValueChanged.AddListener(value => audioManager.OnVolumeChanged(AudioManager.AudioType.SFX, value));
        }
    }

    public void OnClickLogout()
    {
        logoutButton.interactable = false;
        CutSceneManager.Inst.cutSceneType = CutSceneData.CutSceneType.Opening; // �α׾ƿ��� ������ ���� �ٽ� ���
        Backend.BMember.Logout((callback) => {
            if(callback.IsSuccess())
            {
                LogoutGoogle();
                SceneManager.LoadScene("Login");
            }
        });
    }

    public void LogoutGoogle()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.Log("���۷α׾ƿ�");
            PlayGamesPlatform.Instance.SignOut();
            //PlayGamesPlatform.Activate();
        }
        else
        {
            Debug.Log("���� ���� �ƴ�");
            return;
        }
    }
}
