using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;

    public CardData cardData;
    public int level;
    public Player player;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = cardData.cardIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = cardData.cardName;
    }

    // ���� �ؽ�Ʈ ����
    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        textDesc.text = string.Format(cardData.cardDesc);
    }

    public void OnClick()
    {
        switch (cardData.cardType)
        {
            case CardData.CardType.MagicBolt:
                /*
                // ���±� ��ų 2�� ī�� ����(����)  : ���� ���� +30%
                else if(cardData.cardId == 2)
                {
                     // ���� ���� �� ���� �ʿ�
                }
                */

                // ���±� ��ų 3�� ī�� ����        : ����+2 / ���� +10%
                if (cardData.cardId == 3)
                {
                    player.playerData[(int)cardData.cardType].penetrate += 2;
                    player.playerData[(int)cardData.cardType].damage *= 1.1f;
                    Debug.Log("damage : " + player.playerData[(int)cardData.cardType].damage + " penetrate : + " + player.playerData[(int)cardData.cardType].penetrate);
                }
                // ���±� ��ų 7�� ���� ����        : �׼��Ͼƿ� ���ط� +40%
                else if (cardData.cardId == 7)
                {
                    player.playerData[(int)cardData.cardType].damage *= 1.4f;
                    player.playerData[(int)CardData.CardType.Aegs].damage *= 1.4f;
                }
                break;
            // �� ���̽� �ȿ� 1~6�� ī�� �� � ī������ �˻�
            case CardData.CardType.Boom:
                // �ر��� �ϴ� �ý���
                // ������ �����ϴ°� ���� �� ����. level0 �϶��� playerdata �ȿ��� ��Ȱ��ȭ �����̴ٰ� ���⼭ Ŭ���Ǽ� ���� 1�� �Ǹ� �Ʒ� ���� Ȱ��ȭ
                // playerData�� isUnlock �Ҹ��� Ȱ��

            case CardData.CardType.Aqua:
            case CardData.CardType.Lumos:
            case CardData.CardType.Aegs:
            case CardData.CardType.Momen:
            case CardData.CardType.Pines:
                // ����
                // ��ų 1�� ī�� ����(����)  : ���ط� +60 %
                if (cardData.cardId == 1)
                {
                    player.playerData[(int)cardData.cardType].damage *= 1.6f;
                    Debug.Log($"damage : {player.playerData[(int)cardData.cardType].damage}");
                }
                else if (cardData.cardId == 6)
                {
                    player.playerData[(int)cardData.cardType].atkSpeed *= 0.8f;
                }
                // �ر�
                else if (cardData.cardId == 8)
                {
                    player.playerData[(int)cardData.cardType].isUnlocked = true;

                }
                else if (cardData.cardId == 9)
                {
                    // ���� �߰�
                }
                else if (cardData.cardId == 10)
                {
                    // ���� �߰�
                }



                break;
        }

        level++;

        // �� ī�庰 ���� ������ ���� ���� ���ΰ�? �������� �������� ������ �� ���ΰ�?
        if(level == cardData.levels.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// ī�� �������� ��, ī�� �޴�â ���� ���� �簳
    /// ī�� ���� �� ���� ������Ʈ �ϴ� ���� ���� �ʿ�
    /// </summary>
    public void ChooseCard()
    {
        cardMenu.SetActive(false);
        Time.timeScale = 1;
    }


}

/*[System.Serializable]
public class CardData2
{
    public int cardId;
    public int skillId;
    public float damageUp;
    public int penetrateUp;
    public string desc;
}*/