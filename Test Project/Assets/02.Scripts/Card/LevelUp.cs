using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField]
    public enum Chapter { Chapter1 = 1, Chapter2, Chapter3, Chapter4 };

    RectTransform rect;
    Card[] cards;
    public Player player;

    public Chapter chapter;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>();

        ActivateByChapter();
    }

    private void ActivateByChapter()
    {
        StageSelect stageSelect = StageSelect.instance;

        // ���� é�Ϳ� ���� Ȱ��ȭ ���� ����
        bool shouldActivate = ((int)chapter == stageSelect.chapter);

        // Ȱ��ȭ ���ο� ���� ������Ʈ Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        gameObject.SetActive(shouldActivate);

        if (shouldActivate)
        {
            Debug.Log($"é�� {chapter}�� ī�� ��");
        }
    }

    /*
    private void ActivateByChapter()
    {
        StageSelect stageSelect = StageSelect.instance;

        // é�Ϳ� ���� Ȱ��ȭ ���� ����
        switch (chapter)
        {
            case Chapter.Chapter1:
                if (stageSelect.chapter == 1)
                {
                    Debug.Log("é�� 1�� ī�� ��");
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case Chapter.Chapter2:
                if (stageSelect.chapter == 2)
                {
                    Debug.Log("é�� 2�� ī�� ��");
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case Chapter.Chapter3:
                if (stageSelect.chapter == 3)
                {
                    Debug.Log("é�� 3�� ī�� ��");
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case Chapter.Chapter4:
                if (stageSelect.chapter == 4)
                {
                    Debug.Log("é�� 4�� ī�� ��");
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }*/

    public void Show()
    {
        Next();
        Debug.Log(chapter);
        rect.localScale = Vector3.one * 1.3f;
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
