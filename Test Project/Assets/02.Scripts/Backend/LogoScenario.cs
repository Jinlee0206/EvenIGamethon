using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScenario : MonoBehaviour
{
    [SerializeField]
    private Progress progress;

    private void Awake()
    {
        SystemSetup();
    }

    private void SystemSetup()
    {
        Application.runInBackground = true; //��׶��忡�� ������ �����

        //int width = Screen.width;                         
        //int height = (int)(Screen.width * 16 / 9);
        //Screen.SetResolution(width, height, true);        // ��ũ�� �ػ� ���� ���� ����

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        progress.Play(OnAfterProgress);
    }

    private void OnAfterProgress()
    {
        SceneManager.LoadScene("Login");
    }
}