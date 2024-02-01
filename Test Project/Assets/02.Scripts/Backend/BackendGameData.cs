using System;
using UnityEngine;
using BackEnd;
using UnityEngine.Events;
using System.Threading.Tasks;

public class BackendGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackendGameData instance = null;
    public static BackendGameData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new BackendGameData();
            }

            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private string gameDataRowInDate = string.Empty;

    /// <summary>
    /// �ڳ� �ܼ� ���̺� ���ο� ���� ���� �߰�
    /// </summary>
    public void GameDataInsert()
    {
        userGameData.Reset();

        Param param = new Param()
        {
            {"level", userGameData.level },
            {"experience", userGameData.experience },
            {"bread", userGameData.bread },
            {"corn", userGameData.corn},
            {"threadmill", userGameData.threadmill}
        };

        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            if(callback.IsSuccess())
            {
                gameDataRowInDate = callback.GetInDate();

                Debug.Log($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");

                onGameDataLoadEvent?.Invoke();
            }
            else
            {
                Debug.LogError($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
        });
    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺��� ���� ������ �ҷ���
    /// </summary>
    public void GameDataLoad()
     {
         Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
         {
             if (callback.IsSuccess())
             {
                 Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ� : {callback}");

                 try
                 {
                     LitJson.JsonData gameDataJson = callback.FlattenRows();

                     if(gameDataJson.Count <= 0)
                     {
                         Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�");
                         GameDataInsert(); //�����Ͱ� ���ٴ� �Ŵϱ� ���� ����
                     }
                     else
                     {
                         gameDataRowInDate = gameDataJson[0]["inDate"].ToString();

                         userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                         userGameData.experience = float.Parse(gameDataJson[0]["experience"].ToString());
                         userGameData.bread = int.Parse(gameDataJson[0]["bread"].ToString());
                         userGameData.corn = int.Parse(gameDataJson[0]["corn"].ToString());
                         userGameData.threadmill = int.Parse(gameDataJson[0]["threadmill"].ToString());

                         onGameDataLoadEvent?.Invoke();
                     }
                 }
                 catch(System.Exception e)
                 {
                     userGameData.Reset();
                     Debug.LogError(e);
                 }
             }
             else
             {
                 Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ� : {callback}");
             }
         });
     }

    /*public Task GameDataLoad() //�񵿱� ȣ����
    {
        var tcs = new TaskCompletionSource<bool>();

        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            try
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ� : {callback}");

                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�");
                        GameDataInsert(); // �����Ͱ� ���ٴ� �Ŵϱ� ���� ����
                    }
                    else
                    {
                        gameDataRowInDate = gameDataJson[0]["inDate"].ToString();

                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.experience = float.Parse(gameDataJson[0]["experience"].ToString());
                        userGameData.bread = int.Parse(gameDataJson[0]["bread"].ToString());
                        userGameData.corn = int.Parse(gameDataJson[0]["corn"].ToString());
                        userGameData.threadmill = int.Parse(gameDataJson[0]["threadmill"].ToString());
                    }
                    onGameDataLoadEvent?.Invoke();
                    tcs.SetResult(true);
                }
                else
                {
                    Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ� : {callback}");
                    //GameDataInsert(); // �����Ͱ� ���ٴ� �Ŵϱ� ���� ����
                    tcs.SetResult(false);
                }
            }
            catch (System.Exception e)
            {
                userGameData.Reset();
                Debug.LogError(e);
                tcs.SetResult(false);
            }
        });
        return tcs.Task;
    }*/


    /// <summary>
    /// ���� ������ ���� -> ���� ���� ��� Ȱ���� �Լ�
    /// </summary>
    /// <param name="action"></param>
    public void GameDataUpdate(UnityAction action = null)
    {
        if(userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�." +
                           "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level },
            {"experience", userGameData.experience },
            {"bread", userGameData.bread },
            {"corn", userGameData.corn},
            {"threadmill", userGameData.threadmill}
        };

        // ���� ������ ������(gameDataRowInDate)�� ������ ���� �޽��� ���
        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.LogError($"������ inDate ������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        // ���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        // �����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2() ȣ��
        else
        {
            Debug.Log($"{gameDataRowInDate}�� ���� ���� ������ ������ ��û�մϴ�.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");

                    action?.Invoke();
                    GameDataLoad();
                }
                else
                {
                    Debug.LogError($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
            });
        }
    }
}