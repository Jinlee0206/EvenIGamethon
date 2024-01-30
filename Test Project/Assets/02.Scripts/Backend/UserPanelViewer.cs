using UnityEngine;
using TMPro;

public class UserPanelViewer : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFieldNickname;

    public void showNickname()
    {
        inputFieldNickname.placeholder.GetComponent<TextMeshProUGUI>().text = UserInfo.Data.nickname == null ?
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;
    }

    //�� �ؿ� �г��� ���� ��ư ��Ŭ�� �����
    public void UpdateNickname()
    {

    }
}
