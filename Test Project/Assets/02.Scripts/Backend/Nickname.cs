using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using UnityEditor.VersionControl;

public class Nickname : LoginBase
{
    [System.Serializable]
    public class NicknameEvent : UnityEngine.Events.UnityEvent { }
    public NicknameEvent onNicknameEvent = new NicknameEvent();

    [SerializeField]
    private Image imageNickname;
    [SerializeField]
    private TMP_InputField inputFieldNickname;
    [SerializeField]
    private Button btnUpdateNickname;
    [SerializeField]
    private GameObject nicknameWarningPanel;
    [SerializeField]
    private GameObject nicknameWaringBoard;
    [SerializeField]
    private TextMeshProUGUI warningText;

    private void OnEnable()
    {
        ResetUI(imageNickname);
        SetMessage("");
    }

    public void OnClickUpdateNickname()
    {
        ResetUI(imageNickname);
        if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname")) return;

        btnUpdateNickname.interactable = false;
        SetMessage("�г��� �������Դϴ�..");

        UpdateNickname();
    }

    private void UpdateNickname()
    {
        string message = string.Empty;

        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {
            btnUpdateNickname.interactable = true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldNickname.text}\n(��)�� �г�����\n����Ǿ����ϴ�.");
                message = $"{inputFieldNickname.text}\n(��)�� �г�����\n����Ǿ����ϴ�.";
                onNicknameEvent.Invoke();
            }
            else
            {
                switch(int.Parse(callback.GetStatusCode())) 
                {
                    case 400:
                        message = "�г����� ����ų�\n20�� �̻��̰ų�\n��/�ڿ� ������ �ֽ��ϴ�.";
                        break;
                    case 409:
                        message = "�̹� �����ϴ�\n�г����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                GuideForIncorrectlyEnteredData(imageNickname, message);
            }
            nicknameWarningPanel.SetActive(true);
            nicknameWaringBoard.SetActive(true);
            warningText.text = message;
        });
    }
}
