using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using UnityEngine.SceneManagement;
public class Login : LoginBase
{
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputFieldPW;
    [SerializeField]
    private Button btnLogin;

    public void OnClickLogin()
    {
        ResetUI(imageID, imagePW);

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "��й�ȣ")) return;

        btnLogin.interactable = false;

        StartCoroutine(nameof(LoginProcess));

        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
    }

    private void ResponseToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProcess));

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldID.text}�� ȯ���մϴ�.");
                //SceneManager.LoadScene("Lobby");
                SceneManager.LoadScene("CutScene");
            } 
            else
            {
                btnLogin.interactable = true;

                string message = string.Empty;

                switch(int.Parse(callback.GetStatusCode()) )
                {
                    case 401: //�������� �ʴ� ���̵�, Ʋ�� ��й�ȣ
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵� �Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                        break;
                    case 403: //���� or ����̼� ����
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410:
                        message = "Ż�� �������� �����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if(message.Contains("��й�ȣ"))
                {
                    GuideForIncorrectlyEnteredData(imagePW, message);
                }
                else
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
            }
        });
    }

    private IEnumerator LoginProcess()
    {
        float time = 0;

        while(true)
        {
            time += Time.deltaTime;

            SetMessage($"�α��� ���Դϴ�... {time:F1}");

            yield return null;
        }
    }   

    public void PlayUI()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
    }
}
