using BackEnd;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppQuitBoard : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;

    public void OnClickYes()
    {
        yesButton.interactable = false;

        if (Backend.IsInitialized && Backend.UserInDate != null) //������ ����Ǿ��ְ�, �α��� ������ ���
        {
            Backend.BMember.Logout((callback) =>
            {
                if (callback.IsSuccess())
                {
                    if (PlayGamesPlatform.Instance.IsAuthenticated())
                    {
                        Debug.Log("���۷α׾ƿ�");
                        PlayGamesPlatform.Instance.SignOut();
                        Debug.Log("���� ����");
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    }
                }
                else
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                }
            });
        }
        else
        {
            Debug.Log("���� ����");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void OnClickNo()
    {
        noButton.interactable = false;
        Destroy(gameObject);
        AppController.instance.isOn = false;
    }

    private void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }
}
