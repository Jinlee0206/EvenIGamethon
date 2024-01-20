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
    [TextArea]                  // Inspector�� �ؽ�Ʈ�� ���� �� ���� �� �ְ��ϴ� Attribute 
    public string cardDesc;     // ������ ����
    public Sprite cardIcon;     // �������� UI�� ������� ������

    // ���� �ʿ�
    #region
    [Header("# Level Data")]
    // public float baseDamage;    // 0���� ���� �⺻ ���ݷ�
    // public int baseCount;       // 0���� ���� ����
    public float[] levels;     // �� ī���� �Ӽ��� �ش��ϴ� �κ� (�츮��)
    // public int[] counts;
    #endregion
}
