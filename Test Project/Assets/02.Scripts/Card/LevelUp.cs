using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Card[] cards;
    public Player player;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>();
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Inst.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Inst.Resume();
    }

    void Next()
    {
        // 1. ��� ī�� ��Ȱ��ȭ
        foreach (Card card in cards)
        {
            card.gameObject.SetActive(false);
        }

        // 2. ���߿��� ���� 3�� ī�� Ȱ��ȭ
        int[] rnd = new int[3];

        if (rnd.Length > cards.Length) // �� �������� ���� ��� ������ ���������� ũ�� �����ϸ� ���� �߻�
        {
            Debug.LogError("");
            return;
        }

        bool isSkill1Unlocked = player.playerData[0].isUnlocked; // 1�� ��ų �ر� ����
        bool isSkill3Unlocked = player.playerData[2].isUnlocked; // 3�� ��ų �ر� ����
        bool isSkill5Unlocked = player.playerData[4].isUnlocked; // 5�� ��ų �ر� ����
        bool isSkill6Unlocked = player.playerData[5].isUnlocked; // 6�� ��ų �ر� ����

        // ����� üũ
        //if ((isSkill1Unlocked && isSkill5Unlocked) || (isSkill3Unlocked && isSkill6Unlocked))
        {
            for (int idx = 0; idx < rnd.Length; idx++)
            {
                Card rndCard;
                do
                {
                    rnd[idx] = Random.Range(0, cards.Length);
                    rndCard = cards[rnd[idx]];
                } while (IsDuplicate(rnd, idx) || !IsValidCard(rndCard));

                rndCard.gameObject.SetActive(true);
            }
        }
        //else
        //{
        //    // �ٽ� �̴� ���� �߰�
        //    Debug.Log("�ر� ������ ���� �ʽ��ϴ�. �ٽ� �̽��ϴ�.");
        //    Next();
        //}
    }

    // �ߺ� üũ
    bool IsDuplicate(int[] array, int currentIndex)
    {
        for (int i = 0; i < currentIndex; i++)
        {
            if (array[i] == array[currentIndex])
            {
                return true;
            }
        }
        return false;
    }

    // ��ȿ�� ī�� üũ
    bool IsValidCard(Card card)
    {
        return !card.cardData.isLocked && !card.cardData.noExplosion && !card.cardData.noPenetration && card.level < card.cardData.levels.Length;
    }
}
