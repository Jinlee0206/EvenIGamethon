using System;
using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackendGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : /*UnityEngine.Events.*/UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackendGameData instance = null;
    public static BackendGameData Instance
    {
        get
        {
            if (instance == null)
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
        // ���� ������ �ʱⰪ���� ����
        userGameData.Reset();

        // ���̺� �߰��� �����ͷ� ����
        Param param = new Param()
        {
            /*{ "level",      userGameData.level },
            { "experience", userGameData.experience },
            { "heart",      userGameData.heart },
            { "gold",      userGameData.gold },
            { "t2Unlocked", userGameData.t2Unlocked },
            { "t3Unlocked", userGameData.t3Unlocked }*/
        };

        // ù ��° �Ű������� �ڳ� �ܼ��� "���� ���� ����" �ǿ� ������ ���̺� �̸�
        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            // ���� ���� �߰��� �������� ��
            if (callback.IsSuccess())
            {
                // ���� ������ ������
                gameDataRowInDate = callback.GetInDate();

                Debug.Log($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
            // �������� ��
            else
            {
                Debug.LogError($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
        });
    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺��� ���� ������ �ҷ��� �� ȣ��
    /// </summary>
    public void GameDataLoad()
    {
        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            // ���� ���� �ҷ����⿡ �������� ��
            if (callback.IsSuccess())
            {
                Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                // JSON ������ �Ľ� ����
                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    // �޾ƿ� �������� ������ 0�̸� �����Ͱ� ���� ��
                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        // �ҷ��� ���� ������ ������
                        gameDataRowInDate = gameDataJson[0]["inDate"].ToString();
                        // �ҷ��� ���� ������ userData ������ ����
                        /*userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.experience = float.Parse(gameDataJson[0]["experience"].ToString());
                        userGameData.heart = int.Parse(gameDataJson[0]["heart"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.t2Unlocked = bool.Parse(gameDataJson[0]["t2Unlocked"].ToString());
                        userGameData.t3Unlocked = bool.Parse(gameDataJson[0]["t3Unlocked"].ToString());*/
                        onGameDataLoadEvent?.Invoke();
                    }
                }
                // JSON ������ �Ľ� ����
                catch (System.Exception e)
                {
                    // ���� ������ �ʱⰪ���� ����
                    userGameData.Reset();
                    // try-catch ���� ���
                    Debug.LogError(e);
                }
            }
            // �������� ��
            else
            {
                Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }
        });
    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺� �ִ� ���� ������ ����
    /// </summary>
    public void GameDataUpdate(UnityAction action = null)
    {
        if (userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�." +
                           "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param()
        {
            /*{ "level",      userGameData.level },
            { "experience", userGameData.experience },
            { "heart",      userGameData.heart },
            { "gold",      userGameData.gold },
            { "t2Unlocked", userGameData.t2Unlocked },
            { "t3Unlocked", userGameData.t3Unlocked }*/
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
                }
                else
                {
                    Debug.LogError($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
            });
        }
    }


    public void UpdateUserRanking(Action callback = null)
    {
        try
        {
            Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
            {
                if (callback.IsSuccess())
                {
                    var rows = callback.FlattenRows();
                    if (rows.Count > 0)
                    {
                        string userInDate = rows[0]["inDate"].ToString();
                        int userLevel = int.Parse(rows[0]["level"].ToString()); // level Į���� ���
                        Debug.Log(userLevel + "= ����\n");
                        Debug.Log(userInDate + "= inDate\n");
                        UpdateRankingWithLevel(userInDate, userLevel);
                    }
                    else
                    {
                        Debug.LogError("���� �����Ͱ� �������� �ʽ��ϴ�.");
                    }
                }
                else
                {
                    Debug.LogError("������ ��ȸ ����: " + callback);
                }
            });
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UpdateUserRanking ���� �߻�: " + ex.Message);
        }
        callback?.Invoke();
    }

    // ��ŷ�� ������ ������Ʈ�ϴ� �޼��� �߰�
    private void UpdateRankingWithLevel(string userInDate, int level)
    {
        try
        {
            Param param = new Param();
            param.Add("level", level);

            Backend.URank.User.UpdateUserScore("754cf570-9426-11ee-8cb0-55a457ec4ebc", "USER_DATA", userInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log("��ŷ ������Ʈ ����");
                }
                else
                {
                    Debug.LogError("��ŷ ������Ʈ ����: " + callback);
                }
            });
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UpdateRankingWithLevel ���� �߻�: " + ex.Message);
        }
    }
}

[System.Serializable]
public class UserGameData
{
    public int level;           // Lobby Scene�� ���̴� �÷��̾� ����
    public int corn;
    public int exp;
    public int treadmill;



    public void Reset()
    {
        level = 1;
        corn = 0;
        exp = 0;
        treadmill = 0;
    }
}
