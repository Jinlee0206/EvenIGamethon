using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserPanelViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputFieldNickname;
    [SerializeField]
    private TextMeshProUGUI textGamerId; //��µǴ� �г���
    [SerializeField]
    private TextMeshProUGUI textLevel;
    /*[SerializeField]
    private Slider sliderExperience;
    [SerializeField]
    private TextMeshProUGUI textThreadmill;
    [SerializeField]
    private TextMeshProUGUI textCorn;
    /*[SerializeField]
    private TextMeshProUGUI textBread;*/

    private void Awake()
    {
        BackendGameData.Instance.onGameDataLoadEvent.AddListener(UpdateGameData);
    }

    public void showNickname()
    {
        inputFieldNickname.GetComponent<TextMeshProUGUI>().text = UserInfo.Data.nickname == null ?
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;

        textGamerId.text = inputFieldNickname.GetComponent<TextMeshProUGUI>().text;
    }

    public void UpdateGameData()
    {
        textLevel.text = $"{BackendGameData.Instance.UserGameData.level}";

        //�ӽ÷� �ִ� ����ġ�� 100�� �ǰ� ������
        //sliderExperience.value = BackendGameData.Instance.UserGameData.experience / 100;
        //textThreadmill.text = $"{BackendGameData.Instance.UserGameData.threadmill}";
        //textCorn.text = $"{BackendGameData.Instance.UserGameData.corn}";
        //textBread.text = $"{BackendGameData.Instance.UserGameData.bread}";
    }
}
