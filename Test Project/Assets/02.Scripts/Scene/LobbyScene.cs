using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LobbyScene : MonoBehaviour
{
    private static LobbyScene instance = null;
    public static LobbyScene Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LobbyScene();
            }
            return instance;
        }
    }

    [SerializeField]
    private UserInfo user;
    [SerializeField]
    private TextMeshProUGUI textThreadmill;
    [SerializeField]
    private TextMeshProUGUI textCorn;

    [Header("# Background")]
    public GameObject lobbyBackground;
    public GameObject shopBackground;
    public GameObject dogamBackground;

    private void Awake()
    {
        user.GetUserInfoFromBackend();
        BackendGameData.Instance.onGameDataLoadEvent.AddListener(UpdateCurrencyData); //GameData���� ������
        BackendGameData.Instance.GameDataLoad();
    }
    private void Start()
    {
        AudioManager.Inst.StopBgm();
        AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Lobby);
        PopUpHandler.Inst.lobbyLoadEvent.AddListener(loadLobby);

        /*await BackendGameData.Instance.GameDataLoad(); // await�� �߰��Ͽ� �񵿱� �޼��尡 �Ϸ�� ������ ���
        UpdateCurrencyData(); //�� �񵿱Ⱑ ����Ǹ� ȣ��*/
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            BackendGameData.Instance.UserGameData.bread += 1000;
            BackendGameData.Instance.GameDataUpdate();
        }
    }

    public void UpdateCurrencyData()
    {
        Debug.Log("�ڿ� ������Ʈ");
        textThreadmill.text = $"{BackendGameData.Instance.UserGameData.threadmill} " + "/ 10";
        textCorn.text = $"{BackendGameData.Instance.UserGameData.corn}";
    }

    public void OnClickRefreshThreadmill()
    {
        BackendGameData.Instance.UserGameData.threadmill = 10;
        BackendGameData.Instance.GameDataUpdate();
    }

    public void loadLobby()
    {
        lobbyBackground.SetActive(true);
        shopBackground.SetActive(false);
        dogamBackground.SetActive(false);
    }

    public void loadShop()
    {
        lobbyBackground.SetActive(false);
        shopBackground.SetActive(true);
        dogamBackground.SetActive(false);
    }

    public void loadDogam()
    {
        lobbyBackground.SetActive(false);
        shopBackground.SetActive(false);
        dogamBackground.SetActive(true);
    }
}
