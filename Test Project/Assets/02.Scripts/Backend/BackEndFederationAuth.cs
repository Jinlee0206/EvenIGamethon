using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using BackEnd;
using UnityEngine.SceneManagement;

public class BackEndFederationAuth : LoginBase
{
    private const string BoolKey = "HaveGoogleLogined";
    private bool defaultValue = false; // ����� ���� ���� �� ��ȯ�� �⺻��

    // GPGS �α��� 
    void Start()
    {
        Debug.Log("���� �α��� �õ�");
        // GPGS �÷����� ����
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
            .Builder()
            .RequestServerAuthCode(false)
            .RequestEmail() // �̸��� ������ ��� ���� �ʴٸ� �ش� ��(RequestEmail)�� �����ּ���.
            .RequestIdToken()
            .Build();
        //Ŀ���� �� ������ GPGS �ʱ�ȭ
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true; // ����� �α׸� ���� ���� �ʴٸ� false�� �ٲ��ּ���.
                                                  //GPGS ����.
        PlayGamesPlatform.Activate();
        //GPGSLogin();

        // Bool ���� �˻��մϴ�. ����� ���� ������ �⺻���� ����մϴ�.
        bool myBool = GetBool(BoolKey, defaultValue);
        Debug.Log("My bool value: " + myBool);

        // ����� ���� ������ �⺻���� �����մϴ�.
        SetBool(BoolKey, myBool);

        if (myBool) GPGSLogin(); //���� �α��� �� ���� �ִٸ� �ڵ� �α���

        string message = string.Empty;
    }

    private bool GetBool(string key, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(key)) // Ű�� �����ϴ� ���
        {
            return IntToBool(PlayerPrefs.GetInt(key));
        }
        else // Ű�� �������� �ʴ� ���
        {
            return defaultValue;
        }
    }

    private void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, BoolToInt(value));
        PlayerPrefs.Save();
    }

    private int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

    private bool IntToBool(int value)
    {
        return value == 1;
    }

    public void GPGSLogin()
    {
        // �̹� �α��� �� ���
        if (Social.localUser.authenticated == true)
        {
            BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");
            
        }
        else
        {
            Social.localUser.Authenticate((bool success) => {
                if (success)
                {
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
                    // �α��� ���� -> �ڳ� ������ ȹ���� ���� ��ū���� ���� ��û
                    BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");

                    //BackendGameData.Instance.GameDataInsert();
                    //SceneManager.LoadScene("Lobby");
                    //SceneManager.LoadScene("FirstPlay");
                    SetBool(BoolKey, true); //���۷α��� �غôٰ� ����

                    BackendGameData.Instance.GameDataLoad();
                    BackendGameData.Instance.TowerDataLoad();
                    //BackendGameData.Instance.DogamDataLoad();
                    BackendGameData.Instance.ClearDataLoad();
                    BackendGameData.Instance.StarDataLoad();

                    SceneManager.LoadScene("CutScene"); // �ƾ����� �Ѿ��
                }
                else
                {
                    // �α��� ����
                    Debug.Log("Login failed for some reason");
                }
            });
        }
    }

    // ���� ��ū �޾ƿ�
    public string GetTokens()
    {
        string message = string.Empty;

        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // ���� ��ū �ޱ� ù ��° ���
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            // �� ��° ���
            // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            return _IDtoken;
        }
        else
        {
            message = "���ӵǾ� ���� �ʽ��ϴ�. PlayGamesPlatform.Instance.localUser.authenticated :  fail";
            Debug.Log("���ӵǾ� ���� �ʽ��ϴ�. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
            return null;
        }
    }

    public void OnClickGPGSLogin()
    {
        string message = string.Empty;
        BackendReturnObject bro = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs�� ���� ����");
        if(bro.IsSuccess())
        {
            Debug.Log("���� ��ū���� �ڳ� �α��� ���� - ����");
        }
        else
        {
            switch(bro.GetStatusCode())
            {
                case "200":
                    message = "�̹� ȸ�����Ե� ȸ��";
                    Debug.Log("�̹� ȸ�����Ե� ȸ��");
                    break;
                case "403":
                    message = "���ܵ� �����, : " + bro.GetErrorCode();
                    Debug.Log("���ܵ� �����, : " + bro.GetErrorCode());
                    break;
                default:
                    message = "���� ���� ���� " + bro.GetErrorCode();
                    Debug.Log("���� ���� ���� �߻�" + bro.GetMessage());
                    break;
            }
        }
    }

    //�̹� ���Ե� ȸ���� �̸��� ���� ����
    public void OnClickUpdateEmail()
    {
        BackendReturnObject bro = Backend.BMember.UpdateFederationEmail(GetTokens(), FederationType.Google);
        if(bro.IsSuccess())
        {
            Debug.Log("�̸��� �ּ� ���� ����");
        }
        else
        {
            if (bro.GetStatusCode() == "404") Debug.Log("���̵� ã�� �� ����");
        }
    }

    public void OnClickCheckUserAuthenticate()
    {
        BackendReturnObject bro = Backend.BMember.CheckUserInBackend(GetTokens(), FederationType.Google);
        if(bro.GetStatusCode() == "200")
        {
            Debug.Log("�������� ������ " + bro.GetReturnValue());
        }
        else
        {
            Debug.Log("���Ե� ������ �ƴ�");
        }
    }

    //Ŀ���� ������ ������̼� �������� ����
    public void OnClickChangeCustomToFederation()
    {
        BackendReturnObject bro = Backend.BMember.ChangeCustomToFederation(GetTokens(), FederationType.Google);
    }
}
