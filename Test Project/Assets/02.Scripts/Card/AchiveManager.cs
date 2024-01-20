using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardArray
{
    public GameObject[] card;
}

public class AchiveManager : MonoBehaviour
{
    public Player player;
    public CardArray[] unlockCards;           // �ر� ������ ��ü ī���

    // 6���� ȹ�� �رݰ� ����, ���� �߰� ������  �ر��� ���������� ����
    // �켱 ȹ�� �رݺ��� ����
    enum Achive { UnlockBoom, UnlockAqua }
    Achive[] achives;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        Init();                                                    // ��Ʋ ���� ���۵� ������ ī�� �ر� ���´� �ʱ�ȭ
    }

    void Init()
    {
        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);   // �� ��ų 
        }
    }

    private void Start()
    {
        UnlockCard();
    }

    void UnlockCard()
    {
        for (int idx = 0; idx < unlockCards.Length; idx++)
        {
            string achiveName = achives[idx].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;

            for (int j = 0; j < unlockCards[idx].card.Length; j++)
            {
                unlockCards[idx].card[j].SetActive(isUnlock);
            }
        }
    }

    private void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockBoom:
                isAchive = player.playerData[1].isUnlocked == true;
                break;
            case Achive.UnlockAqua:
                isAchive = player.playerData[2].isUnlocked == true;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) // �ش� ������ ó�� �޼��ߴٴ� ����
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        }
    }
}
