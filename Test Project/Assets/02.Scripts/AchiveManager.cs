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
    public CardArray[] unlockCards;           // �ر� ������ ��ü ī���

    // 6���� ȹ�� �رݰ� ����, ���� �߰� ������  �ر��� ���������� ����
    // �켱 ȹ�� �رݺ��� ����
    enum Achive { UnlockBoom, UnlockAqua }
    Achive[] achives;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
    }

}
