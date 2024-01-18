using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Player : MonoBehaviour
{
    public List<PlayerData> playerData = new List<PlayerData>();
    public Transform fireArea;
    public Transform target;
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
            newData.secondDelay = float.Parse(node.SelectSingleNode("secondDelay").InnerText);
            newData.duration = float.Parse(node.SelectSingleNode("duration").InnerText);
            newData.bulletSpeed = float.Parse(node.SelectSingleNode("bulletSpeed").InnerText);
            newData.atkRange = float.Parse(node.SelectSingleNode("atkRange").InnerText);
            newData.explodeDamage = float.Parse(node.SelectSingleNode("explodeDamage").InnerText);
            newData.isExplode = bool.Parse(node.SelectSingleNode("isExplode").InnerText);
            newData.isUnlocked = bool.Parse(node.SelectSingleNode("isUnlocked").InnerText);

            playerData.Add(newData);
        }
    }

    private void Update()
    {
        target = gameObject.GetComponent<Scanner>().nearestTarget;

        if (target == null) return;

        Vector3 offset = target.position - transform.position;
        float distance = offset.magnitude;
        /*if(Input.GetKeyUp(KeyCode.Space))
        {
            Attack(target);
        }*/

        foreach(PlayerData data in playerData)
        {
            if (data.isUnlocked)
            {
                data.UpdateCooldown();
                if (data.CanUseSkill() && distance < data.atkRange)
                {
                    //StartCoroutine(Attack(target, data));
                    switch(data.skillId)
                    {
                        case 0: 
                            StartCoroutine(MagicBall(target, data));
                            break;
                        case 1:
                            StartCoroutine(Bombarda(target, data));
                            break;
                        case 2:
                            StartCoroutine(Aguamenti(target, data));
                            break;
                        case 3:
                            StartCoroutine(Lumos(target, data));
                            break;
                        case 4:
                            StartCoroutine(Aegseonia(target, data));
                            break;
                        case 5:
                            StartCoroutine(Momenseuto(target, data));
                            break;
                        case 6:
                            Pineseuta(target, data);
                            break;
                    }
                    data.StartCoolDown();
                }
            }
        }
    }
    /*void Attack(Transform target) //����ź�� �ƴ� ���� ����� ��ġ�� �׳� �߻�
    {
        //�Ѿ��� �����ϴµ� -> ������ �Ѿ˿� Init�� �ؾߵ� -> �Ϸ�
        BulletSpawn(target);
        //���� �Ѿ��� �������� ��������
    }*/

    IEnumerator MagicBall(Transform target, PlayerData data)
    {
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    IEnumerator Bombarda(Transform target, PlayerData data)
    {
        //ó���� ����ü �ϳ� ���ư��ٰ� ������ 0.7�ʵڿ� ���� -> Bullet���� ����?
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    IEnumerator Aguamenti(Transform target, PlayerData data)
    {
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    IEnumerator Lumos(Transform target, PlayerData data)
    {
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    IEnumerator Aegseonia(Transform target, PlayerData data)
    {
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    IEnumerator Momenseuto(Transform target, PlayerData data)
    {
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    IEnumerator Pineseuta(Transform target, PlayerData data)
    {
        BulletSpawn(target, data);
        yield return new WaitForSeconds(data.atkSpeed);
    }

    void BulletSpawn(Transform target, PlayerData data)
    {
        GameObject bullet = GameManager.Inst.pool.Get(1);
        bullet.transform.position = fireArea.position;
        bullet.GetComponent<Bullet>().Init(data);
        //bullet.GetComponent<Bullet>().target = target;
    }
}

[System.Serializable]
public class PlayerData //����ĳ���� �ɷ�ġ(��ų) ������
{
    public int skillId; //���� ��������
    public int penetrate; //���� Ƚ��
    public float damage; //���ݷ�
    public float atkSpeed; //���� �ӵ�
    public float secondDelay; //�ι�° ������ �ִ� ��ų�� �ι�° ���� ������
    public float duration; //���ӵ����� ��ų�� ���ӽð�
    public float bulletSpeed; //����ü �ӵ�
    public float atkRange; //�����Ÿ�
    public float explodeDamage; //���� ������
    public bool isExplode; //���������� ����
    public bool isUnlocked; //�رݵƴ��� ����
    

    private float cooldownTimer = 0f;

    public bool CanUseSkill()
    {
        return cooldownTimer <= 0f;
    }

    public void StartCoolDown()
    {
        cooldownTimer = atkSpeed;
    }

    public void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            //Debug.Log(skillId + "�� ��ų ��Ÿ�� : " + cooldownTimer);
        }
    }
}
