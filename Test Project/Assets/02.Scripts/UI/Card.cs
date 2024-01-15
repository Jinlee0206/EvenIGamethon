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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) // �׽�Ʈ��
        {
            cardMenu.SetActive(true);
            Time.timeScale = 0;
        }
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
                // ���±� ��ų 1�� ī�� ����(����)  : ���ط� +60%
                if(cardData.cardId == 1)
                {
                    player.playerData[0].damage *= 1.6f;
                    Debug.Log("damage : " + player.playerData[0].damage);
                }
                // ���±� ��ų 2�� ī�� ����(����)  : ���� ���� +30%    
                
                // ���±� ��ų 3�� ī�� ����        : ����+2 / ���� +10% 
                else if (cardData.cardId == 3)
                {
                    player.playerData[0].penetrate += 2;
                    player.playerData[0].damage *= 1.1f;
                    Debug.Log("damage : " + player.playerData[0].damage + " penetrate : + " + player.playerData[0].penetrate);
                }

                // ���±� ��ų 4�� ī�� ��Ÿ�� ���� : -20%
                else if (cardData.cardId == 4)
                {
                    player.playerData[0].atkSpeed *= 1.2f;
                }
                // ���±� ��ų 5�� ī�� ���� ����   : �׼��Ͼƿ� ���ط� +40%
                // ���±� ��ų 6�� ī�� �߰�        : ���� / ���� (�� �������� �ر� �Ǿ���� 2���� 3�� �������� ���´�

                // �� ���̽� �ȿ� 1~6�� ī�� �� � ī������ �˻�
                break;
            case CardData.CardType.Boom:
                // �ر��� �ϴ� �ý���
                // ������ �����ϴ°� ���� �� ����. level0 �϶��� playerdata �ȿ��� ��Ȱ��ȭ �����̴ٰ� ���⼭ Ŭ���Ǽ� ���� 1�� �Ǹ� �Ʒ� ���� Ȱ��ȭ

                break;
            case CardData.CardType.Aqua:
                break;
            case CardData.CardType.Lumos:
                break;
            case CardData.CardType.Exo:
                break;
            case CardData.CardType.Momen:
                break;
            case CardData.CardType.Fines:
                break;
        }

        level++;

        // �� ī�庰 ���� ������ ���� ���� ���ΰ�? �������� �������� ������ �� ���ΰ�?
        if(level == cardData.damages.Length)
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