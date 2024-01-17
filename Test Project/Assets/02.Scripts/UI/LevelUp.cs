using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Card[] cards;

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
        foreach(Card card in cards)
        {
            card.gameObject.SetActive(false);
        }

        // 2. ���߿��� ���� 3�� ī�� Ȱ��ȭ
        int[] rnd = new int[3];

        //while(true)
        //{
        //    rnd[0] = Random.Range(0, cards.Length);
        //    rnd[1] = Random.Range(0, cards.Length);
        //    rnd[2] = Random.Range(0, cards.Length);

        //    if (rnd[0] != rnd[1] && rnd[1] != rnd[2] && rnd[2] != rnd[0]) // �ߺ� ī�尡 �ߴ� ��� �ݺ��� ���������� ���� ���� ����
        //        break;
        //}

        // ���� ����

        if (rnd.Length > cards.Length) // �� �������� ���� ��� ������ ���������� ũ�� �����ϸ� ���� �߻�
        {
            Debug.LogError("");
            return;
        }

        bool[] check = new bool[cards.Length];
        bool isTry = false;
        rnd[0] = Random.Range(0, cards.Length);
        for(int i = 0; i< rnd.Length; i++)
        {
            do
            {
                // ī�� �̱� ���� �ر݉���� üũ�ϴ� ����

                isTry = false;
                rnd[i] = Random.Range(0, cards.Length);     // n��° �������� ����
                if (check[rnd[i]]) isTry = true;            // �ߺ��Ǵ� �� �ִٸ� �ٽ� �ϰ�
                else check[rnd[i]] = true;
            } while (isTry);
        }


        for(int idx = 0; idx < rnd.Length; idx++)
        {
            Card rndCard = cards[rnd[idx]];
            
            // 3. ���� ī���� ��� �� �̻� ���� �ʰ�
            if(rndCard.level == rndCard.cardData.damages.Length)
            {
                // ī�尡 ��� ������ ���
                Debug.Log(rndCard.level); // �����

                // �߰� ���� ���� �ʿ� (ü��ȸ������ ī�� ���׷��̵�� ������� �нú� ī�带 ������ ���� ����)
            }
            else
            {
                rndCard.gameObject.SetActive(true);
            }
        }
    }
    
}
