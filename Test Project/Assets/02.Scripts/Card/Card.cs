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
                
                // ���±� ��ų 2�� ī�� ����(����)  : ���� ���� +30%
                
                

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
                    Debug.Log("Mag damage : " + player.playerData[(int)cardData.cardType].damage);
                    Debug.Log("Aegs damage : " + player.playerData[(int)CardData.CardType.Aegs].damage);
                }
                break;
            // �� ���̽� �ȿ� 1~6�� ī�� �� � ī������ �˻�
            case CardData.CardType.Boom:
                // �ر��� �ϴ� �ý���
                // ������ �����ϴ°� ���� �� ����. level0 �϶��� playerdata �ȿ��� ��Ȱ��ȭ �����̴ٰ� ���⼭ Ŭ���Ǽ� ���� 1�� �Ǹ� �Ʒ� ���� Ȱ��ȭ
                // playerData�� isUnlock �Ҹ��� Ȱ��

                // ��ų 4�� ��ų ���ӽð� ����
                if (cardData.cardId == 4)
                {
                    player.playerData[(int)cardData.cardType].duration += 2f;
                    Debug.Log($"duration : {player.playerData[(int)cardData.cardType].duration}");
                }
                if (cardData.cardId == 3)
                {
                    player.playerData[(int)cardData.cardType].penetrate += 1;
                    player.playerData[(int)cardData.cardType].damage *= 1.2f;
                    Debug.Log("damage : " + player.playerData[(int)cardData.cardType].damage + " penetrate : + " + player.playerData[(int)cardData.cardType].penetrate);
                }
                else if (cardData.cardId == 8)
                {
                    player.playerData[(int)cardData.cardType].isUnlocked = true;
                }
                break;

            case CardData.CardType.Aqua:

                if (cardData.cardId == 4)
                {
                    player.playerData[(int)cardData.cardType].duration += 2.7f;
                    Debug.Log($"duration : {player.playerData[(int)cardData.cardType].duration}");
                }
                else if (cardData.cardId == 5)
                {
                    player.playerData[(int)cardData.cardType].splashRange *= 1.15f;
                    Debug.Log("splashRange : " + player.playerData[(int)cardData.cardType].splashRange);
                }
                else if (cardData.cardId == 7)
                {
                    player.playerData[(int)cardData.cardType].duration += 2.1f;
                    player.playerData[(int)CardData.CardType.Aegs].duration += 2.1f;
                }
                else if (cardData.cardId == 8)
                {
                    player.playerData[(int)cardData.cardType].isUnlocked = true;
                }
                break;
            case CardData.CardType.Lumos:
                if (cardData.cardId == 4)
                {
                    player.playerData[(int)cardData.cardType].duration += 2f;
                    Debug.Log($"duration : {player.playerData[(int)cardData.cardType].duration}");
                }
                else if (cardData.cardId == 8)
                {
                    player.playerData[(int)cardData.cardType].isUnlocked = true;
                }
                break;
            case CardData.CardType.Aegs:
                if (cardData.cardId == 4)
                {
                    player.playerData[(int)cardData.cardType].duration += 2.1f;
                    Debug.Log($"duration : {player.playerData[(int)cardData.cardType].duration}");
                }
                else if (cardData.cardId == 5)
                {
                    player.playerData[(int)cardData.cardType].atkRange *= 1.15f;
                    Debug.Log("atkRange : " + player.playerData[(int)cardData.cardType].atkRange);
                }
                else if (cardData.cardId == 8)
                {
                    player.playerData[(int)cardData.cardType].isUnlocked = true;
                }
                break;
            case CardData.CardType.Momen:
                if (cardData.cardId == 4)
                {
                    player.playerData[(int)cardData.cardType].duration += 2.7f;
                    Debug.Log($"duration : {player.playerData[(int)cardData.cardType].duration}");
                }
                else if (cardData.cardId == 5)
                {
                    player.playerData[(int)cardData.cardType].splashRange *= 1.15f;
                    Debug.Log("splashRange : " + player.playerData[(int)cardData.cardType].splashRange);
                }
                else if (cardData.cardId == 8)
                {
                    player.playerData[(int)cardData.cardType].isUnlocked = true;
                }
                break;
            case CardData.CardType.Pines:
                // ����
                // ��ų 1�� ī�� ����(����)  : ���ط� +60 %
                if (cardData.cardId == 1)
                {
                    player.playerData[(int)cardData.cardType].damage *= 1.6f;
                    Debug.Log($"damage : {player.playerData[(int)cardData.cardType].damage}");
                }
                else if (cardData.cardId == 2)
                {
                    player.playerData[(int)cardData.cardType].explodeDamage *= 1.3f;
                    Debug.Log("explodeDamage : " + player.playerData[(int)cardData.cardType].explodeDamage);
                }
                else if (cardData.cardId == 6)
                {
                    player.playerData[(int)cardData.cardType].atkSpeed *= 0.8f;
                    Debug.Log($"atkSpeed : {player.playerData[(int)cardData.cardType].atkSpeed}");
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

                // �ǳ׽�Ÿ�� �б� �ɸ�
                else if (cardData.cardId == 3)  
                {
                    player.playerData[(int)cardData.cardType].penetrate += 1;
                    player.playerData[(int)cardData.cardType].damage *= 1.2f;
                    Debug.Log("damage : " + player.playerData[(int)cardData.cardType].damage + " penetrate : + " + player.playerData[(int)cardData.cardType].penetrate);
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