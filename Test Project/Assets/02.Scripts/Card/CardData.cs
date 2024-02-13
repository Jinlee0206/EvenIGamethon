using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class CardData : ScriptableObject
{
    public enum CardType { MagicBolt, Boom, Aqua, Lumos, Aegs, Momen, Pines}

    [Header("# Main Info")]
    public CardType cardType;
    public int cardId;          // ī���� ID
    public string cardName;     // ī�� �̸�
    public bool isLocked;       // ī�� ��� �ִ���
    public bool noExplosion;    // ���� ��ų �ر� �ȵ�
    public bool noPenetration;  // ���� ��ų �ر� �ȵ�

    [TextArea]                  // Inspector�� �ؽ�Ʈ�� ���� �� ���� �� �ְ��ϴ� Attribute 
    public string cardUpg;      // ���׷��̵� ����
    [TextArea]                   
    public string cardDesc;     // ������ ����
    public Sprite cardIcon;     // �������� UI�� ������� ������

    [Header("# Level Data")]
    public float[] levels;     // �� ī���� �Ӽ��� �ش��ϴ� �κ� (�츮��)

    private void Awake()
    {
        isLocked = false;
        noExplosion = false;
        noPenetration = false;
    }
}
