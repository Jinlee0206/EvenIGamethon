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

    /*public List<CardData2> cardData2 = new List<CardData2>();
    string xmlFileName = "CardData";

    private void Start()
    {
        LoadXML(xmlFileName);
    }

    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(_fileName);
        if (txtAsset == null)
        {
            Debug.LogError("Failed to load XML file: " + _fileName);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        // ��ü ������ �������� ����.
        XmlNodeList all_nodes = xmlDoc.SelectNodes("root/Sheet1");
        foreach (XmlNode node in all_nodes)
        {
            CardData2 newData = new CardData2();

            newData.cardId = int.Parse(node.SelectSingleNode("cardId").InnerText);
            newData.skillId = int.Parse(node.SelectSingleNode("skillId").InnerText);
            newData.damageUp = float.Parse(node.SelectSingleNode("damageUp").InnerText);
            newData.penetrateUp = int.Parse(node.SelectSingleNode("penetrateUp").InnerText);
            newData.desc = node.SelectSingleNode("Desc").InnerText;

            cardData2.Add(newData);
        }
    }*/

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = cardData.cardIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) // �׽�Ʈ��
        {
            cardMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch (cardData.cardType)
        {
            case CardData.CardType.MagicBolt:
                // ���±� ��ų 1�� ī�� ����(����)  : ���ط� +60%
                // ���±� ��ų 2�� ī�� ����(����)  : ���� ���� +30%    
                // ���±� ��ų 3�� ī�� ����        : ����+2 / ���� +10% 
                // ���±� ��ų 4�� ī�� ��Ÿ�� ���� : -20%
                // ���±� ��ų 5�� ī�� ���� ����   : �׼��Ͼƿ� ���ط� +40%
                // ���±� ��ų 6�� ī�� �߰�        : ���� / ���� (�� �������� �ر� �Ǿ���� 2���� 3�� �������� ���´�
                player.playerData[0].damage *= 1.6f;
                //�� ���̽� �ȿ� 1~6�� ī�� �� � ī������ �˻�
                break;
            case CardData.CardType.Boom:
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