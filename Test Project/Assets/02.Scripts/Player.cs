using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using System.Xml.Serialization;

public class Player : MonoBehaviour
{
    public List<PlayerData> playerData = new List<PlayerData>();
    public Transform fireArea;
    string xmlFileName = "PlayerData";

    void Start()
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
            PlayerData newData = new PlayerData();

            newData.skillId = int.Parse(node.SelectSingleNode("skillId").InnerText);
            newData.penetrate = int.Parse(node.SelectSingleNode("penetrate").InnerText);
            newData.damage = float.Parse(node.SelectSingleNode("damage").InnerText);
            newData.atkSpeed = float.Parse(node.SelectSingleNode("atkSpeed").InnerText);
            newData.bulletSpeed = float.Parse(node.SelectSingleNode("bulletSpeed").InnerText);

            playerData.Add(newData);
        }
    }

    private void Update()
    {
        Transform target = gameObject.GetComponent<Scanner>().nearestTarget;
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Attack(target);
        }
    }
    void Attack(Transform target) //����ź�� �ƴ� ���� ����� ��ġ�� �׳� �߻�
    {
        //�Ѿ��� �����ϴµ� -> ������ �Ѿ˿� Init�� �ؾߵ� -> �Ϸ�
        BulletSpawn(target);
        //���� �Ѿ��� �������� ��������
    }

    void BulletSpawn(Transform target)
    {
        GameObject bullet = GameManager.Inst.pool.Get(1);
        bullet.transform.position = fireArea.position;
        bullet.GetComponent<Bullet>().Init(playerData[0]);
        bullet.GetComponent<Bullet>().target = target;
    }


}

[System.Serializable]
public class PlayerData //����ĳ���� �ɷ�ġ(��ų) ������
{
    public int skillId; //���� ��������
    public int penetrate; //���� Ƚ��
    public float damage; //���ݷ�
    public float atkSpeed; //���� �ӵ�
    public float bulletSpeed; //����ü �ӵ�
}
