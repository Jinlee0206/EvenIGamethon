using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using UnityEngine.SceneManagement;

public class RegisterAccount : LoginBase
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
    private Image imageConfirmPW;
    [SerializeField]
    private TMP_InputField inputFieldConfirmPW;
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;
    [SerializeField]
    private Button btnRegisterAccount;

    public void OnClickRegisterAccount()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "��й�ȣ")) return;
        if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "��й�ȣ Ȯ��")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "���� �ּ�")) return;

        if(!inputFieldPW.text.Equals(inputFieldPW.text))
        {
            GuideForIncorrectlyEnteredData(imageConfirmPW, "��й�ȣ�� ��ġ���� �ʽ��ϴ�");
            return;
        }

        if(!inputFieldEmail.text.Contains("@")) 
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.");
            return;
        }

        btnRegisterAccount.interactable = false;
        SetMessage("���� ���� ���Դϴ�...");

        CustomSignUp();
    }

    private void CustomSignUp() //ȸ������
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;

            if(callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
                {
                    if(callback.IsSuccess())
                    {
                        SetMessage($"���� ���� ����. {inputFieldID.text}�� ȯ���մϴ�.");

                        //���� ������ �������� �� �ش� ������ ���� ���� ����
                        BackendGameData.Instance.GameDataInsert();

                        //�κ�� �̵�
                        SceneManager.LoadScene("Lobby");
                    }
                });
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode())) 
                {
                    case 409: //�ߺ��� ���̵�
                        message = "�̹� �����ϴ� ���̵��Դϴ�.";
                        break;
                    case 403: //���ܴ��� ����̽�
                    case 401: //���� ����
                    case 400: //����̽� ������ null
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if(message.Contains("���̵�"))
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }
}
