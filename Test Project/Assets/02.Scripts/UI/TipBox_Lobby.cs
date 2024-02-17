using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TipBox_Lobby : MonoBehaviour
{
    public TextMeshProUGUI tipText;

    private List<string> tips = new List<string>();

    private void Awake()
    {
        tips.Add("Tip! ��� �Űܺ�!");
        tips.Add("Tip! ���� ���飿 �������Τ���");
        tips.Add("Tip! ��� �����̸� �Ϳ��� ���� �߻��ؿ�!");
        tips.Add("Tip! ������ ���� ��� ��� ����");
        tips.Add("Tip! ���丮 �� �̽��Ϳ��׸� ã�ƺ�����!");
        tips.Add("Tip! �������� ���� �� �� �� �ִٴµ�...?");
        tips.Add("Tip! �عٶ�⾾�� ���̸�? �뺴 ��ȯ ����!");
        tips.Add("Tip! ������ �����ϸ� Ʃ�丮����!");
        tips.Add("Tip! ����� ���� ��̵� 1.5��!");
        tips.Add("Tip! ���� �ϻ� ����� �� @magic_hamzzi");
        tips.Add("Tip! ��Ʃ�꿡 ���� �뺴�� �˻� ����");
    }

    private void Start()
    {
        InvokeRepeating("ShowTips", 0, 4);
    }

    private void ShowTips()
    {
        int idx = Random.Range(0, tips.Count);

        tipText.text = tips[idx];
        TMPDOText(tipText, 2.0f);
    }

    public void TMPDOText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}
